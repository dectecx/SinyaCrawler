namespace SinyaCrawler.Service.Model
{
    public class ShowOptionRespVo
    {
        public Showoption[] showOption { get; set; }
        public Brand[] brands { get; set; }
        public Prices prices { get; set; }
        public Banner[] banner { get; set; }
    }

    public class Prices
    {
        public string maxPrice { get; set; }
        public string minPrice { get; set; }
    }

    public class Showoption
    {
        public string group_id { get; set; }
        public string title { get; set; }
        public string prod_sub_slave_id { get; set; }
        public string link { get; set; }
        public string prods { get; set; }
        public string sort { get; set; }
    }

    public class Brand
    {
        public string prod_tag_webgroup_id { get; set; }
        public string prod_tag_id { get; set; }
        public string prod_tag_title { get; set; }
        public string sort { get; set; }
    }

    public class Banner
    {
        public string id { get; set; }
        public string image { get; set; }
        public string title { get; set; }
        public string link { get; set; }
    }
}
