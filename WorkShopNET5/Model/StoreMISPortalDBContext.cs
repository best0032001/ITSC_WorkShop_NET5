using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkShopNET5.Model.Store;

namespace WorkShopNET5.Model
{
    public class StoreMISPortalDBContext : DbContext
    {
        public StoreMISPortalDBContext(DbContextOptions<StoreMISPortalDBContext> options) : base(options)
        {

        }
        public virtual DbSet<GetAPICheckEmployeeNowByITAccount_Result> GetAPICheckEmployeeNowByITAccount_Result { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GetAPICheckEmployeeNowByITAccount_Result>()
            .HasKey(o => o.EmployeeID);
        }
    }
}
