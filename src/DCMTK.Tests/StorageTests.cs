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
            var request = _dcmtk.StoreSCU("192.168.5.51", 5678, bmp)
                .Set(x => x.CallingAETitle, "DRSHD")
                .Set(x => x.CalledAETitle, "MedXChange")
                .Build();

            // act
            request.Start();

            // assert
            request.Wait();
            Assert.That(string.IsNullOrEmpty(request.ErrorMessage), Is.True);
        }

        [Test]
        public void Can_upload_jpeg_to_storage_server()
        {
            // arrange
            var bmp = CreateSampleDCMImage(ImageToDCMCommandBuilder.InputFormatEnum.Jpeg);
            var request = _dcmtk.StoreSCU("192.168.5.51", 5678, bmp)
                .Set(x => x.CallingAETitle, "DRSHD")
                .Set(x => x.CalledAETitle, "MedXChange")
                .Set(x => x.ProposeJpeg8, true)
                .Build();

            // act
            request.Start();

            // assert
            request.Wait();
            Assert.That(string.IsNullOrEmpty(request.ErrorMessage), Is.True, "Error message is " + request.ErrorMessage);
        }
    }
}
