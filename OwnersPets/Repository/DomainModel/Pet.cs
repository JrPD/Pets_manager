using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OwnersPets.Models
{
	public class Pet
	{
		public Pet()
		{

		}
		public Pet(string name)
		{
			PetName = name;
		}
		public int PetId { get; set; }
		public string PetName { get; set; }

        public Owner OwnerId { get; set; }
	}
}
