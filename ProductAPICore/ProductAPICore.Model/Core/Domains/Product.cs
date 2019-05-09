namespace ProductAPICore.Model.Core.Domains
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

    }
}
