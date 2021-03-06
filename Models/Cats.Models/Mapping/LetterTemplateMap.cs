﻿
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class LetterTemplateMap : EntityTypeConfiguration<LetterTemplate>
    {
        public LetterTemplateMap()
        {
            // Primary Key
            this.HasKey(t => t.LetterTemplateID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            //this.Property(t => t.Template)
            //    .IsRequired();

            this.Property(t => t.FileName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("LetterTemplate");
            this.Property(t => t.LetterTemplateID).HasColumnName("LetterTemplateID");
            this.Property(t => t.Name).HasColumnName("Name");
            //this.Property(t => t.Parameters).HasColumnName("Parameters");
            //this.Property(t => t.Template).HasColumnName("Template");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.TemplateType).HasColumnName("TemplateType");
        }
    }
}
