namespace TopDownZombies.Nidot.Nodes;

public partial class MousePosition : Node2D
{
	public Vector2 GetMousePosition() => GetGlobalMousePosition();
}