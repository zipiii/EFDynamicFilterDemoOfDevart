using System;
using System.Collections.Generic;
using System.Data.Entity;
using EntityFramework.DynamicFilters;

namespace EFDynamicFilterDemo
{
    /// <summary>
    /// 上下文
    /// </summary>
    public class ExampleContext : ContextBase
    {
        public ExampleContext()
            : base("Default")
        {
            Database.SetInitializer(new ContentInitializer1());
            //Database.SetInitializer(new ContentInitializer<ContextBase>());
            Database.Log = log => System.Diagnostics.Debug.WriteLine(log);
            Database.Initialize(false);
            //this.SetFilterScopedParameterValue("MustHaveTenant", "tenantId", 3);
            //this.SetFilterScopedParameterValue("MayHaveTenant", "tenantId", 4);
        }
    }


    /// <summary>
    /// 上下文基类
    /// </summary>
    public abstract class ContextBase : DbContext
    {

        //public DbSet<Account> Accounts { get; set; }
        //public DbSet<BlogEntry> BlogEntries { get; set; }
        public DbSet<MyLanguage> Languages { get; set; }

        public ContextBase(string nameOrConnectionString) : base(nameOrConnectionString)
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //限制所有针对BlogEntry查询的过虑（只获取未删除的）
            //这里的全局过虑，使用了委托，以便在每次需要计算值
            //重要：如果值使用的是一个委托，请确保它在你的应用中是全局的，
            modelBuilder.Filter("SoftDelete", (ISoftDelete d) => d.IsDeleted, true);
            //modelBuilder.Filter("MustHaveTenant", (IMustHaveTenant t, int tenantId) => t.TenantId == tenantId || (int?)t.TenantId == null, 2);
            //modelBuilder.Filter("MayHaveTenant", (IMayHaveTenant t, int? tenantId) => t.TenantId == tenantId, 1);

        }
    }


    public class ContentInitializer<T> : DropCreateDatabaseAlways<T>
        where T : ContextBase
    {
        protected override void Seed(T context)
        {
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
    }

    public class ContentInitializer1 : MigrateDatabaseToLatestVersion<ExampleContext, Configuration>
    {
    }
}
