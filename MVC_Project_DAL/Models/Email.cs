using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_DAL.Models
{
	public class Email : ModelClass
	{
		public string Subject { get; set; }
		public string Body { get; set; }
		public string Receipents { get; set; }
	}
}
