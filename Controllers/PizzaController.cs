using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_ef.Models;
using la_mia_pizzeria_ef.Context;
using System.Net;
using la_mia_pizzeria_ef.Seeds;

namespace la_mia_pizzeria_ef.Controllers
{
    public class PizzaController : Controller
    {
        static Pizzeria pizzeria = new Pizzeria("Da Luigi");

        public IActionResult SeedPizza()
        {
            PizzaSeeder.SeedPizzaDb();
            return RedirectToAction("Index");
        }

        public IActionResult SvuotaDB()
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                db.Pizze.RemoveRange(db.Pizze);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            using(PizzeriaContext db = new PizzeriaContext())
            {
                var pizze = db.Pizze;
                ViewData["nomePizzeria"] = pizzeria.Nome;
                return View(pizze.ToList());
            }
        }

        public IActionResult ShowPizza(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza? pizzaCercata = db.Find<Pizza>(id);
                if(pizzaCercata == null)
                {
                    ViewData["Titolo"] = "Pizza Not Found!";
                    return View("PizzaNotFound");
                }
                else
                {
                    ViewData["nomePizzeria"] = pizzeria.Nome;
                    return View(pizzaCercata);
                }
            }
        }

        public IActionResult CreatePizza()
        {
            Pizza nuovaPizza = new Pizza("", "", null, 0);
            return View(nuovaPizza);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ValidatePizza(Pizza nuovaPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("CreatePizza", nuovaPizza);
            }

            if(nuovaPizza.formFile != null)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                FileInfo newFileInfo = new FileInfo(nuovaPizza.formFile.FileName);
                string fileName = String.Format("{0}{1}", nuovaPizza.Name, newFileInfo.Extension);
                string FullPathName = Path.Combine(path, fileName);
                using (FileStream stream = new FileStream(FullPathName, FileMode.Create))
                {
                    nuovaPizza.formFile.CopyTo(stream);
                    stream.Close();
                }
                nuovaPizza.ImgPath = String.Format("{0}", fileName);
            }
            PizzeriaContext db = new PizzeriaContext();
            db.Add(nuovaPizza);
            db.SaveChanges();
            pizzeria.addPizza(nuovaPizza);
            return View("ShowPizza", nuovaPizza);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ValidateUpdatePizza(Pizza nuovaPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("CreatePizza", nuovaPizza);
            }

            if (nuovaPizza.formFile != null)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                FileInfo newFileInfo = new FileInfo(nuovaPizza.formFile.FileName);
                string fileName = String.Format("{0}{1}", nuovaPizza.Name, newFileInfo.Extension);
                string FullPathName = Path.Combine(path, fileName);
                using (FileStream stream = new FileStream(FullPathName, FileMode.Create))
                {
                    nuovaPizza.formFile.CopyTo(stream);
                    stream.Close();
                }
                nuovaPizza.ImgPath = String.Format("{0}", fileName);
            }
            using (PizzeriaContext db = new PizzeriaContext())
            {
                db.Update(nuovaPizza);
                db.SaveChanges();
            }
            return View("ShowPizza", nuovaPizza);
        }


        //chiamata del server a se stesso per utilizzare il metodo delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePizza(int? id)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://localhost:5084/Pizza/DelPizza");
            var postData = "id=" + id.ToString();
            
            var data = System.Text.Encoding.UTF8.GetBytes(postData);

            request.Method = "DELETE";

            request.ContentType = "application/x-www-form-urlencoded";

            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
;
            if(new StreamReader(response.GetResponseStream()).ReadToEnd() == "true")
            {
                return RedirectToAction("Index", pizzeria.listaPizze);
            }
            return View("PizzaNotFound");
        }

        [HttpDelete]
        public bool DelPizza(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            { 
                db.Pizze.Remove(new Pizza { Id = id });
                db.SaveChanges();
            }
            return true;
        }

        public IActionResult UpdatePizza(int? id)
        {
            if(id == null)
            {
                return View("PizzaNotFound");
            }
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza? pizzaDaModificare = db.Find<Pizza>(id);
                if(pizzaDaModificare != null)
                {
                    return View("EditPizza", pizzaDaModificare);
                }
                return View("PizzaNotFound");
            }
        }
    }
}
