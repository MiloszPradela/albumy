Dokumentacja : 

Projekt zbudowany przy użyciu ASP.NET WebApp MVC.

Paczki użyte w projekcie:



    > Microsoft.AspNet.Identity.EntityFramework (Version 2.2.4)

    > Microsoft.AspNetCore.Components.QuickGrid.EntityFrameworkAdapter (Version 8.0.1)

    > Microsoft.AspNetCore.Identity.EntityFrameworkCore (Version 8.0.1)

    > Microsoft.AspNetCore.Identity.UI (Version 8.0.1)

    > Microsoft.EntityFrameworkCore.Design (Version 8.0.1)

    > Microsoft.EntityFrameworkCore.SqlServer (Version 8.0.1)

    > Microsoft.EntityFrameworkCore.Tools (Version 8.0.1)

    > Microsoft.Extensions.DependencyInjection (Version 8.0.0)



Połączenie z bazą danych należy skonfigurować w AppDbContext w metodzie OnConfiguring:



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    {

        if (!optionsBuilder.IsConfigured)

        {

            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AlbumAppDb;Trusted_Connection=True;");

        }

    }



Baza danych seedowana jest automatycznie, wcześniej jednak należy użyć Package Manager Console (ustawić domyślny projekt jako AlbumApp.Data) w celu stworzenia bazy danych metodą "update-database" (migracje są gotowe w projekcie).



Podstawowe dane logowania:

admin@wsei.edu.pl / ZAQ!2wsx123

user@wsei.edu.pl / ZAQ!2wsx123

Panel Admin jest dostępny tylko dla użytkowników z rolą Admin.

Jako użytkownik możemy wystawić ocenę albumowi, jako admin również.

