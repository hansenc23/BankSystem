using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BankSystem
{
    public class DepositMenu
    {
        Menu menu = new Menu();
        string accountNumber;
        

        public DepositMenu()
        {

        }

        public void depositMenu()
        {
            
            //Create "Deposit" interface
            Console.Clear();

            Console.WriteLine("\t\t----------------------------------------");
            Console.WriteLine("\t\t\t\tDEPOSIT");
            Console.WriteLine("\t\t----------------------------------------");
            Console.WriteLine("\t\t\t     ENTER DETAILS");
            Console.Write("\n\t\tAccount Number: ");
            int cursorFirstNameLeft = Console.CursorLeft;
            int cursorFirstNameTop = Console.CursorTop;


          
            try
            {
                Console.SetCursorPosition(cursorFirstNameLeft, cursorFirstNameTop);
                accountNumber = Console.ReadLine();

                //Account number field checks
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

                
               
                string updatedLine; //variable to store the updated account balance after deposit

                //Check the input account number against the files in the current directory
                string currentpath = Directory.GetCurrentDirectory();
                string[] files = Directory.GetFiles(currentpath, "*.txt");
                string filePath = accountNumber + ".txt";
                foreach (string file in files)
                {
                    

                    if (File.Exists(filePath))
                    {

                        
                        break;

                    }
                    else
                    {
                        throw new Exception("Account not found");
                    }


                }

                
                string[] lines = File.ReadAllLines(filePath);
                

                updatedLine = deposit(lines[0]); //stores the new account details with updated balance in a variable
                
                lines[0] = updatedLine; //get the old account details and replace it with the new account details containing the new balance
                File.WriteAllLines(filePath, lines); //write the new account details into the text file
                Console.WriteLine("\n\t\tDeposit Succesful!");
                Console.ReadKey();
                menu.startMenu();

            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                
                Console.Write("\n\t\tRetry? (Y/N)");
                
                string choice = Console.ReadLine();
                
                if (string.Equals(choice, "Y", StringComparison.OrdinalIgnoreCase))
                {
                    depositMenu();
                }
                else if (string.Equals(choice, "N", StringComparison.OrdinalIgnoreCase))
                {
                    menu.startMenu();
                }
                else
                {
                    depositMenu();
                }

            }


            
            Console.ReadKey();
        }

        //deposit function that returns a string of the updated account details with new balance
        public string deposit(string details)
        {

            string[] splits = details.Split('|');
            Console.Write("\t\tDeposit: $");

            string inputAmount = Console.ReadLine();
            
            string accountNum = splits[0];
            string currbalance = splits[1];
            string firstName = splits[2];
            string lastName = splits[3];
            string address = splits[4];
            string email = splits[5];
            string phone = splits[6];


            double amount = Convert.ToDouble(inputAmount);
            double balance = double.Parse(currbalance, System.Globalization.CultureInfo.InvariantCulture);

            balance += amount;

            string newBalance = balance.ToString();

            splits[1] = newBalance;

            string final = (splits[0] + "|" + splits[1] + "|" + splits[2] + "|" + splits[3] + "|" + splits[4] + "|" + splits[5] + "|" + splits[6] + "|");

            return final;
        }
       

       

       


    }
}
