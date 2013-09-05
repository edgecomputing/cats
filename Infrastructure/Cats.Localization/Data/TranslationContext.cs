using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Localization.Models;
using Cats.Localization.Models.Mapping;

namespace Cats.Localization.Data
{
    public class TranslationContext : DbContext
    {
        static TranslationContext()
        {
            Database.SetInitializer<TranslationContext>(null);
        }

        public TranslationContext()
            : base("Name=CatsContext") { }

        public DbSet<Language> Languages { get; set; }
        public DbSet<Phrase> Phrases { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<LocalizedPhrase> LocalizedPhrases { get; set; }
        public DbSet<LocalizedPagePhrase> LocalizedPagePhrases { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new LanguageMap());
            modelBuilder.Configurations.Add(new PhraseMap());
            modelBuilder.Configurations.Add(new PageMap());
            modelBuilder.Configurations.Add(new LocalizedPhraseMap());
            modelBuilder.Configurations.Add(new LocalizedPagePhraseMap());
        }
    }
}
