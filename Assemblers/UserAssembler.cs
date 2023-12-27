using BankingProjectMVC.Models;
using BankingProjectMVC.Services;
using BankingProjectMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BankingProjectMVC.Assemblers
{
    public class UserAssembler
    {
        private readonly IRoleService _roleService;

        public UserAssembler(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public User ConvertToModel(UserVM userVM)
        {
            var role = _roleService.GetById(userVM.RoleId);
            return new User()
            {
                Id = userVM.Id,
                Username = userVM.Username,
                Password = HashPassword(userVM.Password), // Hash the password,
                Role = role,
            };
        }
        public UserVM ConvertToViewModel(User user)
        {
            return new UserVM()
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                RoleId = user.Role.Id,
            };
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // ComputeHash - returns byte array, convert it to a string
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}