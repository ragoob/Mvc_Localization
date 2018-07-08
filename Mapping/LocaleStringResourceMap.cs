using Techno.Localization.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techno.Core.Data.Mapping;

namespace Techno.Localization.Mapping
{
   public  class LocaleStringResourceMap : TechnoEntityTypeConfiguration<LocaleStringResource>
    {
        public LocaleStringResourceMap()
        {
            ToTable("TechnoLocalization.LocaleStringResource");
            HasKey(lsr => lsr.Id);
            Property(lsr => lsr.ResourceName).IsRequired().HasMaxLength(200);
            Property(lsr => lsr.ResourceValue).IsRequired();
            HasRequired(lsr => lsr.Language)
                .WithMany(l => l.LocaleStringResources)
                .HasForeignKey(lsr => lsr.LanguageId);
        }
    }
}
