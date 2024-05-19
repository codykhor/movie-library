using System;
using static System.Console;

namespace MovieLibrary
{
	public class MemberLogin
	{
        public static Member? DisplayMemberLogin()
        {
            Member? validMember = null;

            bool isValidInfo = false;
            bool isValidPwd = false;
            string firstName;
            string lastName;
            int password = 0;
            int pwd;
            
            while (!isValidInfo)
            {
                WriteLine();
                WriteLine("Member Login | (0 to exit)");
                WriteLine("------------------------------------------------------");
                WriteLine();

                // Get full name
                firstName = CheckStringInput("Enter first name: ");
                if (firstName == "0")
                {
                    WriteLine();
                    MainMenu.DisplayMenu();
                    return null;
                }
                WriteLine();

                lastName = CheckStringInput("Enter last name: ");
                if (lastName == "0")
                {
                    WriteLine();
                    MainMenu.DisplayMenu();
                    return null;
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
                        return null;

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
                validMember = MemberCollection.Members.Authenticate(firstName, lastName, password);
                if (validMember != null)
                {
                    WriteLine("Member login success.");
                    WriteLine();
                    isValidInfo = true;
                }
                else
                {
                    WriteLine("Incorrect username/password or member doesn't exist. Please try again.");
                    WriteLine();
                    isValidPwd = false;
                }

            }
            return validMember;
        }

        public static string CheckStringInput(string question)
        {
            Write(question);
            string? input = ReadLine();

            while (true)
            {
                if (input == "0")
                {
                    MainMenu.DisplayMenu();
                    return null;
                }

                if (string.IsNullOrWhiteSpace(input) || !IsAlphabetsOnly(input))
                {
                    WriteLine("Invalid input. Please try again.");
                    WriteLine();
                    Write(question);
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

