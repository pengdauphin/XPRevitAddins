
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            //DataContext = new MainWindowViewModel();
            //DataContext = new ViewModel();

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
            
            Reference reference = uiDoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element);

            //IList<Reference> references = uiDoc.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element);

            //foreach(Reference reference in references)
            //{
            //    MessageBox.Show(doc.GetElement(reference).Name);
            //}

            Element element = doc.GetElement(reference);
            if (element.IsValidObject)
            {
                string elemType = element.Category.Name;
                //string[] str = new string[] { "dsf", "rerte" };
                ParameterSet parameterSet = element.Parameters;
                foreach (Parameter para in parameterSet)
                {

                    if (para.UserModifiable == true)
                    {
                        cbox_Parameter.Items.Add(para.Definition.Name.ToString());
                    }

                }
                cbox_Parameter.SelectedIndex = 0;
                lable_SelectedEle.Content = elemType;
            }



        }

        private void text_Prefix_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void selectedElements_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void button_Select_Click(object sender, RoutedEventArgs e)
        {
            IList<Reference> references = uiDoc.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element);
            int numb = 0;
            
            foreach(Reference reference in references)
            {
                Element element = doc.GetElement(reference);
                var name = element.Name;
                numb = numb + 1;
                selectedElements.Items.Add(new {NAME = name, parameterValue = numb.ToString() });
                //ElementValuePairs item = new ElementValuePairs(name, numb.ToString());

                //list.Add(item);
            }

            

        }

        //class ElementValuePairs
        //{
        //    public string Element_ID { get; set; }
        //    public string ParameterValue { get; set; }

        //    public ElementValuePairs(string element_ID, string parameterValue)
        //    {
        //        Element_ID = element_ID;
        //        ParameterValue = parameterValue;
        //    }
        //}

        //public class MainWindowViewModel
        //{
        //    public ICollectionView ElementValuePair { get; private set; }
          


        //    public MainWindowViewModel()
        //    {
        //        var _ElementValuePair = new List<ElementValuePair>
        //                         {
        //                             new ElementValuePair
        //                                 {
        //                                     Element_ID = "Christian",
        //                                     ParameterValue = "Moser",
                                             
        //                                 },
        //                             new ElementValuePair
        //                                 {
        //                                     Element_ID = "Peter",
        //                                     ParameterValue = "Meyer",
                                            
        //                                 },
        //                             new ElementValuePair
        //                                 {
        //                                     Element_ID = "Lisa",
        //                                     ParameterValue = "Simpson",
                                             
        //                                 },
        //                             new ElementValuePair
        //                                 {
        //                                     Element_ID = "Betty",
        //                                     ParameterValue = "Bossy",
                                             
        //                                 },
        //                             new ElementValuePair
        //                                 {
        //                                     Element_ID = "Xiaoxuan",
        //                                     ParameterValue = "Peng",

        //                                 }
        //                         };

        //        ElementValuePair = CollectionViewSource.GetDefaultView(_ElementValuePair);

                


        //    }
        //}

       
        //public class ElementValuePair 
        //{
        //    private string _element_ID;
        //    private string _parameterValue;
           

        //    public string Element_ID
        //    {
        //        get { return _element_ID; }
        //        set
        //        {
        //            _element_ID = value;
                   
        //        }
        //    }

        //    public string ParameterValue
        //    {
        //        get { return _parameterValue; }
        //        set
        //        {
        //            _parameterValue = value;
                   
        //        }
        //    }


        //}


    }
}
