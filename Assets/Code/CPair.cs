public class CPair<T, U>
{
    public CPair()
    {
    }

    public CPair( T first, U second )
    {
        this.First = first;
        this.Second = second;
    }

    public T First { get; set; }
    public U Second { get; set; }
};