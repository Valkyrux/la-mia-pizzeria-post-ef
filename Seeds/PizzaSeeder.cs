using Microsoft.EntityFrameworkCore;
using la_mia_pizzeria_ef.Models;
using la_mia_pizzeria_ef.Context;

namespace la_mia_pizzeria_ef.Seeds
{
    public static class PizzaSeeder
    {
        public static void SeedPizzaDb()
        {
            PizzeriaContext db = new PizzeriaContext();
            db.Add(new Pizza("Capricciosa", "Pizza molto buona, per tutti con funghi e altro", "capricciosa.jpg", 10.50));
            db.Add(new Pizza("4 formaggi", "Pizza molto buona, per tutti con 4 formaggi", "4form.webp", 7.80));
            db.Add(new Pizza("Diavola", "Pizza molto buona, piccante", "d1.jpg", 15.00));
            db.Add(new Pizza("Margherita", "La più semplice tra le pizze", "margh.jpg", 13.05));
            db.SaveChanges();
        }
    }
}
