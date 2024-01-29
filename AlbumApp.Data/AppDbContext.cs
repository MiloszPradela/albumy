using AlbumApp.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlbumApp.Data;
public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Album> Albums { get; set; }
    public DbSet<Song> Songs { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Rating> Ratings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // album
        modelBuilder.Entity<Album>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<Album>()
            .Property(a => a.Name)
            .IsRequired();

        modelBuilder.Entity<Album>()
            .HasMany(a => a.Songs)
            .WithOne(s => s.Album)
            .HasForeignKey(s => s.AlbumId);

        modelBuilder.Entity<Album>()
            .HasOne(a => a.Artist)
            .WithMany(b => b.Albums)
            .HasForeignKey(a => a.ArtistId);

        // song
        modelBuilder.Entity<Song>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<Song>()
            .Property(s => s.Title)
            .IsRequired();

        // band
        modelBuilder.Entity<Artist>()
            .HasKey(b => b.Id);

        modelBuilder.Entity<Artist>()
            .Property(b => b.Name)
            .IsRequired();

        modelBuilder.Entity<Artist>()
            .HasMany(b => b.Albums)
            .WithOne(a => a.Artist)
            .HasForeignKey(a => a.ArtistId);

        // rating
        modelBuilder.Entity<Rating>()
            .HasKey(r => new { r.AlbumId, r.IdentityUserId });

        modelBuilder.Entity<Rating>()
            .HasOne(r => r.Album)
            .WithMany(r => r.Ratings)
            .HasForeignKey(r => r.AlbumId);

        modelBuilder.Entity<Rating>()
            .HasOne(r => r.IdentityUser)
            .WithMany()
            .HasForeignKey(r => r.IdentityUserId);

        base.OnModelCreating(modelBuilder);

        string adminId = Guid.NewGuid().ToString();
        string userId = Guid.NewGuid().ToString();
        string adminRoleId = Guid.NewGuid().ToString();
        string userRoleId = Guid.NewGuid().ToString();

        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Name = "admin",
            NormalizedName = "ADMIN",
            Id = adminRoleId,
            ConcurrencyStamp = adminRoleId

        });

        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Name = "user",
            NormalizedName = "USER",
            Id = userRoleId,
            ConcurrencyStamp = userRoleId
        });

        var admin = new IdentityUser
        {
            Id = adminId,
            Email = "admin@wsei.edu.pl",
            EmailConfirmed = false,
            UserName = "admin@wsei.edu.pl",
            NormalizedUserName = "ADMIN@WSEI.EDU.PL",
            NormalizedEmail = "ADMIN@WSEI.EDU.PL"
        };

        var user = new IdentityUser
        {
            Id = userId,
            Email = "user@wsei.edu.pl",
            EmailConfirmed = false,
            UserName = "user@wsei.edu.pl",
            NormalizedUserName = "USER@WSEI.EDU.PL",
            NormalizedEmail = "USER@WSEI.EDU.PL"
        };

        PasswordHasher<IdentityUser> ph = new();
        admin.PasswordHash = ph.HashPassword(admin, "ZAQ!2wsx123");
        user.PasswordHash = ph.HashPassword(user, "ZAQ!2wsx123");

        modelBuilder.Entity<IdentityUser>().HasData(admin);
        modelBuilder.Entity<IdentityUser>().HasData(user);

        modelBuilder.Entity<IdentityUserRole<string>>()
            .HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminId
            });

        modelBuilder.Entity<IdentityUserRole<string>>()
        .HasData(new IdentityUserRole<string>
        {
            RoleId = userRoleId,
            UserId = userId
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AlbumAppDb;Trusted_Connection=True;");
        }
    }
}
