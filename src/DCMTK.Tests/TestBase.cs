using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using DCMTK.DICOM;
using DCMTK.Fluent;
using DCMTK.Serialization;
using NUnit.Framework;

namespace DCMTK.Tests
{
    [TestFixture]
    public class TestBase
    {
        // ReSharper disable InconsistentNaming
        protected DCMTKContext _dcmtk;
        // ReSharper restore InconsistentNaming
        private string _tempDirectory;

        [SetUp]
        public virtual void Setup()
        {
            _dcmtk = new DCMTKContext(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + @"\..\..\..\..\libs\dcmtk-3.6.0-win32-i386\bin"));
            _tempDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFileNameWithoutExtension(Path.GetRandomFileName()));
            if(!Directory.Exists(_tempDirectory))
                Directory.CreateDirectory(_tempDirectory);
        }

        [TearDown]
        public virtual void TearDown()
        {
            if(!string.IsNullOrEmpty(_tempDirectory) && Directory.Exists(_tempDirectory))
                Directory.Delete(_tempDirectory, true);
        }

        protected string TempDirectory
        {
            get { return _tempDirectory; }
        }

        protected string GetTestResource(string resource)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", resource);
        }

        protected string GetTemporaryResource(string resource)
        {
            return Path.Combine(_tempDirectory, resource);
        }

        protected string CreateSampleDCMImage(ImageToDCMCommandBuilder.InputFormatEnum inputFormat, Action<ImageToDCMCommandBuilder> modifier = null)
        {
            var dcmFile = GetTemporaryResource(Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".dcm");
            var builder = _dcmtk.ImageToDCM(GetTestResource(inputFormat == ImageToDCMCommandBuilder.InputFormatEnum.Bmp ? "sampleStill.bmp" : "sampleStill.jpg"), dcmFile)
                .Set(x => x.InputFormat, inputFormat);
            if(modifier != null)
                modifier(builder);
            var request = builder.Build();
            request.Start();
            request.Wait();
            if(!request.WasConversionSuccesful)
                request.ThrowException("Couldn't create the dcm file");
            return dcmFile;
        }

        protected string CreateSampleDCMFile(Action<fileformat> modifier)
        {
            var xmlFile = GetTemporaryResource(Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".xml");
            var dcmFile = GetTemporaryResource(Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".dcm");

            var xml = new fileformat();
            xml.metaheader = new metaheader();
            xml.dataset = new dataset();
            xml.dataset.name = "JPEG Baseline";
            xml.dataset.xfer = "1.2.840.10008.1.2.4.50";

            modifier(xml);
           
            xml.XmlSerializeToFile(xmlFile);

            var request = _dcmtk.XmlToDcm(xmlFile, dcmFile).Build();
            request.Start();
            request.Wait();
            Assert.That(File.Exists(dcmFile), Is.True, request.Output);

            return dcmFile;
        }
    }
}
