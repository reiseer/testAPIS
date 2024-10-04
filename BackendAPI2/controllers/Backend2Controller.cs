using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI2.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Backend2Controller : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await Task.Delay(3000); // Retardo de 3 segundos
            return Ok("Response from Backend API 2");
        }

        [HttpPost]
        public IActionResult Post([FromBody] object data)
        {
            return Ok(new { message = "Data received by Backend API 2", data });
        }
    }
}