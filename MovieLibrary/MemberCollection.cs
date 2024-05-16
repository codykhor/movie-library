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

        // Use Staffmember to Writeline?
        public void Insert(Member member)
        {
            string firstName = member.firstName;
            string lastName = member.lastName;

            if (count < memberArray.Length)
            {
                if (Search(firstName, lastName) == -1)
                {
                    memberArray[count] = member;
                    count++;
                    WriteLine();
                    WriteLine("Member registered successfully!");
                }
                else
                {
                    WriteLine();
                    WriteLine("Registration failed, member already exists.");
                }
            }
            else
            {
                WriteLine("Member collection is full.");
            }
        }

        /* Search if member exists
         * returns array index
         * returns -1 if not found
        */
        // COME BACK AND IMPROVE
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
            for (int i = 0; i < count; i++)
            {
                if(Search(firstName, lastName) != -1)
                {
                    if (memberArray[i].borrowCount == 0)
                    {
                        return 0;
                        
                    }
                    else
                    {
                        return memberArray[i].borrowCount;
                    }
                }
            }
            return -1;
        }


        // COME BACK AND IMPROVE
        public int FindPhoneNumber(string firstName, string lastName)
        {
            for (int i = 0; i < count; i++)
            {
                if (Search(firstName, lastName) != -1)
                {
                    return memberArray[i].phoneNumber;
                }

            }
            return -1;
        }

        public Member? Authenticate(string firstName, string lastName, int password)
        {
            for (int i = 0; i < count; i++)
            {
                if (Search(firstName, lastName) != -1)
                {
                    return memberArray[i];
                }
            }
            return null;
        }

        //public void Print()
        //{
        //    for (int i = 0; i < memberArray.Length; i++)
        //    {
        //        WriteLine(memberArray[i].firstName);
        //    }
        //}

        // Mock Data to populate collection
        public void AddMockData()
        {
            Member[] mockData = new Member[]
            {
                new Member("Alice", "Smith", 0000, 037291739),
                new Member("Bob", "Yong", 1111, 4579824),
                new Member("Dylan", "Tan", 2222, 037917390),
                new Member("Paul", "Zhou", 3333, 7934799)
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

