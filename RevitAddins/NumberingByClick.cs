using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;


namespace XPRevitAddins
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class NumberingByClick : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            MainWindow mainWindow = new MainWindow(commandData);
            mainWindow.Show();
            
            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
