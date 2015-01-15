using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCMTK.Fluent;
using DCMTK.Serialization;
using NUnit.Framework;

namespace DCMTK.Tests
{
    [TestFixture]
    public class XmlTests : TestBase
    {
        [Test]
        public void Can_convert_dcm_to_xml()
        {
            // arrange
            var dcmFile = CreateSampleDCMImage(ImageToDCMCommandBuilder.InputFormatEnum.Jpeg);
            var xmlFile = GetTemporaryResource("test.xml");
            var request = _dcmtk.DcmToXml(dcmFile, xmlFile).Build();

            // act
            request.Start();

            // assert
            request.Wait();
            Assert.That(request.WasSuccessful, Is.True, request.Output);
            Assert.That(File.Exists(request.OutputFile), Is.True);
            var deserialized = File.ReadAllText(xmlFile).XmlDeserializeFromString<fileformat>();
            Assert.That(deserialized, Is.Not.Null);
            Assert.That(deserialized.metaheader.name, Is.EqualTo("Little Endian Explicit"));
        }

        [Test]
        public void Can_convert_xml_to_dcm()
        {
            // arrange
            var xml = File.ReadAllText(GetTestResource("test.xml")).XmlDeserializeFromString<fileformat>();
            var xmlFile = GetTemporaryResource("test.xml");
            var dcmFile = GetTemporaryResource("test.dcm");
            xml.XmlSerializeToFile(xmlFile);
            var request = _dcmtk.XmlToDcm(xmlFile, dcmFile).Build();

            // act
            request.Start();

            // assert
            request.Wait();
            Assert.That(request.WasSuccessful, Is.True, request.Output);
            Assert.That(File.Exists(request.OutputFile), Is.True);
        }
    }
}
