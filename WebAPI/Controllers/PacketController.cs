using Core.DomainServices.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class PacketController : ControllerBase
    {
        private readonly IPacketService _packetService;

        public PacketController(IPacketService packetService)
        {
            _packetService = packetService;
        }

        [HttpPut("{id}/reserve")]
        public async Task<IActionResult> Update(int id)
        {
            var reserve = await _packetService.ReservePacketAsync(id, null);

            if(reserve)
                return Ok(await _packetService.GetPacketByIdAsync(id));

            return StatusCode(409);
        }
    }
}