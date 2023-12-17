using GonToDoApi.Core.DataBase;
using GonToDoApi.Core.Elements;
using GonToDoApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GonToDoApi.Services;

public class AccountService(IOptions<DataBaseSettings> dbSettings) : Service<AccountModel>(dbSettings)
{
    protected override string? GetCollectionName()
    {
        return dbSettings.Value.AccountCollectionName;
    }

    public async Task<IEnumerable<AccountModel>> GetAllAsync()=>
        await collection.Find(_ => true).ToListAsync();
    
    public async Task Create(AccountModel model) =>
        await collection.InsertOneAsync(model);
}