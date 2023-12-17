using GonToDoApi.Core;
using GonToDoApi.Models;
using GonToDoApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GonToDoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(AccountService service) : ControllerBase
{
    // GET: api/Product
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var accounts = await service.GetAllAsync();
        return Ok(accounts);
    }
    
    // POST api/CategoryController
    [HttpPost]
    public async Task<IActionResult> Post(AccountModel model)
    {
        await service.Create(model);
        return Ok("created successfully");
    }
}