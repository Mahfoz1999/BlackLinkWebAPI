using BlackLink_Database.SQLConnection;
using BlackLink_DTO.Interest;
using BlackLink_Models.Models;
using BlackLink_Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BlackLink_Repository.Repository
{
    public class InterestRepository : IInterestRepository
    {
        private readonly BlackLinkDbContext Context;
        private readonly IUserRepository userRepository;
        public InterestRepository(BlackLinkDbContext context, IUserRepository userRepository)
        {
            Context = context;
            this.userRepository = userRepository;
        }
        #region Actions
        public async Task<InterestFormDto> AddInterests(InterestFormDto formDto)
        {
            var Interest = new Interest()
            {
                InterestName = formDto.InterestName
            };
            await Context.Interests.AddAsync(Interest);
            await Context.SaveChangesAsync();
            return formDto;
        }
        public async Task<InterestFormDto> UpdateInterests(InterestFormDto formDto)
        {
            var interest = await Context.Interests.Where(inte => inte.Id == formDto.Id).ExecuteUpdateAsync(i => i.SetProperty(n => n.InterestName, formDto.InterestName));
            if (interest is not 0)
            {
                await Context.SaveChangesAsync();
                return formDto;
            }
            else
                throw new KeyNotFoundException("interests not Found");
        }
        public async Task<bool> AddUserInterests(List<Guid> InterestsIds)
        {
            var Interests = await Context.Interests.Where(inte => InterestsIds.Contains(inte.Id)).ToListAsync();
            var user = await userRepository.GetCurrentUser();
            foreach (var interest in Interests)
            {
                await Context.InterestUsers.AddAsync(new InterestUser() { Interest = interest, User = user });
            }
            await Context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateUserInterests(List<Guid> InterestsIds)
        {
            var Interests = await Context.Interests.Where(inte => InterestsIds.Contains(inte.Id)).ToListAsync();
            var user = await userRepository.GetCurrentUser();
            var UserInterests = await Context.InterestUsers.Where(us => us.User == user).ToListAsync();
            Context.InterestUsers.RemoveRange(UserInterests);
            _ = await Context.SaveChangesAsync();
            foreach (var interest in Interests)
            {
                await Context.InterestUsers.AddAsync(new InterestUser() { Interest = interest, User = user });
            }
            await Context.SaveChangesAsync();
            return true;
        }
        #endregion

        #region Get
        public async Task<IEnumerable<InterestDto>> GetAllInterests()
        {
            var interests = await Context.Interests.Select(inte => new InterestDto()
            {
                Id = inte.Id,
                InterestName = inte.InterestName,

            }).ToListAsync();
            return interests;
        }

        public async Task<InterestDto> GetUserInterests()
        {
            var user = await userRepository.GetCurrentUser();
            var interests = await Context.InterestUsers.Where(inte => inte.User == user)
                .Select(inte => new InterestDto()
                {
                    Id = inte.Interest.Id,
                    InterestName = inte.Interest.InterestName,
                }).SingleOrDefaultAsync();
            return interests is not null
                ? interests
                : throw new KeyNotFoundException("interest not found ");
        }
        #endregion

        #region Remove
        public async Task<bool> RemoveUserInterests(List<Guid> InterestsIds)
        {
            var user = await userRepository.GetCurrentUser();
            await Context.InterestUsers.Where(us => us.User == user && InterestsIds.Contains(us.Interest.Id)).ExecuteDeleteAsync();
            await Context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveInterests(Guid Id)
        {
            var interests = await Context.Interests.Where(inte => inte.Id == Id).ExecuteDeleteAsync();
            if (interests is not 0)
            {
                await Context.SaveChangesAsync();
                return true;
            }
            else
                throw new KeyNotFoundException("interest not found");
        }
        #endregion

    }
}
