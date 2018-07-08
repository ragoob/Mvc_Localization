using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Techno.Localization.DTO;
using Techno.Localization.Services;

namespace Techno.Localization.Mvc
{
    public class LangugeContext : ILangugeContext
    {
        private const string USERLANGUAGE = "User.Language";
        private LanguageDTO _cachedLanguage;
        private ILanguageService _languageService;
        private readonly HttpContextBase _HttpContext;

        public LangugeContext(HttpContextBase HttpContext, ILanguageService languageService)
        {
            this._HttpContext = HttpContext;
            this._languageService = languageService;
        }

        protected virtual LanguageDTO GetLanguageFromBrowserSettings()
        {
            if (_HttpContext == null ||
                _HttpContext.Request == null ||
                _HttpContext.Request.UserLanguages == null)
                return null;

            var userLanguage = _HttpContext.Request.UserLanguages.FirstOrDefault();
            if (String.IsNullOrEmpty(userLanguage))
                return null;

            var language = _languageService
                .GetAll()
                .FirstOrDefault(l => userLanguage.Equals(l.LanguageCulture, StringComparison.InvariantCultureIgnoreCase));
            if (language != null && language.Published)
            {
                return language;
            }

            return null;
        }


        public virtual LanguageDTO WorkingLanguage
        {
            get
            {
                if (_cachedLanguage != null)
                    return _cachedLanguage;
                string userLanguage = "";
                if (_HttpContext == null ||
                   _HttpContext.Request == null ||
                   _HttpContext.Request.UserLanguages == null)
                    return null;


                if (_HttpContext.Request.Cookies[USERLANGUAGE] != null)
                {

                    userLanguage = 
                        _HttpContext.Request.Cookies[USERLANGUAGE].Value.ToString();
                }
                var allLanguages = _languageService.GetAll();

                var language = allLanguages
                      .OrderBy(l => l.DisplayOrder)
                      .FirstOrDefault(l => (l.UniqueSeoCode.ToLower() == userLanguage.ToLower() || userLanguage == ""));
                if (language != null && language.Published)
                {
                    _cachedLanguage = language;
                    return _cachedLanguage;
                }


                else
                    return null;
            }

            set {
                _HttpContext.Response.Cookies.Add(new HttpCookie(USERLANGUAGE, value.UniqueSeoCode));
                _cachedLanguage = null;


            }

          
        }

       
    }
}
