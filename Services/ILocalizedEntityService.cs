using System;
using System.Linq.Expressions;
using Techno.Core.Data.Entities;
using Techno.Core.DTO;
using Techno.Localization.DTO;
using Techno.Localization.Entities;
using Techno.Localization.Services.DTO;

namespace Techno.Localization.Services
{
    /// <summary>
    /// Localized entity service interface
    /// </summary>
    public partial interface ILocalizedEntityService
    {
        /// <summary>
        /// Deletes a localized property
        /// </summary>
        /// <param name="LocalizedPropertyDTO">Localized property</param>
        void DeleteLocalizedProperty(LocalizedProperty LocalizedProperty);

        /// <summary>
        /// Gets a localized property
        /// </summary>
        /// <param name="LocalizedPropertyDTOId">Localized property identifier</param>
        /// <returns>Localized property</returns>
        LocalizedPropertyDTO GetLocalizedPropertyById(long LocalizedPropertyId);

        /// <summary>
        /// Find localized value
        /// </summary>
        /// <param name="languageId">Language identifier</param>
        /// <param name="entityId">Entity identifier</param>
        /// <param name="localeKeyGroup">Locale key group</param>
        /// <param name="localeKey">Locale key</param>
        /// <returns>Found localized value</returns>
        string GetLocalizedValue(long languageId, long entityId, string localeKeyGroup, string localeKey,string defaultvalue = "");

        /// <summary>
        /// Inserts a localized property
        /// </summary>
        /// <param name="LocalizedPropertyDTO">Localized property</param>
        void InsertLocalizedProperty(LocalizedProperty LocalizedProperty);

        /// <summary>
        /// Updates the localized property
        /// </summary>
        /// <param name="LocalizedPropertyDTO">Localized property</param>
        void UpdateLocalizedProperty(LocalizedProperty LocalizedProperty);

        /// <summary>
        /// Save localized value
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="localeValue">Locale value</param>
        /// <param name="languageId">Language ID</param>
        void SaveLocalizedValue<T>(T entity,
            Expression<Func<T, string>> keySelector,
            string localeValue,
            long languageId) where T : BaseDTO, ILocalizedEntity;

        void SaveLocalizedValue<T, TPropType>(T entity,
           Expression<Func<T, TPropType>> keySelector,
           TPropType localeValue,
           long languageId) where T : BaseDTO, ILocalizedEntity;
    }
}
