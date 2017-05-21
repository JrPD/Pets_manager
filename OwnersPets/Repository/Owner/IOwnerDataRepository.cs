using OwnersPets.Models;
using System;

namespace OwnersPets.Models
{
	public interface IOwnerDataRepository : IDisposable
	{
		dynamic GetOwnersByPages(int pageSize, int pageNumber);

		Owner GetOwnerById(int id);
		Owner PostOwner(Owner owner);
		Owner DeleteOwner(int id);
	}
}