using System;
using static System.Console;

namespace MovieLibrary
{
	public class MemberLogin
	{
        public static void DisplayMemberLogin()
        {

            bool isValidInfo = false;
            bool isValidPwd = false;
            string firstName;
            string lastName;
            int password = 0;
            int pwd;
            
            while (!isValidInfo)
            {
                WriteLine("Member Login | (0 to exit)");
                WriteLine("------------------------------------------------------");
                WriteLine();

                // Get full name
                firstName = CheckStringInput("Enter first name: ");
                if (firstName == "0")
                {
                    WriteLine();
                    MainMenu.DisplayMenu();
                    break;
                }
                WriteLine();

                lastName = CheckStringInput("Enter last name: ");
                if (lastName == "0")
                {
                    WriteLine();
                    MainMenu.DisplayMenu();
                    break;
                }
                WriteLine();

                // Get password
                while (!isValidPwd)
                {
                    Write("Enter password: ");
                    string? input = ReadLine();

                    if (input == "0")
                    {
                        WriteLine();
                        MainMenu.DisplayMenu();
                        break;

                    }
                    else if (input?.Length == 4)
                    {
                        if (int.TryParse(input, out pwd))
                        {
                            password = pwd;
                            isValidPwd = true;
                        }
                    }
                    else
                    {
                        WriteLine("Invalid input. Please try again with four-digit password.");
                        WriteLine();
                    }

                }

                // Authentication process
                if (MemberCollection.Members.Authenticate(firstName, lastName, password))
                {
                    WriteLine("Member login success.");
                    WriteLine();
                    MemberMenu.DisplayMemberMenu();
                    break;
                }
                else
                {
                    WriteLine("Incorrect username/password or member doesn't exist. Please try again.");
                    WriteLine();
                    isValidPwd = false;
                }

            }
            
        }

        public static string CheckStringInput(string question)
        {
            Write(question);
            string? input = ReadLine();

            while (true)
            {
                if (input == "0")
                {
                    Clear();
                    MainMenu.DisplayMenu();
                }

                if (string.IsNullOrWhiteSpace(input) || !IsAlphabetsOnly(input))
                {
                    WriteLine("Invalid input. Please try again.");
                    WriteLine();
                    WriteLine(question);
                    input = ReadLine();
                }
                else
                {
                    return input;
                }
            }
        }

        public static bool IsAlphabetsOnly(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}

