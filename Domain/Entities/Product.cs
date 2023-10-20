using ProductDomain;

namespace ProductDomain
{
    public class Product
    {
        public string productcode { get; set; }
        public string productname { get; set; }
        public float price { get; set; }
        public int minQuantity { get; set; }
        public float discRate { get; set; }
        public string ImageUrl { get; set; }
        public String Category { get; set; }

    }
}