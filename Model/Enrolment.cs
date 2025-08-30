using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TraineeFrontend.Model;

namespace TraineeFrontend.Model
{
    public record class Enrolment
    (
        [property: JsonPropertyName("enrollmentID")] int EnrollmentID,   // matches backend EF model
        [property: JsonPropertyName("description")] string Description,
        [property: JsonPropertyName("courseID")] int CourseID,
        [property: JsonPropertyName("traineeID")] int TraineeID,

        // Navigation objects
        [property: JsonPropertyName("course")] Course? Course,
        [property: JsonPropertyName("trainee")] Trainee? Trainee

    );
}
