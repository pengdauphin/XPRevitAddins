using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Newtonsoft.Json;

using XPRevitAddins.GLTFExporter;


namespace XPRevitAddins
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class Revit2GLTF : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //init an asset
            Asset asset = new Asset("2.0");//only require property to init a GLTF
            
            //init a scene 
            Scene scene = new Scene();
            scene.nodes = new List<int>();
            scene.nodes.Add(0);

            //init nodes
            Node node = new Node();
            node.name = "BoomBox";
            

            GLTF obj = new GLTF(asset);
            //adding scene to scene list
            obj.scenes = new List<Scene>();
            obj.scenes.Add(scene);
            obj.nodes = new List<Node>();
            obj.nodes.Add(node);
            

            
            var jso = JsonConvert.SerializeObject(obj);
            //File.WriteAllText(@"C:\Users\xpeng\Desktop\4D Model\revit2glTF.json", jso);

            using(StreamWriter file = File.CreateText(@"C:\Users\xpeng\Desktop\4D Model\revit2glTF.gltf"))
            {
                JsonSerializer serializer = new JsonSerializer
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                serializer.Serialize(file, obj);
            }

            //@TODO generate blob/binary and return index of section
            //@TODO convert object/geometry into binary format
            MessageBox.Show(obj.nodes[0].name);
            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
