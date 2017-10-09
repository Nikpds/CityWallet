using Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace ApiManager
{
    public class UserManager
    {

        public static void InsertUsers()
        {

            FileStream fileStream = new FileStream(@"D:\git\DebtManagment\assets\CitizenDebts_100.txt", FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                }
              
               
            }
           
                //var headers = parser.ReadFields();
                //while (!parser.EndOfData)
                //{
                //    pointer++;
                //    fields = parser.ReadFields();
                //    User game = new User();
                //    for (int i = 0; i < headers.Length; i++)
                //    {
                //        switch (headers[i])
                //        {
                //            case "Interne referentie":
                //              //  game.Code = string.IsNullOrEmpty(fields[i]) ? string.Empty : fields[i].ToString();
                //                break;
                //            case "Releasedatum":
                //               // game.EntryDate = DateTime.Now;
                //                break;
                //            case "Consumenten advies prijs":
                //               // game.Price = string.IsNullOrEmpty(fields[i]) ? "0" : fields[i].ToString();
                //                break;
                //            case "Netto inkoop prijs":
                //               // game.InitialPrice = string.IsNullOrEmpty(fields[i]) ? "0" : fields[i].ToString();
                //                break;
                //            case "Merk":
                //             //   game.Title = string.IsNullOrEmpty(fields[i]) ? "΄Κενό πεδίο" : fields[i].ToString();
                //                break;
                //        }
                     
                //    }
                //    parsedData.Add(game);
                //}

                //parser.Close();
              
          
        }
    }
}
