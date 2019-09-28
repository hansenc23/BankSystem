using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BankSystem
{
    class Program
    {
        //Launch login screen
        static void Main(string[] args)
        {
            LoginScreen loginScreen = new LoginScreen();
            //Menu menu = new Menu();



            loginScreen.login();
            //menu.startMenu();
            

            Console.ReadKey();

        }
    }
}
