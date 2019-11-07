using System;
using System.Collections;
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
            //getting current document from command data
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            string docName = doc.Title;

            //filter elements visible in view 
            if (doc.ActiveView is View3D)
            {
                RevitToGLTF.RevitToGLTFContext context = new RevitToGLTF.RevitToGLTFContext(doc, @"C:\Users\xpeng\Desktop\4D Model\revit2glTF.gltf");
                CustomExporter glTFExporter = new CustomExporter(doc, context);

                glTFExporter.ShouldStopOnError = false;
                glTFExporter.Export(doc.ActiveView as View3D);
                //ElementCategoryFilter titleBlockFilter = new ElementCategoryFilter(BuiltInCategory.OST_TitleBlocks);
                //ElementCategoryFilter levelFilter = new ElementCategoryFilter(BuiltInCategory.OST_Levels, true);
                //ElementCategoryFilter genericAnnoFilter = new ElementCategoryFilter(BuiltInCategory.OST_GenericAnnotation);
                //ElementCategoryFilter detailFilter = new ElementCategoryFilter(BuiltInCategory.OST_DetailComponents);

                //FilteredElementCollector allElementsInView = new FilteredElementCollector(doc, doc.ActiveView.Id)
                //                                                .WhereElementIsNotElementType()
                //                                                .WherePasses(levelFilter);
                                                                
                                                                
                //IList elementsInView = (IList)allElementsInView.ToElements();
                //foreach (Element e in allElementsInView)
                //{
                //    if (e.Category != null)
                //    {
                //        string category = e.Category.Name;

                //        if (category != "Title Blocks" && category != "Generic Annotations" && category != "Detail Items")
                //        {
                //            MessageBox.Show(category);
                //            Reference reference = new Reference(e);
                //            var pri = e.GetGeometryObjectFromReference(reference);
                            

                //        }
                //    }
                   
                    

                //}
                
            }
            else
            {
                MessageBox.Show("Select a 3D view to export GLTF");
            }
            
            //Convert Revit Elements

            //Create one GLTF object 
            GLTF obj = GLTFUtilts.Convert2GLTF();




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
