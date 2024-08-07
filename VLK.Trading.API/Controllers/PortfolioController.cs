using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VLK.Trading.Application.DTOModels;
using VLK.Trading.Application.Interfaces;

namespace VLK.Trading.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly ILogger<PortfolioController> _logger;
        public readonly IPortfolioService _portfolioService;

        public PortfolioController(ILogger<PortfolioController> logger, IPortfolioService portfolioService)
        {
            _logger = logger;
            _portfolioService = portfolioService;
        }

        /// <summary>
        /// Retrieves the portfolio details of the logged in user
        /// </summary>
        /// <returns>Portfolio details of the logged in user</returns>
        /// <response code="200">Portfolio details of the user.</response>
        /// <response code="404">If the Portfolio details were not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        [ProducesResponseType(typeof(UserPortfolioDTO), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> Get()
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
                UserPortfolioDTO portfolioDetails = await _portfolioService.GetPortfolioByUserIdAsync(Guid.Parse(userId));
                if (portfolioDetails == null)
                {
                    return NotFound();
                }
                return Ok(portfolioDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
