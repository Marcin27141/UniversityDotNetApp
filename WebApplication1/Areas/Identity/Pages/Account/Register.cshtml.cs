using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ApiDtoLibrary.Person;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using WebApplication1.Contracts;
using WebApplication1.Services;

namespace WebApplication1.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private const string PROFESSOR_MAIL_PATTERN = @"^[\w\.]+@edu.com";
        private const string STUDENT_MAIL_PATTERN = @"^[\w\.]+@student.edu.com";
        private const string ADMIN_MAIL_PATTERN = @"^[\w\.]+@admin.com";

        private readonly IAuthenticationRepository _authenticationRespository;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(
            IAuthenticationRepository authenticationRespository,
            ILogger<RegisterModel> logger
            )
        {
            _authenticationRespository = authenticationRespository;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }

            [Display(Name = "Is Admin?")]
            public bool HasAdminRights { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _authenticationRespository.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _authenticationRespository.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    UserName = Input.Email,
                    Email = Input.Email,
                    Password = Input.Password
                };
                var result = await _authenticationRespository.CreateUserAsync(user);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    user.Id = await _authenticationRespository.GetIdByUsernameAsync(user.UserName);

                    //Name claim
                    var nameClaim = new Claim("Name", Input.FirstName);
                    await _authenticationRespository.AddClaimAsync(user, nameClaim);

                    //IsAdmin claim
                    if (Input.HasAdminRights)
                    {
                        var adminClaim = new Claim("IsAdmin", Input.HasAdminRights.ToString());
                        await _authenticationRespository.AddClaimAsync(user, adminClaim);
                    }

                    //Status claim {Admin/Student/Professor}
                    var statusClaim = GetStatusString();
                    if (statusClaim != null)
                        await _authenticationRespository.AddClaimAsync(user, new Claim("Status", statusClaim));
                    
                    var code = await _authenticationRespository.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    if (_authenticationRespository.ConfirmedAccountRequired())
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _authenticationRespository.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private string GetStatusString()
        {
            string status = Input.HasAdminRights ? nameof(PersonStatus.Admin) : null;
            if (string.IsNullOrEmpty(status)) {
                if (Regex.IsMatch(Input.Email, STUDENT_MAIL_PATTERN))
                    status = nameof(PersonStatus.Student);
                else if (Regex.IsMatch(Input.Email, PROFESSOR_MAIL_PATTERN))
                    status = nameof(PersonStatus.Professor);
            }
            return status;
        }
    }
}
