using System;

namespace XPTO.Domain.FakeStore
{
    public class FakeStoreProduct
    {
        public string Title { get; set; } 
        public decimal Price { get; set; } 
        public string Description { get; set; } = String.Empty;
        public string Image { get; set; } 
        public string Category { get; set; } = "Default";

        public FakeStoreProduct(Product product) {
            this.Title = product.Title;
            this.Price = product.Price;
            this.Description = product.BarCode;

        }

        public FakeStoreProduct(){
            
        }
    }
}