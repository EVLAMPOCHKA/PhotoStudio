#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EvlampochkaPhotoStudio.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EvlampochkaPhotoStudio.Data
{
    public class EvlampochkaPhotoStudioContext : IdentityDbContext<User, Role, string>
    {
        public EvlampochkaPhotoStudioContext (DbContextOptions<EvlampochkaPhotoStudioContext> options)
            : base(options)
        {
        }

        public DbSet<EvlampochkaPhotoStudio.Models.Room> Room { get; set; }

        public DbSet<EvlampochkaPhotoStudio.Models.Category> Category { get; set; }

        public DbSet<EvlampochkaPhotoStudio.Models.Photo> Photo { get; set; }

        public DbSet<EvlampochkaPhotoStudio.Models.Comment> Comment { get; set; }

        public DbSet<EvlampochkaPhotoStudio.Models.Favorite> Favorite { get; set; }
    }
}
