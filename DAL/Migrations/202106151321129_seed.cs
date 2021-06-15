namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seed : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [Services] VALUES (N'Установка пожарной системы \"Гранит\"', 98500)");
            Sql("INSERT INTO [Services] VALUES (N'Установка пожарной системы \"Аква\"', 4500)");
            Sql("INSERT INTO [Services] VALUES (N'Установка пожарной сигнализации ', 3500)");
            Sql("INSERT INTO [Services] VALUES (N'Проектирование пожарной системы ', 5500)");
        }
        
        public override void Down()
        {
        }
    }
}
