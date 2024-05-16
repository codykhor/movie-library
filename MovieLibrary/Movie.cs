using System;
using static System.Console;

namespace MovieLibrary
{
	public class Movie
	{

		public string title { get; set; }
		public string genre { get; set; }
		public string classification { get; set; }
		public int durationInMin { get; set; }
		public int totalCopies { get; set; }
		public int frequency { get; set; }
		public string[] membersRenting { get; set; }

		// Constructor
		public Movie(string movieTitle, string movieGenre, string movieClass, int duration, int total)
		{
			title = movieTitle;
			genre = movieGenre;
			classification = movieClass;
			durationInMin = duration;
            totalCopies = total;
            membersRenting = new string[total];
            frequency = 0;
		}

		public void UpdateRentingHistory(string firstName, string lastName)
		{
			string fullName = $"{firstName} {lastName}";

            for (int i = 0; i < membersRenting.Length; i++)
			{
				if (membersRenting[i] == null)
				{
					membersRenting[i] = fullName;
                    return;
				}
			}
		}

		public void PrintRentingHistory()
		{
            if (membersRenting[0] == null)
			{
                WriteLine("No members are renting this movie currently.");
				WriteLine();
            }
			else
			{
				WriteLine("Members who are currently renting this movie:");
                for (int i = 0; i < membersRenting.Length; i++)
                {
                    WriteLine($"{membersRenting[i]}");
                }
            }
		}

		public void Print()
        {
			WriteLine($"Movie frequency: {frequency}");
        }
    }
}

