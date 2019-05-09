namespace ProductAPICore.API.ViewModels
{
    public class GetProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}