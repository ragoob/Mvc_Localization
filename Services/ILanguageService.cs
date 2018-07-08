using Techno.Localization.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techno.Localization.Services
{
   public interface ILanguageService
    {
        LanguageDTO GetById(long Id);
        LanguageDTO GetBySeoCode(string SeoCode);
        IList<LanguageDTO> GetAll(bool ShowHidden = false);
        void InsertLanguage(LanguageDTO Language);
        void UpdateLanguage(LanguageDTO Language);
        void DeleteLanguage(LanguageDTO Language);
        void HideLanguage(LanguageDTO Language);
    }
}
