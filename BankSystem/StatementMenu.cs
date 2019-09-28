using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mail;
using System.Threading;
using System.Net;

namespace BankSystem
{
    public class StatementMenu
    {


        Menu menu = new Menu();
        public StatementMenu()
        {

        }

        public void statementMenu()
        {
            
            //Create "Account Statement" interface
            Console.Clear();

            Console.WriteLine("\t\t----------------------------------------");
            Console.WriteLine("\t\t\t STATEMENT");
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


                string getStatement = ""; //variable to store account details

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
                        getStatement = statement(lines[0]);
                        break;


                    }
                    else
                    {
                        throw new Exception("Account not found");
                    }


                }


                //ask user whether to email the account statement
                Console.Write("\n\n\t\tEmail statement? (Y/N) ");
                string choice = Console.ReadLine();
                if (string.Equals(choice, "Y", StringComparison.OrdinalIgnoreCase))
                {
                    sendEmail(getStatement, filePath);
                    Console.ReadKey();
                    menu.startMenu();
                }
                else if (string.Equals(choice, "N", StringComparison.OrdinalIgnoreCase))
                {
                    menu.startMenu();
                }
                else
                {
                    statementMenu();
                }

                

            }
            catch (Exception e)
            {
                Console.WriteLine("\n\t\t{0}", e);

                Console.Write("\n\t\tCheck another account? (Y/N) ");
                string choice = Console.ReadLine();
                if (string.Equals(choice, "Y", StringComparison.OrdinalIgnoreCase))
                {
                    statementMenu();
                }
                else if (string.Equals(choice, "N", StringComparison.OrdinalIgnoreCase))
                {
                    menu.startMenu();
                }
                else
                {
                    statementMenu();
                }


            }
            Console.ReadKey();

        }

        //print account details method
        public void printDetails(string details)
        {
            string[] splits = details.Split('|');
            Console.Write("\n\t\t=======================================");
            Console.Write("\n\t\tAccount found!");
            Console.WriteLine("\n\t\t----------------------------------------");
            Console.WriteLine("\t\t\t ACCOUNT DETAILS");
            Console.WriteLine("\t\t----------------------------------------");
            Console.Write("\t\tAccount Number: {0}", splits[0]);
            Console.Write("\n\t\tAccount Balance:${0}", splits[1]);
            Console.Write("\n\t\tFirst Name: {0}", splits[2]);
            Console.Write("\n\t\tLast Name: {0}", splits[3]);
            Console.Write("\n\t\tAddress: {0}", splits[4]);
            Console.Write("\n\t\tEmail: {0}", splits[5]);
            Console.Write("\n\t\tPhone: {0}", splits[6]);
            Console.Write("\n\t\t----------------------------------------");

        }

        //function to store account statement in a string
        public string statement(string details)
        {
            string[] splits = details.Split('|');
            string accountNum = splits[0];
            string accountBalance = splits[1];
            string firstName = splits[2];
            string lastName = splits[3];
            string address = splits[4];
            string email = splits[5];
            string phone = splits[6];
            string finalStatement = "";
            var sb = new StringBuilder();
           
            sb.Append("Account Number: " + accountNum);
            sb.Append("\nAccount Balance: $" + accountBalance);
            sb.Append("\nFirst Name: " + firstName);
            sb.Append("\nLast Name: " + lastName);
            sb.Append("\nAddress: " + address);
            sb.Append("\nEmail: " + email);
            sb.Append("\nPhone: " + phone);

            finalStatement = sb.ToString();

            return finalStatement;
        }

        //method to setup details needed to send email
        public void sendEmail(string accountStatement, string accountNumber)
        {
            string accountNum = accountNumber.Substring(0, 6);
            string from = "appdemo71@gmail.com";
            string password = "@Bankapp123";
            string subject = accountNum + " Account Statement";
            string message = accountStatement;
            string to = "appdemo71@gmail.com";

            EmailSend(to, password, from, subject, message);
            Console.Write("\n\t\tEmail sent!");

        }

        //send email method
        public void EmailSend(string to, string password, string from, string subject, string message)
        {
            SmtpClient client = new SmtpClient();
            MailMessage msg = new MailMessage(from, to);
            msg.Subject = subject;
            msg.Body = message;
            client.EnableSsl = true;
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(from, password);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Timeout = 50000;
            client.Send(msg);
        }
    }
}
