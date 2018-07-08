using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techno.Core;
using Techno.Core.Data;
using Techno.Core.Data.Entities;
using Techno.Localization.DTO;
using Techno.Localization.Entities;
using Techno.Localization.Mapping;
using Techno.Localization.Mvc;

namespace Techno.Localization.Services
{
    public class LanguageService : ILanguageService
    {
         #region Const
        private const string LANGUAGES_ALL_KEY = "Techno.Language.All";
        private const string LANGUAGES_BY_ID_KEY = "Techno.language.id-{0}";

        #endregion

         #region Ctor
        private IRepository<Language> _LanguageRepository;
        private ICacheManager _CacheManager;

        #endregion

        #region Constractors
        public LanguageService(IRepository<Language> LanguageRepository, ICacheManager CacheManager)
        {
            this._LanguageRepository = LanguageRepository;
            this._CacheManager = CacheManager;
        }

        #endregion

         #region Methods


        public void DeleteLanguage(LanguageDTO Language)
        {
            throw new NotImplementedException();
        }

        public IList<LanguageDTO> GetAll(bool ShowHidden = false)
        {
            string key = string.Format(LANGUAGES_ALL_KEY, ShowHidden);
            var languages = _CacheManager.Get(key, () =>
            {
                var query = _LanguageRepository.TableNoTracking;
                if (!ShowHidden)
                    query = query.Where(l => l.Published);
                query = query.OrderBy(l => l.DisplayOrder).ThenBy(l => l.Id);
                return query.ToList();
            });

            return languages.Select(l => l.ToModel()).ToList();
        }

        public LanguageDTO GetById(long Id)
        {
            if (Id == 0)
                return null;

            string key = string.Format(LANGUAGES_BY_ID_KEY, Id);
           

            return _CacheManager.Get(key, () =>
            {
                return _LanguageRepository.GetById(Id);
            }
            ).ToModel();

        }

        public LanguageDTO GetBySeoCode(string SeoCode)
        {
            if (string.IsNullOrWhiteSpace(SeoCode))
                return null;

            string key = string.Format(LANGUAGES_BY_ID_KEY, SeoCode);
           
                return _CacheManager.Get(key, () =>
             {
                 return _LanguageRepository.Table.Where(l => l.UniqueSeoCode == SeoCode).FirstOrDefault();
             }
                ).ToModel();
           
        }

        public void HideLanguage(LanguageDTO Language)
        {
            var entity = Language.ToEntity();
            entity.Published = false;
            _LanguageRepository.Update(entity);

            _CacheManager.Remove(LANGUAGES_ALL_KEY);
        }

        public void InsertLanguage(LanguageDTO Language)
        {
            var entity = Language.ToEntity();
            _LanguageRepository.Insert(entity);
            Language.Id = entity.Id;
            _CacheManager.Remove(LANGUAGES_ALL_KEY);
        }

        public void UpdateLanguage(LanguageDTO Language)
        {
            var entity = Language.ToEntity();
            _LanguageRepository.Update(entity);
            _CacheManager.Remove(LANGUAGES_ALL_KEY);
        }

#endregion
    }
}
