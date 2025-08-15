using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;


namespace BlurApi.Models
{
  namespace Requests
  {

    public class GenericResponse
    {
      [JsonPropertyName("status")]
      public int Status { get; set; } = 200;

      [JsonPropertyName("data")]
      public object Data { get; set; } = new Dictionary<string, object>();

      [JsonPropertyName("message")]
      public string Message { get; set; } = "Success";

      public static IActionResult JustData(object? data)
      {
        return new ObjectResult(new GenericResponse { Data = data ?? new Dictionary<string, object>() }) { StatusCode = 200 };
      }
    }

    public class HttpException
    {
      public static IActionResult With(int status, string detail)
      {
        var response = new
        {
          status,
          detail
        };
        return new ObjectResult(response) { StatusCode = status };
      }
    }
  }
}