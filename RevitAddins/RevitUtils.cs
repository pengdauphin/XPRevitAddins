using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace XPRevitAddins
{
    public static class RevitUtils
    {
        #region get elements from document
        public static List<T> GetElements<T>(Document document, Type type)
        {
            return null;
        }

        
        /**
         * passing element ID as string to retrive elements from document
         */
        public static List<Element> GetElements(this Document document, List<string> ids)
        {
            List<Element> result = new List<Element>();

            foreach (string id in ids)
            {
                int integerId = 0;
                if (int.TryParse(id, out integerId))
                {
                    result.Add(document.GetElement(new ElementId(integerId)));
                }
            }
            return result;
        }
        /**
         * passing element ID as string to retrive element from document
         */
        public static Element GetElement(Document document, string id)
        {
            Element result = null;

            int integerId = 0;
            if (int.TryParse(id, out integerId))
            {
                result = document.GetElement(new ElementId(integerId));
            }
            return result;
        }

        /**
        * passing element ID as ElementID to retrive element from document
        */
        public static List<Element> GetElements(this Document document, IEnumerable<ElementId> ids)
        {
            List<Element> result = new List<Element>();

            foreach (ElementId id in ids)
            {
                Element element = document.GetElement(id);
                if (element != null) result.Add(element);

            }
            return result;
        }

        public static List<T> GetElementsByCategory<T>(this Document document, BuiltInCategory category, List<ElementId> ids = null) where T : Element
        {
            if (category == BuiltInCategory.INVALID) return new List<T>();
            if (ids == null)
            {
                return new FilteredElementCollector(document).OfCategory(category).Cast<T>().ToList();
            }
            else
                return new FilteredElementCollector(document).OfCategory(category).Where(e => ids.Contains(e.Id)).Cast<T>().ToList();

        }

        public static List<Element> GetByType(this Document document, Type type)
        {
            List<Element> result = new List<Element>();

            if (type == null) return new List<Element>();
            result.AddRange(new FilteredElementCollector(document).OfClass(type));

            return result;
        }


        #endregion

        #region get document's categories
        public static List<Category> GetAllCategories(this Document document)
        {
            return document.Settings.Categories.Cast<Category>().ToList();
        }

        #endregion

        #region get all element types
        public static List<Element> GetAllElementTypes(this Document document)
        {
            List<Element> filteredElementCollector = new FilteredElementCollector(document).WhereElementIsElementType().ToList();

            filteredElementCollector.Sort(delegate (Element e1, Element e2)
            {
                return e1.GetType().Name.CompareTo(e2.GetType().Name);
            });

            return filteredElementCollector;
        }

        #endregion

        #region get all family instance 
        public static List<Element> GetAllFamilyInstances(this Document document)
        {
            List<Element> filteredElementCollector = new FilteredElementCollector(document).OfClass(typeof(FamilyInstance)).ToList();
            return filteredElementCollector;
        }
        #endregion

        #region get views
        public static List<Element> GetAllViews(this Document document)
        {
            return new FilteredElementCollector(document).OfClass(typeof(View)).ToList();
        }

        public static List<Element> GetAllSheets(this Document document)
        {
            return new FilteredElementCollector(document).OfClass(typeof(ViewSheet)).ToList();
        }

        #endregion

        #region get project bases
        public static List<Element> GetAllLevels(this Document document)
        {
            return new FilteredElementCollector(document).OfClass(typeof(Level)).ToList();
        }

        public static List<Element> GetAllGrids(this Document document)
        {
            return new FilteredElementCollector(document).OfClass(typeof(Grid)).ToList();
        }
        #endregion

        #region get parameters
        public static string GetParameterAsString(Element element, string v)
        {
            if (element == null) return string.Empty;
            Parameter p = element.LookupParameter(v);
            if (p != null)
            {
                switch (p.StorageType)
                {
                    case StorageType.Double:
                        //if (p.DisplayUnitType != DisplayUnitType.DUT_UNDEFINED)
                        //{
                        return p.AsValueString();
                    //}
                    //else
                    //{

                    //}
                    case StorageType.Integer:
                        return p.AsInteger().ToString();
                    case StorageType.ElementId:
                        Element e = element.Document.GetElement(p.AsElementId());
                        return e != null ? e.Name : "None";
                    case StorageType.String:
                        return p.AsString() != null ? p.AsString() : "-";

                }
            }
            return string.Empty;
        }

        public static object GetParameterValue(Element element, string v)
        {
            if (element == null) return string.Empty;
            Parameter p = element.LookupParameter(v);
            return GetParameterValue(p);
        }

        public static object GetParameterValue(Parameter p)
        {
            if (p != null)
            {
                switch (p.StorageType)
                {
                    case StorageType.Double:
                        return p.AsDouble();
                    case StorageType.Integer:
                        return p.AsInteger();
                    case StorageType.ElementId:
                        return p.Element.Document.GetElement(p.AsElementId());
                    case StorageType.String:
                        return p.AsString() != null ? p.AsString() : "-";

                }
            }
            return string.Empty;
        }
        #endregion
    }
}
