using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApplication
{
    public class Account
    {
        public static string PathDefaultPicture = "profile_image_default.png";
        public Account()
        {
            ProfilePicturePath = PathDefaultPicture;
            Username = "Stranger";
        }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Detail { get; set; }
        public string ProfilePicturePath { get; set; }
    }
}
