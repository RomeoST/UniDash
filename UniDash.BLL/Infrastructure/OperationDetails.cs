namespace UniDash.BLL.Infrastructure
{
    public class OperationDetails
    {
        public OperationDetails(bool success, string message, object obj = null)
        {
            Success = success;
            Message = message;
            Object = obj;
        }

        public bool Success { get; }
        public string Message { get; }
        public object Object { get; set; }
    }
}
