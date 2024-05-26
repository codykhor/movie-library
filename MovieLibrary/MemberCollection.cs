using System;

using static System.Console;

namespace MovieLibrary
{
	public class MemberCollection
	{
        private static MemberCollection? members;
        private Member[] memberArray;
        private int count;
        private int arrayCapacity;

        // singleton
        public static MemberCollection Members
        {
            get { return members; }
        }

        public static MemberCollection InitializeMemberCollection()
        {
            if (members == null)
                members = new MemberCollection(1000);

            members.AddMockData();

            return members;
        }
        
        // constructor
        public MemberCollection(int num)
        {
            if (num > 0)
                arrayCapacity = num;
            count = 0;
            memberArray = new Member[num];
        }

        public int Insert(Member member)
        {
            string firstName = member.firstName;
            string lastName = member.lastName;

            if (count < memberArray.Length)
            {
                if (Search(firstName, lastName) == -1)
                {
                    memberArray[count] = member;
                    count++;
                    return 1;
                    
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                return -1;     
            }
        }

        /* Search if member exists
         * returns array index
         * returns -1 if not found
        */
        public int Search(string firstName, string lastName)
        {
            for (int i = 0; i < count; i++)
            {
                if (memberArray[i] != null &&
                    memberArray[i].firstName == firstName &&
                    memberArray[i].lastName == lastName)
                {
                    return i;
                }
                    
            }
            return -1;
        }

        public int Remove(string firstName, string lastName)
        {
            int index = Search(firstName, lastName);


            if (index == -1)
            {
                return -1;
            }

            if (memberArray[index].borrowCount != 0)
            {
                return memberArray[index].borrowCount;
            }

            for (int j = index; j < memberArray.Length - 1; j++)
            {
                memberArray[j] = memberArray[j + 1];
            }

            memberArray[memberArray.Length - 1] = null;
            count--;
            return 0;

        }


        public int FindPhoneNumber(string firstName, string lastName)
        {
            int index = Search(firstName, lastName);


            if (index == -1)
            {
                return -1;
            }
            else
            {
                WriteLine(memberArray[index].firstName + memberArray[index].lastName + memberArray[index].phoneNumber);
                return memberArray[index].phoneNumber;
            }
        }

        public Member Authenticate(string firstName, string lastName, int password)
        {
            int index = Search(firstName, lastName);
            if (index != -1 && memberArray[index].CheckPassword(password) == true)
            {
                return memberArray[index];
            }
            return null;
        }

        public void Print()
        {
            if (memberArray == null)
            {
                WriteLine("No members in system.");
                return;
            }

            for (int i = 0; i < memberArray.Length; i++)
            {
                if (memberArray[i] != null)
                {
                    WriteLine(memberArray[i].firstName + " " + memberArray[i].lastName + ", " + "0" + memberArray[i].phoneNumber);
                }
            }
            WriteLine();
        }

        // Mock Data to populate collection
        public void AddMockData()
        {
            Member[] mockData = new Member[]
            {
                new Member("Alice", "Smith", 0000, 0431565721),
                new Member("Bob", "Yong", 1111, 0478493029),
                new Member("Dylan", "Tan", 2222, 037917390),
                new Member("Paul", "Zhou", 3333, 0124917584),
                new Member("Lee", "Ming", 4444, 0493029758),
                new Member("Lee", "Ning", 5555, 0438295739),
                new Member("Cole", "Sprouse", 6666, 0475842840)
            };

            // Add members
            foreach (Member member in mockData)
            {
                if (count < memberArray.Length)
                {
                    memberArray[count] = member;
                    count++;
                }
                else
                {
                    WriteLine("Member collection is full.");
                    break;
                }
            }
        }
    }
}

