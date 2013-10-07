using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hub.Mapping
{
    public class SMSMap : EntityTypeConfiguration<SMS>
    {
        public SMSMap()
        {
            // Primary Key
            this.HasKey(t => new { t.SMSID, t.InOutInd, t.MobileNumber, t.Text, t.Status, t.StatusDate, t.Attempts });

            // Properties
            this.Property(t => t.SMSID);
              //  .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.InOutInd)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.MobileNumber)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.Text)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.Status)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Attempts);
              //  .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.EventTag)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("SMS");
            this.Property(t => t.SMSID).HasColumnName("SMSID");
            this.Property(t => t.InOutInd).HasColumnName("InOutInd");
            this.Property(t => t.MobileNumber).HasColumnName("MobileNumber");
            this.Property(t => t.Text).HasColumnName("Text");
            this.Property(t => t.RequestDate).HasColumnName("RequestDate");
            this.Property(t => t.SendAfterDate).HasColumnName("SendAfterDate");
            this.Property(t => t.QueuedDate).HasColumnName("QueuedDate");
            this.Property(t => t.SentDate).HasColumnName("SentDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.StatusDate).HasColumnName("StatusDate");
            this.Property(t => t.Attempts).HasColumnName("Attempts");
            this.Property(t => t.LastAttemptDate).HasColumnName("LastAttemptDate");
            this.Property(t => t.EventTag).HasColumnName("EventTag");
        }
    }
}
