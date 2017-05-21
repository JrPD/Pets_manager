using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using OwnersPets.Models;
using Microsoft.AspNetCore.Mvc;

namespace OwnersPets.Controllers
{
    [Route("api/[controller]")]
    public class OwnersController : Controller
    {
        private readonly IOwnerDataRepository _repo;

        //public OwnersController()
        //{
        //    _repo = new OwnerDataRepository(new AppDbContext());
        //}
        public OwnersController(OwnerDataRepository repo)
        {
            _repo = repo;
        }
        // GET: api/Owners
        [HttpGet]
        [Route("{pageSize:int}/{pageNumber:int}")]
        public IActionResult GetOwners(int pageSize, int pageNumber)
        {
            var result = _repo.GetOwnersByPages(pageSize, pageNumber);

            return Ok(result);

        }


        // GET: api/Owners/5
        [HttpGet("{id}")]
		public IActionResult GetOwner(int id)
		{
			var owner = _repo.GetOwnerById(id);

			if (owner == null)
			{
				return NotFound();
			}

			return Ok(owner);
		}

		// POST: api/Owners
        [HttpPost]
		public IActionResult PostOwner([FromBody]Owner owner)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			owner = _repo.PostOwner(owner);
	
			return Ok(owner);
		}

		// DELETE: api/Owners/5
        [HttpDelete("{id}")]
		public IActionResult DeleteOwner(int id)
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