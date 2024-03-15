using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
    public class Stock: BaseEntity
    {
        public string Symbol { get; set; }
        public int UpdatedPrice { get; set; }
    }
    public class StockDBContext: DbContext
    {
       
        public StockDBContext(DbContextOptions<StockDBContext> options) : base(options) { }
        public DbSet<Stock> Stock { get; set; }
    }
}
