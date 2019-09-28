using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BankSystem
{
    public class DeleteMenu
    {
        Menu menu = new Menu();
        string accountNumber;
        public DeleteMenu()
        {

        }

        public void deleteMenu()
        {
            Console.Clear();
            Console.WriteLine("\t\t----------------------------------------");
            Console.WriteLine("\t\t\t\tDELETE");
            Console.WriteLine("\t\t----------------------------------------");
            Console.WriteLine("\t\t\t     ENTER DETAILS");
            Console.Write("\n\t\tAccount Number: ");
            int cursorFirstNameLeft = Console.CursorLeft;
            int cursorFirstNameTop = Console.CursorTop;

            try
            {
                Console.SetCursorPosition(cursorFirstNameLeft, cursorFirstNameTop);
                accountNumber = Console.ReadLine();

                //account number field checks
                bool containsint = accountNumber.All(char.IsDigit);

                if (containsint == false)
                {
                    throw new Exception("Invalid account number");
                }
                else if (accountNumber.Length < 6)
                {
                    throw new Exception("Account number must be 6-digit");
                }
                else if (accountNumber.Length > 6)
                {
                    throw new Exception("Account number must be 6-digit");
                }

                //Check the input account number against the files in the current directory
                string currentpath = Directory.GetCurrentDirectory();
                string[] files = Directory.GetFiles(currentpath, "*.txt");
                string filePath = accountNumber + ".txt";
                foreach (string file in files)
                {


                    if (File.Exists(filePath))
                    {

                        string[] lines = File.ReadAllLines(filePath);

                        printDetails(lines[0]);
                        break;

                    }
                    else
                    {
                        throw new Exception("Account not found");
                    }


                }

                //ask user whether to delete account
                Console.Write("\n\n\t\tAre you sure you want to delete the account? (Y/N) ");
                string choice = Console.ReadLine();
                if (string.Equals(choice, "Y", StringComparison.OrdinalIgnoreCase))
                {
                    File.Delete(filePath); //delete text file
                    Console.WriteLine("\n\t\tAccount deleted");
                    Console.ReadKey();
                    menu.startMenu();
                }
                else if (string.Equals(choice, "N", StringComparison.OrdinalIgnoreCase))
                {
                    deleteMenu();
                }
                else
                {
                    deleteMenu();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);

                Console.Write("\n\t\tRetry? (Y/N)");

                string choice = Console.ReadLine();

                if (string.Equals(choice, "Y", StringComparison.OrdinalIgnoreCase))
                {
                    deleteMenu();
                }
                else if (string.Equals(choice, "N", StringComparison.OrdinalIgnoreCase))
                {
                    menu.startMenu();
                }
                else
                {
                    deleteMenu();
                }

            }

           

        }

        //method to print details
        public void printDetails(string details)
        {
            string[] splits = details.Split('|');
            Console.Write("\n\t\t=======================================");
            Console.Write("\n\t\tAccount found!");
            Console.WriteLine("\n\t\t----------------------------------------");
            Console.WriteLine("\t\t\t ACCOUNT DETAILS");
            Console.WriteLine("\t\t----------------------------------------");
            Console.Write("\t\tAccount Number: {0}", splits[0]);
            Console.Write("\n\t\tAccount Balance: ${0}", splits[1]);
            Console.Write("\n\t\tFirst Name: {0}", splits[2]);
            Console.Write("\n\t\tLast Name: {0}", splits[3]);
            Console.Write("\n\t\tAddress: {0}", splits[4]);
            Console.Write("\n\t\tEmail: {0}", splits[5]);
            Console.Write("\n\t\tPhone: {0}", splits[6]);
            Console.Write("\n\t\t----------------------------------------");

        }
    }
}
