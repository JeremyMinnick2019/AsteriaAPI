using Asteria.Classes;
using AsteriaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace AsteriaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public APOD GetPictureOfTheDay()
        {
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.Development.json");

            var config = configuration.Build();

            string APIKey = config.GetValue<string>("APIKey");

            string URL = $"https://api.nasa.gov/planetary/apod?api_key={APIKey}";

            HttpClient Client = new HttpClient();

            HttpResponseMessage Resp = Client.GetAsync(URL).Result;

            string Obj = Resp.Content.ReadAsStringAsync().Result;

            JObject PicOfDayObj = JObject.Parse(Obj);

            bool test = string.IsNullOrWhiteSpace((string?)PicOfDayObj["hdurl"]);

            APOD APODInfo = new APOD()
            {
                CopyRight = (string?)PicOfDayObj["copyright"],
                Date = (string?)PicOfDayObj["date"] ?? "",
                Explanation = (string?)PicOfDayObj["explanation"],
                Url = !string.IsNullOrWhiteSpace((string?)PicOfDayObj["hdurl"]) ? PhotoLinkToByteArray((string?)PicOfDayObj["hdurl"]) : PhotoLinkToByteArray((string?)PicOfDayObj["url"]),
                MediaType = (string?)PicOfDayObj["media_type"],
                Title = (string?)PicOfDayObj["title"],
            };

            return APODInfo;
        }

        private byte[]? PhotoLinkToByteArray(string? Link)
        {
            if (string.IsNullOrWhiteSpace(Link))
                return null;

            HttpClient Client = new HttpClient();

            return Client.GetByteArrayAsync(Link).Result;
        }
    }
}
