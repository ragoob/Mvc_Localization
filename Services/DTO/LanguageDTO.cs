using Techno.Localization.DTO;
using Techno.Localization.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techno.Core.DTO;

namespace Techno.Localization.DTO
{
   public class LanguageDTO : BaseDTO
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [TechnoResourceDisplayName("Language.Fields.Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the LanguageDTO culture
        /// </summary>
        [TechnoResourceDisplayName("Language.Fields.LanguageCulture")]
        public string LanguageCulture { get; set; }

        /// <summary>
        /// Gets or sets the unique SEO code
        /// </summary>       
        [TechnoResourceDisplayName("Language.Fields.UniqueSeoCode")]
        public string UniqueSeoCode { get; set; }

        /// <summary>
        /// Gets or sets the flag image file name
        /// </summary>
        [TechnoResourceDisplayName("Language.Fields.FlagImageFileName")]
        public string FlagImageFileName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the LanguageDTO supports "Right-to-left"
        /// </summary>
        [TechnoResourceDisplayName("Language.Fields.Rtl")]
        public bool Rtl { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether the LanguageDTO is published
        /// </summary>
        [TechnoResourceDisplayName("Language.Fields.Published")]
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        [TechnoResourceDisplayName("Language.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }

    }
}
