using System.Text.Json.Serialization;

namespace TraineeFrontend.Model
{
    public record class Trainee
    (
         [property: JsonPropertyName("id")] int Id,
         [property: JsonPropertyName("name")] string Name,
         [property: JsonPropertyName("email")] string Email,
         [property: JsonPropertyName("address")] string Address,
         [property: JsonPropertyName("phone")] string Phone
        );
    
   

}
