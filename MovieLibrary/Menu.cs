using System;
using static System.Console;

namespace MovieLibrary
{
    public class MainMenu
    {
        public static string DisplayMenu()
        {
            while (true)
            {

                WriteLine("======================================================");
                WriteLine("COMMUNITY LIBRARY MOVIE DVD MANAGEMENT SYSTEM");
                WriteLine("======================================================");
                WriteLine();
                WriteLine("Main Menu");
                WriteLine("------------------------------------------------------");
                WriteLine("Select from the following: ");
                WriteLine();
                WriteLine("1. Staff");
                WriteLine("2. Member");
                WriteLine("0. End the program");
                WriteLine();
                WriteLine("Enter your choice ==> ");

                string? input = ReadLine();
                if (!int.TryParse(input, out int choice) || choice < 0 || choice > 2)
                {
                    WriteLine("That is not a valid number. Try again. ");
                    WriteLine();
                    continue;
                }

                switch(choice)
                {
                    case 1:
                        //StaffMenu.DisplayStaffMenu();
                        break;

                    case 2:
                        //MemberMenu.DisplayMemberMenu();
                        break;

                     case 0:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}

