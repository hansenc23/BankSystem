using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Net.Mail;
using System.Threading;
using System.Net;

namespace BankSystem
{
    public class CreateMenu
    {
        Menu menu = new Menu();
        string firstName, lastName, address, email, phone;
        string accountNumber;
        string balance = "0";
        
        public CreateMenu()
        {

        }

        public void createMenu()
        {

            //Create "Create Account" interface
            Console.Clear();
     
            Console.WriteLine("\t\t----------------------------------------");
            Console.WriteLine("\t\t\t CREATE AN ACCOUNT");
            Console.WriteLine("\t\t----------------------------------------");

            Console.Write("\t\tFirst Name: ");
            int cursorFirstNameLeft = Console.CursorLeft;
            int cursorFirstNameTop = Console.CursorTop;

            Console.Write("\n\t\tLast Name: ");
            int cursorLastNameLeft = Console.CursorLeft;
            int cursorLastNameTop = Console.CursorTop;

            Console.Write("\n\t\tAddress: ");
            int cursorAddressLeft = Console.CursorLeft;
            int cursorAddressTop = Console.CursorTop;

            Console.Write("\n\t\tPhone: ");
            int cursorPhoneLeft = Console.CursorLeft;
            int cursorPhoneTop = Console.CursorTop;

            Console.Write("\n\t\tEmail: ");
            int cursorEmailLeft = Console.CursorLeft;
            int cursorEmailTop = Console.CursorTop;

            try
            {

                Console.SetCursorPosition(cursorFirstNameLeft, cursorFirstNameTop);
                firstName = Console.ReadLine();

                Console.SetCursorPosition(cursorLastNameLeft, cursorLastNameTop);
                lastName = Console.ReadLine();

                Console.SetCursorPosition(cursorAddressLeft, cursorAddressTop);
                address = Console.ReadLine();

                Console.SetCursorPosition(cursorPhoneLeft, cursorPhoneTop);
                phone = Console.ReadLine();

                Console.SetCursorPosition(cursorEmailLeft, cursorEmailTop);
                email = Console.ReadLine();

                

                //Phone number length field checks
                if (phone.Length > 10)
                {
                    //throw new Exception("Phone number is too long");
                    Console.WriteLine("\n\t\tInvalid Phone number, please try again");
                    Console.ReadKey();
                    createMenu();
                }

                //Check if phone number contains a char or string
                bool containsint = phone.All(char.IsDigit);
                if (containsint == false)
                {
                    Console.WriteLine("\n\t\tInvalid phone number, please try again");
                    Console.ReadKey();
                    createMenu();

                }

                if(isValidEmail(email) == false)
                {
                    Console.WriteLine("\n\t\tInvalid email, please try again");
                    Console.ReadKey();
                    createMenu();
                }



                do
                {
                    //ask user whether entered details are correct
                    Console.Write("\n\t\tIs the information correct? (Y/N) ");
                    string choice = Console.ReadLine();
                    //if yes, create a new text file and store account details in that text file and email the account details
                    if (string.Equals(choice, "Y", StringComparison.OrdinalIgnoreCase))
                    {
                        Random rand = new Random();
                        int value = rand.Next(100000, 999999);
                        string uniqueNumber = value.ToString();
                        var fileName = string.Format(@"{0}.txt", uniqueNumber);

                        using (FileStream fs = new FileStream(fileName, FileMode.Create))
                        {
                            using (StreamWriter sw = new StreamWriter(fs))
                            {

                                accountNumber = value.ToString();
                                Account account = new Account();
                                account.FirstName = firstName;
                                account.LastName = lastName;
                                account.Address = address;
                                account.Email = email;
                                account.Phone = phone;
                                account.AccountNumber = accountNumber;
                                account.AccountBalance = balance;

                                string fullText = (accountNumber + "|" + balance + "|" + firstName + "|" + lastName + "|" + address + "|" + email + "|" + phone + "|");

                                sw.WriteLine(fullText);


                            }
                        }
                        Console.Write("\n\t\tAccount Created! details will be provided via email");
                        Console.Write("\n\t\tYour Account Number is: {0}", accountNumber);

                        //Find the file with the corresponding account number in current directory
                        string accountDetails = "";
                        string currentpath = Directory.GetCurrentDirectory();
                        string[] files = Directory.GetFiles(currentpath, "*.txt");
                        string filePath = uniqueNumber + ".txt";
                        foreach (string file in files)
                        {

                            //if found read the file and email the account details
                            if (File.Exists(filePath))
                            {

                                string[] lines = File.ReadAllLines(filePath);

                                accountDetails = getAccountDetails(lines[0]);
                                sendEmail(accountDetails, filePath, email);
                                break;


                            }
                            else
                            {
                                throw new Exception("Account not found");
                            }


                        }
                        Console.WriteLine("\n\t\tSuccess!");
                        Console.ReadKey();
                        menu.startMenu();

                    }
                    else if (string.Equals(choice, "N", StringComparison.OrdinalIgnoreCase))
                    {
                        createMenu();
                    }
                    else
                    {
                        Console.WriteLine("\t\tInvalid input, try again");
                    }



                } while (true);

            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                createMenu();
            }

            


            Console.ReadKey();
        }

        //function to check email format
        public bool isValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            if (email.Contains("@") && (email.Contains("gmail.com") || email.Contains("outlook.com")))
            {
                return true;
            }

            return false;
        }

        //function to store account details in a string
        public string getAccountDetails(string details)
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
        public void sendEmail(string accountStatement, string accountNumber, string to)
        {
            string accountNum = accountNumber.Substring(0, 6);
            string from = "appdemo71@gmail.com";
            string password = "@Bankapp123";
            string subject = accountNum + " Account Details";
            string message = accountStatement;
            

            EmailSend(to, password, from, subject, message);
            

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
