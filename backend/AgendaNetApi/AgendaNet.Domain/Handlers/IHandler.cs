using AgendaNet.Domain.Commands;

namespace AgendaNet.Domain.Handlers
{
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}