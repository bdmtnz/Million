namespace Million.BackEnd.Contracts.Common
{
    public record PaginationResponse<T>(long Total, T Page);
}
