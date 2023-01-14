using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Repositories;

namespace AgendaNet.Infra.Repositories
{
    public class Repository : IRepository
    {
        public IGenericRepository<Contact> Contacts { get; }

        public Repository(IGenericRepository<Contact> contacts)
        {
            Contacts = contacts;
        }
    }
}