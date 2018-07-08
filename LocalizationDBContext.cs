﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Techno.Core.Data;
using Techno.Core.Data.Entities;
using Techno.Core.Data.Mapping;
using Techno.Localization.Entities;
using Techno.Localization.Mapping;

namespace Techno.Localization
{
    public class LocalizationDBContext : DbContext, IDbContext
    {
        #region Ctor

        public LocalizationDBContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            //((IObjectContextAdapter) this).ObjectContext.ContextOptions.LazyLoadingEnabled = true;
        }

        #endregion

        #region Utilities

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new LanguageMap());
            modelBuilder.Configurations.Add(new LocaleStringResourceMap());
            modelBuilder.Configurations.Add(new LocalizedPropertyMap());
            base.OnModelCreating(modelBuilder);
        }

        #endregion

        #region Methods

        public string CreateDatabaseScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        /// <summary>
        /// Install
        /// </summary>
        public void Install()
        {
           
            if (!exists)
            {
                var dbScript = CreateDatabaseScript();
                Database.ExecuteSqlCommand(dbScript);
                SaveChanges();

                // add default language 
                this.Set<Language>().Add(new Language()
                {
                    Name = "English",
                    UniqueSeoCode = "En",
                    DisplayOrder = 1,
                    FlagImageFileName = "",
                    LanguageCulture = "en-US",
                    Published = true,
                    Rtl = true,
                    
                });

                SaveChanges();
            }

            //create the table
          
        }

        public void UnInstall()
        {
            if (exists)
            {
                
                this.Database.ExecuteSqlCommand(@"IF EXISTS(SELECT *
                                                 FROM   TechnoLocalization.LocaleStringResource)
                                                DROP TABLE TechnoLocalization.LocaleStringResource");

                this.Database.ExecuteSqlCommand(@"IF EXISTS(SELECT *
                                                 FROM   TechnoLocalization.LocalizedProperty)
                                                DROP TABLE TechnoLocalization.LocalizedProperty");

                this.Database.ExecuteSqlCommand(@"IF EXISTS(SELECT *
                                                 FROM   TechnoLocalization.Language)
                                                DROP TABLE TechnoLocalization.Language");

            }
        }

        private bool exists { get {
                string query = @"
                         SELECT 1 FROM sys.tables AS T
                         INNER JOIN sys.schemas AS S ON T.schema_id = S.schema_id
                         WHERE S.Name = 'TechnoLocalization' AND (T.Name = 'Language' or T.Name = 'LocaleStringResource' or T.Name = 'LocalizedProperty' )";
                bool exists = this.Database
                    .SqlQuery<int?>(query)
                    .ToList().Count > 0;
                return exists;
            } }
        /// <summary>
        /// Execute stores procedure and load a list of entities at the end
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="commandText">Command text</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Entities</returns>
        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a raw SQL query that will return elements of the given generic type.  The type can be any type that has properties that match the names of the columns returned from the query, or can be a simple primitive type. The type does not have to be an entity type. The results of this query are never tracked by the context even if the type of object returned is an entity type.
        /// </summary>
        /// <typeparam name="TElement">The type of object returned by the query.</typeparam>
        /// <param name="sql">The SQL query string.</param>
        /// <param name="parameters">The parameters to apply to the SQL query string.</param>
        /// <returns>Result</returns>
        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Executes the given DDL/DML command against the database.
        /// </summary>
        /// <param name="sql">The command string</param>
        /// <param name="doNotEnsureTransaction">false - the transaction creation is not ensured; true - the transaction creation is ensured.</param>
        /// <param name="timeout">Timeout value, in seconds. A null value indicates that the default value of the underlying provider will be used</param>
        /// <param name="parameters">The parameters to apply to the command string.</param>
        /// <returns>The result returned by the database after executing the command.</returns>
        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Detach an entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Detach(object entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            ((IObjectContextAdapter)this).ObjectContext.Detach(entity);
        }

        public IList<TElement> ExecuteStoredProcedureNotMappedList<TElement>(string commandText, params object[] parameters)
        {
            throw new NotImplementedException();
        }

       

        public void Update(object entity, string Property)
        {
            throw new NotImplementedException();
        }

        public new void Entry(EntityState entitystate, object entity)
        {
            this.Entry(entity).State = entitystate;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether proxy creation setting is enabled (used in EF)
        /// </summary>
        public virtual bool ProxyCreationEnabled
        {
            get
            {
                return Configuration.ProxyCreationEnabled;
            }
            set
            {
                Configuration.ProxyCreationEnabled = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether auto detect changes setting is enabled (used in EF)
        /// </summary>
        public virtual bool AutoDetectChangesEnabled
        {
            get
            {
                return Configuration.AutoDetectChangesEnabled;
            }
            set
            {
                Configuration.AutoDetectChangesEnabled = value;
            }
        }

        #endregion
    }
}
