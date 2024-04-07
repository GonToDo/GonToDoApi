using GonToDoApi.Core;
using GonToDoApi.Models;
using GonToDoApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GonToDoApi.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class TaskController(TaskService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categoryModels = await service.GetAll();
        return Ok(new Root("Thành công", "Lấy toàn thông tin thành công.", new { categoryModels }));
    }

    [HttpGet("{accountId}")]
    public async Task<IActionResult> GetByAccountId(string accountId)
    {
        var taskModels = await service.GetByAccountId(accountId);
        return Ok(new Root("Thành công", "Lấy thông tin thành công.", new { taskModels }));
    }

    [HttpGet("{taskId}")]
    public async Task<IActionResult> GetByTaskId(string taskId)
    {
        var taskModel = await service.GetByTaskId(taskId);
        return Ok(new Root("Thành công", "Lấy thông tin thành công.", new { taskModel }));
    }

    [HttpPost]
    public async Task<IActionResult> Create(TaskModel taskModel)
    {
        if (await service.CheckIfTaskTitleExists(taskModel.Title))
        {
            await service.Create(taskModel);
            return Ok(new Root("Thành công", "Tạo thành công.", new { taskModel }));
        }

        return Conflict(new Root("Xung đột", "Đã có thẻ này vui lòng thử lại."));
    }

    [HttpPut("{taskId}")]
    public async Task<IActionResult> Update(string taskId, [FromBody] TaskModel taskModel)
    {
        var model = await service.GetByTaskId(taskId);

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (model == null) return BadRequest(new Root("Yêu cầu không hợp lệ", "Yêu cầu không hợp lệ"));
        await service.Update(taskId, taskModel);
        return Ok(new Root("Thành công", "Cập nhập thành công."));
    }


    [HttpDelete("{taskId}")]
    public async Task<IActionResult> Delete(string taskId)
    {
        var model = await service.GetByTaskId(taskId);
        if (model == null) return BadRequest(new Root("Xóa không thành công", "Yêu cầu xóa không thành công"));

        await service.Delete(taskId);
        return Ok(new Root("Thành công", "Xóa thành công."));
    }
}