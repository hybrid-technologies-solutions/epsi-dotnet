using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ASPNETMVCDemo.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ASPNETMVCDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = CreateWebHostBuilder(args).Build();

            // C'est une bonne pratique de faire l'initialisation de la base de données au lancement de l'application
            // en le sortant du Startup.cs pour éviter de polluer la configuration

            // Récupération du DemoDbContext depuis l'injection de dépendances
            // ATTENTION : cette ligne ne marche que si votre Startup a un "AddDbContext" avec le type demandé
            var dbContext = (DemoDbContext)builder.Services.CreateScope().ServiceProvider.GetRequiredService(typeof(DemoDbContext));
            // Je demande à EF de s'assurer que la base est bien crée
            dbContext.Database.EnsureCreated();
            // S'il n'y a pas d'étudiant, j'en créé un factice
            if (!dbContext.Students.Any())
            {

                dbContext.Students.Add(new Student
                {
                    Name = "Jean",
                    LastName = "Valjean"
                });

                dbContext.SaveChanges();
            }
            //Je lance mon site
            builder.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
