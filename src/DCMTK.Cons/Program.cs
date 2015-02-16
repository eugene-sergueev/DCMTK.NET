using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCMTK.Cons
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Do2();
            }
        }

        static void Do2()
        {
            using (var find = new DcmFindSCUNet())
            {
                using (var cond = find.InitializeNetwork(5))
                {
                    if (cond.Bad())
                        throw new Exception("Couldn't initialize network connection. " + cond.Text());
                }

                var worklist = find.QueryWorklist("pacs.medxchange.com", 5678, "DRSHD", "MedXChange", "", "", "", "");
                var count = worklist.Count;
            }
        }
    }
}
