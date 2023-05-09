using ChallengeB3.Domain.Interfaces;
using ChallengeB3.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeB3.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegisterController : ControllerBase
{
	private readonly ILogger<RegisterController> _logger;
	private readonly IQueueProducer _queueProducer;
    public RegisterController(ILogger<RegisterController> logger, IQueueProducer queueProducer)
	{
		_logger = logger;
		_queueProducer = queueProducer;

    }

    [HttpDelete]
    public IActionResult DeleteRegister(int registerID)
    {
        try
        {
            //_queueProducer.PublishMessage(register);

            return Accepted();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{ex.Message}");
            return BadRequest(ex);
        }
    }

    [HttpPost]
    public IActionResult InsertRegister(Register register)
    {
		try
		{
			_queueProducer.PublishMessage(register);

			return Accepted(register);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, $"{ex.Message}");
			return BadRequest(ex);
		}
    }


    [HttpPut]
    public IActionResult UpdateRegister(Register register)
    {
        try
        {
            _queueProducer.PublishMessage(register);

            return Accepted(register);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{ex.Message}");
            return BadRequest(ex);
        }
    }

    [HttpGet]
	public IActionResult GetListRegister()
	{
		try
		{

			return Accepted(new List<Register> { });

		}
		catch (Exception ex)
		{
            _logger.LogError(ex, $"{ex.Message}");
            return BadRequest(ex);
        }
	}


    [HttpGet("{registerId}")]
    public IActionResult GetRegister(int registerId)
    {
        try
        {
            return Accepted(new List<Register> { });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{ex.Message}");
            return BadRequest(ex);
        }
    }


}
