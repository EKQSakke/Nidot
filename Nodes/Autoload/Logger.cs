namespace Nidot;

using System.Collections.Generic;
using Godot;

public partial class Logger : VBoxContainer
{
    readonly Queue<Label> logNodes = new();

    int logId;

    public override void _Ready()
    {
        SetAnchorsPreset(LayoutPreset.Center);
        SetSize(GetTree().Root.Size);
        Alignment = AlignmentMode.End;
    }

    public void Log(string message, LogLevel level = LogLevel.Error)
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
        GD.Print($"Log: {message}");

        var timerManager = this.GetAutoload<TimerManager>();

        timerManager.AddTimer("log" + logId++, 5, () =>
                        {
                            if (logNodes.Count > 0)
                            {
                                var qLabel = logNodes.Dequeue();
                                qLabel.QueueFree();
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