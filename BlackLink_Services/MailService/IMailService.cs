using BlackLink_DTO.Mail;

namespace BlackLink_Services.MailService;

public interface IMailService
{
    public Task<MailRequest> SendEmailAsync(MailRequest mailRequest);
}
