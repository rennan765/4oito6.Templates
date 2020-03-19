namespace _4oito6.Domain.Application.Core.Contracts.Arguments
{
    public class ResponseMessage<TResponse>
        where TResponse : class
    {
        public TResponse Data { get; set; }
        public string Message { get; set; }
        public string[] Errors { get; set; }
        public int? TotalRows { get; set; }
        public int StatusCode { get; set; }

        public ResponseMessage()
        {
        }

        public ResponseMessage(TResponse data, string message, string[] errors, int? totalRows, int statusCode)
        {
            Data = data;
            Message = message;
            Errors = errors;
            TotalRows = totalRows;
            StatusCode = statusCode;
        }
    }
}