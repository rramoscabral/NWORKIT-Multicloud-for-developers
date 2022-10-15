using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Net;
using System.IO;
using System.Drawing;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApp.Controllers;

public class FlightClientController : Controller
{
    private readonly ILogger<FlightClientController> _logger;

    protected readonly IConfiguration _configuration;
 
    public FlightClientController(ILogger<FlightClientController> logger, IConfiguration configuration )
    {
        _logger = logger;
        _configuration = configuration;
    }


    public async Task<IActionResult> Index()
    {
        var webAPIURI = _configuration.GetValue<string>("WebAPI:URI");
        var apiService = _configuration.GetValue<string>("WebAPI:Read");
        var clients = Array.Empty<FlightClient>();

        HttpClient client = new HttpClient {BaseAddress = new Uri(@webAPIURI)};
        var response = await client.GetAsync(apiService);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();

            clients = JsonConvert.DeserializeObject<FlightClient[]>(await response.Content.ReadAsStringAsync());

            //ViewData["Clients"] = clients;
        }
        return View(clients);
    }



    public async Task<ActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        var webAPIURI = _configuration.GetValue<string>("WebAPI:URI");
        var apiService = _configuration.GetValue<string>("WebAPI:Update");
        FlightClient flightClient = new FlightClient();

        HttpClient httpClient = new HttpClient {BaseAddress = new Uri(@webAPIURI)};
        var response = await httpClient.GetAsync(apiService + $"/{id}");

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();

            flightClient = JsonConvert.DeserializeObject<FlightClient>(await response.Content.ReadAsStringAsync());

        }

        return View(flightClient);
    }


    [HttpPost, ActionName("Edit")]
    public async Task<IActionResult> EditPost(int? id, [Bind("Id,Name,DataFormatString,BirthDate,PassaportNumber,Nationality,PhotoBase64")] FlightClient flightClient, IFormFile file)
    {
        if (id == null)
        {
            return NotFound();
        }


        //HttpPostedFileBase don't exist in .NET 6. Use IFormFile.
        if (file != null)
        {
            using (var memoryStream = new MemoryStream())
            {
            await file.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();
            string s = Convert.ToBase64String(fileBytes);
            flightClient.PhotoBase64 = $"data:image/png;base64,{s}";
            }
        }
       

        var webAPIURI = _configuration.GetValue<string>("WebAPI:URI");
        var apiService = _configuration.GetValue<string>("WebAPI:Update");
    

        //HttpWebRequest is obselete
        HttpClient httpClient = new HttpClient {BaseAddress = new Uri(@webAPIURI)};
        var response = await httpClient.PutAsJsonAsync<FlightClient>((apiService + $"/{id}"), flightClient );



        response.EnsureSuccessStatusCode();

        return RedirectToAction("Index");
     
    }
        
    
  



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
