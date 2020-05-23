using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _4oito6.AuditTrail.Tests.TestCases
{
    internal static class AuditTrailTestCases
    {
        internal static IList<AuditTrail.Domain.Entities.AuditTrail> GetAuditTrails(int quantity = 10, DateTime? date = null)
        {
            return new Faker<AuditTrail.Domain.Entities.AuditTrail>()
                .CustomInstantiator(f => new AuditTrail.Domain.Entities.AuditTrail
                (
                    id: new Random().Next().ToString(),
                    date: date ?? DateTime.UtcNow,

                    message: "message",
                    stackTrace: "stackTrace"
                ))
                .Generate(quantity)
                .ToList();
        }
    }
}