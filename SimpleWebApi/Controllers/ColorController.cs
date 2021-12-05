using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace SimpleWebApi.Controllers
{
    [ApiController]    
    public class ColorController : ControllerBase
    {
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public async Task<string> GetRandomColor()
        {
            Random rnd = new Random();
            Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            var s = $"{randomColor.R.ToString().PadLeft(3, '0')}{randomColor.G.ToString().PadLeft(3, '0')}{randomColor.B.ToString().PadLeft(3, '0')}";
            await Task.Delay(1000);
            return s;
        }
    }
}
