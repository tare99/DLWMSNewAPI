using DLWMSApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DLWMSApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<FileDetail> FileDetail {get;set;}
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
    }

}
