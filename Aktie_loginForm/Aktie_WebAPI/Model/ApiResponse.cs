namespace Aktie_WebAPI.Model
{
    public class ApiResponse
    {
        public bool Success { get; }
        public string Message { get; }

        public ApiResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        // Hjælper metoder 
        public static ApiResponse Ok(string message) =>
            new ApiResponse(true, message);

        public static ApiResponse Fail(string message) =>
            new ApiResponse(false, message);
    }
}