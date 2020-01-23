using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETMVCDemo.Models;
using ASPNETMVCDemo.Services;
using ASPNETMVCDemo.Data;

namespace ASPNETMVCDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIdentityFormater formater;
        private readonly DemoDbContext dbContext;

        // Constructeur de mon contrôleur 
        public HomeController(
            // Il a besoin du formater d'identité
            // Grâce au paramètre de constructeur, je n'ai pas à faire de new
            IIdentityFormater formater,
            // Il a besoin d'accéder aux données, donc d'un DbContext (ou d'un repository)
            // Grâce au paramètre de constructeur, j'ai un DbContext déjà configuré sur une base 
            DemoDbContext dbContext
            )
        {
            this.formater = formater; // Je le stocke dans mon contrôleur pour l'utiliser quand je veux
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var student = dbContext.Students.FirstOrDefault();
            var identity = formater.FormatName(student);
            //En utilisant la méthode View(), ASP.NET va retourner la vue qui porte le nom de l'action (ici Index)
            // qui se trouve dans le dossier Views/Home, car le controller s'appelle HomeController
            return View(model: identity);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
