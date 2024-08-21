namespace eCommerce.Erros
{
    public class ApiResponse
    {
        public int _statusCode { get; set; }
        public  string _message { get; set; }


        public ApiResponse(int statusCode , string message = null)
        {
            this._statusCode = statusCode;
            this._message = message;
        }

        public string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "Errors are the path to the dark side. Errors lead to anger.  Anger leads to hate.  Hate leads to career change",
                _ => null
            };
        }


    }
}
