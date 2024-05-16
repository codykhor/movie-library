using System;
using static System.Console;

namespace MovieLibrary
{
    public class MainMenu
    {
        public static void DisplayMenu()
        {
            bool DisplayMainMenu = true;

            while (DisplayMainMenu)
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
                Write("Enter your choice: ");

                string? input = ReadLine();
                if (!int.TryParse(input, out int choice) || choice < 0 || choice > 2)
                {
                    WriteLine("That is not a valid number. Try again. ");
                    WriteLine();
                    continue;
                }
                else
                {
                    DisplayMainMenu = false;
                }

                switch(choice)
                {
                    case 1:
                        Clear();
                        StaffLogin.DisplayStaffLogin();
                        break;

                    case 2:
                        Clear();
                        Member? validMember = MemberLogin.DisplayMemberLogin();
                        if (validMember != null)
                        {
                            MemberMenu.DisplayMemberMenu(validMember);
                        }
                        break;

                     case 0:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}

