using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI1.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Backend1Controller : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await Task.Delay(1000); // Retardo de 1 segundo
            return Ok("Response from Backend API 1");
        }

        [HttpPost]
        public IActionResult Post([FromBody] object data)
        {
            return Ok(new { message = "Data received by Backend API 1", data });
        }
    }
}