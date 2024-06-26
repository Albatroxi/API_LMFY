﻿using Microsoft.EntityFrameworkCore;
using API_LMFY.Models.users;
using API_LMFY.Models.questoes;
using API_LMFY.Models.respostas;
using API_LMFY.Models.provas;
using API_LMFY.Models.provas_org;

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
        public DbSet<API_LMFY.Models.disciplinas.disciplinas> Disciplinas { get; set; } = default!;
        public DbSet<API_LMFY.Models.questoes.questoes> Questoes_Provas { get; set; } = default!;
        public DbSet<API_LMFY.Models.respostas.respostas> Respostas_Alunos { get; set; } = default!;
        public DbSet<API_LMFY.Models.provas.provas> Provas { get; set; } = default!;
        public DbSet<API_LMFY.Models.provas_org.provas_org> tbl_provas { get; set; } = default!;


    }
}
