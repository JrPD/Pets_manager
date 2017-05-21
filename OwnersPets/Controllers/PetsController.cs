using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using OwnersPets.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace OwnersPets.Controllers
{
    [Route("api/[controller]")]
    public class PetsController : Controller
	{
		private readonly IPetDataRepository _repo;

		public PetsController(IPetDataRepository repo)
		{
            _repo = repo;
		}

		// GET: api/Pets/5
        [HttpGet("{id}")]
        [Route("{pageSize:int}/{pageNumber:int}")]
		public IActionResult GetPet(int id, int pageSize, int pageNumber)
		{
			var result = _repo.GetPetsByPages(id, pageSize, pageNumber);
			return Ok(result);
		}

		// GET: api/Pets/5
        [HttpGet("{id}")]
		public IActionResult GetPet(int id)
		{
			var pet = _repo.GetPetById(id);
			if (pet == null)
			{
				return NotFound();
			}

			return Ok(pet);
		}

		
		// POST: api/Pets
        [HttpPost]
		public IActionResult PostPet([FromBody]Pet pet)
		{
			pet = _repo.PostPet(pet);
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return Ok(pet);
		}

		// DELETE: api/Pets/5
        [HttpDelete("{id}")]
		public IActionResult DeletePet(int id)
		{
			var pet = _repo.DeletePet(id);
			if (pet == null)
			{
				return NotFound();
			}
			return Ok(pet);
		}

		

	}
}