using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cats.Models.Security.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Key
            this.HasKey(t => t.UserId);

            // Properties
            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(200);
            this.Property(t => t.FullName)
                .IsRequired()
                .HasMaxLength(200);
            this.Property(t => t.Email)
                .HasMaxLength(50);

            // Table & Column Mapping
            this.ToTable("User");
            this.Property(t => t.Disabled).HasColumnName("Disabled");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.FullName).HasColumnName("FullName");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.UserName).HasColumnName("UserName");
        }
    }
}
