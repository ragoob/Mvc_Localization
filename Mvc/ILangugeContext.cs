using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techno.Localization.DTO;

namespace Techno.Localization.Mvc
{
    public interface ILangugeContext
    {
     
        LanguageDTO WorkingLanguage { get; set; }
      
    }
}
