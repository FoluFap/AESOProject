using Newtonsoft.Json;

namespace MarginalPrice.MPService.Response.CurrentPrice
{

    public class systemMarginalCurrentPriceResp
    {
        public string timestamp { get; set; }
        public string responseCode { get; set; }
        [JsonProperty(PropertyName = "return")]
        public Return _return { get; set; }
    
    }

    public class Return
    {
        public SystemMarginalPriceReport[] SystemMarginalPriceReport { get; set; }
    }

    public class SystemMarginalPriceReport
    {
        public string begin_datetime_utc { get; set; }
        public string begin_datetime_mpt { get; set; }
        public string system_marginal_price { get; set; }
        public string volume { get; set; }
    }


}
