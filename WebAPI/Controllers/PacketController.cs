namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/packet")]
    public class PacketController : ControllerBase
    {
        private readonly IPacketService _packetService;

        public PacketController(IPacketService packetService)
        {
            _packetService = packetService;
        }

        [HttpPut("{id}/reserve")]
        public async Task<ActionResult> Reserve(int id)
        {
            try
            {
                var response = await _packetService.ReservePacketAsync(id, User.Identity?.Name!);

                if (response)
                {
                    var packet = await _packetService.GetPacketByIdAsync(id);

                    foreach (var product in packet.Products!)
                    {
                        product.Packets = null;
                        product.Picture = null;
                    }

                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Dit pakket is succesvol gereserveerd!",
                        Packet = packet
                    });
                }

                throw new Exception();
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = e.Message
                });
            }
        }
    }
}