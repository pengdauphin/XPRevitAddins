using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace XPRevitAddins
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class GLTFExporterOld : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //getting current document from command data
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            string docName = doc.Title;

          

            //get categories from documents
            List<Category> catList = RevitUtils.GetAllCategories(doc);
            foreach (Category cat  in catList)
            {
                string name = cat.Name;
                MessageBox.Show(name);
            }

            List<Element> elementCollector = doc.GetElementsByCategory<Element>(BuiltInCategory.OST_Walls);
            Element element1 = elementCollector[0];
            //get element geometry test 
            Options options = new Options();
            GeometryElement elementGeometry = element1.get_Geometry(options);
            String faceInfo = "";
            //more reference https://thebuildingcoder.typepad.com/blog/2012/03/retrieve-geometry-in-element-coordinate-system.html
            foreach (GeometryObject obj in elementGeometry)
            {
                
                Solid geoSolid = obj as Solid;
                
                if (null != geoSolid)
                {
                    int faces = 0;
                    double totalArea = 0;
                    foreach (Face geomFace in geoSolid.Faces)
                    {
                        faces++;
                        
                        faceInfo += "Face " + faces + " area: " + geomFace.Area.ToString() + "\n";
                        totalArea += geomFace.Area;
                    }
                    faceInfo += "Number of faces: " + faces + "\n";
                    faceInfo += "Total area: " + totalArea.ToString() + "\n";
                    foreach (Edge geomEdge in geoSolid.Edges)
                    {
                        // get wall's geometry edges: https://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2016/ENU/Revit-API/files/GUID-96A0E5EC-D9AD-42F2-901D-CC902ADD0BED-htm.html
                    }
                };

                

            }

            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
