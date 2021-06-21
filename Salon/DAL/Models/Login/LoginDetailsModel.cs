namespace Salon.DAL.Models
{
    public class LoginDetailsModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public short GroupId { get; set; }
        public string GroupName { get; set; }
        public string OTP { get; set; }
        public string AccessToken { get; set; }
    }
}
