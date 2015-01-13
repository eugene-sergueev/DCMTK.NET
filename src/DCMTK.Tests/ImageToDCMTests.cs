using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCMTK.Fluent;
using NUnit.Framework;

namespace DCMTK.Tests
{
    [TestFixture]
    public class ImageToDCMTests : TestBase
    {
        [Test]
        public void Can_generate_dcm_from_bmp()
        {
            // arrange
            var dcmFile = GetTemporaryResource("sampleStill.dcm");
            var request = _dcmtk.ImageToDCM(GetTestResource("sampleStill.bmp"), dcmFile)
                .Set(x => x.InputFormat, ImageToDCMCommandBuilder.InputFormatEnum.Bmp)
                .Build();

            // act
            request.Start();

            // assert
            request.Wait();
            Assert.That(string.IsNullOrEmpty(request.ErrorMessage), Is.True);
            Assert.That(File.Exists(dcmFile), Is.True);
        }

        [Test]
        public void Can_generate_dcm_from_jpeg()
        {
            // arrange
            var dcmFile = GetTemporaryResource("sampleStill.dcm");
            var request = _dcmtk.ImageToDCM(GetTestResource("sampleStill.jpg"), dcmFile)
                .Set(x => x.InputFormat, ImageToDCMCommandBuilder.InputFormatEnum.Jpeg)
                .Build();

            // act
            request.Start();

            // assert
            request.Wait();
            Assert.That(string.IsNullOrEmpty(request.ErrorMessage), Is.True);
            Assert.That(File.Exists(dcmFile), Is.True);
        }

        [Test]
        public void Can_get_error_message()
        {
            // arrange
            var dcmFile = GetTemporaryResource("sampleStill.dcm");
            var request = _dcmtk.ImageToDCM(GetTestResource("sampleStill.jpg"), dcmFile)
                .Set(x => x.InputFormat, ImageToDCMCommandBuilder.InputFormatEnum.Bmp) // this is not a bitmap, error will happen!
                .Build();

            // act
            request.Start();

            // assert
            request.Wait();
            Assert.That(request.ErrorMessage, Is.EqualTo("Error converting file: Not a BMP file - invalid header"));
        }
    }
}
