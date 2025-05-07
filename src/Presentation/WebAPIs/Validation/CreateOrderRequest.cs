using System.ComponentModel.DataAnnotations;

namespace OrderProcessing.Presentation.WebAPIs.Validation
{
    public class CreateOrderRequest : IValidatableObject
    {
        [Required]
        [MaxLength(300, ErrorMessage = "CustomerId must not exceed 300 characters.")]
        public required string CustomerId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one item is required.")]
        [MaxLength(2000, ErrorMessage = "The number of items must not exceed 2000.")]
        public required List<OrderRequestItem> Items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Items.GroupBy(item => item.ProductId).Any(group => group.Count() > 1))
            {
                yield return new ValidationResult("Duplicate ProductId found in items.", new[] { nameof(Items) });
            }
        }
    }

    public class OrderRequestItem
    {
        [Required]
        public Guid ProductId { get; set; }

        [Range(1, 10000, ErrorMessage = "Quantity must be between 1 and 10000.")]
        public int Quantity { get; set; }
    }
}
