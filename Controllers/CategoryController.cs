using GonToDoApi.Core;
using GonToDoApi.Models;
using GonToDoApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GonToDoApi.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class CategoryController(CategoryService service) : ControllerBase
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
        var categoryModels = await service.GetByAccountId(accountId);
        return Ok(new Root("Thành công", "Lấy thông tin thành công.", new { categoryModels }));
    }

    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetByCategoryId(string categoryId)
    {
        var categoryModel = await service.GetByCategoryId(categoryId);
        return Ok(new Root("Thành công", "Lấy thông tin thành công.", new { categoryModel }));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryModel categoryModel)
    {
        if (await service.CheckIfCategoryNameExists(categoryModel.Name))
        {
            await service.Create(categoryModel);
            return Ok(new Root("Thành công", "Tạo thành công.", new { categoryModel }));
        }

        return Conflict(new Root("Xung đột", "Đã có thẻ này vui lòng thử lại."));
    }

    [HttpPut("{categoryId}")]
    public async Task<IActionResult> Update(string categoryId, [FromBody] CategoryModel categoryModel)
    {
        var model = await service.GetByCategoryId(categoryId);

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (model == null) return BadRequest(new Root("Yêu cầu không hợp lệ", "Yêu cầu không hợp lệ"));
        await service.Update(categoryId, categoryModel);
        return Ok(new Root("Thành công", "Cập nhập thành công."));
    }


    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> Delete(string categoryId)
    {
        var model = await service.GetByCategoryId(categoryId);
        if (model == null) return BadRequest(new Root("Xóa không thành công", "Yêu cầu xóa không thành công"));

        await service.Delete(categoryId);
        return Ok(new Root("Thành công", "Xóa thành công."));
    }
}