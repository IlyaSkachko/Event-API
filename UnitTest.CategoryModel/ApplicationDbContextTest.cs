using Events.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace UnitTest.CategoryModel
{
    public class ApplicationDbContextTest : ApplicationDbContext
    {
        public ApplicationDbContextTest(DbContextOptions<ApplicationDbContextTest> options) : base(options) { }
    }
}
