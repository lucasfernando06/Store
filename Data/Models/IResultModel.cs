namespace Store.Data.Models
{
    public interface IResultModel
    {
        string Message { get; }
        object Data { get; }
    }   
}
