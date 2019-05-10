using System.ComponentModel.DataAnnotations;

namespace ProductAPICore.Model.Helpers
{
    public class ProductsResourceParameters
    {

        private const int MaxPageSize = 20;
        /// <summary>
        /// The current page number
        /// </summary>
        [Required]
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        /// <summary>
        /// Number of products per page
        /// </summary>
        [Required]
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public string CompanyName { get; set; }
        public string SearchQuery { get; set; }
    }
}