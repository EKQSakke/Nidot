namespace Nidot;

public partial class TimeScaler : Node, IAutoload
{
    int currentTimeScaleId = 3;
    float[] timeScales = new[] { 0.1f, 0.25f, .5f, 1, 1.5f, 2, 5, 10, 25, 50 };

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("TimeScaleUp") && currentTimeScaleId < timeScales.Length)
        {
            SetTimeScale(++currentTimeScaleId);
        }

        if (@event.IsActionPressed("TimeScaleDown") && currentTimeScaleId > 0)
        {
            SetTimeScale(--currentTimeScaleId);
        }
    }

    void SetTimeScale(int id)
    {
        Engine.TimeScale = timeScales[id];
        this.GetAutoload<Logger>().Log($"TimeScale: {timeScales[id]}");
    }

    /// <inheritdoc />
    public void Reset()
    {
        SetTimeScale(3);
    }
}