namespace Core.Utilities.Results.Concrete
{
    public class ErrorFileResult : FileResult
    {
        public ErrorFileResult() : base(false)
        {
        }
        public ErrorFileResult(string shortPath, string fullPath, string fileName) : base(false, shortPath, fullPath, fileName)
        {
        }
    }
}
