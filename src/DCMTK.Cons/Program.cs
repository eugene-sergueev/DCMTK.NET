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
                Do();
            }
           
        }

        static void Do()
        {
            using (var find = new DcmFindSCUNet())
            {
                using (var result = find.InitializeNetwork(30))
                {
                    var good = result.Good();
                    var text = result.Bad();
                    var code = result.Code();
                    var module = result.Module();
                }
            }
        }
    }
}
