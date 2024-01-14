using GonToDoApi.Core.DataBase;
using GonToDoApi.Core.Elements;
using GonToDoApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GonToDoApi.Services;

public class CategoryService(IOptions<DataBaseSettings> dbSettings) : Service<CategoryModel>(dbSettings)
{
    protected override string? GetCollectionName()
    {
        return dbSettings.Value.CategoryCollectionName;
    }

    public async Task<IEnumerable<CategoryModel>> GetAll()
    {
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task<IEnumerable<CategoryModel>> GetByAccountId(string accountId)
    {
        var filter = Builders<CategoryModel>.Filter.Eq(m => m.AccountId, accountId);
        return await collection.Find(filter).ToListAsync();
    }

    public async Task<CategoryModel> GetByCategoryId(string categoryId)
    {
        var filter = Builders<CategoryModel>.Filter.Eq(m => m.Id, categoryId);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<bool> CheckIfCategoryNameExists(string? name)
    {
        var filter = Builders<CategoryModel>.Filter.Eq(m => m.Name, name);
        var user = await collection.Find(filter).FirstOrDefaultAsync();
        return user == null;
    }

    public async Task Create(CategoryModel categoryModel)
    {
        await collection.InsertOneAsync(categoryModel);
    }

    public async Task Update(string id, CategoryModel categoryModel)
    {
        await collection.ReplaceOneAsync(a => a.Id == id, categoryModel);
    }

    public async Task Delete(string id)
    {
        await collection.DeleteOneAsync(a => a.Id == id);
    }
}