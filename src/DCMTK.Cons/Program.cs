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
                using (var initializeNetworkResult = find.InitializeNetwork(30))
                {
                    if (initializeNetworkResult.Good())
                    using (var queryResult = find.PerformQuery("peer",
                            234, 
                            "outtite", 
                            "peertite", 
                            "abstract syntax", 
                            0, 
                            0,
                            0,
                            0, 
                            false,
                            false,
                            0,
                            false, 0, new List<string> {"PatientName"}, (count, identifiers) =>
                            {
                                string patientName = null;
                                var getresult = identifiers.FindAndGetString(DcmTagKeyNet.PatientName, ref patientName);
                                if (getresult.Good())
                                {
                                    Console.WriteLine("Patient name = " + patientName);
                                }

                            }, 
                            new List<string>()))
                    {

                    }
                }
            }
        }
    }
}
