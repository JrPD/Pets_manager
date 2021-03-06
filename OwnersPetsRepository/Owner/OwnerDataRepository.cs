﻿using System;
using System.Collections.Generic;
using System.Linq;
using OwnersPets.Models;
using Microsoft.EntityFrameworkCore;

namespace OwnersPets.Models
{
    //[LifecycleTransient]
    public class OwnerDataRepository : IOwnerDataRepository
    {
        private AppDbContext _db;
        public OwnerDataRepository(AppDbContext db)
        {
            _db = db;
        }
        #region IOwnerDataRepository Members

        public dynamic GetOwnersByPages(int pageSize, int pageNumber)
        {
            var totalCount = _db.Owners.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);
            var ownersQuery = _db.Owners.Include(p => p.Pets).OrderBy(c => c.OwnerName);
            totalPages = totalPages == 0 ? 1 : totalPages;

            foreach (var item in ownersQuery)
            {
                if (item.Pets != null)
                {
                    item.Count = item.Pets.Count();
                }
            }

            var owners = ownersQuery.Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();
            var result = new
            {
                TotalCount = totalCount,
                totalPages = totalPages,
                Items = owners
            };
            return result;
        }


        public Owner GetOwnerById(int id)
        {
            return _db.Owners.Include(p => p.Pets)
                .Where(o => o.OwnerId == id).FirstOrDefault();
        }

        public Owner PostOwner(Owner owner)
        {
            // make sure, that returns same owner
            _db.Owners.Add(owner);
            _db.SaveChanges();
            return owner;
        }

        public Owner DeleteOwner(int id)
        {
            Owner owner = _db.Owners.Include(p => p.Pets)
                .Where(o => o.OwnerId == id).FirstOrDefault();

            if (owner != null && owner.Pets != null)
            {
                _db.Pets.RemoveRange(owner.Pets);
            }
            if (owner != null)
            {
                _db.Owners.Remove(owner);
            }
            _db.SaveChanges();

            return owner;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        #endregion

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}