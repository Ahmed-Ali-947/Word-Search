using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PathView : MonoBehaviour
{
    [SerializeField] private PathColorPaletteSO palette;

    private static int currentColorIndex;
    private LineRenderer line;
    private float lineWidth;
    private static System.Random random = new();

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    public void Initialize(float width)
    {
        lineWidth = width;

        line.widthMultiplier = lineWidth;
        ApplyNextColor();
    }

    public void Refresh(Vector3 start, Vector3 end)
    {
        line.positionCount = 2;

        if (start == end)
        {
            line.SetPosition(0, start);
            line.SetPosition(1, start);
            return;
        }

        line.SetPosition(0, start);
        line.SetPosition(1, end);
    }

    // public void Freeze()
    // {
    //     // Intentionally empty for now.

    //     // Later:
    //     // - Play animation
    //     // - Enable glow
    //     // - Disable further updates
    // }

    private void ApplyNextColor()
    {
        if (palette == null)
            return;

        if (palette.Gradients.Length == 0)
            return;

        line.colorGradient =
            palette.Gradients[currentColorIndex];

        currentColorIndex++;

        if (currentColorIndex >= palette.Gradients.Length)
            currentColorIndex = 0;
    }
}