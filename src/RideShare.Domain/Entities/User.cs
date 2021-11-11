using System;
using System.Linq;
using RideShare.Domain.Common;

namespace RideShare.Domain.Entities
{
    public class User : AuditableEntity
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        public Driver Driver { get; private set; }
        public Passenger Passenger { get; private set; }

        //for db data seeding
        public User(Guid id, string username, string password)
        {
            if(CheckUsername(username) && CheckPassword(password))
            {
                Id = id;
                Username = username;
                Password = password;
            }
        }

        public User(string username, string password)
        {
            if(CheckUsername(username) && CheckPassword(password))
            {
                Username = username;
                Password = password;
            }
        }

        public User()
        {
            
        }

        public Driver CreateDriver()
        {
            var driver = new Driver(this);
            Driver = driver;
            return driver;
        }

        public Passenger CreatePassenger()
        {
            var passenger = new Passenger(this);
            Passenger = passenger;
            return passenger;
        }

        public void SetPassword(string password)
        {
            if (CheckPassword(password))
            {
                Password = password;
            }
        }

        private bool CheckUsername(string username)
        {
            if (username == null || username.Length < 4 || username.Length > 10)
            {
                throw new ArgumentException();
            }
            else
            {
                return true;
            }
        }

        private bool CheckPassword(string password)
        {
            if (password == null || password.Length < 4 || password.Length > 10)
            {
                throw new ArgumentException();
            }
            else
            {
                return true;
            }
        }
    }
}