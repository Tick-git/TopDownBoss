using System;

public class Setting<T>
{
    private T value;
    public event Action<T> ValueChanged;

    public T Value
    {
        get => value;
        set
        {
            if (!Equals(value, this.value))
            {
                this.value = value;
                ValueChanged?.Invoke(value);
            }
        }
    }
}