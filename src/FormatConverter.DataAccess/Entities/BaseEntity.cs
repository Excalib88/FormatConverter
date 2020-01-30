using System;

namespace FormatConverter.DataAccess.Entities
{
    public class BaseEntity: IEntity
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}