#nullable enable

using ApiDtoLibrary.Professors;

namespace ApiDtoLibrary.GraphQL.Professors
{
    public record UpdateProfessorInput(
        PutProfessor putProfessor
        );
}
