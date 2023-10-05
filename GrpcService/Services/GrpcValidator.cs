namespace GrpcService.Services
{
    public class GrpcValidator
    {
        public class ValidationResult
        {
            public bool WasSuccessful { get; set; }
            public string? Message { get; set; }
        }

        public static ValidationResult CheckIfGradeIsValid(float grade)
        {
            var validGrades = new float[] { 2f, 3f, 3.5f, 4f, 4.5f, 5f, 5.5f };
            if (validGrades.Contains(grade))
                return new ValidationResult { WasSuccessful = true };
            else
                return new ValidationResult { WasSuccessful = false, Message = "A grade can only be one of these: 2; 3; 3,5; 4; 4,5; 5; 5,5" };
        }
    }
}
