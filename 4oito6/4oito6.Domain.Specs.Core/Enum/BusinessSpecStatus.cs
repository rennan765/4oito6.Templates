namespace _4oito6.Domain.Specs.Core.Enum
{
    public enum BusinessSpecStatus
    {
        Success = 200,
        Created = 201,

        Accepted = 202,
        NoContent = 204,

        InvalidInputs = 400,
        Unauthorized = 401,
        PaymentRequired = 402,

        Forbidden = 403,
        ResourceNotFound = 404,

        Conflict = 409,
        Locked = 423,

        InternalError = 500,
        Badgateway = 502,
        ServiceUnavailable = 503
    }
}