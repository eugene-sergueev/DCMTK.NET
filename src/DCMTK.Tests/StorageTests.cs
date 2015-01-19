using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCMTK.DICOM;
using DCMTK.Fluent;
using DCMTK.Serialization;
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
            Assert.That(request.WasScoreSuccesful, Is.True);
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
            Assert.That(request.WasScoreSuccesful, Is.True);
        }

        [Test]
        public void Can_upload_multiple_images_as_the_same_series()
        {
            // arrange
            var studyInstanceId = DicomUid.GenerateUid().UID;
            var seriesInstanceId = DicomUid.GenerateUid().UID;
            Console.WriteLine("StudyInstanceUID=" + studyInstanceId);
            Console.WriteLine("SeriesInstanceUID=" + seriesInstanceId);
            var dcmImages = new List<string>();
            dcmImages.Add(CreateSampleDCMImage(ImageToDCMCommandBuilder.InputFormatEnum.Jpeg,
                builder => builder.AddKey("StudyInstanceUID", studyInstanceId)
                    .AddKey("SeriesInstanceUID", seriesInstanceId)));
            dcmImages.Add(CreateSampleDCMImage(ImageToDCMCommandBuilder.InputFormatEnum.Jpeg,
                builder => builder.AddKey("StudyInstanceUID", studyInstanceId)
                    .AddKey("SeriesInstanceUID", seriesInstanceId)));

            // act
            foreach (var dcmImage in dcmImages)
            {
                var request = _dcmtk.StoreSCU("192.168.5.51", 5678, dcmImage)
                    .Set(x => x.CallingAETitle, "DRSHD")
                    .Set(x => x.CalledAETitle, "MedXChange")
                    .Set(x => x.ProposeJpeg8, true)
                    .Build();
                request.Start();
                request.Wait();

                // assert
                Assert.That(request.WasScoreSuccesful, Is.True, request.Output);
            }
        }

        [Test]
        public void Can_get_error_message()
        {
            // arrange
            var bmp = CreateSampleDCMImage(ImageToDCMCommandBuilder.InputFormatEnum.Jpeg);
            var request = _dcmtk.StoreSCU("192.168.5.51", 5678, bmp)
                .Set(x => x.CallingAETitle, "DRSHD")
                .Set(x => x.CalledAETitle, "MedXChange")
                // we need this to succesfully do a jpeg, so we will get an error
                //.Set(x => x.ProposeJpeg8, true)
                .Build();

            // act
            request.Start();

            // assert
            request.Wait();
            Assert.That(request.WasScoreSuccesful, Is.False);
        }
    }
}
