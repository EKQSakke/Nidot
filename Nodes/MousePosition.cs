namespace Nidot;

using Godot;

public partial class MousePosition : Node2D
{
	public Vector2 GetMousePosition() => GetGlobalMousePosition();
}