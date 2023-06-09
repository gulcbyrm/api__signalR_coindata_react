using Microsoft.AspNetCore.Mvc;
using Bogus;
using GetFromApiAddDB.Models;
using Microsoft.AspNetCore.SignalR;
using Web.Hubs;
using Bogus.DataSets;
using Api.Services;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CoinController : ControllerBase
    {
        private readonly CoinService coinService;

        public CoinController(CoinService coinService)
        {
            this.coinService = coinService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CurrencyResponse>> GetCoins()
        {
            return Ok(coinService.coins);
        }
    }
}
 
 