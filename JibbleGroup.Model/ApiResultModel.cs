using System.Net;

namespace JibbleGroup.Model
{
    /// <summary>
    /// Api ResultViewModel with Generic Type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResultModel<T>
    {
        public HttpStatusCode Status { get; set; }


        public T Data { get; set; }
    }
}
