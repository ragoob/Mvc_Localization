using Techno.Localization.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techno.Core.DTO;

namespace Techno.Localization.DTO
{
    public class LocalizedPropertyDTO : BaseDTO
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public long EntityId { get; set; }

        /// <summary>
        /// Gets or sets the LanguageDTO identifier
        /// </summary>
        public long LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the locale key group
        /// </summary>
        public string LocaleKeyGroup { get; set; }

        /// <summary>
        /// Gets or sets the locale key
        /// </summary>
        public string LocaleKey { get; set; }

        /// <summary>
        /// Gets or sets the locale value
        /// </summary>
        public string LocaleValue { get; set; }
    }
}
