namespace SinyaCrawler.Service.Model
{
    public class ApiProdsRespVo
    {
        /// <summary>
        /// 
        /// </summary>
        public string _event { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Addprod[] addProds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string barcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string category { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string discount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object[] ec_prodFree { get; set; }

        /// <summary>
        /// 商品價格 - 字串
        /// </summary>
        public string price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object[] prodFree { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        public string prod_id { get; set; }

        /// <summary>
        /// 商品縮圖
        /// </summary>
        public string prod_img { get; set; }

        /// <summary>
        /// 商品名稱
        /// </summary>
        public string prod_name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 商品價格
        /// </summary>
        public int sortPrice { get; set; }

        /// <summary>
        /// 庫存說明
        /// "":有貨
        /// "【補貨中】":缺貨
        /// </summary>
        public string stockText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string stocks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string urls { get; set; }
    }

    public class Addprod
    {

        /// <summary>
        /// 
        /// </summary>
        public string old { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string prod_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string prod_name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string s_img { get; set; }
    }

}
