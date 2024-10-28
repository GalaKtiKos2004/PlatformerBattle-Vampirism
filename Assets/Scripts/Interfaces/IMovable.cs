using System;

public interface IMovable
{
    public event Action<float> Moved;
}
