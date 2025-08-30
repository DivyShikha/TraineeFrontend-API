using System.Text.Json.Serialization;

namespace TraineeFrontend.Model
{
    public record class Course
    (
         [property: JsonPropertyName("courseId")] int CourseID,
         [property: JsonPropertyName("title")] string Title ,
         [property: JsonPropertyName("credits")] int Credits
        );



}
