using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace T617878.Models {
    public class NorthwindContext : DbContext {
        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options) {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }

    public class Product {
        [Key]
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        [Required, MaxLength(40)]
        public string ProductName { get; set; }
        [MaxLength(20)]
        public string QuantityPerUnit { get; set; }
        [DisplayFormat(DataFormatString = "c")]
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public bool Discontinued { get; set; }
    }
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        [Required, MaxLength(40)]
        public string CategoryName { get; set; }
        [MaxLength(120)]
        public string Description { get; set; }
    }
}