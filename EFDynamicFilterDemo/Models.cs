using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFDynamicFilterDemo {

    /// <summary>
    /// 软删除接口
    /// </summary>
    public interface ISoftDelete {
        bool IsDeleted { get; set; }
    }

    public interface IMustHaveTenant
    {
        /// <summary>
        /// TenantId of this entity.
        /// </summary>
        int TenantId { get; set; }
    }

    public interface IMayHaveTenant
    {
        /// <summary>
        /// TenantId of this entity.
        /// </summary>
        int? TenantId { get; set; }
    }

    /// <summary>
    /// 账户
    /// </summary>
    public class Account {
        [Key]
        [Required]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserName { get; set; }

        public ICollection<BlogEntry> BlogEntries { get; set; }

    }

    /// <summary>
    /// 博客
    /// </summary>
    public class BlogEntry : ISoftDelete//, IMustHaveTenant, IMayHaveTenant
    {
        [Key]
        [Required]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Account Account { get; set; }
        public int AccountId { get; set; }

        public string Body { get; set; }

        public virtual bool IsDeleted { get; set; }

        public int? IntValue { get; set; }

        public string StringValue { get; set; }
        public DateTime? DateValue { get; set; }

        public bool IsActive { get; set; }
        //public int TenantId { get; set; }
        //int? IMayHaveTenant.TenantId { get; set; }
    }



    public interface IEntitySoftDelete {
        bool IsDeleted { get; set; }
    }

    public class MyLanguage :
         ISoftDelete
    {
        #region abp
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(64)]
        public virtual string DisplayName { get; set; }

        [StringLength(128)]
        public virtual string Icon { get; set; }

        public virtual bool IsDeleted { get; set; }
        #endregion

        public MyLanguage()
        {
        }

        public MyLanguage(int? tenantId, string name, string displayName, string icon = null)
        {
            //TenantId = tenantId;
            Name = name;
            DisplayName = displayName;
            Icon = icon;
        }
    }
}
