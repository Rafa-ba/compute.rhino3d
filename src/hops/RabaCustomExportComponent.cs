using System;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Hops
{
    public class RabaCustomExportComponent : GH_Component
    {
        public RabaCustomExportComponent()
          : base("Custom Export", "CustomExport",
              "Export custom objects",
              "Params", "Util")
        {
        }

        public override Guid ComponentGuid => new Guid("YOUR_GUID_HERE");

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Custom Object", "CO", "Custom object to export", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Output", "O", "Serialized custom object", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            object customObject = null;
            if (!DA.GetData(0, ref customObject))
                return;

            string serialized = SerializeCustomObject(customObject);
            DA.SetData(0, serialized);
        }

        private string SerializeCustomObject(object obj)
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