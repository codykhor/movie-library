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

            MainMenu.DisplayMenu();

            ReadKey();
            
            
        }

    } 
}


