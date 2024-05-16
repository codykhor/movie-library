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
                    Clear();
                    MemberMenu.DisplayMemberMenu(member);
                    break;
                }

                if (string.IsNullOrWhiteSpace(title))
                {
                    WriteLine("Invalid input. Please try again.");
                    WriteLine();
                    continue;
                }

                MovieCollection.Movies.SearchByTitle(title);
                WriteLine();
                isComplete = true;
            }

            MemberMenu.DisplayMemberMenu(member);
        }

        // Action repeats until member wants to Exit
        public static void BorrowMovie(Member member)
        {

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
                Write("Enter movie title: ");
                string? title = ReadLine();

                if (title == "0")
                {
                    Clear();
                    MemberMenu.DisplayMemberMenu(member);
                    break;
                }

                if (string.IsNullOrWhiteSpace(title))
                {
                    WriteLine("Invalid input. Please try again.");
                    WriteLine();
                    continue;
                }

                if (member.borrowCount < member.limit)
                {
                    if (MovieCollection.Movies.Search(title) != -1)
                    {
                        if (!SearchHistory(member, title))
                        {
                            member.borrowHistory[member.borrowCount] = title;
                            member.borrowCount++;
                            WriteLine("Movie borrowed succesfully. Enjoy!");
                            PrintBorrowHistory(member);
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
                else
                {
                    WriteLine("You have reached the maximum borrow limit. Please return movies to proceed.");
                    WriteLine();
                }
            }
        }

        public static void PrintBorrowHistory(Member member)
        {
            for (int i = 0; i < member.borrowHistory.Length; i++)
            {
                WriteLine($"{member.borrowHistory[i]}");
            }
            WriteLine($"Borrow Count: {member.borrowCount}");
        }

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

        // Checks if movie input is null
        //public static string CheckTitleInput(string question)
        //{
        //    Write(question);
        //    string? input = ReadLine();

        //    while (true)
        //    {
        //        if (input == "0")
        //        {
        //            Clear();
        //            MemberMenu.DisplayMemberMenu();
        //        }

        //        if (string.IsNullOrWhiteSpace(input))
        //        {
        //            WriteLine("Invalid input. Please try again.");
        //            WriteLine();
        //            Write(question);
        //            input = ReadLine();
        //        }
        //        else
        //        {
        //            return input;
        //        }
        //    }
        //}

        //// Checks if string input is valid
        //public static string CheckStringInput(string question)
        //{
        //    Write(question);
        //    string? input = ReadLine();

        //    while (true)
        //    {
        //        if (input == "0")
        //        {
        //            Clear();
        //            MemberMenu.DisplayMemberMenu(validMember);
        //        }

        //        if (string.IsNullOrWhiteSpace(input) || !IsAlphabetsOnly(input))
        //        {
        //            WriteLine("Invalid input. Please try again.");
        //            WriteLine();
        //            Write(question);
        //            input = ReadLine();
        //        }
        //        else
        //        {
        //            return input;
        //        }
        //    }
        //}

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
        //public static int CheckIntInput(string question)
        //{
        //    Write(question);
        //    int num;
        //    while (!int.TryParse(ReadLine(), out num) || num == 0)
        //    {
        //        if (num == 0)
        //        {
        //            Clear();
        //            MemberMenu.DisplayMemberMenu();
        //            break;
        //        }
        //        else
        //        {
        //            WriteLine("Invalid input. Please enter a valid integer.");
        //            WriteLine();
        //            Write(question);
        //        }
        //    }
        //    return num;
        //}
    }
}

