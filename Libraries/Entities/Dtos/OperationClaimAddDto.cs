using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class OperationClaimAddDto : IDto
    {
        public string Name { get; set; }
        public bool IsDefault { get; set; }
    }
}
