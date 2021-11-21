using System.ComponentModel;

namespace SinyaCrawler.Service.Model
{
    public class ApiProdsReqVo
    {
        /// <summary>
        /// 群組Id
        /// </summary>
        [Description("item[group_id]")]
        public string group_id { get; set; }

        [Description("item[title]")]
        public string title { get; set; }

        [Description("item[prod_sub_slave_id]")]
        public string prod_sub_slave_id { get; set; }

        [Description("item[link]")]
        public string link { get; set; }

        [Description("item[prods]")]
        public string prods { get; set; }

        [Description("item[sort]")]
        public string sort { get; set; }
    }
}
