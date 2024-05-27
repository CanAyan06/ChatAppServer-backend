using ChatAppServer.WebAPI.Context;
using ChatAppServer.WebAPI.Dtos;
using ChatAppServer.WebAPI.Models;
using GenericFileService.Files;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatAppServer.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController(ApplicationDbContext context) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Register (RegisterDto request, CancellationToken cancellationToken)
            {
            bool isNameExists = await context.Users.AnyAsync(p => p.Name == request.Name, cancellationToken);
            if (isNameExists) 
            { 
                return BadRequest(new {Message="Bu Kullanıcı adı daha önce kullanılmıştır"});

            }
            string avatar=FileService.FileSaveToServer(request.File,"wwwroot/avatar/");

            User user = new()
            {
                Name = request.Name,
                Avatar = avatar
            };
            await context.AddAsync(user,cancellationToken);
            await context.SaveChangesAsync();
            return NoContent();
            }
        [HttpPost]
        public async Task<IActionResult> Login(string name, CancellationToken cancellationToken)
        {

            User? user = await context.Users.FirstOrDefaultAsync(propa => propa.Name == name, cancellationToken);
            if (user == null)
            {
                return BadRequest(new { Message = "Kullanıcı Bulunamadı" });
                
            }
            user.Status="online";
            await context.SaveChangesAsync(cancellationToken);
            return Ok(user);
        }
    }
}
   
