using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.DynamicFilters;

namespace EFDynamicFilterDemo {
    class Program {
        static void Main(string[] args) {

            Devart.Data.Oracle.OracleMonitor monitor = new Devart.Data.Oracle.OracleMonitor() { IsActive = true };
            Devart.Data.Oracle.Entity.Configuration.OracleEntityProviderConfig devartConfig = Devart.Data.Oracle.Entity.Configuration.OracleEntityProviderConfig.Instance;
            devartConfig.Workarounds.IgnoreSchemaName = true;
            devartConfig.Workarounds.DisableQuoting = true;
            devartConfig.CodeFirstOptions.UseDateTimeAsDate = true;
            devartConfig.CodeFirstOptions.UseNonLobStrings = true;
            devartConfig.CodeFirstOptions.UseNonUnicodeStrings = true;
            devartConfig.CodeFirstOptions.TruncateLongDefaultNames = true;

            // 过滤器默认启用
            var context = new ExampleContext();

            Console.WriteLine(" 使用过滤器BlogEntryFilter进行查询");
            Query(context, "homer");

            //禁用过滤器
            context.DisableFilter("SoftDelete");
            
            Console.WriteLine(" 禁用过滤器SoftDelete进行查询");
            Query(context, "homer");

            Console.ReadLine();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userName"></param>
        private static void Query(ExampleContext context, string userName)
        {
            //var account = context.Accounts
            //    .Include(a => a.BlogEntries).FirstOrDefault(a => a.UserName == userName);

            //Console.WriteLine("账号{0}的博客有：",userName);
            //if (account == null) return;
            //foreach (var blog in account.BlogEntries)
            //{
            //    Console.WriteLine("{0}",blog.Id);
            //}

            Console.WriteLine("语言有：");
            foreach (var language in context.Languages)
            {
                Console.WriteLine("{0}", language.Name);
            }
        }
    }
}
