using CandidateHub.Constants;
using CandidateHub.Database;
using CandidateHub.Events;
using CandidateHub.Features.Candidates.Commands;
using CandidateHub.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace CandidateHub.UnitTest.Candidates;

public class CreateAndUpdateCandidateTests
{
    private readonly Mock<ICandidateHubRepository> _mockRepository;
    private readonly Mock<ILogger> _mockLogger;
    private readonly Mock<IMediator> _mockMediator;
    private readonly CreateAndUpdateCandidate.Handler _handler;

    public CreateAndUpdateCandidateTests()
    {
        _mockRepository = new Mock<ICandidateHubRepository>();
        _mockLogger = new Mock<ILogger>();
        _mockMediator = new Mock<IMediator>();
        _handler = new CreateAndUpdateCandidate.Handler(
            _mockLogger.Object,
            _mockMediator.Object,
            _mockRepository.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnOkResponse_WhenCandidateIsAdded()
    {
        // Arrange
        var candidateDto = Payload.GetCandidateDto();
        var command = new CreateAndUpdateCandidate.Command(candidateDto);
        var expectedResponse = AppConstant.Candidate_Added;

        _mockRepository
            .Setup(repo => repo.AddOrUpdateCandidate(It.IsAny<Candidate>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Message.Should().Be(expectedResponse);
        _mockMediator.Verify(m => m.Publish(It.IsAny<CandidateHubLog.CandidateHubLogNotification>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnOkResponse_WhenCandidateIsUpdated()
    {
        // Arrange
        var candidateDto = Payload.GetCandidateDto();
        candidateDto.Comment = "sudip is a great candidate";
        var command = new CreateAndUpdateCandidate.Command(candidateDto);
        var expectedResponse = AppConstant.Candidate_Added;

        _mockRepository
            .Setup(repo => repo.AddOrUpdateCandidate(It.IsAny<Candidate>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Message.Should().Be(expectedResponse);
        _mockMediator.Verify(m => m.Publish(It.IsAny<CandidateHubLog.CandidateHubLogNotification>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldLogError_WhenExceptionIsThrown()
    {
        // Arrange
        var candidateDto = Payload.GetCandidateDto();
        var command = new CreateAndUpdateCandidate.Command(candidateDto);
        var exceptionMessage = "Database error";

        _mockRepository
            .Setup(repo => repo.AddOrUpdateCandidate(It.IsAny<Candidate>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception(exceptionMessage));

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage(exceptionMessage);
        _mockLogger.Verify(
            x => x.Error(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
           
    }
}

public static class Payload
{
    public static CreateAndUpdateCandidate.CandidateDto GetCandidateDto()
    {
        return new CreateAndUpdateCandidate.CandidateDto
        {
            FirstName = "sudip",
            LastName = "rb",
            Email = "sudip.test@example.com",
            Comment = "sudip is a good candidate"
        };
    }
}
