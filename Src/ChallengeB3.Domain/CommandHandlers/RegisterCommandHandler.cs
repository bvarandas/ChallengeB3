using ChallengeB3.Domain.Bus;
using ChallengeB3.Domain.Commands;
using ChallengeB3.Domain.Events;
using ChallengeB3.Domain.Interfaces;
using ChallengeB3.Domain.Models;
using ChallengeB3.Domain.Notifications;
using MediatR;

namespace ChallengeB3.Domain.CommandHandlers;

public class RegisterCommandHandler : CommandHandler,
    IRequestHandler<InsertRegisterCommand, bool>,
    IRequestHandler<UpdateRegisterCommand, bool>,
    IRequestHandler<RemoveRegisterCommand, bool>
{
    private readonly IRegisterRepository _registerRepository;
    private readonly IMediatorHandler _bus;

    public RegisterCommandHandler(IRegisterRepository registerRepository,
        IUnitOfWork uow,
        IMediatorHandler bus,
        INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
        _registerRepository = registerRepository;
        _bus = bus;
    }

    public async Task<bool> Handle(InsertRegisterCommand command, CancellationToken cancellationToken)
    {
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }
        var register = new Register(command.Description, command.Status, command.Date, command.Action);

        await _registerRepository.AddRegisterAsync(register);

        if (Commit())
        {
            await _bus.RaiseEvent(new RegisterInsertedEvent(register.RegisterId, register.Description, register.Status, register.Date , register.Action));
        }

        return await Task.FromResult(true);
    }

    public async Task<bool> Handle(UpdateRegisterCommand command, CancellationToken cancellationToken)
    {
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return await Task.FromResult(false);
        }
        var register = new Register(command.RegisterId, command.Description, command.Status, command.Date, command.Action);

        await _registerRepository.UpdateRegisterAsync(register);

        if (Commit())
        {
            await _bus.RaiseEvent(new RegisterUpdatedEvent(register.RegisterId, register.Description, register.Status, register.Date));
        }

        return await Task.FromResult(true);
    }

    public Task<bool> Handle(RemoveRegisterCommand command, CancellationToken cancellationToken)
    {
        if (!command.IsValid())
        {
            NotifyValidationErrors(command);
            return Task.FromResult(false);
        }

        _registerRepository.DeleteRegisterAsync(command.RegisterId);

        if (Commit())
        {
            _bus.RaiseEvent(new RegisterRemovedEvent(command.RegisterId));
        }

        return Task.FromResult(true);
    }
    public void Dispose()
    {
        _registerRepository.Dispose();
    }
}
