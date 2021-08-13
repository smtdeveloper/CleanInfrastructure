namespace Core.Utilities.Results.Concrete
{
    public class SuccessResult : Result
    {
        public SuccessResult() : base(true, string.Empty)
        {
        }
        public SuccessResult(string message) : base(true, message)
        {
        }
    }
}
