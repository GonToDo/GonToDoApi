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
        return Ok(new Result("Success","Lấy toàn thông tin thành công.", new {accountModels}));
    }    
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var accountModel = await service.GetById(id);
        return Ok(new Result("Success","Lấy thông tin thành công.", new {accountModel}));
    }

    [HttpPost]
    public async Task<IActionResult> Register(AccountModel.RegisterModel registerModel)
    {
        if (await service.CheckIfNameExists(registerModel.UserName))
        {
            var accountModel = new AccountModel
            {
                FullName = registerModel.FullName,
                UserName = registerModel.UserName,
                Password = registerModel.Password
            };

            await service.Create(accountModel);
            return Ok(new Result("Success","Tạo thành công.", new {accountModel}));
        }
        return BadRequest(new Result("Error","Tên đăng nhập đã tồn tại."));
    }

    [HttpPost]
    public async Task<IActionResult> Login(AccountModel.LoginModel loginModel)
    {
        var accountModel = await service.FindByName(loginModel.UserName);
        if (await service.CheckPassword(accountModel, loginModel.Password))
            return Ok(new Result("Success","Đăng nhập thành công.", new {accountModel}));
        return BadRequest(new Result("Error","Tên đăng nhập hoặc mật khẩu không đúng."));
    }
}