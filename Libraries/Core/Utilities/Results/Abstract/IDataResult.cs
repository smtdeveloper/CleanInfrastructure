namespace Core.Utilities.Results.Abstract
{
    public interface IDataResult<TData> : IResult
    {
        public TData Data { get; }
    }
}
