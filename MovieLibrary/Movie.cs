using System;

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
	}
}

