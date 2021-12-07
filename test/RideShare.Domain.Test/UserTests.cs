using System;
using RideShare.Domain.Entities;
using Xunit;

namespace RideShare.Domain.Test
{
    public class UserTests
    {
        [Fact]
        public void ThrowsArgumentException_WhenCreateUser_WithWrongUsername()
        {
            string username = "wrongusernamewithover10length";
            Assert.Throws<ArgumentException>(()=>{
                var user = new User(username,"testpass");
            });
        }

        [Fact]
        public void ThrowsArgumentException_WhenCreateUser_WithWrongPassword()
        {
            string password = "123";
            Assert.Throws<ArgumentException>(()=>{
                var user = new User("testuser",password);
            });
        }

        [Fact]
        public void Success_WhenCreateUser_WithGoodUsernameAndPassword()
        {
            string username = "goodUser";
            string password = "123456";
            var user = new User(username,password);
            Assert.NotNull(user);
        }

        [Fact]
        public void Success_WhenCreateDriver()
        {
            var user = new User("goodUser","123456");
            var driver = user.CreateDriver();
            Assert.NotNull(driver);
        }

        [Fact]
        public void Success_WhenCreatePassenger()
        {
            var user = new User("goodUser","123456");
            var passenger = user.CreatePassenger();
            Assert.NotNull(passenger);
        }

        [Fact]
        public void ThrowsArgumentException_WhenSetPassword_WithWrongPassword()
        {
            string username = "goodUser";
            string password = "123456";
            var user = new User(username,password);

            Assert.Throws<ArgumentException>(()=>{
                user.SetPassword("wrongpassword123456");
            });
        }

        [Fact]
        public void Success_WhenSetPassword_WithGoodPassword()
        {
            string username = "goodUser";
            string password = "123456";
            var user = new User(username,password);

            string newPassword = "goodpass";
            user.SetPassword(newPassword);
            Assert.Equal(newPassword, user.Password);
        }
    }
}