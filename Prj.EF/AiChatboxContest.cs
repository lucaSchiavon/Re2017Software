namespace Ls.Prj.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Ls.Base.EF;
    using Ls.Prj.Entity;
    using Ls.Prj.EF.TypeConfigurations;

    public partial class AiChatboxContest : LsDbContext
    {
        public AiChatboxContest()
            : base("AiChatBotConn")
        {
        }

        private const string schema = "dbo";
        public virtual DbSet<Audit> Audits { get; set; }
        public virtual DbSet<Chapter> Chapters { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Ls.Prj.Entity.Type> Types { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<TagValue> TagValues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AuditTypeConfiguration(schema));
            modelBuilder.Configurations.Add(new UserTypeConfiguration(schema));
            modelBuilder.Configurations.Add(new RoleTypeConfiguration(schema));
            modelBuilder.Configurations.Add(new ChapterTypeConfiguration(schema));
            modelBuilder.Configurations.Add(new DocumentTypeConfiguration(schema));
            modelBuilder.Configurations.Add(new ImageTypeConfiguration(schema));
            modelBuilder.Configurations.Add(new TagTypeConfiguration(schema));
            modelBuilder.Configurations.Add(new TypeTypeConfiguration(schema));
            modelBuilder.Configurations.Add(new TagValueTypeConfiguration(schema));
        }
    }
}
