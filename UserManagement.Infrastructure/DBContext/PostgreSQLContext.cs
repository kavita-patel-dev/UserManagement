using Microsoft.EntityFrameworkCore;
using UserManagement.Domain;
using UserManagement.Infrastructure.DataSeeder;

namespace UserManagement.Infrastructure.DBContext
{
    public class PostgreSQLContext : BaseContext
    {
        public PostgreSQLContext(DbContextOptions<PostgreSQLContext> options) : base(options) { }


    }
}
