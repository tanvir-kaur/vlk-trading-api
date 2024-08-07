using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VLK.Trading.API.Models;
using VLK.Trading.Application.DTOModels;
using VLK.Trading.Application.Exceptions;
using VLK.Trading.Application.Interfaces;

namespace VLK.Trading.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly ILogger<StocksController> _logger;
        private readonly IStocksService _stocksService;
        public StocksController(ILogger<StocksController> logger, IStocksService stocksService)
        {
            _logger = logger;
            _stocksService = stocksService;
        }

        /// <summary>
        /// Retrieves the list of all stocks
        /// </summary>
        /// <returns>List of all stocks</returns>
        /// <response code="200">List of all stocks</response>
        /// <response code="404">If no stock was found</response>
        /// <response code="500">If there is an internal server error.</response>
        [ProducesResponseType(typeof(List<StockDTO>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<StockDTO> stocksDetails = await _stocksService.GetAllStocksAsync();
                if (stocksDetails.Count == 0)
                {
                    return NotFound();
                }
                return Ok(stocksDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets stock details by Ticker Symbol
        /// </summary>
        /// <param name="tickerSymbol">Ticker Symbol</param>
        /// <returns>Requested stock details</returns>
        /// <response code="200">Details of the Stock</response>
        /// <response code="400">If ticker symbol is empty</response>
        /// <response code="404">If no stock was found</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpGet("{tickerSymbol}")]
        public async Task<IActionResult> GetByTickerSymbol(string tickerSymbol)
        {
            try
            {
                if (String.IsNullOrEmpty(tickerSymbol))
                {
                    return BadRequest();
                }
                var stockDetails = await _stocksService.GetStockDetailsByTickerSymbolAsync(tickerSymbol);
                if (stockDetails == null)
                {
                    return NotFound();
                }
                return Ok(stockDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Buys a specific stock of a particular quantity
        /// </summary>
        /// <param name="buyStockRequest">Buy stock Request</param>
        /// <returns>Success</returns>
        /// <response code="200">
        /// 1. isSuccess= true (If purchase of the stock was successfull)
        /// 2. isSuccess= false , message =  Error Message( If purchase of the stock was not successfull)
        /// </response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpPut]
        public async Task<IActionResult> BuyStock([FromBody] BuyStockRequest buyStockRequest)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity == null)
                {
                    return Unauthorized();
                }

                IEnumerable<Claim> claims = identity.Claims;
                string userId = claims.FirstOrDefault(x => x.Type == "userId").Value;
                await _stocksService.BuyStockAsync(Guid.Parse(userId), buyStockRequest.BuyingStockUnit, buyStockRequest.TickerSymbol);
                return Ok(new { isSuccess = true });
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex.Message);
                return Ok(new { isSuccess = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
