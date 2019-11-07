using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Newtonsoft.Json;
using XPRevitAddins.GLTFExporter;

namespace XPRevitAddins.RevitToGLTF
{
    public class RevitToGLTFContext : IExportContext
    {
        #region custom setting
        /// <summary>
        /// Scale entire top level BIM object node in JSON
        /// output. A scale of 1.0 will output the model in 
        /// millimetres. Currently we scale it to decimetres
        /// so that a typical model has a chance of fitting 
        /// into a cube with side length 100, i.e. 10 metres.
        /// </summary>
        double _scale_bim = 1.0;

        /// <summary>
        /// Scale applied to each vertex in each individual 
        /// BIM element. This can be used to scale the model 
        /// down from millimetres to metres, e.g.
        /// Currently we stick with millimetres after all
        /// at this level.
        /// </summary>
        double _scale_vertex = 1.0;

        /// <summary>
        /// If true, switch Y and Z coordinate 
        /// and flip X to negative to convert from
        /// Revit coordinate system to standard 3d
        /// computer graphics coordinate system with
        /// Z pointing out of screen, X towards right,
        /// Y up.
        /// </summary>
        bool _switch_coordinates = true;
        #endregion

        #region VertexLookupXyz
        /// <summary>
        /// A vertex lookup class to eliminate 
        /// duplicate vertex definitions.
        /// </summary>
        class VertexLookupXyz : Dictionary<XYZ, int>
        {
            #region XyzEqualityComparer
            /// <summary>
            /// Define equality for Revit XYZ points.
            /// Very rough tolerance, as used by Revit itself.
            /// </summary>
            class XyzEqualityComparer : IEqualityComparer<XYZ>
            {
                const double _sixteenthInchInFeet
                  = 1.0 / (16.0 * 12.0);

                public bool Equals(XYZ p, XYZ q)
                {
                    return p.IsAlmostEqualTo(q,
                      _sixteenthInchInFeet);
                }

                public int GetHashCode(XYZ p)
                {
                    return 12; // Util.PointString(p).GetHashCode(); need to fix this one later
                }
            }
            #endregion // XyzEqualityComparer

            public VertexLookupXyz()
              : base(new XyzEqualityComparer())
            {
            }

            /// <summary>
            /// Return the index of the given vertex,
            /// adding a new entry if required.
            /// </summary>
            public int AddVertex(XYZ p)
            {
                return ContainsKey(p)
                  ? this[p]
                  : this[p] = Count;
            }
        }
        #endregion // VertexLookupXyz

        #region VertexLookupInt
        /// <summary>
        /// An integer-based 3D point class.
        /// </summary>
        class PointInt : IComparable<PointInt>
        {
            public long X { get; set; }
            public long Y { get; set; }
            public long Z { get; set; }

            //public PointInt( int x, int y, int z )
            //{
            //  X = x;
            //  Y = y;
            //  Z = z;
            //}

            /// <summary>
            /// Consider a Revit length zero 
            /// if is smaller than this.
            /// </summary>
            const double _eps = 1.0e-9;

            /// <summary>
            /// Conversion factor from feet to millimetres.
            /// </summary>
            const double _feet_to_mm = 25.4 * 12;

            /// <summary>
            /// Conversion a given length value 
            /// from feet to millimetre.
            /// </summary>
            static long ConvertFeetToMillimetres(double d)
            {
                if (0 < d)
                {
                    return _eps > d
                      ? 0
                      : (long)(_feet_to_mm * d + 0.5);

                }
                else
                {
                    return _eps > -d
                      ? 0
                      : (long)(_feet_to_mm * d - 0.5);

                }
            }

            public PointInt(XYZ p, bool switch_coordinates)
            {
                X = ConvertFeetToMillimetres(p.X);
                Y = ConvertFeetToMillimetres(p.Y);
                Z = ConvertFeetToMillimetres(p.Z);

                if (switch_coordinates)
                {
                    X = -X;
                    long tmp = Y;
                    Y = Z;
                    Z = tmp;
                }
            }

            public int CompareTo(PointInt a)
            {
                long d = X - a.X;

                if (0 == d)
                {
                    d = Y - a.Y;

                    if (0 == d)
                    {
                        d = Z - a.Z;
                    }
                }
                return (0 == d) ? 0 : ((0 < d) ? 1 : -1);
            }
        }

        /// <summary>
        /// A vertex lookup class to eliminate 
        /// duplicate vertex definitions.
        /// </summary>
        class VertexLookupInt : Dictionary<PointInt, int>
        {
            #region PointIntEqualityComparer
            /// <summary>
            /// Define equality for integer-based PointInt.
            /// </summary>
            class PointIntEqualityComparer : IEqualityComparer<PointInt>
            {
                public bool Equals(PointInt p, PointInt q)
                {
                    return 0 == p.CompareTo(q);
                }

                public int GetHashCode(PointInt p)
                {
                    return (p.X.ToString()
                      + "," + p.Y.ToString()
                      + "," + p.Z.ToString())
                      .GetHashCode();
                }
            }
            #endregion // PointIntEqualityComparer

            public VertexLookupInt()
              : base(new PointIntEqualityComparer())
            {
            }

            /// <summary>
            /// Return the index of the given vertex,
            /// adding a new entry if required.
            /// </summary>
            public int AddVertex(PointInt p)
            {
                return ContainsKey(p)
                  ? this[p]
                  : this[p] = Count;
            }
        }
        #endregion // VertexLookupInt

        #region global properties
        //Accessible object from Revit
        Document doc;
        string fileName;

        //Accessible object of GLTF
        GLTF GLTFObj; //a container or middelware to store all revit data to GLTF
        
        Dictionary<string, GLTFExporter.Node> _objects;
        Dictionary<string, GLTFExporter.Mesh> _geometries;
        Dictionary<string, GLTFExporter.Accessor> _accessors;
        Dictionary<string, GLTFExporter.BufferView> _bufferViews;
        Dictionary<string, GLTFExporter.Buffer> _buffers;
        Dictionary<string, GLTFExporter.Material> _materials;
        Dictionary<string, GLTFExporter.Texture> _textures;
        Dictionary<string, GLTFExporter.Image> _images;
        Dictionary<string, GLTFExporter.Sampler> _samplers;
        GLTFExporter.Camera _camera;

        //Accessible object of current element
        GLTFExporter.Node _currentElement;


        //Keyed on material uid to handle several material per element:
        Dictionary<string, GLTFExporter.Node> _currentObject;
        Dictionary<string, GLTFExporter.Mesh> _currentGeometry;
        Dictionary<string, VertexLookupInt> _vertices;

        Stack<ElementId> _elementStack = new Stack<ElementId>();
        Stack<Transform> _transformationStack = new Stack<Transform>();

        string _currentMaterialUid;

        GLTFExporter.Node CurrentObjectPerMaterial
        {
            get
            {
                return _currentObject[_currentMaterialUid];
            }
        }

        GLTFExporter.Mesh CurrentGeometryPerMaterial
        {
            get
            {
                return _currentGeometry[_currentMaterialUid];
            }
        }

        VertexLookupInt CurrentVerticesPerMaterial
        {
            get
            {
                return _vertices[_currentMaterialUid];
            }
        }

        Transform CurrentTransform
        {
            get
            {
                return _transformationStack.Peek();
            }
        }
        #endregion

        #region set current material
        void SetCurrentMaterial(string uidMaterial)
        {
            if (!_materials.ContainsKey(uidMaterial))
            {
                Autodesk.Revit.DB.Material material = doc.GetElement(uidMaterial) as Autodesk.Revit.DB.Material; //Revit material
                GLTFExporter.Material gMaterial = new GLTFExporter.Material();//GLTF material

                //convert Revit material to GLTF material
                gMaterial.name = material.Name;
                //TODO: more property need to be obtain and convert to GLTF material

                _materials.Add(uidMaterial, gMaterial);

            }

            _currentMaterialUid = uidMaterial;

            string uid_per_material = _currentElement.name + "-" + uidMaterial; //

            
        }
        #endregion

        public RevitToGLTFContext(Document document, string filename)
        {
            doc = document;
            fileName = filename;
        }
        void IExportContext.Finish()
        {
            throw new NotImplementedException();
        }

        bool IExportContext.IsCanceled()
        {
            return false;
        }

        RenderNodeAction IExportContext.OnElementBegin(ElementId elementId)
        {
            throw new NotImplementedException();
        }

        void IExportContext.OnElementEnd(ElementId elementId)
        {
            throw new NotImplementedException();
        }

        RenderNodeAction IExportContext.OnFaceBegin(FaceNode node)
        {
            throw new NotImplementedException();
        }

        void IExportContext.OnFaceEnd(FaceNode node)
        {
            throw new NotImplementedException();
        }

        RenderNodeAction IExportContext.OnInstanceBegin(InstanceNode node)
        {
            throw new NotImplementedException();
        }

        void IExportContext.OnInstanceEnd(InstanceNode node)
        {
            throw new NotImplementedException();
        }

        void IExportContext.OnLight(LightNode node)
        {
            throw new NotImplementedException();
        }

        RenderNodeAction IExportContext.OnLinkBegin(LinkNode node)
        {
            throw new NotImplementedException();
        }

        void IExportContext.OnLinkEnd(LinkNode node)
        {
            throw new NotImplementedException();
        }

        void IExportContext.OnMaterial(MaterialNode node)
        {
            throw new NotImplementedException();
        }

        void IExportContext.OnPolymesh(PolymeshTopology node)
        {
            throw new NotImplementedException();
        }

        void IExportContext.OnRPC(RPCNode node)
        {
            throw new NotImplementedException();
        }

        RenderNodeAction IExportContext.OnViewBegin(ViewNode node)
        {
            // If we did not do so before we invoked the custom export
            // we can get information about the view from the supplied view node,
            // That includes : rendering settings, sun settings, camera data, etc.

            CameraInfo camera = node.GetCameraInfo();
            var angle = camera.IsPerspective;

            return RenderNodeAction.Proceed;
        }

        void IExportContext.OnViewEnd(ElementId elementId)
        {
            throw new NotImplementedException();
        }

        bool IExportContext.Start()
        {
            _materials = new Dictionary<string, GLTFExporter.Material>();
            _geometries = new Dictionary<string, GLTFExporter.Mesh>();
            _objects = new Dictionary<string, Node>();
            _transformationStack.Push(Transform.Identity); //need to understand what's this transform. 
            Asset asset = new Asset("0.0"); 
            GLTFObj = new GLTF(asset); //
            GLTFObj.meshes = new List<GLTFExporter.Mesh>();


            return true;
        }
    }
}
