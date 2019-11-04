using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Media.Imaging;

namespace XPRevitAddins
{
    public class CsAddPanel : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            // Create a custom ribbon tab
            String tabName = "XP Tools";
            application.CreateRibbonTab(tabName);

            
            //compose first ribbon panel
            RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName,"Functions");
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            //Button #1
            PushButtonData buttonData = new PushButtonData("cmd_Numbering", "Numbering", thisAssemblyPath, "XPRevitAddins.NumberingByClick");
            PushButton pushButton = ribbonPanel.AddItem(buttonData) as PushButton;
            pushButton.ToolTip = "Perform numbering of a parameter.";
            //Button #2
            PushButtonData buttonData_CreateSheets = new PushButtonData("cmd_createSheets", "Create Sheets", thisAssemblyPath, "XPRevitAddins.CreateSheets");
            PushButton pushButton_CreateSheets = ribbonPanel.AddItem(buttonData_CreateSheets) as PushButton;
            pushButton_CreateSheets.ToolTip = "Create Sheets from website.";
            //Button #3
            PushButtonData buttonData_PrintSet = new PushButtonData("cmd_PrintSet", "Print Set", thisAssemblyPath, "XPRevitAddins.CreateSheets");
            PushButton pushButton_PrintSet = ribbonPanel.AddItem(buttonData_PrintSet) as PushButton;
            pushButton_PrintSet.ToolTip = "Print Sheets/Views Set";


            //PushButtonData button1 = new PushButtonData("Button1", "My Button #1", @"C:\ExternalCommands.dll", "Revit.Test.Command1");
            //PushButtonData button2 = new PushButtonData("Button2", "My Button #2",
            //    @"C:\ExternalCommands.dll", "Revit.Test.Command2");


            RibbonPanel ribbonPanel2 = application.CreateRibbonPanel(tabName, "IO");
            //Button #2-1
            PushButtonData buttonData_2_1 = new PushButtonData("exportGLTF", "GLTFExportor", thisAssemblyPath, "XPRevitAddins.Revit2GLTF");
            PushButton pushButton_ELTFExporter = ribbonPanel2.AddItem(buttonData_2_1) as PushButton;
            pushButton_ELTFExporter.ToolTip = "Export 3D view to GLTF format";

            return Result.Succeeded;
        }

        
    }

  
}
