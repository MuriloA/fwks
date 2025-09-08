namespace FwksLabs.Libs.Core.Types;

public record PageQuery(int PageNumber, int PageSize)
{
    public int GetSkip()
    {
        return PageSize < 0 ? -1 : (PageNumber - 1) * PageSize;
    }

    public static PageQuery All()
    {
        return new PageQuery(1, -1);
    }
}