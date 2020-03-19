using _4oito6.Template.Domain.Model.Entities;
using System;
using System.Collections.Generic;

namespace _4oito6.Template.Infra.Data.Bus.Contracts.Interfaces
{
    public interface IPhoneBus : IDisposable
    {
        IList<Phone> GetByNumbers(IList<Tuple<string, string>> numbers);
    }
}