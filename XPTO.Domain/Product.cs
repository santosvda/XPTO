using System;

namespace XPTO.Domain
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string BarCode { get; set; }
        public decimal Price { get; set; }
        public byte[] Image { get; set; }
        public Guid ImageId { get; set; }
    }
}