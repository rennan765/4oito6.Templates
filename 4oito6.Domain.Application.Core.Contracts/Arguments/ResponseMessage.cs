namespace _4oito6.Domain.Application.Core.Contracts.Arguments
{
    public class ResponseMessage
    {
        public string Message { get; set; }
        public string[] Errors { get; set; }
        public int? TotalRows { get; set; }
        public int StatusCode { get; set; }

        public ResponseMessage()
        {
        }

        public ResponseMessage(string message, int? totalRows, int statusCode)
        {
            Message = message;
            TotalRows = totalRows;
            StatusCode = statusCode;
        }

        public ResponseMessage(string message, string[] errors, int? totalRows, int statusCode)
        {
            Message = message;
            Errors = errors;
            TotalRows = totalRows;
            StatusCode = statusCode;
        }

        public ResponseMessage(string message, string[] errors, int statusCode)
        {
            Message = message;
            Errors = errors;
            StatusCode = statusCode;
        }

        public ResponseMessage(string message, int statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }
    }

    public class ResponseMessage<TResponse> : ResponseMessage
        where TResponse : class
    {
        public TResponse Data { get; set; }

        public ResponseMessage()
            : base()
        {
        }

        public ResponseMessage(TResponse data, string message, int? totalRows, int statusCode)
            : base(message, totalRows, statusCode)
        {
            Data = data;
        }

        public ResponseMessage(TResponse data, string message, string[] errors, int? totalRows, int statusCode)
            : base(message, errors, totalRows, statusCode)
        {
            Data = data;
        }

        public ResponseMessage(TResponse data, string message, string[] errors, int statusCode)
            : base(message, errors, statusCode)
        {
            Data = data;
        }

        public ResponseMessage(TResponse data, string message, int statusCode)
            : base(message, statusCode)
        {
            Data = data;
        }
    }
}