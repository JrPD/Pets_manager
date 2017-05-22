using OwnersPets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OwnersPets.DAL
{
	public interface IOwnerDataRepository : IDisposable
	{
		dynamic GetOwnersByPages(int pageSize, int pageNumber);

		Owner GetOwnerById(int id);
		Owner PostOwner(Owner owner);
		Owner DeleteOwner(int id);
	}
}