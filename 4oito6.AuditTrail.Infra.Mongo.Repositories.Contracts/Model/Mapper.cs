namespace _4oito6.AuditTrail.Infra.Mongo.Repositories.Contracts.Model
{
    public static class Mapper
    {
        public static AuditTrailDto ToAuditTrailDto(this Domain.Entities.AuditTrail bo)
            => new AuditTrailDto
            {
                Id = bo.Id,
                Date = bo.Date,
                Message = bo.Message,
                StackTrace = bo.StackTrace
            };

        public static Domain.Entities.AuditTrail ToAuditTrail(this AuditTrailDto dto)
            => new Domain.Entities.AuditTrail
            (
                id: dto.Id,
                date: dto.Date,

                message: dto.Message,
                stackTrace: dto.StackTrace
            );
    }
}