using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetApp.Models;

namespace PetApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PetApp.Models.Ljubimac> Ljubimac { get; set; }
        public DbSet<PetApp.Models.PostSklonista> PostSklonista { get; set; }
        public DbSet<PetApp.Models.PostUdruge> PostUdruge { get; set; }
        public DbSet<PetApp.Models.Posvajatelj> Posvajatelj { get; set; }
        public DbSet<PetApp.Models.Skloniste> Skloniste { get; set; }
        public DbSet<PetApp.Models.Udruga> Udruga { get; set; }
    }
}
