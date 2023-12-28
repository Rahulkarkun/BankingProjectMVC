using BankingProjectMVC.Models;
using BankingProjectMVC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingProjectMVC.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User Add(User user)
        {
            return _userRepository.Add(user);
        }
        public string Update(User user)
        {
            return _userRepository.Update(user);
        }
        public string Delete(User user)
        {
            return _userRepository.Delete(user);
        }
        public User GetById(int userId)
        {
            return _userRepository.GetById(userId);
        }
        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }
        public User GetUserWithRole(int id)
        {
            return _userRepository.GetUserWithRole(id);
        }
        public User GetUserByUsername(string username)
        {
            return _userRepository.GetUserByUsername(username);
        }
    }
}