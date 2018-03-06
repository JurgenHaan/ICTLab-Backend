using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using ICTLab_backend.Controllers;
using ICTLab_backend.DTO;
using ICTLab_backend.Models;
using Xunit;

namespace Tests
{
    public class UserControllerTest
    {
        //Empty Active User used for testing.
        public readonly ActiveUser emptyActiveUser = new ActiveUser();

        //Test User. Used for in memory testing only.
        User user = new User
        {
            ID = 1,
            fullName = "Test",
            Password = "TestPassword",
            UserName = "TestUser",
            UserType = ""
        };
        
        //Create Loginrequest. Consumed by UserController.
        public LoginRequest _sessionKeyLogin = new LoginRequest
        {
            username = "TestUser",
            password = "TestPassword",
            sessionKey = null
        };

        //Creation test. Password encryption test
        [Fact]
        public void Create_And_Save_User_And_Returns_User_ID()
        {
            //Init DBContext
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "CreateUserDB")
                .Options;
            var context = new DBContext(options);
            var service = new UserController(context);

            //Should create user
            service.CreateUser("Test", "TestPassword", null);

            //Checks for User ID. Should be 1.
            Assert.Equal(1, context.User.Single().ID);

            //Password encryption test. Password should not be same. If (same) {Password encryption no worky}
            Assert.NotEqual("TestPassword", context.User.Single().Password);
            
            context.Database.EnsureDeleted();
        }

        [Fact]
        private void Add_User_Should_Return_Not_Null_Active_User()
        {
            //Init DBContext
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "GetUserDB")
                .Options;
            var context = new DBContext(options);
            var service = new UserController(context);

            //Save user to database
            context.Add(user);
            context.SaveChanges();

            //Shouldn't return Active user
            ActiveUser activeUser = service.Login(_sessionKeyLogin);

            //Should not be empty.
            Assert.False(emptyActiveUser == activeUser);
            
            context.Database.EnsureDeleted();
        }

        [Fact]
        public void Add_User_To_DB_And_Delete_User_Count_0()
        {
            //Init DBContext
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "DeleteUserDB")
                .Options;
            var context = new DBContext(options);
            var service = new UserController(context);

            //Save user to database
            context.Add(user);
            context.SaveChanges();

            //Written to database? Should be 1 after line above.
            Assert.Equal(1, context.User.Count());

            //Deletes created user
            service.DeleteUser(1);

            //Confirmed delete
            Assert.Equal(0, context.User.Count());
            
            context.Database.EnsureDeleted();
        }

        //Add 2 same people to the database. Not possible.
        [Fact]
        public void Add_2_Users_To_DB_And_Raise_Error()
        {
            //Init DBContext
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "Add2UsersToUserDB")
                .Options;
            var context = new DBContext(options);
            var service = new UserController(context);
            
            //Should create user
            service.CreateUser("Test", "TestPassword", null);
            
            //Should not create user. User in database.
            //Assert.False(service.CreateUser("Test", "TestPassword", null));

            context.Database.EnsureDeleted();
        }

        //
        //Integration test
        //
        [Fact]
        public void t()
        {
            //Init DBContext
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "Diiubvivb")
                .Options;
            var context = new DBContext(options);
            var service = new UserController(context);
            
            
            //Should create user
            service.CreateUser("TestUser", "TestPassword", null);
            
            //Logging in the user.
            ActiveUser activeUser = service.Login(_sessionKeyLogin);
            
            //New sessionkey should have been generated
            Assert.NotNull(activeUser.sessionKey);
        }
    }
}

