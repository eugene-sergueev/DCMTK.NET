using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DCMTK.DICOM;
using DCMTK.Fluent;
using DCMTK.Serialization;
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
            Assert.That(request.WasConversionSuccesful, Is.True);
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
            Assert.That(request.WasConversionSuccesful, Is.True);
            Assert.That(File.Exists(dcmFile), Is.True);
        }

        [Test]
        public void Can_create_dcm_file_with_prefined_uids()
        {
            // arrange
            var sopInstanceId = DicomUid.GenerateUid().UID;
            var studyInstanceId = DicomUid.GenerateUid().UID;
            var seriesInstanceId = DicomUid.GenerateUid().UID;
            var dcmFile = GetTemporaryResource("sampleStill.dcm");
            var request = _dcmtk.ImageToDCM(GetTestResource("sampleStill.jpg"), dcmFile)
                .Set(x => x.InputFormat, ImageToDCMCommandBuilder.InputFormatEnum.Jpeg)
                .AddKey("PatientID", "testid")
                .AddKey("ImplementationVersionName", "test 1.3")
                .AddKey("SOPInstanceUID", sopInstanceId)
                .AddKey("StudyInstanceUID", studyInstanceId)
                .AddKey("SeriesInstanceUID", seriesInstanceId)
                .Build();

            // act
            request.Start();

            // assert
            request.Wait();
            Assert.That(request.WasConversionSuccesful, Is.True);
            Assert.That(File.Exists(dcmFile), Is.True);
            var xmlfile = GetTemporaryResource("sampleStill.xml");
            var dcmToFileRequest = _dcmtk.DcmToXml(dcmFile, xmlfile).Build();
            dcmToFileRequest.Start();
            dcmToFileRequest.Wait();
            Assert.IsTrue(dcmToFileRequest.WasSuccessful);
            var xml = File.ReadAllText(xmlfile).XmlDeserializeFromString<fileformat>();

            // ReSharper disable PossibleNullReferenceException

            var sopElement = (element)xml.dataset.Items.FirstOrDefault(x => x is element && ((element) x).name == "SOPInstanceUID");
            Assert.That(sopElement, Is.Not.Null.Or.Empty);
            Assert.That(sopElement.Value, Is.EqualTo(sopInstanceId));

            var studyInstanceIdElement = (element)xml.dataset.Items.FirstOrDefault(x => x is element && ((element)x).name == "StudyInstanceUID");
            Assert.That(studyInstanceIdElement, Is.Not.Null.Or.Empty);
            Assert.That(studyInstanceIdElement.Value, Is.EqualTo(studyInstanceId));

            var seriesInstanceIdElement = (element)xml.dataset.Items.FirstOrDefault(x => x is element && ((element)x).name == "SeriesInstanceUID");
            Assert.That(seriesInstanceIdElement, Is.Not.Null.Or.Empty);
            Assert.That(seriesInstanceIdElement.Value, Is.EqualTo(seriesInstanceId));

            var implementationNameElement = (element)xml.dataset.Items.FirstOrDefault(x => x is element && ((element)x).name == "ImplementationVersionName");
            Assert.That(implementationNameElement, Is.Not.Null.Or.Empty);
            Assert.That(implementationNameElement.Value, Is.EqualTo("test 1.3"));

            // ReSharper restore PossibleNullReferenceException
        }

        [Test]
        public void Can_create_image_with_patient_data()
        {
            // arrange/act
            var dcmFile = CreateSampleDCMImage(ImageToDCMCommandBuilder.InputFormatEnum.Jpeg, builder => builder
                // (0008,0020)	DA	StudyDate	            1	DICOM_2009
                .AddKey("StudyDate", "20140215")
                // (0008,0030)	TM	StudyTime	            1	DICOM_2009
                .AddKey("StudyTime", "090909")
                // (0010,0010)  PN  PatientName             1   DICOM_2009
                .AddKey("PatientName", "VIVALDI^ANTONIO")
                // (0010,0030)  DA  PatientBirthDate        1	DICOM_2009
                .AddKey("PatientBirthDate", "19960502")
                // (0010,0040)	CS	PatientSex	            1	DICOM_2009
                .AddKey("PatientSex", "M")
                // (0010,0020)	LO	PatientID	            1	DICOM_2009
                .AddKey("PatientID", "testpid")
                // (0008,0090)	PN	ReferringPhysicianName	1	DICOM_2009
                .AddKey("ReferringPhysicianName", "HAYDN^FRANZ^JOSEPH")
                // (0008,1050)	PN	PerformingPhysicianName	1-n	DICOM_2009
                .AddKey("PerformingPhysicianName", "BEETHOVEN^LUDWIG^VAN")
                // (0008,1010)	SH	StationName	            1	DICOM_2009
                .AddKey("StationName", "OR1")
                // (0008,1090)	LO	ManufacturerModelName	1	DICOM_2009
                .AddKey("ManufacturerModelName", "TestDeviceHD")
                // (0018,1020)	LO	SoftwareVersions	    1-n	DICOM_2009
                .AddKey("SoftwareVersions", "1.0.0.1")
                // (0008,0070)	LO	Manufacturer	        1	DICOM_2009
                .AddKey("Manufacturer", "TestDevice")
                // (0008,0060)	CS	Modality	            1	DICOM_2009 ?
                .AddKey("Modality", "OT")
                // (0008,0050)	SH	AccessionNumber	        1	DICOM_2009
                .AddKey("AccessionNumber", "20151212003")
                // (0020,0013)	IS	InstanceNumber	        1	DICOM_2009
                .AddKey("InstanceNumber", "2")
                // (0020,0011)	IS	SeriesNumber	        1	DICOM_2009
                .AddKey("SeriesNumber", "1"));

            // assert
            // TODO
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
            Assert.That(request.WasConversionSuccesful, Is.False);
            Assert.That(request.OutputFatal, Has.Count.EqualTo(1));
            Assert.That(request.OutputFatal[0], Is.EqualTo("Error converting file: Not a BMP file - invalid header"));
        }
    }
}
