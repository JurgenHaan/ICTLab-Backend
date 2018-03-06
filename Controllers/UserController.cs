using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ICTLab_backend.DTO;
using ICTLab_backend.Models;

namespace ICTLab_backend.Controllers {
    
    [Route("api/[controller]")]
    public class UserController : Controller 
    {
        //Database context instance *IMPORTANT*
        private readonly DBContext _context;
        private readonly PasswordHasher _passwordHasher = new PasswordHasher();
        public UserController(DBContext context)
        {
            _context = context;
        }
        
        //Retrieves username and session
        [HttpPost]
        public ActiveUser Login([FromBody]LoginRequest request) 
        {
            //Checks if there is a sessionkey present in the sent object
            if (!string.IsNullOrEmpty(request.sessionKey))
            {
                //Checks if there is sessionkey matching in the database.
                var userSes = _context.UserSession.Single(i => i.SessionKey == request.sessionKey);
                if (userSes == null) return new ActiveUser();
                
                //Retrieves the user with the userID
                var user = _context.User.Single(i => i.ID == userSes.UserID);
                
                if (user == null) return new ActiveUser();
                
                //Returns the user.
                return new ActiveUser(user.UserName, user.fullName, request.sessionKey);
            }
            
            //Username and Password check
            if (!string.IsNullOrEmpty(request.password) || !string.IsNullOrEmpty(request.username))
            {
                //First checks for a username. If nothing is found, returns.
                var user = _context.User.Single(m => m.UserName == request.username);
                if (user == null) return new ActiveUser();
                
                //Hashes password and compares it to the requested user's password.
                var pw = _passwordHasher.GenerateSHA256(request.password);
                if (user.Password != pw) return new ActiveUser();
                
                //Checks if there is a valid sessionkey present
                var sessionCheck = _context.UserSession.Find(user.ID);
                if (sessionCheck != null)
                {
                    return new ActiveUser(user.UserName, user.fullName, sessionCheck.SessionKey);
                }
                
                //Creates a key if none is found
                var userSession = new UserSession
                {
                    SessionKey = Guid.NewGuid().ToString(),
                    User = user,
                    UserID = user.ID,
                    ExpireDate = DateTime.Now.AddDays(30)
                };
                //Adds sessionkey to the context
                _context.Add(userSession);
                
                //Writes context to the database
                _context.SaveChanges();
                
                //Returns the user
                return new ActiveUser(user.UserName,user.fullName,userSession.SessionKey);
            }
            //If nothing is sent, returns empty user
            return new ActiveUser();
        }

        //Creates a user
        [HttpPut]
        public validAction CreateUser(string username, string password, string fullname)
        {
            //Checks if there is a user present
            var userNameList = from m in _context.User
                select m.UserName;
            if (!userNameList.Contains(username)) return new validAction { valid = false };

            //Creates user and hashes password
            var user = new User
            {
                Password = _passwordHasher.GenerateSHA256(password),
                UserName = username,
                UserType = "ADMIN",
                fullName = fullname
            };
            //Adds users to the context
            _context.Add(user);
            
            //Writes context to the database
            _context.SaveChanges();
            
            return new validAction { valid = true };
        }
        
        //User delete function
        [HttpDelete]
        public validAction DeleteUser(int id)
        {
            //Checks for user's id in the database
            var user = _context.User.Single(i => i.ID == id);
            if (user == null) return new validAction { valid = false };
            
            //Removes user from the context
            _context.Remove(user);
            
            //Writes context to the database
            _context.SaveChanges();

            return new validAction { valid = true };
        }
    }
}