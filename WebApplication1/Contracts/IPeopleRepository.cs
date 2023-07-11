using ApiDtoLibrary.Person;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Services.People;

namespace WebApplication1.Contracts
{
    public interface IPeopleRepository
    {
        List<Person> GetAllPersonalData();
        Task DeleteAsync(Guid id);
    }
}
