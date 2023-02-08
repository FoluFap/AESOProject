namespace MarginalPrice.MPService.Request
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string date1 { get; set; }
        public string date2 { get; set; }
        public string pricemarginalprice { get; set; }
        public string volumen { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}
