namespace SchoolWebInternalAPI.API.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }

        public ApiResponse(T data, string? message = null)
        {
            Success = true;
            Data = data;
            Message = message;
        }

        public ApiResponse(string message, List<string>? errors = null)
        {
            Success = false;
            Message = message;
            Errors = errors;
        }

        public static ApiResponse<T> Ok(T data, string? message = null) =>
            new ApiResponse<T>(data, message);

        public static ApiResponse<T> Fail(string message, List<string>? errors = null) =>
            new ApiResponse<T>(message, errors);
    }
}
