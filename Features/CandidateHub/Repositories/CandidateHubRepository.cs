using CandidateHub.Constants;
using CandidateHub.Database;
using CandidateHub.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace CandidateHub.Repositories;

public class CandidateHubRepository : ICandidateHubRepository
{
    private readonly SigmaContext sigmaContext;
    public CandidateHubRepository(SigmaContext sigmaContext)
    {
        this.sigmaContext = sigmaContext;
    }
    public async Task<string> AddOrUpdateCandidate(Candidate candidate,CancellationToken cancellationToken)
    {
        var response = AppConstant.Candidate_Added;
        var existingCandidate = await sigmaContext.Candidate
             .FirstOrDefaultAsync(c => c.Email == candidate.Email, cancellationToken);

        if (existingCandidate is not null)
        {
            existingCandidate.Email = candidate.Email;
            existingCandidate.FirstName = candidate.FirstName;
            existingCandidate.LastName = candidate.LastName;
            existingCandidate.PhoneNumber = candidate.PhoneNumber;
            existingCandidate.Comment = candidate.Comment;
            existingCandidate.TimeInterval = candidate.TimeInterval;
            existingCandidate.Linkedin = candidate.Linkedin;
            existingCandidate.GitHub = candidate.GitHub;

            response = AppConstant.Candidate_Updated;
        }
        else
        {
            await sigmaContext.Candidate.AddAsync(candidate);
        }
        await sigmaContext.SaveChangesAsync();
        return response;
    }

    public async Task<List<Candidate>> GetAllCandidates(CancellationToken cancellationToken)
    {
        return await sigmaContext.Candidate
            .AsNoTracking()
            .Where(c => !c.IsDeleted)
            .ToListAsync(cancellationToken);
    }
}
