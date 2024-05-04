﻿using API_LMFY.Data;
using API_LMFY.Models.users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_LMFY.Controllers.users
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly APIContextoDB _context;

        public usersController(APIContextoDB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Models.users.users>> UserEMAIL(string email)
        {
            var usersModel = await _context.users.FindAsync(email);

            if (usersModel == null)
            {
                return NotFound();
            }

            return usersModel;
        }

        [HttpPost]
        public async Task<ActionResult<Models.users.users>> registerUser(Models.users.users usersModel)
        {
            _context.users.Add(usersModel);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
