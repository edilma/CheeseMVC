using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        static private Dictionary<string, string> Cheeses = new Dictionary<string, string>();
        //static private List<string> Cheeses = new List<string>();

        public IActionResult Index()
        {

            //List<string> cheeses = new List<string>();
            //cheeses.Add("Cheddar");
            //cheeses.Add("Munster");
            //cheeses.Add("Parmesan");
            //cheeses.Add("Harvaty");
            //ViewBag.cheeses = cheeses;
            
            ViewBag.cheeses = Cheeses;
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Remove()
        {
            ViewBag.cheeses = Cheeses;
            return View();
        }

        public IActionResult Delete()
        {
            ViewBag.cheeses = Cheeses;
            return View();
        }

        [HttpPost]
        [Route("/Cheese/Add")]
        public IActionResult NewCheese(string name, string description)
        {
            string error = validate(name);
            if (error == "")
            {
                Cheeses.Add(name, description);
                return Redirect("/Cheese");
            }
            else
            {
                ViewBag.cheeses = Cheeses;
                ViewBag.error = error;
                return View("Index");
            }
           
                
            
        }

        [HttpPost]
        [Route("/Cheese/Remove")]
        public IActionResult Remove(string[] cheese)
        {
            //Remove a checked cheese
            for (int i = 0; i < cheese.Length; i++)
            {
                Cheeses.Remove(cheese[i]);
            }
            
                        
            return Redirect("/Cheese");
        }

        [HttpPost]
        [Route("/Cheese/Delete")]
        public IActionResult DelCheese(string cheese)
        {
            //Delete the cheese using the select drop down list
            Cheeses.Remove(cheese);
            
            return Redirect("/Cheese");
        }

        private static string validate(string name)
        {
            //string alphabeth = "abcdefghijklmnopqrstuvwxyz";
            string errors = "";
            if (name==null || name=="")
            {
                errors += "A name for the Cheese is needed";      
            }

            else if (name.Any(char.IsDigit))
            {
                errors += "A name cannot contain a number";
            }

            return errors;
        }

    }


}