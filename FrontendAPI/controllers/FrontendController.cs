using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace FrontendAPI.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FrontendController : ControllerBase
    {
        private readonly HttpClient _httpClient;

    public FrontendController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet("invoke-apis")]
    public async Task<IActionResult> InvokeApis()
    {
        var api1Url = "http://localhost:5001/api/backend1"; // URL de BackendAPI1
        var api2Url = "http://localhost:5002/api/backend2"; // URL de BackendAPI2

        // Llamadas asincrónicas a las APIs backend
        var api1Task = _httpClient.GetStringAsync(api1Url);
        var api2Task = _httpClient.GetStringAsync(api2Url);

        // Espera a ambas respuestas de manera asincrónica
        await Task.WhenAll(api1Task, api2Task);

        return Ok(new
        {
            api1Response = api1Task.Result,
            api2Response = api2Task.Result
        });
    }

    [HttpPost("post-data")]
    public async Task<IActionResult> PostData([FromBody] object data)
    {
        var api1Url = "http://localhost:5001/api/backend1"; 
        var api2Url = "http://localhost:5002/api/backend2"; 

        // Enviar datos a las APIs backend
        var api1Task = _httpClient.PostAsJsonAsync(api1Url, data);
        var api2Task = _httpClient.PostAsJsonAsync(api2Url, data);

        await Task.WhenAll(api1Task, api2Task);

            return Ok(new { message = "Data posted to both APIs successfully" });
        }
    }
}