using System;
using static System.Console;

namespace MovieLibrary
{
	public class StaffMember
	{
        private string username;
		private string password;

		public StaffMember(string username, string password)
		{
			this.username = username;
			this.password = password;
		}

		public bool Authenticate(string usernameInput, string passwordInput)
		{
			return username == usernameInput && password == passwordInput;
		}

		public void AddDVD()
		{
			
		}

		public void RemoveDVD()
		{

		}

		public void RegisterMember()
		{

		}

		public void RemoveMember()
		{

		}

		public void FindMemberPhoneNumber()
		{

		}

		public void FindRentingMembers()
		{

		}
	}
}

