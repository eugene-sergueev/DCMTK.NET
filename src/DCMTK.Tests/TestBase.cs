﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using DCMTK.Fluent;
using NUnit.Framework;

namespace DCMTK.Tests
{
    [TestFixture]
    public class TestBase
    {
        // ReSharper disable InconsistentNaming
        protected DCMTK _dcmtk;
        // ReSharper restore InconsistentNaming
        private string _tempDirectory;

        [SetUp]
        public virtual void Setup()
        {
            _dcmtk = new DCMTK(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + @"\..\..\..\..\libs\dcmtk-3.6.0-win32-i386\bin"));
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

        protected string CreateSampleDCMImage(ImageToDCMCommandBuilder.InputFormatEnum inputFormat)
        {
            var dcmFile = GetTemporaryResource("image.dcm");
            var request = _dcmtk.ImageToDCM(GetTestResource(inputFormat == ImageToDCMCommandBuilder.InputFormatEnum.Bmp ? "sampleStill.bmp" : "sampleStill.jpg"), dcmFile)
                .Set(x => x.InputFormat, inputFormat)
                .Build();
            request.Start();
            request.Wait();
            if (!string.IsNullOrEmpty(request.ErrorMessage))
                throw new Exception("Couldn't create a dcm file");
            return dcmFile;
        }
    }
}
