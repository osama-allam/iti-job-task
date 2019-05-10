using System.ComponentModel.DataAnnotations;

namespace ProductAPICore.API.ViewModels
{
    /// <summary>
    /// A product view-model for update action with Name, ImageUrl, Price and CompanyId 
    /// </summary>
    public class UpdateProductViewModel
    {
        /// <summary>
        /// Product Name
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// Product ImageUrl
        /// </summary>

        [Required]
        [MaxLength(1000)]
        public string ImageUrl { get; set; }
        /// <summary>
        /// Product Price
        /// </summary>
        [Required]
        public double Price { get; set; }
        /// <summary>
        /// Product producer Company Id
        /// </summary>
        [Required]
        public int CompanyId { get; set; }

    }
}