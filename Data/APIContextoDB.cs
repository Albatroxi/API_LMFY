using Microsoft.EntityFrameworkCore;
using API_LMFY.Models.users;

namespace API_LMFY.Data
{
    public class APIContextoDB : DbContext
    {
        public APIContextoDB(DbContextOptions<APIContextoDB> options) : base(options)
        {

        }
        public DbSet<API_LMFY.Models.users.usersModel> usersModel { get; set; } = default!;
    }
}
