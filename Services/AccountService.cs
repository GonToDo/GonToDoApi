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
        return await collection.Find(a => a.Id == id).FirstOrDefaultAsync();
    }

    public async Task Create(AccountModel model)
    {
        await collection.InsertOneAsync(model);
    }

    public async Task<AccountModel> FindByName(string? userName)
    {
        var filter = Builders<AccountModel>.Filter.Eq("UserName", userName);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<bool> CheckPassword(AccountModel model, string? password)
    {
        return model.Password == password;
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
}