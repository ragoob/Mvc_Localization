using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techno.Core.Data.Entities;
using Techno.Localization.DTO;
using Techno.Localization.Entities;

namespace Techno.Localization.Mvc
{
   public static class LocalizationMappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            return mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            return mapper.Map(source, destination);
        }

        #region Language
        public static LanguageDTO ToModel(this Language entity)
        {
            return entity.MapTo<Language, LanguageDTO>();
        }

        public static Language ToEntity(this LanguageDTO model)
        {
            return model.MapTo<LanguageDTO, Language>();
        }
        #endregion

        #region Localization 

        public static LocaleStringResourceDTO ToModel(this LocaleStringResource entity)
        {
            return entity.MapTo<LocaleStringResource, LocaleStringResourceDTO>();
        }

        public static LocaleStringResource ToEntity(this LocaleStringResourceDTO model)
        {
            return model.MapTo<LocaleStringResourceDTO, LocaleStringResource>();
        }


        #endregion

        #region LocalizedProprties
        public static LocalizedProperty ToEntity(this LocalizedPropertyDTO model)
        {
            return model.MapTo<LocalizedPropertyDTO, LocalizedProperty>();
        }
        public static LocalizedPropertyDTO ToModel(this LocalizedProperty entity)
        {
            return entity.MapTo<LocalizedProperty, LocalizedPropertyDTO>();
        }
        #endregion
    }
}
