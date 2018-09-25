
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace XPRevitAddins
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="application"></param>
        public MainWindow(ExternalCommandData commandData)
        {
            InitializeComponent();
            m_CommandData = commandData;
            uiApp = m_CommandData.Application;
            uiDoc = uiApp.ActiveUIDocument;
            app = uiApp.Application;
            doc = uiDoc.Document;

        }

        ExternalCommandData m_CommandData = null;
        UIApplication uiApp;
        UIDocument uiDoc;
        Autodesk.Revit.ApplicationServices.Application app;
        Document doc;


        private void lable_SelectedEle_TextInput(object sender, TextCompositionEventArgs e)
        {
            lable_SelectedEle.Content = "New Model Type";
        }

        private void selectElement_Click(object sender, RoutedEventArgs e)
        {
            List<string> parameterNames = new List<string>() { "dsf", "rerte"};
            Reference reference = uiDoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element);
            Element element = doc.GetElement(reference);
            string elemType = element.GetType().Name;
            ParameterSet parameterSet = element.Parameters;
            foreach(Parameter para in parameterSet)
            {
                parameterNames.Add(para.AsString());
            }

            string[] str = new string[] { "Foo", "Bar" };
            lable_SelectedEle.Content = elemType;
            cbox_Parameter.ItemsSource = str;

        }
    }
}
