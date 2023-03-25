using BlackLink_Commends.Commend.InterestCommends.Commend;
using BlackLink_DTO.Interest;

namespace BlackLink_Services.InterestService;

public interface IInterestService
{
    public Task<AddInterestCommend> AddInterest(AddInterestCommend commned);
    public Task UpdateInterest(UpdateInterestCommend commned);
    public Task<IEnumerable<InterestDto>> GetAllInterests();
    public Task<InterestDto> GetInterest(Guid Id);
    public Task RemoveInterest(Guid Id);
}
