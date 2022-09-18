using Microsoft.Extensions.Logging;
using YouPie.Application.Interfaces;
using YouPie.Core.Interfaces;
using YouPie.Core.Models;

namespace YouPie.Application.Services;

public class EventService : IEventService
{
    private readonly ILogger _logger;
    private readonly IRepository<Event> _repository;

    public EventService(ILogger logger, IRepository<Event> repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<IEnumerable<Event>> Get()
    {
        _logger.LogInformation("All Events have been brought {Time}", DateTime.Now);
        return await _repository.GetAllAsync();
    }

    public async Task<Event?> Get(string id)
    {
        var localEvent = await _repository.GetAsync(id);
        if (localEvent == null)
            _logger.LogWarning("Event has not been found {Time}", DateTime.Now);
        _logger.LogInformation("Event has been brought {Time}", DateTime.Now);
        return localEvent;
    }

    public async Task Post(Event newEvent)
    {
        await _repository.CreateAsync(newEvent);
        _logger.LogInformation("New Event has been created {Time}",DateTime.Now);
        
    }

    public async Task Post(IEnumerable<Event> newEvents)
    {
        await _repository.CreateManyAsync(newEvents);
        _logger.LogInformation("New Events have been created {Time}",DateTime.Now);
    }

    public async Task<bool> Put(string id, Event updatedEvent)
    {
        var localEvent = await _repository.GetAsync(id);
        if (localEvent is null)
        {
            _logger.LogWarning("Event could not been updated, no Event found {Time}",DateTime.Now);
            return false;
        }

        updatedEvent.Id = localEvent.Id;
        await _repository.ReplaceAsync(id, updatedEvent);
        _logger.LogInformation("Event has been updated {Time}",DateTime.Now);
        return true;
    }

    public async Task Delete()
    {
        await _repository.DeleteAllAsync();
        _logger.LogInformation("All Events have been deleted {Time}", DateTime.Now);
    }

    public async Task<bool> Delete(string id)
    {
        var localEvent = await _repository.GetAsync(id);
        if (localEvent is null)
        {
            _logger.LogWarning("Event could not been deleted, no Event found {Time}",DateTime.Now);
            return false;
        }

        await _repository.DeleteAsync(id);
        _logger.LogInformation("Event has been deleted {Time}", DateTime.Now);
        return true;
    }
}