using System;
using static System.Console;

namespace MovieLibrary
{
	public class StaffMenu
	{
		public static void DisplayStaffMenu()
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
                WriteLine("7. PRINT TO DEBUG - TO BE DELETED AFTERWARDS");
                WriteLine("0. Return to main menu");
                WriteLine();
                Write("Enter your choice: ");

                string? input = ReadLine();
                // CHANGE BACK TO 6 AFTER REMOVING DEBUG OPTION
                if (!int.TryParse(input, out int choice) || choice < 0 || choice > 7)
                {
                    WriteLine("That is not a valid number. Try again. ");
                    WriteLine();
                    continue;

                }

                switch (choice)
                {
                    case 0:
                        WriteLine();
                        MainMenu.DisplayMenu();
                        break;
                    case 1:
                        StaffMember.AddDVD();
                        WriteLine();
                        break;
                    case 2:
                        StaffMember.RemoveDVD();
                        WriteLine();
                        break;
                    case 3:
                        StaffMember.RegisterMember();
                        WriteLine();
                        break;
                    case 4:
                        StaffMember.RemoveMember();
                        break;
                    case 5:
                        StaffMember.FindMemberPhoneNumber();
                        WriteLine();
                        break;
                    case 6:
                        StaffMember.FindRentingMembers();
                        break;
                    case 7:
                        StaffMember.PrintForDebug();
                        break;
                    default:
                        WriteLine("That is not a valid number. Try again. ");
                        break;
                }
            }

        }
    }
}



