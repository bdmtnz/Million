namespace Million.BackEnd.Contracts.Common
{
    public record PaginationResponse<T>(int Total, T Page);
}
