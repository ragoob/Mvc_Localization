using Techno.Localization.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techno.Core.Data.Mapping;

namespace Techno.Localization.Mapping
{
   public  class LanguageMap : TechnoEntityTypeConfiguration<Language>
    {
        public LanguageMap()
        {
            ToTable("TechnoLocalization.Language");
            HasKey(l => l.Id);
            Property(l => l.Name).IsRequired().HasMaxLength(100);
            Property(l => l.LanguageCulture).IsRequired().HasMaxLength(20);
            Property(l => l.UniqueSeoCode).HasMaxLength(2);
            Property(l => l.FlagImageFileName).HasMaxLength(50);
        }
    }
}
