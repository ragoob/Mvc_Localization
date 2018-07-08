using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Techno.Core.Infrastructure;
using Techno.Core;
using Techno.Core.Data;
using Techno.Localization.Entities;
using Techno.Localization.Services;
using Techno.Localization.Mvc;

namespace Techno.Localization.Infrastructure
{
    public class LocalizationDependencyRegistrar : IDependencyRegistrar
    {
        private string _ConnectionStringOrname;

        public LocalizationDependencyRegistrar(string ConnectionStringOrname)
        {
            this._ConnectionStringOrname = ConnectionStringOrname;
        }
        public int Order { get; }

        public void Register(ContainerBuilder builder)
        {
           
            //register services

            builder.RegisterType<LanguageService>().As<ILanguageService>().InstancePerLifetimeScope();
            builder.RegisterType<LocalizedEntityService>().As<ILocalizedEntityService>().InstancePerLifetimeScope();
            builder.RegisterType<LocaleStringResourceService>().As<ILocaleStringResourceService>().InstancePerLifetimeScope();
            //builder.RegisterType<LangugeContext>().As<ILangugeContext>().InstancePerLifetimeScope();
         
            //register localization dbcontext
            this.RegisterExternalDataContext<LocalizationDBContext>(builder, "techno_object_context_localization",_ConnectionStringOrname);


            //override required repository with our custom context
            builder.RegisterType<EfRepository<Language>>()
                .As<IRepository<Language>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("techno_object_context_localization"))
                .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<LocaleStringResource>>()
               .As<IRepository<LocaleStringResource>>()
               .WithParameter(ResolvedParameter.ForNamed<IDbContext>("techno_object_context_localization"))
               .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<LocalizedProperty>>()
               .As<IRepository<LocalizedProperty>>()
               .WithParameter(ResolvedParameter.ForNamed<IDbContext>("techno_object_context_localization"))
               .InstancePerLifetimeScope();
        }
    }
}
