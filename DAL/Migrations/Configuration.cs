namespace DAL.Migrations
{
    using DAL.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.AllDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            
        }

        protected override void Seed(DAL.AllDbContext context)
        {
            

            /*
             * 
            Sql("INSERT INTO [Services] VALUES ('Установка пожарной системы \"Гранит\"', 2500)");
            Sql("INSERT INTO [Services] VALUES ('Установка пожарной системы \"Аква\"', 4500)");
            Sql("INSERT INTO [Services] VALUES ('Установка пожарной сигнализации ', 3500)");
            Sql("INSERT INTO [Services] VALUES ('Проектирование пожарной системы ', 5500)");
             * */

            context.Services.AddOrUpdate
                (
                    new Service { Name = "Установка пожарной системы \"Аква\"", Cost = 2900 },
                    new Service { Name = "Установка пожарной системы \"Гранит\"", Cost = 2567 },
                    new Service { Name = "Установка пожарной сигнализации", Cost = 2567 },
                    new Service { Name = "Проектирование пожарной системы", Cost = 1900 }
                );
            context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
