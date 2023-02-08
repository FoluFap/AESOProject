using MarginalPrice.MPService.Request;

namespace MarginalPrice.MPService
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
