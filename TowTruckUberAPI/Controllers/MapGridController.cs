using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TowTruckUberAPI.Models;
using TowTruckUberAPI.Models.Dtos;

namespace TowTruckUberAPI.Controllers
{
    [Route("mapgrid")]
    [ApiController]
    public class MapGridController : Controller
    {
        private readonly AppDbContext _dbContext;

        public MapGridController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("{mapGridId}")]
        public IActionResult CreateMapGrid([FromRoute][Required] int mapGridId, [FromBody] MapGrid mapGrid)
        {
            throw new NotImplementedException();
        }


        [HttpGet]
        [Route("{map_gridId}")]
        public async Task<IActionResult> GetMapGridById([FromRoute][Required] int mapGridId)
        {
            MapGrid mapGridExists = await _dbContext.MapGrids.FindAsync(mapGridId);

            if (mapGridExists is null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Cannot find mapgrid with this Id." });

            return Ok(JsonSerializer.Serialize(mapGridExists));
        }
    }
}
