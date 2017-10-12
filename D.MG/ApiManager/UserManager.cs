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
            FileStream fileStream = new FileStream(@"D:\git\DebtManagment\assets\CitizenDebts_1M_3.txt", FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                var firstline = reader.ReadLine();
                headers = firstline.Split(";");

                try
                {
                    while (!reader.EndOfStream)
                    {
                        counter++;
                        User user = new User();
                        Debt debt = new Debt();
                        user.Password = AuthProvider.PasswordHasher.HashPassword("123456");
                        string line = reader.ReadLine();
                        fields = line.Split(";");

                        if (fields.Length != headers.Length) { break; }

                        for (int i = 0; i < headers.Length; i++)
                        {
                            if (counter == 72)
                            {
                                var temp = fields;
                            }
                            switch (headers[i])
                            {
                                case "FIRST_NAME":
                                    user.Name = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
                                    break;
                                case "LAST_NAME":
                                    user.Lastname = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
                                    break;
                                case "ADDRESS":
                                    user.Address.Street = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
                                    break;
                                case "VAT":
                                    user.Vat = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
                                    break;
                                case "COUNTY":
                                    user.Address.Country = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
                                    break;
                                case "EMAIL":
                                    user.Email = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
                                    break;
                                case "PHONE":
                                    user.Mobile = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
                                    break;
                                case "AMOUNT":
                                    debt.Amount = string.IsNullOrEmpty(fields[i].ToString()) ? 0 : Double.Parse(fields[i]);
                                    break;
                                case "DESCRIPTION":
                                    debt.Description = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
                                    break;
                                case "BILL_ID":
                                    debt.BillId = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
                                    break;
                                case "DATE_DUE":
                                    debt.DateDue = string.IsNullOrEmpty(fields[i]) ? DateTime.Now : DateTime.Now;
                                    break;
                            }

                        }
                        var userIndex = users.FindIndex(x => x.Vat == user.Vat);
                        Console.WriteLine(counter);
                        if(userIndex != -1)
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
            }

            return users;
        }
    }
}
