using EntityFramework.DynamicFilters;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDynamicFilterDemo
{
    public sealed class Configuration : DbMigrationsConfiguration<ExampleContext>
    {
        private const string ProviderName = "Devart.Data.Oracle";

        public Configuration()
        {
            //AutomaticMigrationsEnabled = false;
            //ContextKey = "AbpZeroTemplate";

            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            //ContextKey = typeof(EntityFramework.AbpZeroTemplateDbContext).FullName;
            ContextKey = typeof(ExampleContext).FullName;

            SetSqlGenerator(ProviderName, new Devart.Data.Oracle.Entity.Migrations.OracleEntityMigrationSqlGenerator());

            InintDevartOracle();
        }

        protected override void Seed(ExampleContext context)
        {
            context.DisableAllFilters();

            System.Diagnostics.Debug.Print("Seeding db");

            //两个account实体，9个blog实体，其中4个blog被删除
            var homer = new Account
            {
                Id = 1,
                UserName = "homer",
                BlogEntries = new List<BlogEntry>
                {
                    new BlogEntry { Id = 1, Body="Homer's first blog entry", IsDeleted=false, IsActive=true, StringValue="1"},
                    new BlogEntry { Id = 2, Body="Homer's second blog entry", IsDeleted=false, IsActive=true, StringValue="2"},
                    new BlogEntry { Id = 3, Body="Homer's third blog entry (deleted)", IsDeleted=true, IsActive=true, StringValue="3"},
                    new BlogEntry { Id = 4, Body="Homer's fourth blog entry (deleted)", IsDeleted=true, IsActive=true, StringValue="4"},
                    new BlogEntry { Id = 5, Body="Homer's 5th blog entry (inactive)", IsDeleted=false, IsActive=false, StringValue="5"},
                    new BlogEntry { Id = 6, Body="Homer's 6th blog entry (deleted and inactive)", IsDeleted=true, IsActive=false, StringValue="6"},
                }
            };
            //context.Accounts.Add(homer);

            var bart = new Account
            {
                Id = 2,
                UserName = "bart",
                BlogEntries = new List<BlogEntry>
                {
                    new BlogEntry { Id = 7, Body="Bart's first blog entry", IsDeleted=false, IsActive=true, StringValue="7"},
                    new BlogEntry { Id = 8, Body="Bart's second blog entry", IsDeleted=false, IsActive=true, StringValue="8"},
                    new BlogEntry { Id = 9, Body="Bart's third blog entry", IsDeleted=false, IsActive=true, StringValue="9"},
                    new BlogEntry { Id = 10, Body="Bart's fourth blog entry (deleted)", IsDeleted=true, IsActive=true, StringValue="10"},
                    new BlogEntry { Id = 11, Body="Bart's fifth blog entry (deleted)", IsDeleted=true, IsActive=true, StringValue="11"},
                    new BlogEntry { Id = 12, Body="Bart's 6th blog entry (inactive)", IsDeleted=false, IsActive=false, StringValue="12"},
                    new BlogEntry { Id = 13, Body="Bart's 7th blog entry (deleted and inactive)", IsDeleted=true, IsActive=false, StringValue="13"},
                }
            };
            //context.Accounts.Add(bart);

            context.Languages.Add(new MyLanguage(null, "en", "English", "famfamfam-flag-gb"));
            context.Languages.Add(new MyLanguage(null, "ar", "العربية", "famfamfam-flag-sa"));
            context.Languages.Add(new MyLanguage(null, "de", "German", "famfamfam-flag-de"));
            context.Languages.Add(new MyLanguage(null, "it", "Italiano", "famfamfam-flag-it"));
            context.Languages.Add(new MyLanguage(null, "fr", "Français", "famfamfam-flag-fr"));
            context.Languages.Add(new MyLanguage(null, "pt-BR", "Portuguese", "famfamfam-flag-br"));
            context.Languages.Add(new MyLanguage(null, "tr", "Türkçe", "famfamfam-flag-tr"));
            context.Languages.Add(new MyLanguage(null, "ru", "Русский", "famfamfam-flag-ru"));
            context.Languages.Add(new MyLanguage(null, "zh-CN", "简体中文", "famfamfam-flag-cn"));

            context.SaveChanges();
        }

        private void InintDevartOracle()
        {
            Devart.Data.Oracle.OracleMonitor monitor = new Devart.Data.Oracle.OracleMonitor() { IsActive = true };
            Devart.Data.Oracle.Entity.Configuration.OracleEntityProviderConfig devartConfig = Devart.Data.Oracle.Entity.Configuration.OracleEntityProviderConfig.Instance;
            devartConfig.Workarounds.IgnoreSchemaName = true;
            devartConfig.Workarounds.DisableQuoting = true;
            devartConfig.CodeFirstOptions.UseDateTimeAsDate = true;
            devartConfig.CodeFirstOptions.UseNonLobStrings = true;
            devartConfig.CodeFirstOptions.UseNonUnicodeStrings = true;
            devartConfig.CodeFirstOptions.TruncateLongDefaultNames = true;
            //devartConfig.DatabaseScript.Column.MaxStringSize = Devart.Data.Oracle.Entity.Configuration.OracleMaxStringSize.Standard;
        }
    }
}
