using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BankSystem
{
    public class SearchMenu
    {
        
        
        Menu menu = new Menu();
        public SearchMenu()
        {

        }

        public void searchMenu()
        {

            //Create "Search Account" interface
            Console.Clear();

            Console.WriteLine("\t\t----------------------------------------");
            Console.WriteLine("\t\t\t SEARCH AN ACCOUNT");
            Console.WriteLine("\t\t----------------------------------------");
            Console.WriteLine("\t\t\t ENTER DETAILS");
            Console.Write("\n\t\tAccount Number: ");
            int cursorFirstNameLeft = Console.CursorLeft;
            int cursorFirstNameTop = Console.CursorTop;


            


            try
            {
                string accountNumber;

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
                    
                    //if found, read the text file
                    if (File.Exists(filePath))
                    {

                        string[] lines = File.ReadAllLines(filePath);

                        printDetails(lines[0]);
                        break;


                    }
                    //if not found, show error message
                    else if(!File.Exists(filePath))
                    {
                        Console.WriteLine("\n\t\tAccount not found!");
                        break;
                        //throw new Exception("Account not found");
                    }


                }

                

                //Option to check for another account
                Console.Write("\n\n\t\tCheck another account? (Y/N) ");
                string choice = Console.ReadLine();
                if (string.Equals(choice, "Y", StringComparison.OrdinalIgnoreCase))
                {
                    searchMenu();
                }
                else if (string.Equals(choice, "N", StringComparison.OrdinalIgnoreCase))
                {
                    menu.startMenu();
                }
                else
                {
                    searchMenu();
                }

            }
            catch (Exception e)
            {
                //Console.WriteLine("\n\t\t{0}", e);
                Console.WriteLine("\n\t\tInvalid account number");

                Console.Write("\n\t\tCheck another account? (Y/N) ");
                string choice = Console.ReadLine();
                if (string.Equals(choice, "Y", StringComparison.OrdinalIgnoreCase))
                {
                    searchMenu();
                }
                else if (string.Equals(choice, "N", StringComparison.OrdinalIgnoreCase))
                {
                    menu.startMenu();
                }
                else
                {
                    searchMenu();
                }


            }
            Console.ReadKey();
           
        }


        //Print the contents of the text file
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
