using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OwnersPets.Models;
using OwnersPets.DAL;

namespace OwnersPets.Controllers
{
	public class OwnersController : ApiController
	{
		private OwnerDataRepository _repo;

		public OwnersController()
		{
			_repo = new OwnerDataRepository(new AppDbContext());
		}
		public OwnersController(OwnerDataRepository repo)
		{
			_repo = repo;
		}
		// GET: api/Owners
		//[Route("{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}")]
		public IHttpActionResult GetOwners(int pageSize, int pageNumber)
		{
			var result = _repo.GetOwnersByPages(pageSize, pageNumber);

			return Ok(result);
			
		}


		// GET: api/Owners/5
		[ResponseType(typeof(Owner))]
		public IHttpActionResult GetOwner(int id)
		{
			var owner = _repo.GetOwnerById(id);

			if (owner == null)
			{
				return NotFound();
			}

			return Ok(owner);
		}

		// POST: api/Owners
		[ResponseType(typeof(Owner))]
		public IHttpActionResult PostOwner(Owner owner)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			owner = _repo.PostOwner(owner);
	
			return CreatedAtRoute("DefaultApi", new { id = owner.OwnerId }, owner);
		}

		// DELETE: api/Owners/5
		[ResponseType(typeof(Owner))]
		public IHttpActionResult DeleteOwner(int id)
		{
			var owner = _repo.DeleteOwner(id);

			if (owner == null)
			{
				return NotFound();
			}
			return Ok(owner);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
			_repo.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}