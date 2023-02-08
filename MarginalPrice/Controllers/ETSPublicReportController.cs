using MarginalPrice.Helper;
using MarginalPrice.MPService;
using MarginalPrice.MPService.Request;
using MarginalPrice.ServiceIntegration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarginalPrice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ETSPublicReportController : ControllerBase
    {
        private readonly IaesoServiceInt _iaesoService;
        private readonly ILogger<ETSPublicReportController> _logger;
        private readonly IMailService _mailService;

        public ETSPublicReportController(IaesoServiceInt iaesoService, ILogger<ETSPublicReportController> logger, IMailService mailService)
        {
            _iaesoService = iaesoService;
            _logger = logger;
            _mailService = mailService;
        }

        [HttpGet]
        public IActionResult GetSystemMaginalPrice(string startDate, string endDate)
        {
            var statusResp = new genericResponse();
            try
            {
                //do the validation of the inputted values, to ensure its not empty
                if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
                {
                    return BadRequest();
                }

                var maginalPriceReport = _iaesoService.GetSystemMaginalPrice(startDate, endDate)._return.SystemMarginalPriceReport.ToList();

                
                //call a method to generate the list of marginal report to CSV
                //var respfromCSVgenerator = 
                bool statusOfCSVgeneration = new SaveToCSV().SaveToCsv(maginalPriceReport, "C:/MarginalReport/marginalreport.csv");

                if (statusOfCSVgeneration)
                {
                    statusResp = new genericResponse()
                    {
                        responseCode = "00",
                        responseMessage = "Marginal Report Generated Successfully"
                    };

                    return Ok(statusResp);
                }
                statusResp = new genericResponse()
                {
                    responseCode = "99",
                    responseMessage = "Marginal Report generation Failed "
                };

                return Ok(statusResp);

            }
            catch (Exception)
            {

                statusResp = new genericResponse()
                {
                    responseCode = "01",
                    responseMessage = "Error occur at the Point of Callling AESO Service"
                };

                return BadRequest(statusResp);

            }


        }


      
        
        [HttpGet("{currentStatus}", Name = "GetLatestMaginalPrice")]
        public IActionResult GetLatestMaginalPrice(string currentStatus)
        {
            var statusResp = new genericResponse();
            try
            {
                var latestmaginalPriceReport = _iaesoService.GetSystemMarginalCurrentPrice(currentStatus)._return.SystemMarginalPriceReport.ToList();

                
                //call a method to generate the list of marginal report to CSV
                //var respfromCSVgenerator = 
                bool statusOfCSVgeneration = new SaveToCSV().SaveToCsv(latestmaginalPriceReport, "C:/MarginalReport/Latestmarginalreport.csv");

                if (statusOfCSVgeneration)
                {
                    statusResp = new genericResponse()
                    {
                        responseCode = "00",
                        responseMessage = "Marginal Report Generated Successfully"
                    };

                    return Ok(statusResp);
                }
                statusResp = new genericResponse()
                {
                    responseCode = "99",
                    responseMessage = "Marginal Report generation Failed "
                };

                return Ok(statusResp);

            }
            catch (Exception)
            {

                statusResp = new genericResponse()
                {
                    responseCode = "01",
                    responseMessage = "Error occur at the Point of Callling AESO Service"
                };

                return BadRequest(statusResp);
            }

           


        }

        [HttpPost]
        public async Task<IActionResult> GetLatestMaginalPriceAndSendMailAsync(string currentStatus, string email)
        {

            var statusResp = new genericResponse();

            try
            {
                var latestmaginalPriceReport = _iaesoService.GetSystemMarginalCurrentPrice(currentStatus)._return.SystemMarginalPriceReport.ToList();

               
                //call a method to generate the list of marginal report to CSV
                //var respfromCSVgenerator = 
                //Get the details of the current price and detail and send it to the email provided
                var reqbodyforemail = new MailRequest()
                {
                    ToEmail = email,
                    Subject = "Alert for Latest Price",
                    Attachments = null,
                    date1 = latestmaginalPriceReport[0].begin_datetime_utc,
                    date2 = latestmaginalPriceReport[0].begin_datetime_mpt,
                    pricemarginalprice = latestmaginalPriceReport[0].system_marginal_price,
                    volumen = latestmaginalPriceReport[0].volume


                };

                await _mailService.SendEmailAsync(reqbodyforemail);

                bool statusOfCSVgeneration = new SaveToCSV().SaveToCsv(latestmaginalPriceReport, "C:/MarginalReport/Latestmarginalreport.csv");

                if (statusOfCSVgeneration)
                {
                    statusResp = new genericResponse()
                    {
                        responseCode = "00",
                        responseMessage = $"New Price Report Generated and sent to :::: {email}"
                    };

                    return Ok(statusResp);
                }
                statusResp = new genericResponse()
                {
                    responseCode = "99",
                    responseMessage = "Marginal Report generation Failed "
                };

                return Ok(statusResp);

            }
            catch (Exception ex)
            {

                statusResp = new genericResponse()
                {
                    responseCode = "01",
                    responseMessage = "Error occur at the Point of Callling AESO Service"
                };
                return BadRequest(statusResp); 
            }

            return null;

        }


    }
}