using AutoMapper;
using ChallengeB3.Application.EventSourceNormalizes;
using ChallengeB3.Application.ViewModel;
using ChallengeB3.Domain.Bus;
using ChallengeB3.Domain.Commands;
using ChallengeB3.Domain.Interfaces;
using ChallengeB3.Domain.Models;
using ChallengeB3.Infra.Data.Repository.EventSourcing;

namespace ChallengeB3.Application.Services;

public  class RegisterService : IRegisterService
{
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _bus;
    private readonly IRegisterRepository _registerRepository;
    private readonly IEventStoreRepository _eventStoreRepository;
    public RegisterService(
        IMapper mapper, 
        IRegisterRepository registerRepository, 
        IMediatorHandler bus, 
        IEventStoreRepository eventStoreRepository)
    {
        _mapper = mapper;
        _registerRepository = registerRepository;
        _eventStoreRepository = eventStoreRepository;
        _bus = bus;
    }
    public async Task<IEnumerable<Register>> GetListAllAsync()
    {
        return await _registerRepository.GetAllRegisterAsync();
    }

    public async Task<Register> GetRegisterByIDAsync(int registerId)
    {
        return await _registerRepository.GetRegisterByIDAsync(registerId);
    }

    public async Task<RegisterCommand> AddRegisterAsync(RegisterViewModel register)
    {
        var addCommand = _mapper.Map<InsertRegisterCommand>(register);
        await _bus.SendCommand(addCommand);

        return addCommand;
    }

    public async Task<RegisterCommand> UpdateRegister(RegisterViewModel register)
    {
        var updateCommand =  _mapper.Map<UpdateRegisterCommand>(register);
        await _bus.SendCommand(updateCommand);

        return updateCommand;
    }

    public async void RemoveRegister(int registerId)
    {
        var deleteCommand = new RemoveRegisterCommand(registerId);
        await _bus.SendCommand(deleteCommand);
    }

    public IList<RegisterHistoryData> GetAllHistory(int registerId)
    {
        return RegisterHistory.ToJavaScriptRegisterHistory(_eventStoreRepository.All(registerId));
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}