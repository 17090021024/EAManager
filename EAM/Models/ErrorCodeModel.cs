
namespace EAManager.Models
{
    public class ErrorCodeModel
    {
        public string ErrorCode { get; set; }

        private static ErrorCodeModel ErrorModel = new ErrorCodeModel();
        public static ErrorCodeModel Error(string errorCode)
        {
            ErrorModel.ErrorCode = errorCode;
            return ErrorModel;
        }

    }

}