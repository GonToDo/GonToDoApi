using GonToDoApi.Core.DataBase;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GonToDoApi.Core.Elements;

public abstract class Service<TCollection>
    where TCollection : class
{
    protected readonly IMongoCollection<TCollection> collection;
    protected readonly IOptions<DataBaseSettings> dbSettings;

    /// <summary>
    ///  return dbSettings.Value.[CollectionName]
    /// </summary>
    /// <returns></returns>
    protected abstract string? GetCollectionName();

    protected Service(IOptions<DataBaseSettings> dbSettings)
    {
        this.dbSettings = dbSettings;
        var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DataBaseName);
        
        collection = mongoDatabase.GetCollection<TCollection>(GetCollectionName());
    }
}