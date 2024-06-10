using CandidateHub.Constants;
using CandidateHub.Database.Context;
using CandidateHub.Database;
using CandidateHub.Repositories;
using Microsoft.EntityFrameworkCore;
using CandidateHub.Features.Candidates.Commands;

namespace CandidateHub.UnitTest.Repositories;




public class CandidateHubRepositoryIntegrationTests
{
    private readonly DbContextOptions<SigmaContext> _dbContextOptions;

    public CandidateHubRepositoryIntegrationTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<SigmaContext>()
            .UseInMemoryDatabase(databaseName: "SigmaTestDatabase")
            .Options;
    }

    [Fact]
    public async Task AddOrUpdateCandidate_ShouldAddNewCandidate()
    {
        // Arrange
        using var context = new SigmaContext(_dbContextOptions);
        var repository = new CandidateHubRepository(context);
        var candidate = DatabaseObject.GetCandidateToCreate();

        // Act
        var result = await repository.AddOrUpdateCandidate(candidate, CancellationToken.None);

        // Assert
        Assert.Equal(AppConstant.Candidate_Added, result);
        var addedCandidate = context.Candidate.FirstOrDefault(c => c.Email == candidate.Email);
        Assert.NotNull(addedCandidate);
        Assert.Equal(candidate.FirstName, addedCandidate.FirstName);
        Assert.Equal(candidate.LastName, addedCandidate.LastName);
    }

    [Fact]
    public async Task AddOrUpdateCandidate_ShouldUpdateExistingCandidate()
    {
        // Arrange
        using var context = new SigmaContext(_dbContextOptions);
        var repository = new CandidateHubRepository(context);

        var candidate = DatabaseObject.GetCandidateToUpdate(); 
        await context.Candidate.AddAsync(candidate);
        await context.SaveChangesAsync();

        // update some property
        candidate.Comment = "comment is updated";

        // Act
        var result = await repository.AddOrUpdateCandidate(candidate, CancellationToken.None);

        // Assert
        Assert.Equal(AppConstant.Candidate_Updated, result);

        var updatedExistingCandidate = context.Candidate.FirstOrDefault(c => c.Email == candidate.Email);
        Assert.NotNull(updatedExistingCandidate);
        Assert.Equal(candidate.FirstName, updatedExistingCandidate.FirstName);
        Assert.Equal(candidate.LastName, updatedExistingCandidate.LastName);
        Assert.Equal(candidate.Comment, updatedExistingCandidate.Comment);
    }

    [Fact]
    public async Task GetAllCandidates_ShouldReturnAllCandidates()
    {
        // Arrange
        using var context = new SigmaContext(_dbContextOptions);
        var repository = new CandidateHubRepository(context);
        
        context.Candidate.RemoveRange(context.Candidate);

        var candidates = DatabaseObject.GetCandidates();
        await context.Candidate.AddRangeAsync(candidates);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetAllCandidates(CancellationToken.None);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, c => c.Email == "candidate1@example.com");
        Assert.Contains(result, c => c.Email == "candidate2@example.com");
    }



}
public static class DatabaseObject
{
    public static Candidate GetCandidateToCreate()
    {
        return new Candidate
        {
            FirstName = "sudip",
            LastName = "rb",
            Email = "sudip.test@example.com",
            Comment = "sudip is a good candidate",
            ExposeId = Guid.NewGuid().ToString(),
        };
    }
    public static Candidate GetCandidateToUpdate()
    {
        return new Candidate
        {
            FirstName = "sudip",
            LastName = "rb",
            Email = "sudip.testupdate@example.com",
            Comment = "sudip is a good candidate",
            ExposeId = Guid.NewGuid().ToString(),
        };
    }
    public static List<Candidate> GetCandidates()
    {
        return new List<Candidate>
        {
            new Candidate
            {
                Email = "candidate1@example.com",
                FirstName = "Candidate1",
                LastName = "One",
                Comment = "Comment1",
                ExposeId = Guid.NewGuid().ToString()
            },
            new Candidate
            {
                Email = "candidate2@example.com",
                FirstName = "Candidate2",
                LastName = "Two",
                Comment = "Comment2",
                ExposeId = Guid.NewGuid().ToString()
            }
        };
    }
}

