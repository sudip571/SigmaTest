using CandidateHub.Database;
using Sigma.Shared.Interface;

namespace CandidateHub.Repositories;

public interface ICandidateHubRepository : ITransientService
{
    Task<string> AddOrUpdateCandidate(Candidate candidate, CancellationToken cancellationToken);
    Task<List<Candidate>> GetAllCandidates(CancellationToken cancellationToken);
}
