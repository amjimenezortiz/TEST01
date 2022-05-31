using KUNAK.VMS.CORE.CustomEntities;

namespace KUNAK.VMS.API.Responses
{
    public class ApiResponse<T>
    {
        public ApiResponse(T data)
        {
            Data = data;
        }
        public T Data { get; set; }

        public Pagination Pagination { get; set; }
    }
}
