using System;
using static System.Console;

namespace MovieLibrary
{
	public class MemberMenu
	{
		public static void DisplayMemberMenu(Member member)
		{
            bool DisplayMainOptions = true;

            while (DisplayMainOptions)
            {
                WriteLine("Member Menu");
                WriteLine("------------------------------------------------------");
                WriteLine();
                WriteLine("1. Browse all the movies");
                WriteLine("2. Display all the information about a movie, given the title of the movie");
                WriteLine("3. Borrow a movie DVD");
                WriteLine("4. Return a movie DVD");
                WriteLine("5. List current borrowing movies");
                WriteLine("6. Display the top 3 movies rented by the members");
                WriteLine("7. PRINT FOR DEBUG - TO BE REMOVED LATER");
                WriteLine("0. Return the main menu");
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
                        Member.DisplayAllMovies();
                        WriteLine();
                        break;
                    case 2:
                        Member.SearchMovies(member);
                        WriteLine();
                        break;
                    case 3:
                        Member.BorrowMovie(member);
                        WriteLine();
                        break;
                    case 4:
                        // logic
                        break;
                    case 5:
                        Member.PrintBorrowHistory(member);
                        WriteLine();
                        break;
                    case 6:
                        // logic
                        break;
                    case 7:
                        // logic
                        break;
                    default:
                        WriteLine("That is not a valid number. Try again. ");
                        break;
                }
            }
            
        }
	}
}

