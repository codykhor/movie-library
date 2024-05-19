using System;
using static System.Console;

namespace MovieLibrary
{
	public class StaffMember
	{
        private string username;
		private string password;
        
        public StaffMember(string username, string password)
		{
			this.username = username;
			this.password = password;
		}

		public bool Authenticate(string usernameInput, string passwordInput)
		{
			return username == usernameInput && password == passwordInput;
		}

		public static void AddDVD()
		{
            string title ="";
            string genre = "";
            string classification ="";
            int duration;
            int copies;

            WriteLine("===================== Add DVD ========================");
            WriteLine();
            WriteLine("Enter DVD information below: | (0 to Exit)");
            WriteLine("------------------------------------------------------");

			// Get movie title
			title = CheckTitleInput("Enter movie title: ");
			WriteLine();

			// Get movie genre
			string[] genres = { "Drama", "Adventure", "Family", "Action", "Sci-fi", "Comedy", "Animated", "Thriller", "Other" };
			WriteLine("Select movie genre: ");
			for (int i = 0; i < genres.Length; i++)
			{
				WriteLine($"{i + 1}. {genres[i]}");
			}
            
			int genreChoice;
			bool isValidGenre = false;
			while (!isValidGenre)
			{
                WriteLine();
                WriteLine("Enter your choice: ");
				string? input = ReadLine();

				if (input == "0")
				{
					WriteLine();
                    StaffMenu.DisplayStaffMenu();
					break;
				}
				else if (int.TryParse(input, out genreChoice) && genreChoice >= 1 && genreChoice <= genres.Length)
				{
					genre = genres[genreChoice - 1];
                    WriteLine();
                    isValidGenre = true;
				}
				else
				{
                    WriteLine("That is not a valid number. Try again. ");
                }
                
            }

			// Get movie classification
			string[] classifications = { "General (G)", "Parental Guidance (PG)", "Mature (M15+)", "Mature Accompanied (MA15+)" };
            WriteLine("Select movie classification: ");
            for (int i = 0; i < classifications.Length; i++)
            {
                WriteLine($"{i + 1}. {classifications[i]}");
            }


            int classificationChoice;
            bool isValidClassification = false;
            while (!isValidClassification)
            {
                WriteLine();
                WriteLine("Enter your choice: ");
                string? input = ReadLine();

                if (input == "0")
                {
                    WriteLine();
                    StaffMenu.DisplayStaffMenu();
                    break;
                }
                else if (int.TryParse(input, out classificationChoice) && classificationChoice >= 1 && classificationChoice <= classifications.Length)
                {
                    classification = classifications[classificationChoice - 1];
                    WriteLine();
                    isValidClassification = true;
                }
                else
                {
                    WriteLine("That is not a valid number. Try again. ");
                }

            }

            // Get movie duration
            duration = CheckIntInput("Enter duration of the movie (in minutes): ");
            WriteLine();

            // Get number of new copies
            copies = CheckIntInput("Enter number of new copies: ");
            WriteLine();

            // Add movie to collection
            Movie newMovie = new Movie(title, genre, classification, duration, copies);
            MovieCollection.Movies.Insert(newMovie);
            
        }

        public static void RemoveDVD()
		{
            string title = "";
            int copies;

            WriteLine("==================== Remove DVD ======================");
            WriteLine();
            WriteLine("Enter DVD information below: | (0 to Exit)");
            WriteLine("------------------------------------------------------");

            // Get movie title
            title = CheckTitleInput("Enter movie title: ");
            WriteLine();
            copies = CheckIntInput("Enter number of copies to delete: ");

            // Remove movie from collection
            MovieCollection.Movies.Delete(title, copies);
        }

        public static void RegisterMember()
		{
            string firstName;
            string lastName;
            int phoneNumber;
            int password = 0;

            WriteLine();
            WriteLine("================== Register New Member =====================");
            WriteLine();
            WriteLine("Enter member's personal information below: | (0 to Exit)");
            WriteLine("------------------------------------------------------------");

            // Get basic info
            firstName = CheckStringInput("Enter first name: ");
            lastName = CheckStringInput("Enter last name: ");
            phoneNumber = CheckIntInput("Enter phone number: ");

            // Get password
            int pwd;
            bool isValidPwd = false;
            while(!isValidPwd)
            {
                Write("Enter password: ");
                string? input = ReadLine();

                if (input == "0")
                {
                    WriteLine();
                    StaffMenu.DisplayStaffMenu();
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
                }
               
            }

            // Add new member to collection
            Member newMember = new Member(firstName, lastName, phoneNumber, password);
            MemberCollection.Members.Insert(newMember);
        }

		public static void RemoveMember()
		{
            string firstName;
            string lastName;
            int borrowCount;

            WriteLine();
            WriteLine("==================== Remove Existing Member ===================");
            WriteLine();
            WriteLine("Enter member's personal information below: | (0 to Exit)");
            WriteLine("---------------------------------------------------------------");

            // Get full name
            firstName = CheckStringInput("Enter first name: ");
            lastName = CheckStringInput("Enter last name: ");

            // Check if member is still holding DVDs
            borrowCount = MemberCollection.Members.Remove(firstName, lastName);
            if (borrowCount > 0)
            {
                WriteLine();
                WriteLine($"Member is still holding {borrowCount} DVDs, can't be removed from the system. ");
            }
            else if (borrowCount == 0)
            {
                WriteLine();
                WriteLine("Member removed successfully.");
            }
            else
            {
                WriteLine();
                WriteLine("Member doesn't exist.");
            }
        }

		public static void FindMemberPhoneNumber()
		{
            string firstName;
            string lastName;
            int phoneNumber;

            // Get full name
            WriteLine();
            firstName = CheckStringInput("Enter first name: ");
            lastName = CheckStringInput("Enter last name: ");

            phoneNumber = MemberCollection.Members.FindPhoneNumber(firstName, lastName);

            if (phoneNumber != -1)
            {
                WriteLine();
                WriteLine($"The phone number for {firstName} {lastName} is {phoneNumber}");
                WriteLine();
            }
            else
            {
                WriteLine();
                WriteLine("Phone number not found.");
                WriteLine();
            }
        }

		public static void FindRentingMembers()
		{
            string title;

            WriteLine();
            WriteLine("============== Search Renting Members ================");
            WriteLine();
            WriteLine("Search for renting members | (0 to Exit)");
            WriteLine("------------------------------------------------------");
            WriteLine();

            // Get movie title
            title = CheckTitleInput("Enter movie title: ");
            WriteLine();

            Movie? searchedMovie = MovieCollection.Movies.SearchByTitle(title);
            if (searchedMovie == null)
            {
                WriteLine("This movie doesn't exist in the system.");
                WriteLine();
            }
            else
            {
                searchedMovie.PrintRentingHistory();
            }
        }

        public static void PrintForDebug()
        {
            MovieCollection.Movies.PrintAll();
        }


        // Checks if movie input is null
        public static string CheckTitleInput(string question)
		{
			Write(question);
			string? input = ReadLine();

			while (true)
			{
                if (input == "0")
                {
					StaffMenu.DisplayStaffMenu();
                }

                if (string.IsNullOrWhiteSpace(input))
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

        // Checks if string input is valid
        public static string CheckStringInput(string question)
        {
            Write(question);
            string? input = ReadLine();

            while (true)
            {
                if (input == "0")
                {
                    StaffMenu.DisplayStaffMenu();
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

        // Checks if integer input is valid
        public static int CheckIntInput(string question)
        {
            Write(question);
            int num;
            while (!int.TryParse(ReadLine(), out num) || num == 0)
            {
                if (num == 0)
                {
                    StaffMenu.DisplayStaffMenu();
                    break;
                }
                else
                {
                    WriteLine("Invalid input. Please enter a valid integer.");
                    WriteLine();
                    Write(question);
                }
            }
            return num;
        }
    }
}

