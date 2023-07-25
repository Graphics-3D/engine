namespace Engine;

using Meshes;

public class Scene
{
    public List<Mesh> Meshes { get; private set; } = new();
    public List<Light> Lights { get; private set; } = new();
    public Color BackgroundColor { get; set; } = Color.White;

    private static Scene crr = new Scene();

    private Scene() { }
    
    public static Scene Current => crr;

    public static void Create(params Mesh[] meshes)
    {
        Scene scene = new Scene();
        
        scene.Meshes.AddRange(meshes);

        crr = scene;
    }

    public static void Create(IEnumerable<Mesh> meshes)
    {
        Scene scene = new Scene();
        
        scene.Meshes.AddRange(meshes);

        crr = scene;
    }

    public static void Create(IEnumerable<Mesh> meshes, IEnumerable<Light> lights)
    {
        Scene scene = new Scene();
        
        scene.Meshes.AddRange(meshes);
        scene.Lights.AddRange(lights);

        crr = scene;
    }
}