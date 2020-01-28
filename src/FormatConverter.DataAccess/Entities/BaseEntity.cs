using System;

namespace FormatConverter.DataAccess.Entities
{
    public class BaseEntity: IEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
            DateCreated = DateTime.Now;
        }
        
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}