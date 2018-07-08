using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Techno.Core;
using Techno.Localization.DTO;
using Techno.Localization.Mvc;

namespace Techno.Localization.Services
{
    public class XmlLocaleStringResourceService : ILocaleStringResourceService
    {
        #region Const
        private const string LOCALSTRINGRESOURCES_ALL_KEY = "LocaleStringResources.All.{0}";
        private const string LOCALSTRINGRESOURCES_PATTERN_KEY = "LocaleStringResources.All.";
        private const string LOCALIZATIONXMLFILEPATH = "~/App_Data";

        #endregion

        #region Ctor

        private ILangugeContext _langugeContext;
        private ICacheManager _CacheManager;


        #endregion

        #region Constractors
        public XmlLocaleStringResourceService( ICacheManager CacheManager,
            ILangugeContext langugeContext
           )
        {
           
            this._langugeContext = langugeContext;
            this._CacheManager = CacheManager;

        }

        #endregion

        #region Utilites
        public virtual Dictionary<string, KeyValuePair<long, string>> GetAllResourceValues(long languageId)
        {
            string key = string.Format(LOCALSTRINGRESOURCES_ALL_KEY, languageId);
            return _CacheManager.Get(key, () =>
            {
                List<LocaleStringResourceDTO> Localelist = new List<LocaleStringResourceDTO>();
                XmlSerializer serializer = new XmlSerializer(typeof(List<LocaleStringResourceDTO>));
                try
                {
                    using (FileStream stream = File.OpenWrite(System.Web.HttpContext.Current.Server.MapPath(LOCALIZATIONXMLFILEPATH)))
                    {

                        serializer.Serialize(stream, Localelist);
                    }
                }
                catch (Exception)
                {

                   
                }
                //we use no tracking here for performance optimization
                //anyway records are loaded only for read-only operations
                var query = from l in Localelist
                            orderby l.ResourceName
                            where l.LanguageId == languageId && l.ResourceValue != null && l.ResourceValue != ""
                            select l;
                var locales = query.ToList();
                //format: <name, <id, value>>
                var dictionary = new Dictionary<string, KeyValuePair<long, string>>();
                foreach (var locale in locales)
                {
                    var resourceName = locale.ResourceName.ToLowerInvariant();
                    if (!dictionary.ContainsKey(resourceName))
                        dictionary.Add(resourceName.ToLower(), new KeyValuePair<long, string>(locale.Id, locale.ResourceValue));
                }
                return dictionary;
            });
        }
        #endregion

        #region Methods
        public void DeleteLocaleStringResource(LocaleStringResourceDTO LocaleStringResource)
        {
            throw new NotImplementedException();
        }

        public IList<LocaleStringResourceDTO> GetAll()
        {

            var List = _CacheManager.Get(LOCALSTRINGRESOURCES_ALL_KEY, int.MaxValue, () =>
            {
                List<LocaleStringResourceDTO> Localelist = new List<LocaleStringResourceDTO>();
                XmlSerializer serializer = new XmlSerializer(typeof(List<LocaleStringResourceDTO>));
                try
                {
                    using (FileStream stream = File.OpenWrite(System.Web.HttpContext.Current.Server.MapPath(LOCALIZATIONXMLFILEPATH)))
                    {

                        serializer.Serialize(stream, Localelist);
                    }
                }
                catch (Exception)
                {


                }

                return Localelist;

            });
            return List;
        }

        public string GetResource(string resourceName, long languageId, bool logIfNotFound = false, string defaultValue = "", bool returnEmptyIfNotFound = false)
        {

            string result = string.Empty;
            if (resourceName == null)
                resourceName = string.Empty;

            resourceName = resourceName.Trim().ToLowerInvariant();

            //load all records (we know they are cached)
            var resources = GetAllResourceValues(languageId);
            if (resources.ContainsKey(resourceName))
            {
                result = resources[resourceName].Value;
            }


            if (String.IsNullOrEmpty(result))
            {



                if (!String.IsNullOrEmpty(defaultValue))
                {
                    result = defaultValue;
                }
                else
                {
                    if (!returnEmptyIfNotFound)
                        result = resourceName;
                }
            }
            return result;
        }

        public string GetResource(string resourceName)
        {


            return GetResource(resourceName, _langugeContext.WorkingLanguage.Id);

        }

        public void InsertLocaleStringResource(LocaleStringResourceDTO LocaleStringResource)
        {
            throw new NotImplementedException();
        }

        public void UpdateLocaleStringResource(LocaleStringResourceDTO LocaleStringResource)
        {
            throw new NotImplementedException();
        }

#endregion
    }
}
