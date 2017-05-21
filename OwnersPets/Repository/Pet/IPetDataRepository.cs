using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OwnersPets.Models;

namespace OwnersPets.Models
{
	public interface IPetDataRepository:IDisposable
	{
		dynamic GetPetsByPages(int id, int pageSize, int pageNumber);

		Pet GetPetById(int id);
		Pet PostPet(Pet owner);
		Pet DeletePet(int id);
	}
}
