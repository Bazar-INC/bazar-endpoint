
namespace Shared.CommonUtils;

public class CommonUtils
{
    #region Public
    public static IQueryable<T> Paginate<T>(IQueryable<T> items, int perPage, int page, out int totalPages)
    {
        var count = items.Count();

        items = items.Skip((page - 1) * perPage).Take(perPage);

        if (count <= perPage && count != 0)
        {
            totalPages = 1;
            return items;
        }

        totalPages = count / perPage;

        if (totalPages % perPage > 0)
        {
            ++totalPages;
        }
        return items;
    }
    #endregion
}
