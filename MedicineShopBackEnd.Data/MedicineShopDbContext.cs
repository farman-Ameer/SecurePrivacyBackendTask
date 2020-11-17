using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MedicineShopBackEnd.Model;

namespace MedicineShopBackEnd.Data
{
    public class MedicineShopDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private IDbConnection DbConnection { get; }

        public MedicineShopDbContext(DbContextOptions<MedicineShopDbContext> options, IConfiguration configuration)
           : base(options)
        {
            this._configuration = configuration;
            DbConnection = new SqlConnection(this._configuration.GetConnectionString("DatabseContext"));
        }
        public DbSet<StudentModel> Student { get; set; }
        public DbSet<Model.Entities.User> Users { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<UserRoleModel> UserRoles { get; set; }
        public DbSet<RolesModel> Roles { get; set; }
        public DbSet<DeliveryInfoModel> DeliveryInfo { get; set; }
        public DbSet<OrdersModel> Orders { get; set; }
        public DbSet<OrderDetailsModel> OrderDetails { get; set; }
        public DbSet<NotificationModel> Notifications { get; set; }

    }
}
