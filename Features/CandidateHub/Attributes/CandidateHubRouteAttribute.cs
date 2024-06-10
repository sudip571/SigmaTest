using Microsoft.AspNetCore.Mvc;

namespace CandidateHub.Attributes;


public class CandidateHubRouteAttribute : RouteAttribute
{
    private const string _routePrefix = "api/candidate-hub";
    
    public CandidateHubRouteAttribute(string template = "") : base($"{_routePrefix}{template}")
    {
    }
}
