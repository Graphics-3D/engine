namespace Engine;

public interface ITransformable<T>
    where T : ITransformable<T>
{
    T Translate(float x, float y, float z);

    T RotateX(float cos, float sin);

    T RotateY(float cos, float sin);

    T RotateZ(float cos, float sin);

    T Scale(float x, float y, float z);

    T RotateX(float x, float y, float z, float cos, float sin) =>
        Translate(-x, -y, -z)
            .RotateX(cos, sin)
            .Translate(x, y, z);

    T RotateY(float x, float y, float z, float cos, float sin) =>
        Translate(-x, -y, -z)
            .RotateY(cos, sin)
            .Translate(x, y, z);

    T RotateZ(float x, float y, float z, float cos, float sin) =>
        Translate(-x, -y, -z)
            .RotateZ(cos, sin)
            .Translate(x, y, z);
    
    T RotateX(Point3D p, float cos, float sin) =>
        RotateX(p.X, p.Y, p.Z, cos, sin);

    T RotateY(Point3D p, float cos, float sin) =>
        RotateY(p.X, p.Y, p.Z, cos, sin);

    T RotateZ(Point3D p, float cos, float sin) =>
        RotateZ(p.X, p.Y, p.Z, cos, sin);
}