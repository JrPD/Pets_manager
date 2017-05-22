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
using Newtonsoft.Json;
using OwnersPets.DAL;


namespace OwnersPets.Controllers
{
	public class PetsController : ApiController
	{
		private PetDataRepository _repo;

		public PetsController()
		{
			_repo = new PetDataRepository(new AppDbContext());
		}

		// GET: api/Pets/5
		[ResponseType(typeof(Pet))]
		public IHttpActionResult GetPet(int id, int pageSize, int pageNumber)
		{
			var result = _repo.GetPetsByPages(id, pageSize, pageNumber);
			return Ok(result);
		}

		// GET: api/Pets/5
		[ResponseType(typeof(Pet))]
		public IHttpActionResult GetPet(int id)
		{
			var pet = _repo.GetPetById(id);
			if (pet == null)
			{
				return NotFound();
			}

			return Ok(pet);
		}

		
		// POST: api/Pets
		[ResponseType(typeof(Pet))]
		public IHttpActionResult PostPet(Pet pet)
		{
			pet = _repo.PostPet(pet);
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return CreatedAtRoute("DefaultApi", new { id = pet.PetId }, pet);
		}

		// DELETE: api/Pets/5
		[ResponseType(typeof(Pet))]
		public IHttpActionResult DeletePet(int id)
		{
			var pet = _repo.DeletePet(id);
			if (pet == null)
			{
				return NotFound();
			}
			return Ok(pet);
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