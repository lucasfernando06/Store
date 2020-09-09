namespace Store.Data.Models
{
    public class ResultModel : IResultModel
    {
        public string Message { get; set; }
        public object Data { get; set; }

        public ResultModel(object data)
        {
            Message = "Sucesso";           
            Data = data;
        }

        public ResultModel(object data, string message)
        {
            Message = message;
            Data = data;
        }
    }
}
