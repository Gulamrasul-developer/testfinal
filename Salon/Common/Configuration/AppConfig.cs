namespace Salon.Common.Configuration
{
    public static class DBConfig
    {
        public static string Connection { get; set; }
    }
    public static class UserConfig
    {
        public static long UserId { get; set; }
        public static int GroupId { get; set; }
    }
    public static class FilePathConfig
    {
        public static string Customer { get; set; }
        public static string Supplier { get; set; }
        public static string DeliveryBoy { get; set; }
        public static string Product { get; set; }
    }
}
