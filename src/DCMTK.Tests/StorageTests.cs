using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCMTK.Fluent;
using NUnit.Framework;

namespace DCMTK.Tests
{
    [TestFixture]
    public class StorageTests : TestBase
    {
        [Test]
        public void Can_upload_bmp_to_storage_server()
        {
            // arrange
            var bmp = CreateSampleDCMImage(ImageToDCMCommandBuilder.InputFormatEnum.Bmp);
        }
    }
}
