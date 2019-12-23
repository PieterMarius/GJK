using CollisionDetection.MathUtility;
using System;

namespace CollisionDetection
{
    class Program
    {
        static void Main(string[] args)
        {
            var collisionEngine = new GJK(3, 1E-6, 0.09);

            var obj1 = new VertexProperties[8];
            obj1[0] = new VertexProperties(new Vector3d(-1.0, -2.0, 1.0));
            obj1[1] = new VertexProperties(new Vector3d(-1.0, 2.0, 1.0));
            obj1[2] = new VertexProperties(new Vector3d(1.0, 2.0, 1.0));
            obj1[3] = new VertexProperties(new Vector3d(1.0, -2.0, 1.0));
            obj1[4] = new VertexProperties(new Vector3d(-1.0, -2.0, -1.0));
            obj1[5] = new VertexProperties(new Vector3d(-1.0, 2.0, -1.0));
            obj1[6] = new VertexProperties(new Vector3d(1.0, 2.0, -1.0));
            obj1[7] = new VertexProperties(new Vector3d(1.0, -2.0, -1.0));

            var obj2 = new VertexProperties[8];
            obj2[0] = new VertexProperties(new Vector3d(3.0, -2.0, 1.0));
            obj2[1] = new VertexProperties(new Vector3d(3.0, 2.0, 1.0));
            obj2[2] = new VertexProperties(new Vector3d(5.0, 2.0, 1.0));
            obj2[3] = new VertexProperties(new Vector3d(5.0, -2.0, 1.0));
            obj2[4] = new VertexProperties(new Vector3d(3.0, -2.0, -1.0));
            obj2[5] = new VertexProperties(new Vector3d(3.0, 2.0, -1.0));
            obj2[6] = new VertexProperties(new Vector3d(5.0, 2.0, -1.0));
            obj2[7] = new VertexProperties(new Vector3d(5.0, -2.0, -1.0));

            var res = collisionEngine.Execute(obj1, obj2);


        }
    }
}
