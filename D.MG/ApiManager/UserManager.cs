using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ApiManager
{
    public class UserManager
    {
        public static List<User> InsertUsers()
        {
            var users = new List<User>();
            string[] headers;
            string[] fields;
            int counter = 0;
            var fileStream = File.ReadAllLines(@"D:\git\DebtManagment\assets\CitizenDebts_1M_3.txt");

            var firstline = fileStream[0];
            headers = firstline.Split(";");
            try
            {
                for (var j = 1; j < fileStream.Length; j++)
                {
                    counter++;
                    User user = new User();
                    Debt debt = new Debt();
                    user.Password = AuthProvider.PasswordHasher.HashPassword("123456");
                    fields = fileStream[j].Split(";");
                    user.Name = fields[1];
                    user.Lastname = fields[2];
                    user.Address.Street = fields[5];
                    user.Vat = fields[0];
                    user.Address.Country = fields[6];
                    user.Email = fields[3];
                    user.Mobile = fields[4];
                    debt.Amount = Double.Parse(fields[9]);
                    debt.Description = fields[8];
                    debt.DateDue = DateTime.Now;
                    debt.BillId = fields[7];
                    //if (fields.Length != headers.Length) { break; }

                    //for (int i = 0; i < headers.Length; i++)
                    //{
                    //    if (counter == 72)
                    //    {
                    //        var temp = fields;
                    //    }
                    //    switch (headers[i])
                    //    {
                    //        case "FIRST_NAME":
                    //            user.Name = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
                    //            break;
                    //        case "LAST_NAME":
                    //            user.Lastname = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
                    //            break;
                    //        case "ADDRESS":
                    //            user.Address.Street = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
                    //            break;
                    //        case "VAT":
                    //            user.Vat = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
                    //            break;
                    //        case "COUNTY":
                    //            user.Address.Country = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
                    //            break;
                    //        case "EMAIL":
                    //            user.Email = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
                    //            break;
                    //        case "PHONE":
                    //            user.Mobile = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
                    //            break;
                    //        case "AMOUNT":
                    //            debt.Amount = string.IsNullOrEmpty(fields[i].ToString()) ? 0 : Double.Parse(fields[i]);
                    //            break;
                    //        case "DESCRIPTION":
                    //            debt.Description = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
                    //            break;
                    //        case "BILL_ID":
                    //            debt.BillId = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
                    //            break;
                    //        case "DATE_DUE":
                    //            debt.DateDue = string.IsNullOrEmpty(fields[i]) ? DateTime.Now : DateTime.Now;
                    //            break;
                    //    }
                    //}
                    var userIndex = users.FindIndex(x => x.Vat == user.Vat);
                    Console.WriteLine(counter);
                    if (userIndex != -1)
                    {
                        users[userIndex].Debts.Add(debt);
                    }
                    else
                    {
                        user.Debts.Add(debt);
                        users.Add(user);
                    }

                }

            }
            catch (Exception ex)
            {
                var error = counter;
                var _ex = ex;
            }


            return users;
        }
    }
}
