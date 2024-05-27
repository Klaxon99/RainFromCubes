using System;

public interface IObjectsCounter
{
    public int CreatingObjectsCount { get; }
    public int ActiveObjectsCount { get; }

    public event Action<int> CreatingObjectsCountChanged;
    public event Action<int> ActiveObjectsCountChanged;
}