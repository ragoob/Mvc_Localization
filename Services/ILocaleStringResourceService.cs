
using Techno.Localization.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Techno.Localization.Services
{
    public interface ILocaleStringResourceService
    {
        string GetResource(string resourceName);
        string GetResource(string resourceName, long languageId, bool logIfNotFound = false, string defaultValue = "", bool returnEmptyIfNotFound = false);
        void InsertLocaleStringResource(LocaleStringResourceDTO LocaleStringResource);
        void UpdateLocaleStringResource(LocaleStringResourceDTO LocaleStringResource);
        void DeleteLocaleStringResource(LocaleStringResourceDTO LocaleStringResource);
    
        IList<LocaleStringResourceDTO> GetAll();
    }
}
