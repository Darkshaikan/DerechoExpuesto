using DerechoExpuesto.Data;
using DerechoExpuesto.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// ============================================
// MVC
// ============================================

builder.Services.AddControllersWithViews();


// ============================================
// CADENA DE CONEXIÓN
// ============================================

var connectionString =
    builder.Configuration.GetConnectionString(
        "DefaultConnection"
    );

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "No se encontró la cadena de conexión DefaultConnection."
    );
}


// ============================================
// BASE DE DATOS PRINCIPAL
// ============================================
//
// DEVELOPMENT
//      SQLite
//
// PRODUCTION
//      PostgreSQL
//
// ApplicationDbContext es el contexto
// utilizado normalmente por:
// - Controllers
// - Identity
// - Servicios
// - Consultas
// - Configuración
//
// ============================================

builder.Services.AddDbContext<ApplicationDbContext>(
    options =>
    {
        if (builder.Environment.IsDevelopment())
        {
            // ========================================
            // DESARROLLO LOCAL
            // ========================================

            options.UseSqlite(
                connectionString
            );
        }
        else
        {
            // ========================================
            // PRODUCCIÓN
            // ========================================

            options.UseNpgsql(
                connectionString
            );
        }
    }
);


// ============================================
// ASP.NET CORE IDENTITY
// ============================================

builder.Services
    .AddIdentity<IdentityUser, IdentityRole>(
        options =>
        {
            // ========================================
            // CONTRASEÑA
            // ========================================

            options.Password.RequiredLength = 8;

            options.Password.RequireDigit = true;

            options.Password.RequireUppercase = true;

            options.Password.RequireLowercase = true;

            options.Password.RequireNonAlphanumeric = false;


            // ========================================
            // BLOQUEO
            // ========================================

            options.Lockout.MaxFailedAccessAttempts = 5;

            options.Lockout.DefaultLockoutTimeSpan =
                TimeSpan.FromMinutes(5);


            // ========================================
            // USUARIO
            // ========================================

            options.User.RequireUniqueEmail = true;
        }
    )
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


// ============================================
// COOKIE DEL ADMINISTRADOR
// ============================================

builder.Services.ConfigureApplicationCookie(
    options =>
    {
        options.LoginPath =
            "/Admin/Login";

        options.AccessDeniedPath =
            "/Admin/AccesoDenegado";

        options.ExpireTimeSpan =
            TimeSpan.FromHours(8);

        options.SlidingExpiration = true;

        options.Cookie.HttpOnly = true;

        options.Cookie.SecurePolicy =
            CookieSecurePolicy.Always;

        options.Cookie.SameSite =
            SameSiteMode.Lax;
    }
);


var app = builder.Build();


// ============================================
// MIGRACIONES POSTGRESQL EN PRODUCCIÓN
// ============================================
//
// IMPORTANTE:
//
// NO utilizamos:
//
// context.Database.Migrate()
//
// sobre ApplicationDbContext.
//
// ApplicationDbContext también tiene
// las migraciones SQLite originales.
//
// En Production creamos temporalmente
// PostgreSqlApplicationDbContext,
// que es el contexto asociado a:
//
// MigrationsPostgreSQL
//
// ============================================

if (!app.Environment.IsDevelopment())
{
    var postgresOptions =
        new DbContextOptionsBuilder<ApplicationDbContext>();


    postgresOptions.UseNpgsql(
        connectionString
    );


    await using var postgresMigrationContext =
        new PostgreSqlApplicationDbContext(
            postgresOptions.Options
        );


    await postgresMigrationContext
        .Database
        .MigrateAsync();
}


// ============================================
// DATOS INICIALES
// ============================================

using (var scope = app.Services.CreateScope())
{
    var services =
        scope.ServiceProvider;


    var context =
        services.GetRequiredService<
            ApplicationDbContext
        >();


    // ========================================
    // SERVICIOS INICIALES
    // ========================================
    //
    // Esto solamente carga los cuatro
    // servicios originales cuando la tabla
    // está completamente vacía.
    //
    // Si el cliente ya creó servicios,
    // no se modifica nada.
    //
    // ========================================

    if (!await context.Servicios.AnyAsync())
    {
        context.Servicios.AddRange(

            new Servicio
            {
                Titulo =
                    "Derecho Laboral",

                Descripcion =
                    "Defensa y asesoramiento en conflictos laborales."
            },


            new Servicio
            {
                Titulo =
                    "Derecho de Familia",

                Descripcion =
                    "Divorcios, alimentos y tenencia."
            },


            new Servicio
            {
                Titulo =
                    "Accidentes",

                Descripcion =
                    "Reclamos por accidentes laborales y civiles."
            },


            new Servicio
            {
                Titulo =
                    "Asesoramiento",

                Descripcion =
                    "Consultas legales personalizadas."
            }

        );


        await context.SaveChangesAsync();
    }


    // ========================================
    // ADMINISTRADOR
    // ========================================

    var userManager =
        services.GetRequiredService<
            UserManager<IdentityUser>
        >();


    var adminEmail =
        builder.Configuration[
            "AdminUser:Email"
        ];


    var adminPassword =
        builder.Configuration[
            "AdminUser:Password"
        ];


    if (
        !string.IsNullOrWhiteSpace(
            adminEmail
        )
        &&
        !string.IsNullOrWhiteSpace(
            adminPassword
        )
    )
    {
        var usuarioExistente =
            await userManager.FindByEmailAsync(
                adminEmail
            );


        // ========================================
        // CREAR ADMIN SI NO EXISTE
        // ========================================

        if (usuarioExistente == null)
        {
            var admin =
                new IdentityUser
                {
                    UserName =
                        adminEmail,

                    Email =
                        adminEmail,

                    EmailConfirmed =
                        true
                };


            var resultado =
                await userManager.CreateAsync(
                    admin,
                    adminPassword
                );


            if (!resultado.Succeeded)
            {
                var errores =
                    string.Join(
                        Environment.NewLine,

                        resultado.Errors.Select(
                            error =>
                                $"{error.Code}: {error.Description}"
                        )
                    );


                throw new Exception(
                    "No se pudo crear el administrador:"
                    +
                    Environment.NewLine
                    +
                    errores
                );
            }
        }
    }
}


// ============================================
// CONFIGURACIÓN HTTP
// ============================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(
        "/Home/Error"
    );

    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


// ============================================
// AUTENTICACIÓN
// ============================================

app.UseAuthentication();

app.UseAuthorization();


// ============================================
// RUTAS MVC
// ============================================

app.MapControllerRoute(
    name: "default",

    pattern:
        "{controller=Home}/{action=Index}/{id?}"
);


// ============================================
// EJECUTAR APLICACIÓN
// ============================================

app.Run();