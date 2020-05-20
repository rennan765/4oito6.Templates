using _4oito6.Domain.Model.Core.Entities;
using _4oito6.Domain.Specs.Core.Enum;
using _4oito6.Domain.Specs.Core.Extensions;
using _4oito6.Domain.Specs.Core.Interfaces;
using _4oito6.Infra.CrossCutting.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace _4oito6.Domain.Specs.Core.Models
{
    public abstract class BusinessSpec : IBusinessSpec
    {
        protected EntityBase Entity { get; private set; }
        public IList<BusinessSpecMessage> Messages { get; private set; }

        public BusinessSpec(EntityBase entity) : this()
        {
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
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

        public HttpStatusCode GetStatusCode()
            => Messages.Select(m => m.Status).ToList().ToMajorStatus().ToHttpStatusCode();
    }
}