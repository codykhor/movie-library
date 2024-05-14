using System;
using static System.Console;

namespace MovieLibrary
{
    class Program 
    {
        public static void Main()
        {
            
            
            MovieCollection movieCollection = MovieCollection.InitializeMovieCollection();
            MemberCollection memberCollection = MemberCollection.InitializeMemberCollection();

            // Mock data
            //Movie movie1 = new Movie("The Shawshank Redemption", "Drama", "R", 142, 10);
            //Movie movie2 = new Movie("The Godfather", "Crime", "R", 175, 8);
            //Movie movie3 = new Movie("The Dark Knight", "Action", "PG-13", 152, 9);
            //Movie movie4 = new Movie("Pulp Fiction", "Crime", "R", 154, 7);
            //Movie movie5 = new Movie("Forrest Gumpp", "Drama", "PG-13", 142, 6);

            //movieCollection.Insert(movie1);
            //movieCollection.Insert(movie2);
            //movieCollection.Insert(movie3);
            //movieCollection.Insert(movie4);
            //movieCollection.Insert(movie5);

            //Member member1 = new Member("Alice", "Smith", 037291739, 0000);
            //Member member2 = new Member("Bob", "Yong", 4579824, 1111);
            //Member member3 = new Member("Dylan", "Tan", 037917390, 2222);
            //Member member4 = new Member("Paul", "Zhou", 7934799, 3333);

            //memberCollection.Insert(member1);
            //memberCollection.Insert(member2);
            //memberCollection.Insert(member3);
            //memberCollection.Insert(member4);

            MainMenu.DisplayMenu();

            //movieCollection.Print();

            ReadKey();
            
            
        }

    } 
}


