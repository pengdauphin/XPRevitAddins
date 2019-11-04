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
            GLTFObject obj = new GLTFObject();
            obj.asset = new Dictionary<string, string>();

            obj.asset.Add("generator", "Revit to glTF generator");
            obj.asset.Add("version", "1.0");

            var glbBlob = new Blob();
            var jso = JsonConvert.SerializeObject(obj);
            //File.WriteAllText(@"C:\Users\xpeng\Desktop\4D Model\revit2glTF.json", jso);

            using(StreamWriter file = File.CreateText(@"C:\Users\xpeng\Desktop\4D Model\revit2glTF.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, obj);
            }

            //@TODO generate blob/binary and return index of section
            //@TODO convert object/geometry into binary format
            MessageBox.Show("2 GLTF!");
            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
