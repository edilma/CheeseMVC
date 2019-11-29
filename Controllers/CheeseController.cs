using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        //public Dictionary<string, string> Cheeses.CheeseMVC(name , description ) ;
        //static private Dictionary<string, string> Cheeses = new Dictionary<string, string>();
        //static private List<string> Cheeses = new List<string>();
        

        public IActionResult Index()
        {

            List<Cheese> cheeses = CheeseData.GetAll();
            return View(cheeses);
        }
       
        public IActionResult Add()
        {
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel();
            return View(addCheeseViewModel);
        }

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = CheeseData.GetAll();
            return View();
        }

        public IActionResult Delete()
        {
            ViewBag.cheeses = CheeseData.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                Cheese mycheese= CreateCheese( addCheeseViewModel);
                CheeseData.Add(mycheese);
                return Redirect("/Cheese");
            }
            return View(addCheeseViewModel);
            
        }

        [HttpPost]
        [Route("/Cheese/Remove")]
        public IActionResult Remove(int[] cheeseIds)
        {
            

            //Remove a checked cheese
            foreach (int cheeseId in cheeseIds)

            {
                CheeseData.Remove(cheeseId); ;
            }
            
                        
            return Redirect("/");
        }

        [HttpPost]
        [Route("/Cheese/Delete")]
        public IActionResult DelCheese(int cheeseId)
        {
            //Delete the cheese using the select drop down list
            CheeseData.Remove(cheeseId);
            
            return Redirect("/Cheese");
        }
        //This is the get
        public IActionResult Edit(int cheeseId)
        {
            //https:localhost:44311/Cheese/Edit?cheeseId=1
            //int cheeseId = Request.QueryString["cheeseId"];

            AddEditCheeseViewModel addEditCheeseViewModel = new AddEditCheeseViewModel();
            Cheese cheeseToEdit = CheeseData.GetById(cheeseId);
            addEditCheeseViewModel.Name = cheeseToEdit.Name;
            addEditCheeseViewModel.Description = cheeseToEdit.Description;
            addEditCheeseViewModel.Type = cheeseToEdit.Type;
            addEditCheeseViewModel.cheeseId = cheeseToEdit.CheeseId;

            return View(addEditCheeseViewModel);
            
        }

        [HttpPost]
        [Route("/Cheese/Edit")]
        public IActionResult Edit (AddEditCheeseViewModel addEditCheeseViewModel)
        {
            
            Cheese myCheese = CheeseData.GetById(addEditCheeseViewModel.cheeseId);
            myCheese.Name = addEditCheeseViewModel.Name;
            myCheese.Description = addEditCheeseViewModel.Description;
            myCheese.Type = addEditCheeseViewModel.Type;

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