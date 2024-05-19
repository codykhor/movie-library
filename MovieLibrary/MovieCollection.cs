using System;
using static System.Console;

// Hashtable adapted from IFN664 learning materials by Maolin Tang

namespace MovieLibrary
{
	public class MovieCollection
	{
        private static MovieCollection? movies;
        private int count; // number of key-and-value pairs currently stored in hashtable
		private int buckets; // number of buckets
		private KeyValuePair<string, Movie>[] table; // table storing key-and-value pairs
		private const string empty = "empty"; // an empty bucket
		private const string deleted = "deleted"; // a bucket where a key-and-value pair was deleted

		// singleton
		public static MovieCollection Movies
		{
			get { return movies; }
		}

		public static MovieCollection InitializeMovieCollection()
		{
			if (movies == null)
				movies = new MovieCollection(1000);

            movies.AddMockData();

            return movies;
		}

        // constructor
        public MovieCollection(int buckets)
        {
            if (buckets > 0)
                this.buckets = buckets;
            count = 0;
            table = new KeyValuePair<string, Movie>[buckets];
            for (int i = 0; i < buckets; i++)
                table[i] = new KeyValuePair<string, Movie>(empty, null);
        }

        public int Count
		{
			get { return count; }
		}

		public int Capacity
		{
			get { return buckets; }
			set { buckets = Capacity; }
		}

        /* pre:  the hashtable is not full
		* post: return the bucket for inserting the key
		*/
        private int FindInsertionBucket(string key)
		{
			int bucket = Hashing(key);
			int i = 0;
			int offset = 0;
			while ((i < buckets) &&
				(table[(bucket + offset) % buckets].Key != empty) &&
				(table[(bucket + offset) % buckets].Key != deleted))
			{
				offset = Probing(offset);
				i++;
			}
			return (offset + bucket) % buckets;
		}

		public void Insert(Movie movie)
		{
			string key = movie.title;
			int search = Search(key);

            // check pre-condition
            if (Count < table.Length)
			{
				if (search == -1)
				{
					int bucket = FindInsertionBucket(key);
					table[bucket] = new KeyValuePair<string, Movie>(key, movie);
					count++;
					WriteLine("Movie added successfully.");
				}
				else
				{
                    table[search].Value.totalCopies += movie.totalCopies;
                    WriteLine("Movie exists and the number of copies is updated.");
                }
			}
			else
				WriteLine("The system is full.");
		}

        /* pre:  true
		* post: return the bucket where the key is stored
		*		 if the given key in the hashtable;
		*		 otherwise, return -1.
		*/
        public int Search(string key)
		{

			int bucket = Hashing(key);

			int i = 0;
			int offset = 0;
			while((i < buckets) &&
				(table[(bucket + offset) % buckets].Key != key) &&
				(table[(bucket + offset) % buckets].Key != empty))
			{
                offset = Probing(offset);

            }
			if (table[(bucket + offset) % buckets].Key == key)
				return (offset + bucket) % buckets;
			else
				return -1;

		}

        /* pre:  nil
		* post: the given key has been removed from the hashtable if the given key is in the hashtable
		*/
        public void Delete(string key, int copies)
		{
			int bucket = Search(key);

			if (bucket != -1)
			{
				if (copies == table[bucket].Value.totalCopies)
				{
					table[bucket] = new KeyValuePair<string, Movie>(deleted, null);
                    count--;
                    WriteLine("Movie has been removed from system.");
                }
				else if (copies < table[bucket].Value.totalCopies)
				{
                    table[bucket].Value.totalCopies -= copies;
                    WriteLine("The current number of copies available is updated.");
                }
				else
				{
                    WriteLine("The requested number of copies to delete exceeds the current number of copies available in the library.");
                }
            }
			else
				WriteLine("Oops. Movie doesn't exist in the system.");

		}

		/* pre: nil
		/* post: print elements in dictionary order
		*/
		public void PrintSorted()
		{
			Movie[] sortedMoviesArray = new Movie[count];
			int index = 0;

			for (int i = 0; i < buckets; i++)
			{
				if (table[i].Key != empty && table[i].Key != deleted)
				{
					sortedMoviesArray[index] = table[i].Value;
					index++;
				}
			}

			Array.Sort(sortedMoviesArray, (a, b) => string.Compare(a.title, b.title));



			for (int i = 0; i < sortedMoviesArray.Length; i++)
			{
				WriteLine("Title: " + sortedMoviesArray[i].title + ", Genre: " +
                       sortedMoviesArray[i].genre + ", Classification: " + sortedMoviesArray[i].classification +
					   ", Duration: " + sortedMoviesArray[i].durationInMin + ", Total Copies: " + sortedMoviesArray[i].totalCopies);
			}
			WriteLine();

		}

		public Movie? SearchByTitle(string title)
		{
            int search = Search(title);

            if (search == -1)
			{
				return null;
			}
			else
			{
				return table[search].Value;
            }
        }

        /* pre: nil
		/* post: print all elements in hashtable
		*/
        public void PrintAll()
        {
            for (int i = 0; i < buckets; i++)
            {
                if (table[i].Key == "empty")
                    WriteLine("--");
                else if (table[i].Key == "deleted")
                    WriteLine("--del--");
                else
                    WriteLine("Title: " + table[i].Value.title + ", Genre: " +
                        table[i].Value.genre + ", Classification: " + table[i].Value.classification +
                        ", Duration: " + table[i].Value.durationInMin + ", Total Copies: " + table[i].Value.totalCopies);
            }
            WriteLine();
        }

        // Come back and update with a better hash function (might need to change key value to int)
        /* pre: key >= 0
		/* post: return the bucket (location) for the given key
		*/
		// Using middle square method
		private int Hashing(string key)
		{
			long num = 0;

			foreach (char c in key.ToLower())
			{
				num += (int)c;
			}

			num = num * num; // key is squared

			string numString = num.ToString();
			
			int middleIndex = (numString.Length / 2) - 1;
            string middleBits = numString.Substring(middleIndex, 3);
			int index = int.Parse(middleBits) % buckets;

            return index;
		}

		// linear probing - come back and update probing method
		private int Probing(int offset)
		{
			return offset + 1;
		}

        // Mock Data to populate collection
        public void AddMockData()
        {
            Movie[] mockData = new Movie[]
            {
                new Movie("Inception", "Sci-Fi", "PG-13", 148, 9),
				new Movie("The Matrix", "Sci-Fi", "R", 136, 8),
				new Movie("Mulan", "Thriller", "R", 118, 8),
				new Movie("Jurassic Park", "Action", "PG-13", 127, 7),
				new Movie("The Avengers", "Action", "PG-13", 143, 8),
				new Movie("Titanic", "Romance", "PG-13", 195, 7),
				new Movie("Mamma Mia", "Fantasy", "PG", 152, 9),
				new Movie("The Lion King", "Animation", "G", 88, 9),
				new Movie("Interstellar", "Sci-Fi", "PG-13", 169, 9),
				new Movie("Forrest Gump", "Drama", "PG-13", 142, 9), 
				new Movie("The Intern", "Crime", "R", 202, 9),
				new Movie("Ocean 8", "Biography", "R", 195, 9),
				new Movie("Goodfellas", "Crime", "R", 146, 8),
				new Movie("Lilo and Stitch", "Drama", "R", 189, 9),
				new Movie("Fight Club", "Drama", "R", 139, 8),
				new Movie("Avatar", "Sci-Fi", "PG-13", 162, 8),
				new Movie("Gladiator", "Action", "R", 155, 8),
				new Movie("Braveheart", "Biography", "R", 178, 9),
				new Movie("The Departed", "Crime", "R", 151, 9),
				new Movie("Saving Private Ryan", "War", "R", 169, 9)
        };

            // Add movies -- rewriting to avoid printing feedback messages when initialized
            foreach (Movie movie in mockData)
            {
                string key = movie.title;
                int search = Search(key);

                if (Count < table.Length)
                {
                    if (search == -1)
                    {
                        int bucket = FindInsertionBucket(key);
                        table[bucket] = new KeyValuePair<string, Movie>(key, movie);
                        count++;
                    }
                }
                else
                    WriteLine("The system is full.");
            }
        }
    }
}

