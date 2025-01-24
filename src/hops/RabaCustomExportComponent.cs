using System;
using Grasshopper.Kernel;
using Rhino.Geometry;


namespace CustomExportNamespace


{
    public class RabaCustomExportComponent : GH_Component
    {
        public RabaCustomExportComponent()
          : base("Custom Export", "CustomExport", "Export custom objects", "Params", "Util")
        {
        }

        public override Guid ComponentGuid => new Guid("YOUR_GUID_HERE");

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("customObj", "CO", "Custom object to export", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Output", "O", "Serialized custom object", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            object retrievedObj = null;
            if (!DA.GetData(0, ref retrievedObj))
                return;

            string serialized = SerializeCustomObject(retrievedObj);
            DA.SetData(0, serialized);
        }

        public string SerializeCustomObject(object obj)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, $"Failed to serialize object: {ex.Message}");
                return null;
            }
        }
    }
}