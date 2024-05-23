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
                i++;
                offset = Probing(key, i);
				
			}
			return (offset + bucket) % buckets;
		}

		public int Insert(Movie movie)
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
					return 1;
				}
				else
				{
					table[search].Value.totalCopies += movie.totalCopies;
					return 2;
				}
			}
			else
				return -1;
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
                i++;
                offset = Probing(key, i);

            }
			if (table[(bucket + offset) % buckets].Key == key)
				return (offset + bucket) % buckets;
			else
				return -1;

		}

        /* pre:  nil
		* post: the given key has been removed from the hashtable if the given key is in the hashtable
		*/
        public int Delete(string key, int copies)
		{
			int bucket = Search(key);

			if (bucket != -1)
			{
				if (copies == table[bucket].Value.totalCopies)
				{
					table[bucket] = new KeyValuePair<string, Movie>(deleted, null);
					count--;
					return 1;
				}
				else if (copies < table[bucket].Value.totalCopies)
				{
					table[bucket].Value.totalCopies -= copies;
					return 2;
				}
				else
				{
					return 3;
				}
			}
			else
				return -1;
		}

		/* pre: nil
		/* post: print elements in dictionary order
		*/
		public Movie[] PrintSorted()
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

			return sortedMoviesArray;

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
						", Duration: " + table[i].Value.durationInMin + ", Total Copies: " + table[i].Value.totalCopies + "freq: " + table[i].Value.frequency);
            }
            WriteLine();
			
        }

		public void DisplayTop3()
		{
			QuickSort(table, 0, table.Length - 1);

            WriteLine("Top 3 Movies:");
            for (int i = 0; i < table.Length; i++)
            {
				if (table[i].Key != empty && table[i].Key != deleted)
				{
                    WriteLine($"{table[i].Key}: {table[i].Value.frequency}");
                }
                
            }

        }

		private void QuickSort(KeyValuePair<string, Movie>[] table, int low, int high)
		{
			if (low < high)
			{
				int pivot = Partition(table, low, high);

				QuickSort(table, low, pivot - 1);
				QuickSort(table, pivot + 1, high);
			}
		}


        private int Partition(KeyValuePair<string, Movie>[] table, int low, int high)
		{
			Movie pivot = table[high].Value;

			// Decrement high until there is non-null value to set as pivot
            while (high > low && pivot == null)
            {
                high--;
                pivot = table[high].Value; 
            }

            //WriteLine("high: " + high);


            int i = low - 1;

			for (int j = low; j < high; j++)
			{
				if (table[j].Key != empty && table[j].Key != deleted && table[j].Value.frequency >= pivot.frequency)
				{
					i++;
					KeyValuePair<string, Movie> temp = table[i];
					table[i] = table[j];
					table[j] = temp;
				}	
			}

			KeyValuePair<string, Movie> temp2 = table[i + 1];
			table[i + 1] = table[high];
			table[high] = temp2;

			return i + 1;
		}

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

		private int SecondaryHash(string key)
		{
			long num = 0;

			foreach (char c in key.ToLower())
			{
				num = (num * 31) + (int)c;
			}

			long step = 991 - (num % 991);

			return (int)step;
		}

		// double probing
		private int Probing(string key, int i)
		{
			return i * SecondaryHash(key);
		}

        // Mock Data to populate collection
        public void AddMockData()
        {
            Movie[] mockData = new Movie[]
            {
				new Movie("Inception", "Sci-Fi", "PG-13", 148, 9) { frequency = 15 },
				new Movie("The Matrix", "Sci-Fi", "R", 136, 8) { frequency = 12 },
				new Movie("Mulan", "Thriller", "R", 118, 8) { frequency = 10 },
				new Movie("Jurassic Park", "Action", "PG-13", 127, 7) { frequency = 20 },
				new Movie("The Avengers", "Action", "PG-13", 143, 8) { frequency = 25 },
				new Movie("Titanic", "Romance", "PG-13", 195, 7) { frequency = 30 },
				new Movie("Mamma Mia", "Fantasy", "PG", 152, 9) { frequency = 5 },
				new Movie("The Lion King", "Animation", "G", 88, 9) { frequency = 18 },
				new Movie("Interstellar", "Sci-Fi", "PG-13", 169, 9) { frequency = 22 },
				new Movie("Forrest Gump", "Drama", "PG-13", 142, 9) { frequency = 35 },
				new Movie("The Intern", "Crime", "R", 202, 9) { frequency = 7 },
				new Movie("Ocean 8", "Biography", "R", 195, 9) { frequency = 9 },
				new Movie("Goodfellas", "Crime", "R", 146, 8) { frequency = 13 },
				new Movie("Lilo and Stitch", "Drama", "R", 189, 9) { frequency = 14 },
				new Movie("Fight Club", "Drama", "R", 139, 8) { frequency = 28 },
				new Movie("Avatar", "Sci-Fi", "PG-13", 162, 8) { frequency = 19 },
				new Movie("Gladiator", "Action", "R", 155, 8) { frequency = 24 },
				new Movie("Braveheart", "Biography", "R", 178, 9) { frequency = 16 },
				new Movie("The Departed", "Crime", "R", 151, 9) { frequency = 23 },
				new Movie("Alice in Wonderland", "War", "R", 169, 9) { frequency = 21 }
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

