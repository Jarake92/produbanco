﻿namespace shared.comun.hetoas.extensions;

public class PagedList<T> : List<T>
{
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }

    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);
    }

    public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
    {
        var enumerable = source as T[] ?? source.ToArray();
        var count = enumerable.Length;
        var items = enumerable.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }

    public static PagedList<T> ToPagedList(T item)
    {
        var source = new List<T>
        {
            item
        };
        return ToPagedList(source, 1, 1);
    }
}