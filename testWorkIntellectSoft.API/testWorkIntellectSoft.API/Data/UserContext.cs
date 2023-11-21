using Microsoft.EntityFrameworkCore;
using testWorkIntellectSoft.API.Models.Struct;

namespace testWorkIntellectSoft.API.Data
{
    public class UserContext : DbContext
    {

        public UserContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserDBStruct> Users { get; set; }
    }
}
