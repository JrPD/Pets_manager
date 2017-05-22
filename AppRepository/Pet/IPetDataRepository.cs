using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OwnersPets.Models;

namespace OwnersPets.DAL
{
	public interface IPetDataRepository
	{
		dynamic GetPetsByPages(int id, int pageSize, int pageNumber);

		Pet GetPetById(int id);
		Pet PostPet(Pet owner);
		Pet DeletePet(int id);
	}
}
