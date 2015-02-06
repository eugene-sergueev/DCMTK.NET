using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCMTK.DICOM
{
    public class FindResult
    {
        public FindResult()
        {
            Properties = new List<FindProperty>();
        }

        public IList<FindProperty> Properties { get; private set; }  
    }

    public class FindProperty
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string VR { get; set; }
        public string Value { get; set; }
    }
}
