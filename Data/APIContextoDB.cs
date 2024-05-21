﻿using Microsoft.EntityFrameworkCore;
using API_LMFY.Models.users;

namespace API_LMFY.Data
{
    public class APIContextoDB : DbContext
    {
        public APIContextoDB(DbContextOptions<APIContextoDB> options) : base(options)
        {

        }
        public DbSet<API_LMFY.Models.users.usuarios> usuario { get; set; } = default!;
        public DbSet<API_LMFY.Models.cursos.cursos> cursos { get; set; } = default!;
        public DbSet<API_LMFY.Models.turmas.turmas> Turmas { get; set; } = default!;


    }
}
