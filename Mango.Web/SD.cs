namespace Mango.Web
{
    public static class SD
    {
        public static string ProductAPIBase { get; set; }
        public static string ShoppingCartApiBase { get; set; }
        public static string CouponApiBase { get; set; }

        public enum APIType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
