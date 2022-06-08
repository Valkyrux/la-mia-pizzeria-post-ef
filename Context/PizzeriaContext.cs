using Microsoft.EntityFrameworkCore;
using la_mia_pizzeria_ef.Models;

namespace la_mia_pizzeria_ef.Context
{
    public class PizzeriaContext : DbContext
    {
        public DbSet<Pizza>? Pizze { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer("Data Source=DESKTOP-IN7U4B6;Initial Catalog=pizzeria_ef;User ID=sa;Password=sa;Pooling=False");
        }
    }
}
