using Microsoft.EntityFrameworkCore;
using UserManagement.Domain;

namespace UserManagement.Infrastructure.DBContext
{
    public class MySQLContext : BaseContext
    {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }


    }
}
