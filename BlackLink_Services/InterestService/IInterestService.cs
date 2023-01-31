using BlackLink_Commends.Commend.InterestCommend.Commend;
using BlackLink_Commends.Commend.InterestCommend.Query;
using BlackLink_DTO.Interest;

namespace BlackLink_Services.InterestService;

public interface IInterestService
{
    public Task<AddInterestCommend> AddInterest(AddInterestCommend commned);
    public Task<UpdateInterestCommend> UpdateInterest(UpdateInterestCommend commned);
    public Task<IEnumerable<InterestDto>> GetAllInterests();
    public Task<InterestDto> GetInterest(GetInterestByIdQuery query);
    public Task<RemoveInterestCommend> RemoveInterest(RemoveInterestCommend commend);
}
