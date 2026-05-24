using Microsoft.AspNetCore.Mvc;

namespace CursoApis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];
        private static WeatherForecast[] ListWeatherForecast;
        public WeatherForecastController() {
            ListWeatherForecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
           .ToArray();

        }
        /// <summary>
        /// Return all weather forecasts
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return ListWeatherForecast;
        }
        /// <summary>
        /// Retrun weathjer by position
        /// </summary>
        /// <param name="id">position</param>
        /// <returns></returns>
        [HttpGet()]
        [Route("{id}")]
        public ActionResult<WeatherForecast> GetByPosition(int id)
        {
            if (id > 5 || id < 0)
            {
                return BadRequest();
            }
            return Ok(ListWeatherForecast[id]);
        }
        //[HttpPost]
        //public async Task<ActionResult> PostAsync([FromBody] WeatherForecast weatherForecast)
        //{
        //    if (weatherForecast != null)
        //    {
        //        await ListWeatherForecast.AddAsync(weatherForecast);
        //    }
        //    return BadRequest();
        //}

    }
}
