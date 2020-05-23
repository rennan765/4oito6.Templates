using _4oito6.AuditTrail.Domain.Services.Contract.Arguments;
using _4oito6.AuditTrail.Domain.Services.Implementation;
using _4oito6.AuditTrail.Infra.CrossCutting.Messages;
using _4oito6.AuditTrail.Infra.Data.Bus.Contracts;
using FluentAssertions;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Moq.AutoMock;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using static _4oito6.AuditTrail.Tests.TestCases.AuditTrailTestCases;

namespace _4oito6.AuditTrail.Tests.Domain.Services
{
    public class AuditTrailServiceTest
    {
        [Fact(DisplayName = "CreateAsync_ShouldReturnNull_DueToExceptionNull")]
        [Trait("CreateAsync", "AuditTrailService")]
        public async Task CreateAsync_ShouldReturnNull_DueToExceptionNull()
        {
            //Arrange
            var comparison = new CompareLogic();
            var mocker = new AutoMocker();
            var service = mocker.CreateInstance<AuditTrailService>();

            var date = DateTime.UtcNow.Date;
            var messages = new string[] { AuditTrailServiceSpecMessages.ExceptionNula };

            //Act
            await service.CreateAsync(null, date).ConfigureAwait(false);

            //Assert
            mocker.Verify();
            service.GetStatusCode().Should().BeEquivalentTo(HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "CreateAsync_ShouldExecuteSuccessfully")]
        [Trait("CreateAsync", "AuditTrailService")]
        public async Task CreateAsync_ShouldExecuteSuccessfully()
        {
            //Arrange
            var mocker = new AutoMocker();
            var service = mocker.CreateInstance<AuditTrailService>();

            var date = DateTime.UtcNow.Date;
            var exMessae = "message";
            var exStackTrace = "stack trace";

            mocker.GetMock<Exception>()
                .SetupGet(ex => ex.Message)
                .Returns(exMessae)
                .Verifiable();

            mocker.GetMock<Exception>()
                .SetupGet(ex => ex.StackTrace)
                .Returns(exStackTrace)
                .Verifiable();

            var at = new AuditTrail.Domain.Entities.AuditTrail
            (
                exception: mocker.GetMock<Exception>().Object,
                date: date
            );

            var testAt = new Func<AuditTrail.Domain.Entities.AuditTrail, bool>(a =>
            {
                return a.Date.Date == at.Date.Date &&
                    a.Message == at.Message &&
                    a.StackTrace == at.StackTrace;
            });

            mocker.GetMock<IAuditTrailBus>()
                .Setup(b => b.CreateAsync(It.Is<AuditTrail.Domain.Entities.AuditTrail>(a => testAt(a))))
                .Verifiable();

            //Act
            await service.CreateAsync(mocker.GetMock<Exception>().Object, date).ConfigureAwait(false);

            //Assert
            mocker.Verify();
            service.GetStatusCode().Should().BeEquivalentTo(HttpStatusCode.OK);
        }

        [Fact(DisplayName = "GetByDateAsync_ShouldExecuteSuccessfully")]
        [Trait("GetByDateAsync", "AuditTrailService")]
        public async Task GetByDateAsync_ShouldExecuteSuccessfully()
        {
            //Arrange
            var mocker = new AutoMocker();
            var service = mocker.CreateInstance<AuditTrailService>();

            var endDate = DateTime.UtcNow.Date;
            var startDate = endDate.Date.AddDays(-10);

            var date = endDate.Date.AddDays(-5);
            var ats = GetAuditTrails(date: date);

            mocker.GetMock<IAuditTrailBus>()
                .Setup(b => b.GetByDateAsync(startDate, endDate))
                .ReturnsAsync(ats)
                .Verifiable();

            var expectedResult = ats.Select(at => (AuditTrailResponse)at).ToList();

            //Act
            var result = await service.GetByDateAsync(startDate, endDate).ConfigureAwait(false);

            //Assert
            new CompareLogic().Compare(expectedResult, result).AreEqual.Should().BeTrue();
            mocker.Verify();
            service.GetStatusCode().Should().BeEquivalentTo(HttpStatusCode.OK);
        }
    }
}