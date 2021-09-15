using System;

namespace XPTO.Domain
{
    public class BaseEntity
    {
        public bool Active { get; set; }
        public DateTime DateInsert { get; set; }
        public DateTime? DateDeactivated { get; set; }
    }
}