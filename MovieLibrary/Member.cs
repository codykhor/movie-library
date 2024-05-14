using System;
using static System.Console;

namespace MovieLibrary
{
	public class Member
	{
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int password { get; set; }
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
	}
}

