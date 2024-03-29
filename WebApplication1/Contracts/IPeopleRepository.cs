﻿using ApiDtoLibrary.Notifications;
using ApiDtoLibrary.Person;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.Contracts
{
    public interface IPeopleRepository
    {
        Task<Person> GetPerson(Guid id);
        List<Person> GetAllPersonalData();
        Task DeleteAsync(Guid id);
        Task<IList<Notification>> GetNotifications(string recipientId);
    }
}
