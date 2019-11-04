using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace XPRevitAddins
{
    public static class GeometryUtils
    {
        #region unit convertor
        public const double FeetToMeter = 0.3048;
        public const double MeterToFeet = 3.28084;
        public static double MeterToRevitUnit(double dub)
        {
            return UnitUtils.ConvertToInternalUnits(dub, DisplayUnitType.DUT_METERS);
        }
        public static double RevitUnitToMeter(double dub)
        {
            return UnitUtils.ConvertFromInternalUnits(dub, DisplayUnitType.DUT_METERS);
        }

        #endregion

        #region get geometry
        //geometry to faces/edges/
        

        #endregion

    }
}
