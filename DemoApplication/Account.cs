using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApplication
{
    public class Account
    {
        public Account()
        {
            ProfilePicturePath = "profile_image_default.png";
        }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ProfilePicturePath { get; set; }
    }
}
