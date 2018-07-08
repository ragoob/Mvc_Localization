using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Techno.Core.DTO;
using Techno.Localization.Services.DTO;
using System.Web.Mvc;
using Techno.Localization.Services;
using Techno.Core;

namespace Techno.Localization.Mvc
{
   public static class LocalizationExtensions
    {
        public static string GetLocalized<T>(this T entity,
            Expression<Func<T, string>> keySelector, long languageId,
            bool returnDefaultValue = true, bool ensureTwoPublishedLanguages = true)
            where T : BaseDTO, ILocalizedEntity
        {
            return GetLocalized<T, string>(entity, keySelector, languageId, returnDefaultValue, ensureTwoPublishedLanguages);
        }


        public static TPropType GetLocalized<T, TPropType>(this T entity,
       Expression<Func<T, TPropType>> keySelector, long languageId,
       bool returnDefaultValue = true, bool ensureTwoPublishedLanguages = true)
       where T : BaseDTO, ILocalizedEntity
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var member = keySelector.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    keySelector));
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format(
                       "Expression '{0}' refers to a field, not a property.",
                       keySelector));
            }

            TPropType result = default(TPropType);
            string resultStr = string.Empty;

            //load localized value
            string localeKeyGroup = typeof(T).Name.Replace("DTO","");
            string localeKey = propInfo.Name;

            if (languageId > 0)
            {
                //ensure that we have at least two published languages
                bool loadLocalizedValue = true;
                if (ensureTwoPublishedLanguages)
                {
                    var lService = DependencyResolver.Current.GetService<ILanguageService>();
                    var totalPublishedLanguages = lService.GetAll().Count;
                    loadLocalizedValue = totalPublishedLanguages >= 2;
                }

                //localized value
                if (loadLocalizedValue)
                {
                    var leService = DependencyResolver.Current.GetService<ILocalizedEntityService>();
                    resultStr = leService.GetLocalizedValue(languageId, entity.Id, localeKeyGroup, localeKey);
                    if (!String.IsNullOrEmpty(resultStr))
                        result = CommonHelper.To<TPropType>(resultStr);
                }
            }

            //set default value if required
            if (String.IsNullOrEmpty(resultStr) && returnDefaultValue)
            {
                var localizer = keySelector.Compile();
                result = localizer(entity);
            }

            return result;
        }
    }
}
