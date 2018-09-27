
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
using System.Text.RegularExpressions;
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
            text_initial.Text = "0";
            text_Prefix.Text = "";
            text_Suffix.Text = "";
            //DataContext = new MainWindowViewModel();
            //DataContext = new ViewModel();

        }

        ExternalCommandData m_CommandData = null;
        UIApplication uiApp;
        UIDocument uiDoc;
        Autodesk.Revit.ApplicationServices.Application app;
        Document doc;
        List<ElementValuePair> elementValuePairs = null;
        Parameter TargetParameter;
        ElementId TargetParameterID;
        ParameterSet parameterSet;
       // ICollectionView ElementValueMap;
        IList<ElementValuePair> ElementValueMap;


        private void lable_SelectedEle_TextInput(object sender, TextCompositionEventArgs e)
        {
            lable_SelectedEle.Content = "New Model Type";
        }

        private void selectElement_Click(object sender, RoutedEventArgs e)
        {
            
            Reference reference = uiDoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element);

            

            Element element = doc.GetElement(reference);
            if (element.IsValidObject)
            {
                string elemType = element.Category.Name;
                //string[] str = new string[] { "dsf", "rerte" };
                parameterSet = element.Parameters;
                foreach (Parameter para in parameterSet)
                {
                    cbox_Parameter.Items.Add(para.Definition.Name.ToString());
                    

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

            int numb = Convert.ToInt32(text_initial.Text);
            

            foreach (Reference reference in references)
            {
                
                Element element = doc.GetElement(reference);
                var name = element.Name;
                numb = numb + 1;
                //MessageBox.Show(numb.ToString());
                string _paraValue = text_Prefix.Text +" "+ numb +" "+ text_Suffix.Text;
                selectedElements.Items.Add(new {NAME = name, parameterValue = _paraValue });
                ElementValuePair valuePair = new ElementValuePair(reference.ElementId, _paraValue);
                ElementValueMap.Add(valuePair);
            }

            

        }

        private void text_initial_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
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

        public class ElementParameterMaps
        {
            public ICollectionView ElementValuePair { get; private set; }



            public ElementParameterMaps()
            {
                var _ElementValuePair = new List<ElementValuePair>
                                 {
                                     //new ElementValuePair
                                     //    {
                                     //        Element_ID = "Christian",
                                     //        ParameterValue = "Moser",

                                     //    },
                                     //new ElementValuePair
                                     //    {
                                     //        Element_ID = "Peter",
                                     //        ParameterValue = "Meyer",

                                     //    },
                                     //new ElementValuePair
                                     //    {
                                     //        Element_ID = "Lisa",
                                     //        ParameterValue = "Simpson",

                                     //    },
                                     //new ElementValuePair
                                     //    {
                                     //        Element_ID = "Betty",
                                     //        ParameterValue = "Bossy",

                                     //    },
                                     //new ElementValuePair
                                     //    {
                                     //        Element_ID = "Xiaoxuan",
                                     //        ParameterValue = "Peng",

                                     //    }
                                 };

                ElementValuePair = CollectionViewSource.GetDefaultView(_ElementValuePair);




            }
        }


        public class ElementValuePair
        {
            private ElementId _element_ID;
            private string _parameterValue;


            public ElementId Element_ID
            {
                get { return _element_ID; }
                set
                {
                    _element_ID = value;

                }
            }

            public string ParameterValue
            {
                get { return _parameterValue; }
                set
                {
                    _parameterValue = value;

                }
            }

            public ElementValuePair(ElementId elementId, string paramaterValue)
            {
                _element_ID = elementId;
                _parameterValue = paramaterValue;
            }
        }

        private void button_Process_Click(object sender, RoutedEventArgs e)
        {
            using (Transaction transaction = new Transaction(doc, "Set Parameter"))
            {
                transaction.Start();
                try
                {
                    foreach (ElementValuePair i in elementValuePairs)
                    {
                        Element element = doc.GetElement(i.Element_ID);
                        TargetParameter.Set(i.ParameterValue);

                    }
                }
                catch (Exception)
                {

                    throw;
                }

                transaction.Commit();

            }
        }

        private void cbox_Parameter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selPara = cbox_Parameter.SelectedItem.ToString();
            foreach (Parameter para in parameterSet)
            {
                if (para.Definition.Name == selPara)
                {
                    TargetParameter = para;
                    TargetParameterID = para.Element.Id;
                }

            }
            
        }
    }
}
