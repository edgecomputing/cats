using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hubs.Mapping
{
    public class ForgetPasswordRequestMap : EntityTypeConfiguration<ForgetPasswordRequest>
    {
        public ForgetPasswordRequestMap()
        {
            // Primary Key
            this.HasKey(t => t.ForgetPasswordRequestID);

            // Properties
            this.Property(t => t.RequestKey)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ForgetPasswordRequest");
            this.Property(t => t.ForgetPasswordRequestID).HasColumnName("ForgetPasswordRequestID");
            this.Property(t => t.UserProfileID).HasColumnName("UserProfileID");
            this.Property(t => t.GeneratedDate).HasColumnName("GeneratedDate");
            this.Property(t => t.ExpieryDate).HasColumnName("ExpieryDate");
            this.Property(t => t.Completed).HasColumnName("Completed");
            this.Property(t => t.RequestKey).HasColumnName("RequestKey");
        }
    }
}
