using CandidateHub.Database;
using FluentValidation;
using MediatR;
using Sigma.Shared.Responses;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CandidateHub.Features.Candidates.Commands;

public static class CreateAndUpdateCandidate
{
    #region Command
    public sealed record Command(CandidateDto candidate) : IRequest<Response<string>>;

    #endregion Command

    #region Validation
    public class CommandValidator : AbstractValidator<Command>
    {
       
        public CommandValidator()
        {           

            RuleFor(x => x.candidate.FirstName)
               .NotNull()
               .NotEmpty()
               .WithMessage("First name is required.");

            RuleFor(x => x.candidate.LastName)
               .NotNull()
               .NotEmpty()
               .WithMessage("Last name is required.");

            RuleFor(x => x.candidate.Email)
              .NotNull()
              .NotEmpty()              
              .WithMessage("Email is required.")
              .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.candidate.Comment)
              .NotNull()
              .NotEmpty()
              .WithMessage("Comment is required.");

            RuleFor(x => x.candidate.GitHub)
                .Must(BeAValidGitHubUrl)
                .When(x => !string.IsNullOrEmpty(x.candidate.GitHub))
                .WithMessage("Invalid GitHub URL format.");

           
            RuleFor(x => x.candidate.Linkedin)
                .Must(BeAValidLinkedInUrl)
                .When(x => !string.IsNullOrEmpty(x.candidate.Linkedin))
                .WithMessage("Invalid LinkedIn URL format.");

            RuleFor(x => x.candidate.PhoneNumber)          
           .Matches(@"^\+?\d{10,15}$")
           .When(x => !string.IsNullOrEmpty(x.candidate.PhoneNumber))
           .WithMessage("Invalid phone number format.");


        }
        private bool BeAValidGitHubUrl(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out var uriResult))
            {
                return uriResult.Host.EndsWith("github.com");
            }
            return false;
        }

        private bool BeAValidLinkedInUrl(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out var uriResult))
            {
                return uriResult.Host.EndsWith("linkedin.com");
            }
            return false;
        }

    }

    #endregion Validation

    #region Response and DTO Model
    public class CandidateDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Comment { get; set; }

        public string PhoneNumber { get; set; }

        public string TimeInterval { get; set; }

        public string Linkedin { get; set; }

        public string GitHub { get; set; }

        public Candidate ToCandidate()
        {
            return new Candidate
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Comment = Comment,
                PhoneNumber = PhoneNumber,
                TimeInterval = TimeInterval,
                Linkedin = Linkedin,
                GitHub = GitHub
            };
        }
    }
    #endregion
}
