using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SecurePrivacy.Model;

namespace SecurePrivacy.Data
{
    public class SecurePrivacyDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private IDbConnection DbConnection { get; }

        public SecurePrivacyDbContext(DbContextOptions<SecurePrivacyDbContext> options, IConfiguration configuration)
           : base(options)
        {
            this._configuration = configuration;
            DbConnection = new SqlConnection(this._configuration.GetConnectionString("DatabseContext"));
        }
        public DbSet<StudentModel> Student { get; set; }
       
    }
}
