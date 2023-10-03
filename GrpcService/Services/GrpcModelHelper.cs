using Grpc.Core;
using Microsoft.IdentityModel.Tokens;

namespace GrpcService.Services
{
    public class GrpcModelHelper
    {
        public static void VerifyStringsNullabilityRequirements(List<String> strings)
        {
            if (CheckIfAnyIsNullOrEmpty(strings))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Nulls or empty strings were passed for non-nullable properties"));
            }
        }

        public static bool CheckIfAnyIsNullOrEmpty(List<String> strings)
        {
            return strings.Any(s => s.IsNullOrEmpty());
        }

        public static void VerifyGuidsValidity(List<String> strings)
        {
            if (!CheckIfAllStringAreGuid(strings))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Guids were expected for UUID properties"));
            }
        }

        public static bool CheckIfAllStringAreGuid(List<String> strings)
        {
            return strings.All(s => Guid.TryParse(s, out Guid _));
        }

        public static void VerifyGradeValidity(float grade)
        {
            if (!CheckIfGradeIsValid(grade))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"{grade} is not a valid grade"));
            }
        }

        public static bool CheckIfGradeIsValid(float grade)
        {
            var validGrades = new float[] { 2f, 3f, 3.5f, 4f, 4.5f, 5f, 5.5f };
            return validGrades.Contains(grade);
        }
    }
}
