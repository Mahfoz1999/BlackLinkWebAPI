using BlackLink_DTO.Mail;

namespace BlackLink_MailService.IServices
{
    public interface IMailService
    {
        public Task<MailRequest> SendEmailAsync(MailRequest mailRequest);
    }
}
