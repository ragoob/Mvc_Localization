
using Techno.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Techno.Localization.Services;

namespace Techno.Localization.Mvc
{
    public class TechnoResourceDisplayName : System.ComponentModel.DisplayNameAttribute, IModelAttribute
    {
        private string _resourceValue = string.Empty;
        public TechnoResourceDisplayName(string resourceKey)
            : base(resourceKey)
        {
            ResourceKey = resourceKey;
        }

        public string ResourceKey { get; set; }

        public override string DisplayName
        {
            get
            {
                
                var langId =
                     DependencyResolver.Current.GetService<ILangugeContext>().WorkingLanguage.Id;
                _resourceValue = DependencyResolver.Current.GetService<ILocaleStringResourceService>()
                    .GetResource(ResourceKey);
                    ;

               
                return !string.IsNullOrEmpty(_resourceValue) ? _resourceValue : ResourceKey;
            }
        }

        public string Name
        {
            get { return "TechnoResourceDisplayName"; }
        }
    }
}
