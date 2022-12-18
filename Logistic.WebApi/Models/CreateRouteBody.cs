namespace Logistic.WebApi.Models;

public class CreateRouteBody
{
    public int StartPointId { get; set; }

    public int EndPointId { get; set; }

    public int RouteLength { get; set; }

    public string Name { get; set; }
}