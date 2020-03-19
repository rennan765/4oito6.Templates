using _4oito6.Domain.Model.Core.Entities;
using _4oito6.Domain.Specs.Core.Enum;
using _4oito6.Domain.Specs.Core.Extensions;
using _4oito6.Domain.Specs.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _4oito6.Domain.Specs.Core.Models
{
    public abstract class BusinessSpec<TEntity> : IBusinessSpec<TEntity>
        where TEntity : EntityBase
    {
        protected TEntity Entity { get; private set; }
        public IList<BusinessSpecMessage> Messages { get; private set; }

        public BusinessSpec(TEntity entity) : this()
        {
            Entity = entity ?? throw new ArgumentNullException(nameof(TEntity));
        }

        public BusinessSpec()
        {
            Messages = new List<BusinessSpecMessage>();
        }

        public void AddMessage(BusinessSpecStatus status, string message)
        {
            Messages.Add(new BusinessSpecMessage(status, message));
        }

        public virtual bool IsSatisfied()
        {
            if (Messages.Any(m => m.Status == BusinessSpecStatus.Success))
                return true;

            return !Messages.Any
                          (
                              m =>
                                  m.Status.FirstCodeNumber() == 4 ||
                                  m.Status.FirstCodeNumber() == 5
                          );
        }
    }
}