using Techno.Localization.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techno.Core.Data.Mapping;

namespace Techno.Localization.Mapping
{
    public class LocalizedPropertyMap : TechnoEntityTypeConfiguration<LocalizedProperty>
    {
        public LocalizedPropertyMap()
        {
            ToTable("TechnoLocalization.LocalizedProperty");
            HasKey(lp => lp.Id);
            Property(lp => lp.LocaleKeyGroup).IsRequired().HasMaxLength(400);
            Property(lp => lp.LocaleKey).IsRequired().HasMaxLength(400);
            Property(lp => lp.LocaleValue).IsRequired();
            HasRequired(lp => lp.Language)
                .WithMany()
                .HasForeignKey(lp => lp.LanguageId);
        }
    }
}
