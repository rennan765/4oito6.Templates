using _4oito6.Domain.Model.Core.Entities;
using _4oito6.Domain.Specs.Core.Enum;
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

        public BusinessSpec(TEntity entity)
        {
            Entity = entity ?? throw new ArgumentNullException(nameof(TEntity));
            Messages = new List<BusinessSpecMessage>();
        }

        public void AddMessage(BusinessSpecStatus status, string message)
        {
            Messages.Add(new BusinessSpecMessage(status, message));
        }

        public virtual bool IsSatisfied()
            => !Messages.Any
               (
                   m =>
                       m.Status.ToString().Substring(1, 1) == "4" ||
                       m.Status.ToString().Substring(1, 1) == "5"
               );
    }
}