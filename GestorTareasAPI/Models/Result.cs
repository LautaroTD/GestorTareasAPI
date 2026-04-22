namespace GestorTareasAPI.Models
{
    public class Result
    {
        public bool Success { get; set; }
        public string Error { get; set; }

        protected Result(bool success, string error)
        {
            Success = success;
            Error = error;
        }

        public static Result Ok() => new(true, "");
        public static Result Fail(string error) => new(false, error);
    }
}
