using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETMVCDemo.Data
{
    public class DemoDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DemoDbContext(
            DbContextOptions<DemoDbContext> options) //En passant une instance de DbContextOptions<Type>, on va permettre la configuration depuis "l'extérieur" et éviter ainsi d'overrider OnConfiguring
            : base(options) // Il est nécessaire de passer ces options à la classe de base pour qu'elle puisse l'exploiter
        {

        }
    }
}
