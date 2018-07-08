using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techno.Localization.DTO;
using Techno.Core.Data;
using Techno.Core;
using Techno.Localization.Mapping;
using Techno.Core.Data.Entities;
using Techno.Localization.Mvc;
using Techno.Localization.Entities;

namespace Techno.Localization.Services
{
    public class LocaleStringResourceService : ILocaleStringResourceService
    {
        #region Const
        private const string LOCALSTRINGRESOURCES_ALL_KEY = "LocaleStringResources.All.{0}";
        private const string LOCALSTRINGRESOURCES_PATTERN_KEY = "LocaleStringResources.All.";
    
        #endregion

         #region Ctor
        private IRepository<LocaleStringResource> _LocaleStringResourceRepository;
        private ILangugeContext _langugeContext;
        private ICacheManager _CacheManager;
       

        #endregion

        #region Constractors
        public LocaleStringResourceService(IRepository<LocaleStringResource> LocaleStringResourceRepository,  ICacheManager CacheManager,
            ILangugeContext langugeContext
           )
        {
            this._LocaleStringResourceRepository = LocaleStringResourceRepository;
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
                //we use no tracking here for performance optimization
                //anyway records are loaded only for read-only operations
                var query = from l in _LocaleStringResourceRepository.TableNoTracking
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
            var entity = LocaleStringResource.ToEntity();
            _LocaleStringResourceRepository.Delete(entity);
        }

        public string GetResource(string resourceName,long languageId, bool logIfNotFound = false, string defaultValue = "", bool returnEmptyIfNotFound = false)
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
            var entity = LocaleStringResource.ToEntity();
            _LocaleStringResourceRepository.Insert(entity);
            _CacheManager.RemoveByPattern(LOCALSTRINGRESOURCES_PATTERN_KEY);


        }

        public void UpdateLocaleStringResource(LocaleStringResourceDTO LocaleStringResource)
        {
            var entity = LocaleStringResource.ToEntity();
            _LocaleStringResourceRepository.Update(entity);
            _CacheManager.RemoveByPattern(LOCALSTRINGRESOURCES_PATTERN_KEY);
        }

       

        public IList<LocaleStringResourceDTO> GetAll()
        {
          
            var List = _CacheManager.Get(LOCALSTRINGRESOURCES_ALL_KEY, int.MaxValue, () =>
            {
                return _LocaleStringResourceRepository.Table.ToList();

            });
            return List.Select(l=> l.ToModel()).ToList();
        }

       

        #endregion



    }
}
