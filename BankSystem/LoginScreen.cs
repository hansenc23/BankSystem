using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BankSystem
{
    public class LoginScreen
    {
        Menu menu = new Menu();
        public LoginScreen()
        {

        }

        public void login()
        {
            string username, password;

            //Create login interface
            Console.Clear();
            Console.WriteLine("\t\t------------------------------------------");
            Console.WriteLine("\t\t Welcome to UTS Bank");
            Console.WriteLine("\t\t------------------------------------------");

            Console.Write("\t\tUsername: ");
            int cursorPosNameLeft = Console.CursorLeft;
            int cursorPosNameTop = Console.CursorTop;

            Console.Write("\n\t\tPassword: ");
            int cursorPosPassLeft = Console.CursorLeft;
            int cursorPosPassTop = Console.CursorTop;

            Console.Write("\n\t\t------------------------------------------");

            Console.SetCursorPosition(cursorPosNameLeft, cursorPosNameTop);
            username = Console.ReadLine();

            Console.SetCursorPosition(cursorPosPassLeft, cursorPosPassTop);
            password = "";

            try
            {
                //Replace password characters to '*'
                do
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        password += key.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                        {
                            password = password.Substring(0, (password.Length - 1));
                            Console.Write("\b \b");
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                            break;
                        }
                    }
                } while (true);;

                //Read login.txt file
                using (StreamReader sr = new StreamReader("login.txt"))
                {
                    
                    string[] lines = System.IO.File.ReadAllLines("login.txt");

                    int lineCount = 1;
                    List<Login> loginDetail = new List<Login>();
                    //store each line in login.txt file as a Login object in a list 
                    foreach (string line in lines)
                    {
                        string[] column = line.Split('|');
                        if (lineCount != 0)
                        {
                            Login login = new Login();
                            login.UserName = column[0];
                            login.Password = column[1];
                            loginDetail.Add(login);
                        }
                        lineCount++;
                    }
                  
                    
                    //Check input username and password with login.txt file
                    int counter = 0;
                    for (var i = 0; i < loginDetail.Count; i++)
                    {
                        if ((username.Equals(loginDetail[i].UserName)) && (password.Equals(loginDetail[i].Password)))
                        {
                            
                            Console.WriteLine("\n\n\t\tValid Credentials! Press Any Key to Continue...");
                            Console.ReadKey();
                            menu.startMenu();
                            break;
                        }
                        counter++;
                        
                    }


                    if(counter == loginDetail.Count)
                    {
                        Console.WriteLine("\n\n\t\tInvalid Credentials! Please Try Again");
                        Console.ReadKey();
                        login();
                    }
                  
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                login();
            }
           

             Console.ReadKey();
        }

    }
}
