using Core.Entities.Abstract;
using System;

namespace Core.Entities.Concrete
{
    public class OperationClaim : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
    }
}