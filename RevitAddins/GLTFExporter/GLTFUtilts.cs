using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace XPRevitAddins.GLTFExporter
{
    class GLTFUtilts
    {
       public static GLTF Convert2GLTF() //will take view 3D as input 
        {
            
            //init an asset
            Asset asset = new Asset("2.0");//only require property to init a GLTF

            //init a scene 
            Scene scene = new Scene();
            scene.nodes = new List<int>();
            scene.nodes.Add(0);

            //init node
            Node node = new Node();
            node.name = "BoomBox";
            node.mesh = 0;

            //init camera
            Camera camera = new Camera { name="new Camera"};

            

            //init mesh
            Attribute attribute = new Attribute
            {
                TEXCOORD_0 = 0,
                NORMAL = 1,
                TANGENT = 2,
                POSITION = 3
            };
            Primitive primitive = new Primitive(attribute);//init primitive for mesh
            primitive.indices = 4;
            primitive.material = 0;
            GLTFExporter.Mesh mesh = new GLTFExporter.Mesh();
            mesh.primitives = new List<Primitive> { primitive };
            mesh.name = "test mesh";
            

            GLTF obj = new GLTF(asset);
            //adding scene to scene list
            obj.scenes = new List<Scene>();
            obj.scenes.Add(scene);
            //Add nodes to GLTF
            obj.nodes = new List<Node>();
            obj.nodes.Add(node);
            //Add meshes to GLTF
            obj.meshes = new List<Mesh>();
            obj.meshes.Add(mesh);
            return obj;
        }
    }
}
