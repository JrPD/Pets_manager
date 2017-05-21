using System;
using System.Collections.Generic;
using System.Linq;
using OwnersPets.Models;
using Microsoft.EntityFrameworkCore;

namespace OwnersPets.Models
{
	//[LifecycleTransient]
	public class PetDataRepository : IPetDataRepository
	{
		private readonly AppDbContext _db;

		public PetDataRepository(AppDbContext appDbContext)
		{
			_db = appDbContext;
		}

		#region IPetDataRepository Members

		public dynamic GetPetsByPages(int id, int pageSize, int pageNumber)
		{
			var totalCount = _db.Pets.Where(o => o.OwnerId.OwnerId == id).Count();
			var totalPages = Math.Ceiling((double)totalCount / pageSize)==0?1: Math.Ceiling((double)totalCount / pageSize);

			var petsQuery = _db.Pets.Where(o => o.OwnerId.OwnerId == id);
			petsQuery = petsQuery.OrderBy(c => c.PetName);

			var pets = petsQuery.Skip((pageNumber - 1) * pageSize)
									.Take(pageSize)
									.ToList();

			var result = new
			{
				TotalCount = totalCount,
				totalPages = totalPages,
				Items = pets
			};
			return result;
		}

		public Pet GetPetById(int id)
		{
			return _db.Pets.Find(id);
		}

		public Pet PostPet(Pet pet)
		{
			Owner owner = _db.Owners.Include(p => p.Pets)
				.Where(o => o.OwnerId == pet.OwnerId.OwnerId).FirstOrDefault();
			pet.OwnerId = owner;
			_db.Pets.Add(pet);
			_db.SaveChanges();
			return pet;
		}

		public Pet DeletePet(int id)
		{
			Pet pet = _db.Pets.Find(id);

			if (pet==null)
			{
				return pet;
			}
			_db.Pets.Remove(pet);
			_db.SaveChanges();
			return pet;
		}

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion



    }
}