using TopDownZombies.Nidot.Nodes;

namespace TopDownZombies.Nidot.DevCam;

public partial class DevCamera : Node3D
{
    Camera3D cam;
    MousePosition mousePosition;
    Node selectedNode;

    const float Speed = 10.0f;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ProcessMode = ProcessModeEnum.Always;
        mousePosition = new MousePosition();
        AddChild(mousePosition);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        if (cam == null) return;
        Vector2 inputDir = Input.GetVector("left", "right", "up", "down");
        Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
        if (direction != Vector3.Zero)
        {
            var floatDelta = (float)delta;
            var x = direction.X * Speed * floatDelta;
            var z = direction.Z * Speed * floatDelta;
            cam.GlobalPosition += new Vector3(x, 0, z);
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsPressed() && @event.IsAction("DevCam"))
        {
            GD.Print("DevCam");
            if (cam == null)
                StartCamera();
            else
                StopCamera();
        }

        if (@event.IsPressed() && @event.IsAction("Fire"))
        {
            if (cam == null)
                return;

            var screenPos = mousePosition.GetMousePosition();
            var worldPos = cam.ProjectPosition(screenPos, 1000);
            var spaceState = GetWorld3D().DirectSpaceState;
            var parameters = new PhysicsRayQueryParameters3D()
            {
                From = cam.GlobalPosition,
                To = worldPos,
            };
            var result = spaceState.IntersectRay(parameters);
            if (!result.TryGetValue("collider", out var collider))
                return;

            selectedNode = collider.AsGodotObject() as Node;
            if (selectedNode is IDevCamerable devCamerable)
            {
                GD.Print(devCamerable.GetPrintInfo());
            }
        }
    }

    private void StopCamera()
    {
        cam.QueueFree();
        cam.Current = false;
        GetTree().Paused = false;
        cam = null;
    }

    private void StartCamera()
    {
        cam = new Camera3D
        {
            ProcessMode = ProcessModeEnum.Always
        };
        AddChild(cam);
        var playCam = GetViewport().GetCamera3D();
        cam.Current = true;
        GetTree().Paused = true;
        cam.GlobalPosition = playCam.GlobalPosition;
        cam.GlobalRotation = playCam.GlobalRotation;
    }
}
