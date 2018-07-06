using System.Net;

namespace CookingApp.Models
{
    public class ResponseModel
    {
        public ResponseModel(string responseContent, HttpStatusCode statusCode, bool isSuccessStatusCode = false)
        {
            this.ResponseContent = responseContent;
            this.StatusCode = statusCode;
            this.IsSuccessStatusCode = isSuccessStatusCode;
        }

        public string ResponseContent { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccessStatusCode { get; set; }
    }
}
