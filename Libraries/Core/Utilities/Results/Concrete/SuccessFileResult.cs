namespace Core.Utilities.Results.Concrete
{
    public class SuccessFileResult : FileResult
    {
        public SuccessFileResult() : base(true)
        {
        }
        public SuccessFileResult(string shortPath, string fullPath, string fileName) : base(true, shortPath, fullPath, fileName)
        {
        }
    }
}
