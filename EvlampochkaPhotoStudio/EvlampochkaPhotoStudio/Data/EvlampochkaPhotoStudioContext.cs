#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EvlampochkaPhotoStudio.Models;

namespace EvlampochkaPhotoStudio.Data
{
    public class EvlampochkaPhotoStudioContext : DbContext
    {
        public EvlampochkaPhotoStudioContext (DbContextOptions<EvlampochkaPhotoStudioContext> options)
            : base(options)
        {
        }

        public DbSet<EvlampochkaPhotoStudio.Models.Room> Room { get; set; }

        public DbSet<EvlampochkaPhotoStudio.Models.Category> Category { get; set; }

        public DbSet<EvlampochkaPhotoStudio.Models.Photo> Photo { get; set; }
    }
}
