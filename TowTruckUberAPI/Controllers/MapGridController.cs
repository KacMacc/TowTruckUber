using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TowTruckUberAPI.Models;

namespace TowTruckUberAPI.Controllers
{
    [Route("mapgrid")]
    [ApiController]
    public class MapGridController : Controller
    {

        [HttpPost]
        [Route("{mapGridId}")]
        public IActionResult CreateMapGrid([FromRoute][Required] int mapGridId, [FromBody] MapGrid mapGrid)
        {
            throw new NotImplementedException();
        }


        [HttpGet]
        [Route("{map_gridId}")]
        public IActionResult GetMapGridById([FromRoute][Required] int mapGridId)
        {
            throw new NotImplementedException();
        }
    }
}
