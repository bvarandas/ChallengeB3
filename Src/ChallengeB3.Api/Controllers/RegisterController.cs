using ChallengeB3.Domain.Interfaces;
using ChallengeB3.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ChallengeB3.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegisterController : ControllerBase
{
	private readonly ILogger<RegisterController> _logger;
	private readonly IQueueProducer _queueProducer;
    private readonly IRegisterService _registerService;
    public RegisterController(ILogger<RegisterController> logger, IQueueProducer queueProducer, IRegisterService registerService)
	{
		_logger = logger;
		_queueProducer = queueProducer;
        _registerService = registerService;
    }

    [HttpDelete]
    public IActionResult DeleteRegister(int registerId)
    {
        try
        {
            var register = new Register(registerId, string.Empty, null, DateTime.MinValue, "remove");
            _queueProducer.PublishMessage(register);

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
            register.Action = "insert";
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
            register.Action = "update";
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
            var listRegister = _registerService.GetListAllAsync();
			return Ok(listRegister);
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
            var register = _registerService.GetRegisterByIDAsync(registerId);
            return Ok(register);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{ex.Message}");
            return BadRequest(ex);
        }
    }
}