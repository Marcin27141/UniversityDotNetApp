using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using WebApplication1.Contracts;
using WebApplication1.Services;

namespace WebApplication1.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private const string PROFESSOR_MAIL_PATTERN = @"^[\w\.]+@edu.com";
        private const string STUDENT_MAIL_PATTERN = @"^[\w\.]+@student.edu.com";
        private const string ADMIN_MAIL_PATTERN = @"^[\w\.]+@admin.com";
        private const string RETURN_URL_PROFESSOR = "/AfterLogin/Professor";
        private const string RETURN_URL_STUDENT = "/AfterLogin/Student";
        private const string RETURN_URL_ADMIN = "/AfterLogin/Admin";

        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(ILogger<LoginModel> logger,
            IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _authenticationRepository.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _authenticationRepository.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                //TODO separate login pages for students and workers?
                returnUrl = GetReturnUrl();

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var user = new ApplicationUser { Email = Input.Email, Password = Input.Password };
                var result = await _authenticationRepository.PasswordSignInAsync(user, Input.RememberMe, false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private string GetReturnUrl()
        {
            if (Regex.IsMatch(Input.Email, PROFESSOR_MAIL_PATTERN))
                return RETURN_URL_PROFESSOR;
            else if (Regex.IsMatch(Input.Email, STUDENT_MAIL_PATTERN))
                return RETURN_URL_STUDENT;
            else if (Regex.IsMatch(Input.Email, ADMIN_MAIL_PATTERN))
                return RETURN_URL_ADMIN;
            return null;
        }
    }
}

