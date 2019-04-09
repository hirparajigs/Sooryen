using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Sooryen.Data.Models.Mapping
{
    public class NoteMap : EntityTypeConfiguration<Note>
    {
        public NoteMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(100);

            this.Property(t => t.UserMaster)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Notes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Body).HasColumnName("Body");
            this.Property(t => t.InsertDate).HasColumnName("InsertDate");
            this.Property(t => t.InsertUser).HasColumnName("InsertUser");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.UpdateUser).HasColumnName("UpdateUser");
            this.Property(t => t.UserMaster).HasColumnName("UserMaster");

            // Relationships
            this.HasOptional(t => t.User)
                .WithMany(t => t.Notes)
                .HasForeignKey(d => d.UpdateUser);
            this.HasOptional(t => t.User1)
                .WithMany(t => t.Notes1)
                .HasForeignKey(d => d.InsertUser);

        }
    }
}
