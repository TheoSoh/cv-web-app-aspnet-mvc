using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace CV_ASPMVC_GROUP2.Models
{
    public class TestDbContext : IdentityDbContext<User>
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Cv> Cvs { get; set; }
        public DbSet<Competence> Competences { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CvCompetence>()
                .HasKey(cc => new { cc.CvId, cc.CompetenceId });
            builder.Entity<CvEducation>()
                .HasKey(ce => new { ce.CvId, ce.EducationId });
            builder.Entity<CvExperience>()
                .HasKey(cex => new { cex.CvId, cex.ExperienceId });
            builder.Entity<UserProject>()
                .HasKey(up => new { up.UserId, up.ProjectId });

            builder.Entity<Message>()
                .HasOne(m => m.FromUser)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.FromUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Message>()
                .HasOne(m => m.ToUser)
                .WithMany(u => u.RecievedMessages)
                .HasForeignKey(m => m.ToUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
