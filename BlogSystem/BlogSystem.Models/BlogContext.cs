using System.Data.Entity.ModelConfiguration.Conventions;

namespace BlogSystem.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BlogContext : DbContext
    {
       
        public BlogContext()
            : base("con")
        {
            Database.SetInitializer<BlogContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }


        public DbSet<User> Users { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleToCategory> ArticleToCategories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Fans> Fans { get; set; }
    }

    
}