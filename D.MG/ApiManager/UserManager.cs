using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ApiManager
{
    public class UserManager
    {
        public static Tuple<List<User>, List<Debt>> InsertUsers()
        {
            var users = new List<User>();
            var distinctUsers = new List<User>();
            var debts = new List<Debt>();
            string[] headers;
            string[] fields;
            int counter = 0;
            FileStream fileStream = new FileStream(@"D:\git\DebtManagment\assets\CitizenDebts_100.txt", FileMode.Open);
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
                        string line = reader.ReadLine();
                        fields = line.Split(";");
                        for (int i = 0; i < headers.Length; i++)
                        {
                            switch (headers[i])
                            {
                                case "FIRST_NAME":
                                    user.Name = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
                                    break;
                                case "LAST_NAME":
                                    user.Lastname = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
                                    break;
                                case "ADDRESS":
                                    //user.Address.Street = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
                                    break;
                                case "VAT":
                                    user.Vat = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
                                    debt.Vat = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
                                    break;
                                case "COUNTY":
                                    //user.Address.Country = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
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
                            //user.Password = AuthProvider.PasswordHasher.HashPassword("123456");
                            user.Password = "123456";
                        }
                        debts.Add(debt);
                        users.Add(user);
                    }
                    distinctUsers = users.GroupBy(e => e.Vat).Select(group => group.First()).ToList();

                }
                catch (Exception ex)
                {
                    var error = counter;
                    var _ex = ex;
                }
            }

            return new Tuple<List<User>, List<Debt>>(distinctUsers, debts);
        }
    }
}
