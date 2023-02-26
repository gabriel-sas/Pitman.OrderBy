namespace Pitman.OrderBy
{
    public interface IOrderBy
    {
        IQueryable<T> ApplyOrder<T>(IQueryable<T> query);
    }
}
