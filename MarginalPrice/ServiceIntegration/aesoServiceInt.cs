using MarginalPrice.MPService.Response;
using MarginalPrice.MPService.Response.CurrentPrice;
using Newtonsoft.Json;
using RestSharp;

namespace MarginalPrice.ServiceIntegration
{
    public class aesoServiceInt : IaesoServiceInt
    {
        private readonly IConfiguration _config;

        public aesoServiceInt(IConfiguration config)
        {
            _config = config;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public systemMaginalPriceResp GetSystemMaginalPrice(string startDate, string enddate)
        {
            //var fullUrl = $"{_config.GetValue<string>("AesoService:BaseURL")}/price/systemMarginalPrice?startDate=2018-01-01&endDate=2018-01-31");
            //var client = new RestSharp.RestClient(fullUrl);  
           
            //call the AESO service here to get the value

            var client = new RestClient($"https://api.aeso.ca/report/v1.1/price/systemMarginalPrice?startDate={startDate}&endDate={enddate}");
            var request = new RestRequest("", Method.Get);
            request.AddHeader("x-api-key", "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ4eGs3NTciLCJpYXQiOjE2NzU1NDQ2NTZ9.SZJK_BLNwWQK0jirDYN399W5V8D_FfiIhYmY8oomwco");
            var response = client.Execute<systemMaginalPriceResp>(request);
            //replace the json content to replace the system maginal name
            var myString = response.Content.Replace(@"System Marginal Price Report", "SystemMarginalPriceReport");
            var responseobj = JsonConvert.DeserializeObject<systemMaginalPriceResp>(myString);
            return responseobj;

            //RestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public systemMarginalCurrentPriceResp GetSystemMarginalCurrentPrice(string status)
        {
            //call the AESO here to get the latest price..
            var client = new RestClient("https://api.aeso.ca/report/v1.1/price/systemMarginalPrice/current");
            var request = new RestRequest("", Method.Get);
            request.AddHeader("x-api-key", "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ4eGs3NTciLCJpYXQiOjE2NzU1NDQ2NTZ9.SZJK_BLNwWQK0jirDYN399W5V8D_FfiIhYmY8oomwco");
            request.AddHeader("Cookie", "AESO-Cookie=!N1tVah23/A+gB9tCWk7oCU+boSuqT1BbSJaumLuacjGIp1TbwoBjRkzzKAedUNC1VhrMQYSTJ02skq8=");
            
            var response = client.Execute<systemMarginalCurrentPriceResp>(request);
            var myString = response.Content.Replace(@"System Marginal Price Report", "SystemMarginalPriceReport");
            var responseobj = JsonConvert.DeserializeObject<systemMarginalCurrentPriceResp>(myString);

            return responseobj;
            //RestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
        }
    }
}
