using Azure;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using My_Final_Project.Areas.Identity.Data;
using My_Final_Project.Models;
using System.Diagnostics.Metrics;
using System.IO;
using System.Reflection.Emit;

namespace My_Final_Project.Areas.Identity.Data;

public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
{

    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<Award> Awards { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<ContentType> ContentTypes { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<Download> Downloads { get; set; }

    public virtual DbSet<Episode> Episodes { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieFragment> MovieFragments { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<RolesActor> RolesActors { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.ActorId).HasName("PK__Actors__E57403ED5B0C4E2A");

            entity.HasOne(d => d.Country).WithMany(p => p.Actors).HasConstraintName("FK__Actors__Country___2A4B4B5E");
        });

        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasKey(e => e.AnnouncementId).HasName("PK__Announce__853AB7CF91279941");

            entity.HasOne(d => d.Movie).WithMany(p => p.Announcements).HasConstraintName("FK__Announcem__Movie__3C69FB99");
        });

        modelBuilder.Entity<Award>(entity =>
        {
            entity.HasKey(e => e.AwardId).HasName("PK__Awards__30C01BCD528A7834");

            entity.HasOne(d => d.Movie).WithMany(p => p.Awards).HasConstraintName("FK__Awards__Movie_ID__693CA210");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__Cities__DE9DE0209382E0F6");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities).HasConstraintName("FK__Cities__Country___2D27B809");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__99FC143B9CCE4679");

            entity.HasOne(d => d.Movie).WithMany(p => p.Comments).HasConstraintName("FK__Comments__Movie___534D60F1");

            entity.HasOne(d => d.ParentComment).WithMany(p => p.InverseParentComment).HasConstraintName("FK__Comments__Parent__5535A963");

            entity.HasOne(d => d.Review).WithMany(p => p.Comments).HasConstraintName("FK__Comments__Review__5629CD9C");

            entity.HasOne(d => d.User).WithMany(p => p.Comments).HasConstraintName("FK__Comments__User_I__5441852A");
        });

        modelBuilder.Entity<ContentType>(entity =>
        {
            entity.HasKey(e => e.ContentTypeId).HasName("PK__Content___33E2D62219ACA25C");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__Countrie__8036CB4EEFC4EE01");
        });

        modelBuilder.Entity<Director>(entity =>
        {
            entity.HasKey(e => e.DirectorId).HasName("PK__Director__3939BCE1696F064B");
        });

        modelBuilder.Entity<Download>(entity =>
        {
            entity.HasKey(e => e.DownloadId).HasName("PK__Download__423297A7C412C268");

            entity.HasOne(d => d.Movie).WithMany(p => p.Downloads).HasConstraintName("FK__Downloads__Movie__46E78A0C");

            entity.HasOne(d => d.User).WithMany(p => p.Downloads).HasConstraintName("FK__Downloads__User___47DBAE45");
        });

        modelBuilder.Entity<Episode>(entity =>
        {
            entity.HasKey(e => e.EpisodeId).HasName("PK__Episodes__5ABD44031620DA0D");

            entity.HasOne(d => d.Movie).WithMany(p => p.Episodes).HasConstraintName("FK__Episodes__Movie___398D8EEE");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.FavoriteId).HasName("PK__Favorite__749FA5A71BA754A0");

            entity.HasOne(d => d.Movie).WithMany(p => p.Favorites).HasConstraintName("FK__Favorites__Movie__5812160E");

            entity.HasOne(d => d.User).WithMany(p => p.Favorites).HasConstraintName("FK__Favorites__User___571DF1D5");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genres__964A2006799799D3");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movies__7A880405F20AA26B");

            entity.HasOne(d => d.Country).WithMany(p => p.Movies).HasConstraintName("FK__Movies__Country__36B12243");

            entity.HasOne(d => d.ContentType).WithMany(p => p.Movies).HasConstraintName("FK__Movies__Content___37A5467C");

            entity.HasMany(d => d.Actors).WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieActor",
                    r => r.HasOne<Actor>().WithMany()
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Movie_Act__Actor__403A8C7D"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Movie_Act__Movie__3F466844"),
                    j =>
                    {
                        j.HasKey("MovieId", "ActorId").HasName("PK__Movie_Ac__B4DF443BA52AC0A3");
                        j.ToTable("Movie_Actor");
                        j.IndexerProperty<int>("MovieId").HasColumnName("Movie_ID");
                        j.IndexerProperty<int>("ActorId").HasColumnName("Actor_ID");
                    });

            entity.HasMany(d => d.Directors).WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieDirector",
                    r => r.HasOne<Director>().WithMany()
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Movie_Dir__Direc__440B1D61"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Movie_Dir__Movie__4316F928"),
                    j =>
                    {
                        j.HasKey("MovieId", "DirectorId").HasName("PK__Movie_Di__791B9FCB5E6F89FE");
                        j.ToTable("Movie_Director");
                        j.IndexerProperty<int>("MovieId").HasColumnName("Movie_ID");
                        j.IndexerProperty<int>("DirectorId").HasColumnName("Director_ID");
                    });

            entity.HasMany(d => d.Genres).WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "GenreMovie",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Genre_Mov__Genre__5FB337D6"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Genre_Mov__Movie__5EBF139D"),
                    j =>
                    {
                        j.HasKey("MovieId", "GenreId").HasName("PK__Genre_Mo__03ECA605F3AF6449");
                        j.ToTable("Genre_Movie");
                        j.IndexerProperty<int>("MovieId").HasColumnName("Movie_ID");
                        j.IndexerProperty<int>("GenreId").HasColumnName("Genre_ID");
                    });

            entity.HasMany(d => d.Tags).WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Movie_Tag__Tag_I__5BE2A6F2"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Movie_Tag__Movie__5AEE82B9"),
                    j =>
                    {
                        j.HasKey("MovieId", "TagId").HasName("PK__Movie_Ta__5782C1C6B3634117");
                        j.ToTable("Movie_Tag");
                        j.IndexerProperty<int>("MovieId").HasColumnName("Movie_ID");
                        j.IndexerProperty<int>("TagId").HasColumnName("Tag_ID");
                    });

        });

        modelBuilder.Entity<MovieFragment>(entity =>
        {
            entity.HasKey(e => e.FragmentId).HasName("PK__Movie_Fr__D36055D2EC26CF7B");

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieFragments).HasConstraintName("FK__Movie_Fra__Movie__628FA481");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__Ratings__BE48C8253983175E");

            entity.HasOne(d => d.Movie).WithMany(p => p.Ratings).HasConstraintName("FK__Ratings__Movie_I__4AB81AF0");

            entity.HasOne(d => d.User).WithMany(p => p.Ratings).HasConstraintName("FK__Ratings__User_ID__4BAC3F29");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__F85DA7EB44FDD2ED");

            entity.HasOne(d => d.Movie).WithMany(p => p.Reviews).HasConstraintName("FK__Reviews__Movie_I__4F7CD00D");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews).HasConstraintName("FK__Reviews__User_ID__5070F446");
        });

        modelBuilder.Entity<RolesActor>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles_Ac__D80AB49B70635F5D");

            entity.HasOne(d => d.Actor).WithMany(p => p.RolesActors).HasConstraintName("FK__Roles_Act__Actor__656C112C");

            entity.HasOne(d => d.Movie).WithMany(p => p.RolesActors).HasConstraintName("FK__Roles_Act__Movie__66603565");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tags__D0AC5C33A5E8C8DB");
        });

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__206D91905A81CD68");

            entity.HasOne(d => d.Country).WithMany(p => p.Users).HasConstraintName("FK__Users__Country_I__300424B4");
        });

        base.OnModelCreating(modelBuilder);
    }
}
