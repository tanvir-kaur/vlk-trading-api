using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VLK.Trading.Application.Interfaces;
using VLKModel = VLK.Trading.API.Models;

namespace VLK.Trading.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        /// <summary>
        /// Validates user with email and password
        /// </summary>
        /// <remarks>
        /// This API endpoint authenticates the user
        /// </remarks>
        /// <param name="loginModel"></param>
        /// <returns>Token valid for 1 hour</returns>
        /// <response code="200">Returns token which can be used to access other APIs</response>
        /// <response code="400">If user or password is invalid</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] VLKModel.LoginRequest loginModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Messages.LoginRequired);
                }
                string response = await _accountService.LoginAsync(loginModel.Email, loginModel.Password);
                if (string.IsNullOrEmpty(response))
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(new { token = response });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
