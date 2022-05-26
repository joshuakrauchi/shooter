/**
 * Defines a float value that is locked between [min, max].
 * It throws no errors if set with values lower than min or higher than max.
 * If set to a number that is lower than min, it sets it to min,
 * and if set to a number higher than max, sets it to max.
 */
public struct LockedFloat
{
    public float Value
    {
        get => _value;
        set
        {
            if (value < MinValue)
            {
                _value = MinValue;
            }
            else if (value > MaxValue)
            {
                _value = MaxValue;
            }
            else
            {
                _value = value;
            }
        }
    }

    private float MinValue { get; set; }
    private float MaxValue { get; set; }

    private float _value;

    public LockedFloat(float initialValue, float minValue, float maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
        _value = 0.0f;
        Value = initialValue;
    }

    public static float operator +(LockedFloat lockedFloat) => lockedFloat.Value;
    public static float operator -(LockedFloat lockedFloat) => -lockedFloat.Value;
    public static float operator +(LockedFloat left, LockedFloat right) => left.Value + right.Value;
    public static float operator -(LockedFloat left, LockedFloat right) => left.Value - right.Value;
    public static float operator *(LockedFloat left, LockedFloat right) => left.Value * right.Value;
    public static float operator /(LockedFloat left, LockedFloat right) => left.Value / right.Value;
}