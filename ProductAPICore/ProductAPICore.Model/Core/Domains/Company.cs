using System.Collections.Generic;

namespace ProductAPICore.Model.Core.Domains
{
    public class Company
    {
        public Company()
        {
            Products = new HashSet<Product>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
