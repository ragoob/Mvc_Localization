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
   public class LocaleStringResourceDTO : BaseDTO
    {
        /// <summary>
        /// Gets or sets the LanguageDTO identifier
        /// </summary>
        [TechnoResourceDisplayName("LocaleStringResource.Fields.LanguageId")]

        public long LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the resource name
        /// </summary>
        [TechnoResourceDisplayName("LocaleStringResource.Fields.ResourceName")]
        public string ResourceName { get; set; }

        /// <summary>
        /// Gets or sets the resource value
        /// </summary>
        [TechnoResourceDisplayName("LocaleStringResource.Fields.ResourceValue")]
        public string ResourceValue { get; set; }
    }
}
