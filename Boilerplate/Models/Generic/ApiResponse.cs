namespace Boilerplate.Models.Generic
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public object Result { get; set; }
        public Error Error { get; set; }
        public bool IsUnAuthorizedRequest { get; set; }
    }

    public class Error
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}