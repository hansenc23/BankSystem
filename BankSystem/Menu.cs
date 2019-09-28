using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    public class Menu
    {
        int option;
       
        public Menu()
        {
            
        }


        public void startMenu()
        {
            //Create menu interface
            Console.Clear();
            Console.WriteLine("\t\t------------------------------------------");
            Console.WriteLine("\t\t WELCOME TO SIMPLE BANKING SYSTEM");
            Console.WriteLine("\t\t------------------------------------------");
            Console.Write("\t\t 1. Create a new account");
            Console.Write("\n\t\t 2. Search for an account");
            Console.Write("\n\t\t 3. Deposit");
            Console.Write("\n\t\t 4. Withdraw");
            Console.Write("\n\t\t 5. A/C Statement");
            Console.Write("\n\t\t 6. Delete account");
            Console.Write("\n\t\t 7. Exit");
            Console.Write("\n\t\t------------------------------------------");
            Console.Write("\n\t\t Please enter you choice (1-7): ");

            
            try
            {
                //Menu options
                string userInput = Console.ReadLine();
                option = Convert.ToInt32(userInput);
                switch (option)
                {
                    case 1:
                        createMenu();
                        break;
                    case 2:
                        searchMenu();
                        break;
                    case 3:
                        depositMenu();
                        break;
                    case 4:
                        withdrawMenu();
                        break;
                    case 5:
                        statementMenu();
                        break;
                    case 6:
                        deleteMenu();
                        break;
                    case 7:
                        System.Environment.Exit(0);
                        break;

                }

                //input check if number is between 1 and 7
                if(!betweenRange(1,7,option))
                {
                    Console.WriteLine("\n\t\tInvalid option, please try again");
                    Console.ReadKey();
                    startMenu();

                }
                
                    
            }
            catch(Exception e)
            {
                Console.WriteLine(e);


                Console.ReadKey();
                startMenu();

            }
            

            


            //Console.ReadKey();
        }

        //check if number is between a range
        public bool betweenRange(int a, int b, int number)
        {
            return (a <= number && number <= b);
        }

        public void createMenu()
        {
            CreateMenu menu = new CreateMenu();
            menu.createMenu();
        }

        public void searchMenu()
        {
            SearchMenu menu = new SearchMenu();
            menu.searchMenu();
        }

        public void depositMenu()
        {
            DepositMenu menu = new DepositMenu();
            menu.depositMenu();
        }

        public void withdrawMenu()
        {
            WithdrawMenu menu = new WithdrawMenu();
            menu.withdrawMenu();

        }

        public void deleteMenu()
        {
            DeleteMenu menu = new DeleteMenu();
            menu.deleteMenu();

        }

        public void statementMenu()
        {
            StatementMenu menu = new StatementMenu();
            menu.statementMenu();
        }



    }
}
