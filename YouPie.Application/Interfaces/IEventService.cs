using YouPie.Core.Models;

namespace YouPie.Application.Interfaces;

public interface IEventService
{
    Task<IEnumerable<Event>> Get();
    Task<Event?> Get(string id);
    Task Post(Event newEvent);
    Task Post(IEnumerable<Event> newEvents);
    Task<bool> Put(string id, Event updatedEvent);
    Task Delete();
    Task<bool> Delete(string id);
}