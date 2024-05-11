using System;
using static System.Console;

namespace MovieLibrary
{
	public class StaffMenu
	{
		public static void DisplayStaffMenu()
		{
            StaffMember staff = new StaffMember("staff", "today123");
            bool isAuthenticated = false;

            while(!isAuthenticated)
            {
                WriteLine("Enter staff username: ");
                string? username = ReadLine();

                WriteLine("Enter staff password: ");
                string? password = ReadLine();

                if (username != null && password != null && staff.Authenticate(username, password))
                {
                    isAuthenticated = true;
                    WriteLine("Login success.");
                    Console.Clear();
                }
                else
                {
                    WriteLine("Incorrect username or password. Please try again.");
                    WriteLine();
                }

            }

            DisplayMenuOptions();

        }

        private static void DisplayMenuOptions()
        {
            bool DisplayMainOptions = true;

            while (DisplayMainOptions)
            {
                WriteLine("Staff Menu");
                WriteLine("------------------------------------------------------");
                WriteLine();
                WriteLine("1. Add DVDs to system");
                WriteLine("2. Remove DVDs from system");
                WriteLine("3. Register a new member to system");
                WriteLine("4. Remove a registered member from system");
                WriteLine("5. Find a member's contact phone number, given the member's name");
                WriteLine("6. Find members who are currently renting a particular movie");
                WriteLine("0. Return to main menu");
                WriteLine();
                WriteLine("Enter your choice ==> ");

                string? input = ReadLine();
                if (!int.TryParse(input, out int choice) || choice < 0 || choice > 6)
                {
                    WriteLine("That is not a valid number. Try again. ");
                    WriteLine();
                    continue;

                }
                else
                {
                    DisplayMainOptions = false;
                }

                switch (choice)
                {
                    case 0:
                        WriteLine();
                        MainMenu.DisplayMenu();
                        break;
                    case 1:
                        // Add DVDs logic
                        break;
                    case 2:
                        // Remove DVDs logic
                        break;
                    case 3:
                        // Register new member logic
                        break;
                    case 4:
                        // Remove member logic
                        break;
                    case 5:
                        // Find member's phone number logic
                        break;
                    case 6:
                        // Find members renting a particular movie logic
                        break;
                    default:
                        WriteLine("That is not a valid number. Try again. ");
                        break;
                }
            }
            
        }
    }
}



