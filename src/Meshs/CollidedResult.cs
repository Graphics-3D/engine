namespace Engine.Meshes;

public enum CollidedResult : byte
{
    False = 0,
    True = 1,
    Front = 2,
    Back = 4,
    Left = 8,
    Right = 16,
    Top = 32,
    Bottom = 64,
}