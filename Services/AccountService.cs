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

    public async Task<IEnumerable<AccountModel>> GetAll()
    {
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task<AccountModel> GetById(string id)
    {
        var filter = Builders<AccountModel>.Filter.Eq(m => m.Id, id);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task Create(AccountModel accountModel)
    {
        await collection.InsertOneAsync(accountModel);
    }

    public async Task<AccountModel> FindByName(string? userName)
    {
        var filter = Builders<AccountModel>.Filter.Eq(m => m.UserName, userName);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public static Task<bool> CheckPassword(AccountModel accountModel, string? password)
    {
        return Task.FromResult(accountModel.Password == password);
    }

    public async Task Update(string id, AccountModel accountModel)
    {
        await collection.ReplaceOneAsync(a => a.Id == id, accountModel);
    }


    /// <summary>
    ///     userName not existed yet => true
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public async Task<bool> CheckIfNameExists(string? userName)
    {
        var filter = Builders<AccountModel>.Filter.Eq(m => m.UserName, userName);
        var user = await collection.Find(filter).FirstOrDefaultAsync();
        return user == null;
    }

    public async Task Delete(string id)
    {
        await collection.DeleteOneAsync(a => a.Id == id);
    }
}