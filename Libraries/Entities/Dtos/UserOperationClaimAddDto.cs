using Core.Entities.Abstract;
using System;

namespace Entities.Dtos
{
    public class UserOperationClaimAddDto : IDto
    {
        public Guid UserId { get; set; }
        public Guid OperationClaimId { get; set; }
    }
}
