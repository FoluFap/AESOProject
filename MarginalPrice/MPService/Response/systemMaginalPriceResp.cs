using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace MarginalPrice.MPService.Response
{






    public class systemMaginalPriceResp
    {
        public string timestamp { get; set; }
        public string responseCode { get; set; }
        [JsonProperty(PropertyName = "return")]
        public Return _return { get; set; }
    }

    public class Return
    {
        public Systemmarginalpricereport[] SystemMarginalPriceReport { get; set; }
    }

    public class Systemmarginalpricereport
    {
        public string begin_datetime_utc { get; set; }
        public string end_datetime_utc { get; set; }
        public string begin_datetime_mpt { get; set; }
        public string end_datetime_mpt { get; set; }
        public string system_marginal_price { get; set; }
        public string volume { get; set; }
    }






    //public class systemMaginalPriceResp
    //{
    //    public string timestamp { get; set; }
    //    public string responseCode { get; set; }
    //    //[JsonProperty(PropertyName = "System Marginal Price Report")]
    //    public Return SystemMarginalPriceReport { get; set; }
    //}


    //public class Return
    //{

    //    public SystemMarginalPriceReport[] _return { get; set; }
    //}



    //public class SystemMarginalPriceReport
    //{
    //    public string begin_datetime_utc { get; set; }
    //    public string end_datetime_utc { get; set; }
    //    public string begin_datetime_mpt { get; set; }
    //    public string end_datetime_mpt { get; set; }
    //    public string system_marginal_price { get; set; }
    //    public string volume { get; set; }
    //}






}
