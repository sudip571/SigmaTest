namespace CandidateHub.Helpers
{
    internal static class SwaggerHelper
    {
        internal static class V1
        {
            public const string Version = "V1";
            public const string CandidateHubAPIGroup = "candidate-hub-v1";
            public const string Title = "Candidate Hub API";
            public const string Description = "This api helps you for storing information about job candidates";
            public const string Terms = @"https://sigma-company/terms";
        }
        internal static class Contact
        {
            public const string Name = "Sigma - Candidate Hub";
            public const string Email = "sigma@test.com";
            public const string Url = @"https://sigma-company/contact";
        }
        internal static class License
        {
            public const string Name = "Use under Sigma's terms and conditions";
            public const string Url = @"https://sigma-company/license";
        }
    }
}
