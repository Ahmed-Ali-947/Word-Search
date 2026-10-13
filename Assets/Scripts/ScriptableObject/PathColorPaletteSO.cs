using UnityEngine;

[CreateAssetMenu(
    menuName = "Word Search/Path Color Palette",
    fileName = "PathColorPalette")]
public class PathColorPaletteSO : ScriptableObject
{
    public Gradient[] Gradients;
}