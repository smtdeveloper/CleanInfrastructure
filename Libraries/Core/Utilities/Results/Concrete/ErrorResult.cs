namespace Core.Utilities.Results.Concrete
{
    public class ErrorResult : Result
    {
        public ErrorResult() : base(false, string.Empty)
        {
        }
        public ErrorResult(string message) : base(false, message)
        {
        }
    }
}
