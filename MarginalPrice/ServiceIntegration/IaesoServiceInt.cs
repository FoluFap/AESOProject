using MarginalPrice.MPService.Response;
using MarginalPrice.MPService.Response.CurrentPrice;

namespace MarginalPrice.ServiceIntegration
{
    public interface IaesoServiceInt
    {
        systemMaginalPriceResp GetSystemMaginalPrice(string startDate, string enddate);
        systemMarginalCurrentPriceResp GetSystemMarginalCurrentPrice(string currentStatus);
    }
}
