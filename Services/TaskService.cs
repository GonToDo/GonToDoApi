using GonToDoApi.Core.DataBase;
using GonToDoApi.Core.Elements;
using GonToDoApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GonToDoApi.Services;

public class TaskService(IOptions<DataBaseSettings> dbSettings) : Service<TaskModel>(dbSettings)
{
    protected override string? GetCollectionName()
    {
        return dbSettings.Value.TaskCollectionName;
    }

    public async Task<IEnumerable<TaskModel>> GetAll()
    {
        return await collection.Find(_ => true).ToListAsync();
    }

    public async Task<IEnumerable<TaskModel>> GetByAccountId(string accountId)
    {
        var filter = Builders<TaskModel>.Filter.Eq(m => m.AccountId, accountId);
        return await collection.Find(filter).ToListAsync();
    }

    public async Task<TaskModel> GetByTaskId(string taskId)
    {
        var filter = Builders<TaskModel>.Filter.Eq(m => m.Id, taskId);
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<bool> CheckIfTaskTitleExists(string? title)
    {
        var filter = Builders<TaskModel>.Filter.Eq(m => m.Title, title);
        var user = await collection.Find(filter).FirstOrDefaultAsync();
        return user == null;
    }

    public async Task Create(TaskModel taskModel)
    {
        await collection.InsertOneAsync(taskModel);
    }

    public async Task Update(string id, TaskModel taskModel)
    {
        await collection.ReplaceOneAsync(a => a.Id == id, taskModel);
    }

    public async Task Delete(string id)
    {
        await collection.DeleteOneAsync(a => a.Id == id);
    }
}