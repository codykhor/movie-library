using System;
using static System.Console;

namespace MovieLibrary
{
	public class StaffLogin
	{
		public static void DisplayStaffLogin()
		{
            StaffMember staff = new StaffMember("staff", "today123");
            bool isAuthenticated = false;

            while (!isAuthenticated)
            {
                WriteLine();
                WriteLine("Staff Login | (0 to exit)");
                WriteLine("------------------------------------------------------");
                WriteLine();

                Write("Enter staff username: ");
                string? username = ReadLine();
                if (username == "0")
                {
                    WriteLine();
                    MainMenu.DisplayMenu();
                    break;
                }
                WriteLine();

                Write("Enter staff password: ");
                string? password = ReadLine();
                if (password == "0")
                {
                    WriteLine();
                    MainMenu.DisplayMenu();
                    break;
                }

                if (username != null && password != null && staff.Authenticate(username, password))
                {
                    WriteLine("Staff login success.");
                    WriteLine();
                    isAuthenticated = true;
                }
                else
                {
                    WriteLine("Incorrect username or password. Please try again.");
                    WriteLine();
                }

            }

            StaffMenu.DisplayStaffMenu();
        }
	}
}

