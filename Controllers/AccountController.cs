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
        return Ok(accountModels);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var accountModel = await service.GetById(id);
        return Ok(accountModel);
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
            return Ok(accountModel);
        }

        return Conflict("Tên đăng nhập đã tồn tại.");
    }

    [HttpPost]
    public async Task<IActionResult> Login(string userName, string password)
    {
        var accountModel = await service.FindByName(userName);
        if (await AccountService.CheckPassword(accountModel, password))
            return Ok(accountModel);
        return Unauthorized("Tên đăng nhập hoặc mật khẩu không đúng.");
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] AccountModel accountModel)
    {
        var model = await service.GetById(id);

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (model == null) return BadRequest("Yêu cầu không hợp lệ");
        await service.Update(id, accountModel);
        return Ok("Đăng nhập thành công.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var model = await service.GetById(id);
        if (model == null) return BadRequest("Yêu cầu xóa không thành công");

        await service.Delete(id);
        return Ok("Xóa thành công.");
    }
}