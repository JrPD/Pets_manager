using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace OwnersPets.Models
{
	public class Owner
	{
		public Owner()
		{

		}
		public Owner(string name)
		{
			//todo verify
			OwnerName = name;
			Pets = new List<Pet>();
		}
		public int OwnerId { get; set; }
		public string OwnerName { get; set; }

		public IList<Pet> Pets { get; set; }

		[NotMapped]
		public int Count { get; set; }
	}
}
