public interface ITimeData
{
    /**
     * If a TimeObject IsDisabled for the full length of a TimeData list, it will call a handler,
     * which will usually destroy the object as it cannot revert to a non-disabled state.
     */
    public bool IsDisabled { get; }
}