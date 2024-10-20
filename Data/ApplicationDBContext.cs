using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPIProject.Models;

namespace WebAPIProject.Data
{
    public class ApplicationDBContext :IdentityDbContext<AppUser>
    {
         /* application DBcontext permet de conneter nos classa a la baqse de donnée via entity core
         base ici va nous permettre le transfere dans ola BD car il est impossible de la faire 
         en C# directment dans le copntroleur 
         */
             public ApplicationDBContext(DbContextOptions dbContextOptions) 
             : base(dbContextOptions)
            {
            }

            public DbSet<Stockage> Stockages { get; set; }
            public DbSet<Comments> Comments { get; set; }
            public DbSet<Portfolio> Portfolios {get;set;}
            protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);

                builder.Entity<Portfolio>(x => x.HasKey(p => new {p.AppUserId, p.StockageId}));
                builder.Entity<Portfolio>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.AppUserId);

                builder.Entity<Portfolio>()
                .HasOne(u => u.Stockage)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.StockageId);

                List<IdentityRole> roles = new List<IdentityRole>
                {
                    new IdentityRole
                    {
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },
                    new IdentityRole
                    {
                        Name = "User",
                        NormalizedName = "USER"
                    },
                };
                builder.Entity<IdentityRole>().HasData(roles);
            }
    }   
    }
