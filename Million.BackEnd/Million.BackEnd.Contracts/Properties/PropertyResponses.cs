namespace Million.BackEnd.Contracts.Properties
{
    public record PropertyOwnerResponse(
        string Name,
        string Address,
        string Photo,
        DateTime BornOnUtc);
    public record PropertyTraceResponse(
        string Name,
        decimal Value,
        DateTime SaledOnUtc);
    public record PropertyResponse(
        string Id,
        string Name,
        string Address,
        decimal Price,
        string Code,
        int Year,
        string? Image,
        PropertyOwnerResponse Owner,
        PropertyTraceResponse? Trace,
        DateTime CreatedOnUtc);
    public record PropertyFilteredResponse(
        string Id,
        string Name,
        string Address,
        decimal Price,
        string Code,
        int Year,
        string Image,
        DateTime CreatedOnUtc);
}
