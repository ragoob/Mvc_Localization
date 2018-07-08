using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Techno.Localization.Services;
using Techno.Localization.Services.DTO;

namespace Techno.Localization.Mvc.Helpers
{
   public static class LocalizationHelper
    {
        public static HelperResult LocalizedEditor<T, TLocalizedModelLocal>(this HtmlHelper<T> helper,
        string name,
        Func<int, HelperResult> localizedTemplate,
        Func<T, HelperResult> standardTemplate,
        bool ignoreIfSeveralStores = false)
        where T : ILocalizedModel<TLocalizedModelLocal>
        where TLocalizedModelLocal : ILocalizedModelLocal
        {
            return new HelperResult(writer =>
            {
                var localizationSupported = helper.ViewData.Model.Locales.Count > 1;
              
                if (localizationSupported)
                {
                    var tabStrip = new StringBuilder();
                    tabStrip.AppendLine(string.Format("<div id=\"{0}\" class=\"nav-tabs-custom nav-tabs-localized-fields\">", name));
                    tabStrip.AppendLine("<ul class=\"nav nav-tabs\">");

                    //default tab
                    tabStrip.AppendLine("<li class=\"active\">");
                    tabStrip.AppendLine(string.Format("<a data-tab-name=\"{0}-{1}-tab\" href=\"#{0}-{1}-tab\" data-toggle=\"tab\">{2}</a>",
                            name,
                            "standard",
                            DependencyResolver.Current.GetService<ILocaleStringResourceService>().GetResource("Common.Standard")));
                    tabStrip.AppendLine("</li>");

                    tabStrip.AppendLine("<li>");
                    tabStrip.AppendLine(string.Format("<a data-tab-name=\"{0}-{1}-tab\" href=\"#{0}-{1}-tab\" data-toggle=\"tab\">{2}</a>",
                         name,
                         "Localization",
                         DependencyResolver.Current.GetService<ILocaleStringResourceService>().GetResource("Common.Localization")));
                    tabStrip.AppendLine("</li>");


                    var languageService = DependencyResolver.Current.GetService<ILanguageService>();
                   
                    tabStrip.AppendLine("</ul>");

                    //default tab
                    tabStrip.AppendLine("<div class=\"tab-content\">");
                    tabStrip.AppendLine(string.Format("<div class=\"tab-pane active\" id=\"{0}-{1}-tab\">", name, "standard"));
                    tabStrip.AppendLine(standardTemplate(helper.ViewData.Model).ToHtmlString());
                    tabStrip.AppendLine("</div>");
                                
                    tabStrip.AppendLine(string.Format("<div class=\"tab-pane\" id=\"{0}-{1}-tab\">", name, "Localization"));
                    var dividingNumber = 12 / helper.ViewData.Model.Locales.Count;
                    tabStrip.AppendLine("<div class=\"row\">");
                    for (int i = 0; i < helper.ViewData.Model.Locales.Count; i++)
                    {
                        var language = languageService.GetById(helper.ViewData.Model.Locales[i].LanguageId);
                        tabStrip.AppendLine(string.Format(" <div class=\"col-md-{0}\">", dividingNumber));
                        tabStrip.AppendLine(" <fieldset>");
                        tabStrip.AppendLine(string.Format(" <legend>{0}</legend>", DependencyResolver.Current.GetService<ILocaleStringResourceService>().GetResource(string.Format("Common.Languages.{0}",language.LanguageCulture))));
                        tabStrip.AppendLine(localizedTemplate(i).ToHtmlString());
                        tabStrip.AppendLine("</fieldset>");
                        tabStrip.AppendLine(" </div>");
                    }
                    tabStrip.AppendLine("</div>");
                    tabStrip.AppendLine("</div>");

                   
                    tabStrip.AppendLine("</div>");
                    tabStrip.AppendLine("</div>");
                    writer.Write(new MvcHtmlString(tabStrip.ToString()));
                }
                else
                {
                    standardTemplate(helper.ViewData.Model).WriteTo(writer);
                }
            });
        }
    }
}
