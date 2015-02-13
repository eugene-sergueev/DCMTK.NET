using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCMTK.Cons
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var dcmtk = new DCMTKContext(AppDomain.CurrentDomain.BaseDirectory);
                var request = dcmtk.Find("192.168.5.51", 5678)
                    .Set(x => x.CallingAETitle, "DRSHD")
                    .Set(x => x.CalledAETitle, "MedXChange")
                    .AddKey("PatientName", "")
                    .Build();

                request.Start();

                request.Wait();

                if(request.HasErrors)
                    throw new Exception(request.ErrorMessage);

                foreach (var result in request.Result)
                {
                    Console.WriteLine("---");
                    foreach (var property in result.Properties)
                    {
                        Console.WriteLine(property.Name + "=" + property.Value);
                    }
                }
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
