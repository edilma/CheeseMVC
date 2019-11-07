using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        //public Dictionary<string, string> Cheeses.CheeseMVC(name , description ) ;
        


        public IActionResult Index()
        {

            //List<string> cheeses = new List<string>();
            //cheeses.Add("Cheddar");
            //cheeses.Add("Munster");
            //cheeses.Add("Parmesan");
            //cheeses.Add("Harvaty");
            //ViewBag.cheeses = cheeses;

            ViewBag.cheeses = CheeseData.GetAll();
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = CheeseData.GetAll();
            return View();
        }

        public IActionResult Delete()
        {
            ViewBag.title = "Delete Cheeses";
            ViewBag.cheeses = CheeseData.GetAll();
            return View();
        }

        [HttpPost]
        [Route("/Cheese/Add")]
        public IActionResult NewCheese(/*string name, string description=""*/ Cheese newCheese)
        {
            string error = validate(newCheese.Name);
            if (error == "")
            {
                /*Cheese newCheese = new Cheese
                {
                    Description = description,
                    Name = name

                };*/
                CheeseData.Add(newCheese);

                return Redirect("/Cheese");
            }
            else
            {
                ViewBag.cheeses = CheeseData.GetAll();
                ViewBag.error = error;
                return View("Index");
            }
        }
                
       
        [HttpPost]
        [Route("/Cheese/Remove")]
        public IActionResult Remove(int[] cheeseIds)
        {
            //Remove a checked cheese
            /*for (int i = 0; i < cheeseIds.Length; i++)
            {
                Cheeses.Remove(cheeseIds[i]);
            }
            */
            foreach (int cheeseId in cheeseIds)
            {
                CheeseData.Remove(cheeseId);
            }
                      
            return Redirect("/Cheese");
        }
        

           
        [HttpPost]
        [Route("/Cheese/Delete")]
        public IActionResult DelCheese(int cheeseId)
        {
            //Delete the cheese using the select drop down list
            //Cheeses.Remove(cheese);
            CheeseData.Remove(cheeseId);


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