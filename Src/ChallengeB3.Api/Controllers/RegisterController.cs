using ChallengeB3.Domain.Interfaces;
using ChallengeB3.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System;

namespace ChallengeB3.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegisterController : ControllerBase
{
	private readonly ILogger<RegisterController> _logger;
	private readonly IQueueProducer _queueProducer;
    private readonly IQueueConsumer _queueConsumer;

    public RegisterController(ILogger<RegisterController> logger, 
        IQueueProducer queueProducer,
        IQueueConsumer queueConsumer        )
	{
		_logger = logger;
		_queueProducer = queueProducer;
        _queueConsumer = queueConsumer;
        //_registerService = registerService;
        _queueConsumer.ExecuteAsync();
    }

    [HttpDelete("{registerId}")]
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
            var register = new Register() { Action = "getall" };
            _queueProducer.PublishMessage(register);
            
			return Ok();
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
            var register = new Register() { Action = "get", RegisterId=registerId };
            //_queueProducer.PublishMessage(register);
            //_registerService.GetRegisterByIDAsync(registerId);

            return Ok(_queueConsumer.RegisterGetById(registerId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{ex.Message}");
            return BadRequest(ex);
        }
    }
}