using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DCMTK.Tests
{
    [TestFixture]
    public class TestBase
    {
        // ReSharper disable InconsistentNaming
        protected DCMTK _dcmtk;
        // ReSharper restore InconsistentNaming

        [SetUp]
        public virtual void Setup()
        {
            _dcmtk = new DCMTK(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + @"\..\..\..\..\libs\dcmtk-3.6.0-win32-i386\bin"));
        }
    }
}
