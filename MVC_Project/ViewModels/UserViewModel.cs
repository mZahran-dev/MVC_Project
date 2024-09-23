using System;
using System.Collections;
using System.Collections.Generic;

namespace MVC_Project_PL.ViewModels
{
	public class UserViewModel
	{
		public UserViewModel() 
		{
			Id = Guid.NewGuid().ToString();
		}
		public string Id {  get; set; }
		public string Fname { get; set; }
		public string Lname { get; set; }
		public string Email { get; set; }
		public string phoneNumber { get; set; }
		public IEnumerable<string> Roles { get; set; }
	}
}
