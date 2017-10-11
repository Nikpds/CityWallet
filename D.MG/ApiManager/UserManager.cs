using Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace ApiManager
{
    public class UserManager
    {
        public static List<User> InsertUsers()
        {
            var users = new List<User>();
            FileStream fileStream = new FileStream(@"D:\git\DebtManagment\assets\CitizenDebts_100.txt", FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                var headers = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                }
            }

            return users;
        }
    }
}
