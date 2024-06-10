using CandidateHub.Features.Candidates.Commands;


namespace CandidateHub.UnitTest.Candidates;

public class CreateAndUpdateCandidateValidatorTests
{
    private readonly CreateAndUpdateCandidate.CommandValidator _validator;

    public CreateAndUpdateCandidateValidatorTests()
    {
        _validator = new CreateAndUpdateCandidate.CommandValidator();
    }

    [Fact]
    public void Should_Have_Error_When_FirstName_Is_Null()
    {
        var candidateDto = new CreateAndUpdateCandidate.CandidateDto { FirstName = null };
        var command = new CreateAndUpdateCandidate.Command(candidateDto);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.candidate.FirstName);
    }

    [Fact]
    public void Should_Have_Error_When_LastName_Is_Null()
    {
        var candidateDto = new CreateAndUpdateCandidate.CandidateDto { LastName = null };
        var command = new CreateAndUpdateCandidate.Command(candidateDto);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.candidate.LastName);
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        var candidateDto = new CreateAndUpdateCandidate.CandidateDto { Email = "invalidemail" };
        var command = new CreateAndUpdateCandidate.Command(candidateDto);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.candidate.Email);
    }

    [Fact]
    public void Should_Have_Error_When_GitHubUrl_Is_Invalid()
    {
        var candidateDto = new CreateAndUpdateCandidate.CandidateDto { GitHub = "invalidurl" };
        var command = new CreateAndUpdateCandidate.Command(candidateDto);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.candidate.GitHub);
    }

    [Fact]
    public void Should_Have_Error_When_LinkedInUrl_Is_Invalid()
    {
        var candidateDto = new CreateAndUpdateCandidate.CandidateDto { Linkedin = "invalidurl" };
        var command = new CreateAndUpdateCandidate.Command(candidateDto);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.candidate.Linkedin);
    }

    [Fact]
    public void Should_Have_Error_When_PhoneNumber_Is_Invalid()
    {
        var candidateDto = new CreateAndUpdateCandidate.CandidateDto { PhoneNumber = "12345" };
        var command = new CreateAndUpdateCandidate.Command(candidateDto);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.candidate.PhoneNumber);
    }

    [Fact]
    public void Should_Not_Have_Error_When_PhoneNumber_Is_Valid()
    {
        var candidateDto = new CreateAndUpdateCandidate.CandidateDto { PhoneNumber = "+1234567890" };
        var command = new CreateAndUpdateCandidate.Command(candidateDto);
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.candidate.PhoneNumber);
    }
}
