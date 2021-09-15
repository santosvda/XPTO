using System.ComponentModel.DataAnnotations;

namespace XPTO.API.Dtos
{
    public class ProductDto
    {
        public int Id { get; private set; }
        [Required(ErrorMessage = "Please, type a correct Title")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "The Title must contain a minimum of 5 and a maximum of 200 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please, type a correct BarCode")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "The BarCode must contain a minimum of 5 and a maximum of 200 characters")]
        public string BarCode { get; set; }
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Invalid Target Price; Maximum Two Decimal Points.")]
         [Range(0, 99999999.99, ErrorMessage = "Invalid Target Price; Max 10 digits")]
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}