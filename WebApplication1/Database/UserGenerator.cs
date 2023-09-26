using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ApiDtoLibrary.Person;
using System.Threading.Tasks;
using Google.Rpc;
using System.Collections.Specialized;

namespace WebApplication1.Database
{
    public class UserGenerator
    {

        private string _testPassword = "P@ssw0rd";
        private readonly UserManager<WebAppUser> _userManager;

        public UserGenerator(UserManager<WebAppUser> userManager)
        {
            this._userManager = userManager;
        }

        private string GetStudentMail(int index) => $"stud{index}@student.edu.com";
        private string GetProfessorMail(int index) => $"prof{index}@edu.com";
        private WebAppUser GetUserByEmail(string email) =>
            new WebAppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = email,
                NormalizedUserName = email.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                EmailConfirmed = true,
            };

        private IEnumerable<WebAppUser> GetStudentAccounts(int quantity) => Enumerable.Range(1, quantity).Select(i => GetUserByEmail(GetStudentMail(i)));
        private IEnumerable<WebAppUser> GetProfessorAccounts(int quantity) => Enumerable.Range(1, quantity).Select(i => GetUserByEmail(GetProfessorMail(i)));

        private void AddClaimsForStatus(WebAppUser user, PersonStatus status)
        {
            _ = _userManager.AddClaimAsync(user, new Claim("Status", Enum.GetName(status))).Result;
            _ = _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id)).Result;
        }

        private void GenerateUsersWithClaims(IEnumerable<WebAppUser> users, PersonStatus status)
        {
            foreach (WebAppUser user in users)
            {
                IdentityResult result = _userManager.CreateAsync(user, _testPassword).Result;
                if (result.Succeeded)
                    AddClaimsForStatus(user, status);
            }
            //users.Select(async user =>
            //{
            //    IdentityResult result = await _userManager.CreateAsync(user, _testPassword);
            //    if (result.Succeeded)
            //    {
            //        await AddClaimsForStatus(user, status);
            //        Console.WriteLine("added claim");
            //    }
                    
            //});
        }

        public void SeedUsers(int quantityPerRole)
        {
            GenerateUsersWithClaims(GetStudentAccounts(quantityPerRole), PersonStatus.Student);
            GenerateUsersWithClaims(GetProfessorAccounts(quantityPerRole), PersonStatus.Professor);
        }
    }
}
