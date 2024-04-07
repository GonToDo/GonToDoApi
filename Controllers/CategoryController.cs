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
        return Ok(categoryModels);
    }

    [HttpGet("{accountId}")]
    public async Task<IActionResult> GetByAccountId(string accountId)
    {
        var categoryModels = await service.GetByAccountId(accountId);
        return Ok(categoryModels);
    }

    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetByCategoryId(string categoryId)
    {
        var categoryModel = await service.GetByCategoryId(categoryId);
        return Ok(categoryModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryModel categoryModel)
    {
        if (await service.CheckIfCategoryNameExists(categoryModel.Name))
        {
            await service.Create(categoryModel);
            return Ok(categoryModel);
        }

        return Conflict("Đã có thẻ này vui lòng thử lại.");
    }

    [HttpPut("{categoryId}")]
    public async Task<IActionResult> Update(string categoryId, [FromBody] CategoryModel categoryModel)
    {
        var model = await service.GetByCategoryId(categoryId);

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (model == null) return BadRequest("Yêu cầu không hợp lệ");
        await service.Update(categoryId, categoryModel);
        return Ok("Cập nhập thành công.");
    }


    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> Delete(string categoryId)
    {
        var model = await service.GetByCategoryId(categoryId);
        if (model == null) return BadRequest("Yêu cầu xóa không thành công");

        await service.Delete(categoryId);
        return Ok("Xóa thành công.");
    }
}