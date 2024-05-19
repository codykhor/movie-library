using System;
using static System.Console;

namespace MovieLibrary
{
	public class Member
	{
        public string firstName { get; set; }
        public string lastName { get; set; }
        private readonly int password;
        public int phoneNumber { get; set; }
        public string[] borrowHistory { get; set; }
        public int borrowCount { get; set; }
        public int limit = 5; // Max borrow limit 

        // constructor
        public Member(string first, string last, int pwd, int number)
        {
           firstName = first;
           lastName = last;
           password = pwd;
           phoneNumber = number;
           borrowHistory = new string[limit];
           borrowCount = 0;
        }

        public bool CheckPassword(int inputPwd)
        {
            if (password == inputPwd)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static void DisplayAllMovies()
        {
            WriteLine();
            WriteLine("================== Browse All DVDs ===================");
            WriteLine();
            WriteLine("Information about all movies DVDs | (0 to Exit)");
            WriteLine("------------------------------------------------------");
            WriteLine();

            MovieCollection.Movies.PrintSorted();
        }

        public static void SearchMovies(Member member)
        {
            
            WriteLine();
            WriteLine("============= Search Movies With Title ===============");
            WriteLine();
            WriteLine("Search for movie information | (0 to Exit)");
            WriteLine("------------------------------------------------------");
            WriteLine();

            // Get movie title

            bool isComplete = false;
            while (!isComplete)
            {
                Write("Enter movie title: ");
                string? title = ReadLine();
                
                if (title == "0")
                {
                    MemberMenu.DisplayMemberMenu(member);
                    break;
                }

                if (string.IsNullOrWhiteSpace(title))
                {
                    WriteLine("Invalid input. Please try again.");
                    WriteLine();
                    continue;
                }

                Movie? searchedMovie = MovieCollection.Movies.SearchByTitle(title);

                if (searchedMovie == null)
                {
                    WriteLine("Oops. Movie doesn't exist in the system.");
                    WriteLine();
                }
                else
                {
                    WriteLine($"Title: {searchedMovie.title}, Genre: {searchedMovie.genre}, Classification: {searchedMovie.classification}, " +
                        $"Duration: {searchedMovie.durationInMin}, Total Copies: {searchedMovie.totalCopies}, {searchedMovie.membersRenting[1]}");
                    WriteLine();
                }

                isComplete = true;
            }

            MemberMenu.DisplayMemberMenu(member);
        }

        // Action repeats until member wants to Exit
        public static void BorrowMovie(Member member)
        {
            string firstName = member.firstName;
            string lastName = member.lastName;

            WriteLine();
            WriteLine("============= Borrow Movie With Title ================");
            WriteLine();
            WriteLine("Borrow movie DVD | (0 to Exit)");
            WriteLine("------------------------------------------------------");
            WriteLine();

            // Get movie title
            bool isComplete = false;
            while (!isComplete)
            {

                if (member.borrowCount == member.limit)
                {
                    WriteLine("You have reached the maximum borrow limit. Please return movies to proceed.");
                    WriteLine();
                }

                Write("Enter movie title: ");
                string? title = ReadLine();

                if (title == "0")
                {
                    MemberMenu.DisplayMemberMenu(member);
                    break;
                }

                if (string.IsNullOrWhiteSpace(title))
                {
                    WriteLine("Invalid input. Please try again.");
                    WriteLine();
                    continue;
                }

                if (MovieCollection.Movies.Search(title) != -1)
                {
                    if (!SearchHistory(member, title))
                    {
                        // Update movie's renting history and frequency
                        Movie? searchedMovie = MovieCollection.Movies.SearchByTitle(title);
                        searchedMovie.AddToRentingHistory(firstName, lastName);
                        searchedMovie.frequency += 1;

                        // Update member's borrow history
                        member.borrowHistory[member.borrowCount] = title;
                        member.borrowCount++;
                        WriteLine("Movie borrowed succesfully. Enjoy!");
                        //PrintBorrowHistory(member);
                        WriteLine();
                    }
                    else
                    {
                        WriteLine("Sorry, you're unable to borrow more than one copy of the same movie at the same time.");
                        WriteLine();
                    }
                }
                else
                {
                    WriteLine("Movie doesn't exist in the system");
                    WriteLine();
                }
            }
        }

        public static void ReturnMovie(Member member)
        {
            string firstName = member.firstName;
            string lastName = member.lastName;

            WriteLine();
            WriteLine("============= Return Movie With Title ================");
            WriteLine();
            WriteLine("Return movie DVD | (0 to Exit)");
            WriteLine("------------------------------------------------------");
            WriteLine();

            // Get movie title
            bool isComplete = false;
            while (!isComplete)
            {
                if (member.borrowCount == 0)
                {
                    WriteLine("You don't have any movies on loan right now.");
                    WriteLine();
                    return;
                }

                Write("Enter movie title: ");
                string? title = ReadLine();

                if (title == "0")
                {
                    MemberMenu.DisplayMemberMenu(member);
                    break;
                }

                if (string.IsNullOrWhiteSpace(title))
                {
                    WriteLine("Invalid input. Please try again.");
                    WriteLine();
                    continue;
                }

                if (MovieCollection.Movies.Search(title) != -1)
                {
                    if (SearchHistory(member, title))
                    {
                        // Update movie's renting history
                        Movie? searchedMovie = MovieCollection.Movies.SearchByTitle(title);
                        searchedMovie.RemoveFromRentingHistory(firstName, lastName);

                        // Update member's borrow history
                        for (int i = 0; i < member.borrowHistory.Length; i++)
                        {
                            for (int j = i; j < member.borrowHistory.Length - 1; j++)
                            {
                                member.borrowHistory[j] = member.borrowHistory[j + 1];
                            }

                            member.borrowHistory[member.borrowHistory.Length - 1] = null;
                        }

                        member.borrowCount--;
                        WriteLine("Movie returned succesfully.");
                        WriteLine();
                    }
                    else
                    {
                        WriteLine("Sorry, this movie is not in your rental history.");
                        WriteLine();
                    }
                }
                else
                {
                    WriteLine("Movie doesn't exist in the system");
                    WriteLine();
                }
            }
        }

        public static void PrintBorrowHistory(Member member)
        {
            if (member.borrowHistory[0] == null)
            {
                WriteLine("There are currently no movies being borrowed.");
            }
            else
            {
                for (int i = 0; i < member.borrowHistory.Length; i++)
                {
                    if (member.borrowHistory[i] != null)
                    {
                        WriteLine($"{member.borrowHistory[i]}");
                    }
                }
            }
            
            //WriteLine($"Borrow Count: {member.borrowCount}");
        }

        // Checks if movie exists in member's borrow history to prevent renting multiple copies of the same movie
        public static bool SearchHistory(Member member, string title)
        {

            for (int i = 0; i < member.borrowHistory.Length; i++)
            {
                if (member.borrowHistory[i] == title)
                {
                    return true;
                }
            }
            return false;
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

