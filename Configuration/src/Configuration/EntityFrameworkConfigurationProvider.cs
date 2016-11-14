using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Configuration
{
    public class EntityFrameworkConfigurationProvider : ConfigurationProvider
    {
        public Action<DbContextOptionsBuilder> OptionsAction { get; }
        public EntityFrameworkConfigurationProvider(Action<DbContextOptionsBuilder> optionsAction)
        {
            OptionsAction = optionsAction;
        }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<ConfigurationContext>();
            OptionsAction(builder);
            using (var dbContext = new ConfigurationContext(builder.Options))
            {
                dbContext.Database.EnsureCreated();
                //dbContext.Database.ExecuteSqlCommand(
                //                    "create table [dbo].[Values](Id varchar(100) primary key, value varchar(100))");
                Data = !dbContext.Values.Any()
                    ? CreateAndSaveDefaultValues(dbContext)
                    : dbContext.Values.ToDictionary(c => c.Id, c => c.Value);
            }
        }

        private IDictionary<string, string> CreateAndSaveDefaultValues(ConfigurationContext dbContext)
        {
            var configValues = new Dictionary<string, string>()
            {
                {"key1","value from ef1"},
                {"key2","value from ef2"}
            };
            dbContext.Values.AddRange(configValues.Select(it => new ConfigurationValue() { Id = it.Key, Value = it.Value }));
            dbContext.SaveChanges();
            return configValues;
        }
    }
}
