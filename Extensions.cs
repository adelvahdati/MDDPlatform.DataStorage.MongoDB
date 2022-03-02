using MDDPlatform.DataStorage.MongoDB.Options;
using MDDPlatform.DataStorage.MongoDB.Repositories;
using MDDPlatform.SharedKernel.Entities;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace  MDDPlatform.DataStorage.MongoDB
{
    public static class Extensions
    {
        public static IServiceCollection AddMongoDB
            (this IServiceCollection services,IConfiguration configuration, string sectionName)
        {
            var mongoOption = new MongoDbOption();
            configuration.GetSection(sectionName).Bind(mongoOption);
            services.AddSingleton(mongoOption);
            services.AddScoped<IMongoClient>(sp=>{
                var options = sp.GetRequiredService<MongoDbOption>();
                return new MongoClient(options.ConnectionString);
            });
            services.AddTransient<IMongoDatabase>(sp=>{
                var options = sp.GetRequiredService<MongoDbOption>();
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(options.Database);
            });
            return services;            
        }
         public static IServiceCollection AddMongoRespository<TEntity,TId>
            (this IServiceCollection services,string collectionName) where TEntity : BaseEntity<TId>
         {
             services.AddScoped<IMongoRepository<TEntity,TId>>(sp=>{
                 var database = sp.GetRequiredService<IMongoDatabase>();
                 return new MongoRepository<TEntity,TId>(database,collectionName);
             });
             return services;
         }
    }
    
}