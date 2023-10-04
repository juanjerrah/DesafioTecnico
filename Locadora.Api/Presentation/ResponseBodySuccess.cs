using System.Collections;
using System.Net;

namespace Locadora.Api.Presentation;

public class ResponseBodySuccess<T>
{
    public HttpStatusCode Status { get; set; }
    public int DataCount => GetDataCount(Data);
    public T Data { get; set; }

    public ResponseBodySuccess(T data, HttpStatusCode statusCode)
    {
        Data = data;
        Status = statusCode;
    }
    
    private int GetDataCount<TR>(TR data)
    {
        if (data is ICollection collection)
            return collection.Count;

        return data != null ? 1 : 0;
    }
}