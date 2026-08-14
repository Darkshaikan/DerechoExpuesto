using DerechoExpuesto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DerechoExpuesto.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser>
            _signInManager;


        public AccountController(
            SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }


        // ============================================
        // LOGIN
        // ============================================

        [AllowAnonymous]
        [HttpGet("/Admin/Login")]
        public IActionResult Login(
            string? returnUrl = null)
        {
            ViewData["ReturnUrl"] =
                returnUrl;


            return View();
        }


        // ============================================
        // LOGIN POST
        // ============================================

        [AllowAnonymous]
        [HttpPost("/Admin/Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            LoginViewModel model,
            string? returnUrl = null)
        {
            ViewData["ReturnUrl"] =
                returnUrl;


            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var resultado =
                await _signInManager
                    .PasswordSignInAsync(
                        model.Email,
                        model.Password,
                        model.Recordarme,
                        lockoutOnFailure: true
                    );


            if (resultado.Succeeded)
            {
                if (
                    !string.IsNullOrWhiteSpace(
                        returnUrl
                    )
                    &&
                    Url.IsLocalUrl(returnUrl)
                )
                {
                    return Redirect(
                        returnUrl
                    );
                }


                return RedirectToAction(
                    "Index",
                    "Admin"
                );
            }


            if (resultado.IsLockedOut)
            {
                ModelState.AddModelError(
                    string.Empty,

                    "La cuenta está temporalmente bloqueada. Intentá nuevamente más tarde."
                );


                return View(model);
            }


            ModelState.AddModelError(
                string.Empty,

                "Email o contraseña incorrectos."
            );


            return View(model);
        }


        // ============================================
        // LOGOUT
        // ============================================

        [Authorize]
        [HttpPost("/Admin/Logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager
                .SignOutAsync();


            return RedirectToAction(
                "Index",
                "Home"
            );
        }


        // ============================================
        // ACCESO DENEGADO
        // ============================================

        [HttpGet("/Admin/AccesoDenegado")]
        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}