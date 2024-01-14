using GonToDoApi.Core;
using GonToDoApi.Models;
using GonToDoApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GonToDoApi.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class AccountController(AccountService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var accountModels = await service.GetAll();
        return Ok(new Root("Thành công", "Lấy toàn thông tin thành công.", new { accountModels }));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var accountModel = await service.GetById(id);
        return Ok(new Root("Thành công", "Lấy thông tin thành công.", new { accountModel }));
    }

    [HttpPost]
    public async Task<IActionResult> Register(string fullName, string userName, string password)
    {
        if (await service.CheckIfNameExists(userName))
        {
            var accountModel = new AccountModel
            {
                FullName = fullName,
                UserName = userName,
                Password = password
            };

            await service.Create(accountModel);
            return Ok(new Root("Thành công", "Tạo thành công.", new { accountModel }));
        }

        return Conflict(new Root("Xung đột", "Tên đăng nhập đã tồn tại."));
    }

    [HttpPost]
    public async Task<IActionResult> Login(string userName, string password)
    {
        var accountModel = await service.FindByName(userName);
        if (await AccountService.CheckPassword(accountModel, password))
            return Ok(new Root("Thành công", "Đăng nhập thành công.", new { accountModel }));
        return Unauthorized(new Root("Không được phép", "Tên đăng nhập hoặc mật khẩu không đúng."));
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] AccountModel accountModel)
    {
        var model = await service.GetById(id);

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (model == null) return BadRequest(new Root("Yêu cầu không hợp lệ", "Yêu cầu không hợp lệ"));
        await service.Update(id, accountModel);
        return Ok(new Root("Thành công", "Đăng nhập thành công."));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var model = await service.GetById(id);
        if (model == null) return BadRequest(new Root("Xóa không thành công", "Yêu cầu xóa không thành công"));

        await service.Delete(id);
        return Ok(new Root("Thành công", "Xóa thành công."));
    }
}