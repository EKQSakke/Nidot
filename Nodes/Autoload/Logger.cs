using TopDownZombies.Nidot.Extensions;

namespace TopDownZombies.Nidot.Nodes.Autoload;

public partial class Logger : VBoxContainer
{
    readonly Queue<Label> logNodes = new();
    readonly Queue<Label3D> logNodes3D = new();

    int logId;

    public override void _Ready()
    {
        SetAnchorsPreset(LayoutPreset.Center);
        SetSize(GetTree().Root.Size);
        Alignment = AlignmentMode.End;
    }

    public void Log(string message, LogLevel level = LogLevel.Error, Vector3 globalPosition = new Vector3())
    {
        var label = new Label
        {
            Text = message,
            LabelSettings = new LabelSettings
            {
                FontSize = 12,
                FontColor = GetLogColor(level),
            },
        };

        logNodes.Enqueue(label);
        AddChild(label);

        bool addToWorld = globalPosition != Vector3.Zero;
        if (addToWorld)
        {
            var label3D = new Label3D
            {
                Text = message,
                //TODO: Material override to color
            };
            logNodes3D.Enqueue(label3D);
            GetTree().Root.AddChild(label3D);
            label3D.LookAtFromPosition(globalPosition, this.GetNodeFromAll<Camera3D>().GlobalPosition, useModelFront: true);
        }


        GD.Print($"Log: {message}");

        var timerManager = this.GetAutoload<TimerManager>();

        timerManager.AddTimer("log" + logId++, 2, () =>
                        {
                            if (logNodes.Count > 0)
                            {
                                var qLabel = logNodes.Dequeue();
                                qLabel.QueueFree();
                            }
                            if (logNodes3D.Count > 0 && addToWorld)
                            {
                                var qLabel3D = logNodes3D.Dequeue();
                                qLabel3D.QueueFree();
                            }
                        });
    }

    static Color GetLogColor(LogLevel level) => level switch
    {
        LogLevel.Info => new Color(0, 0, 0),
        LogLevel.Warning => new Color(1, 1, 0),
        LogLevel.Error => new Color(1, 0, 0),
        _ => new Color(0, 0, 0),
    };

}


public enum LogLevel
{
    Game, // Game is not shown on screen, only locally
    Info,
    Warning,
    Error
}