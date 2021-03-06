﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace DCMTK.Tests
{
    [TestFixture]
    public class FindTests : TestBase
    {
        [Test]
        public void Can_find_worklist()
        {
            // arrange
            var request = _dcmtk.Find("192.168.5.51", 5678)
                .Set(x => x.CallingAETitle, "DRSHD")
                .Set(x => x.CalledAETitle, "DRSHD")
                .AddKey("PatientName", "")
                .AddKey("PatientID", "")
                .AddKey("StudyInstanceUID", "")
                .AddKey("AccessionNumber", "")
                .AddKey("ScheduledProcedureStepSequence[0].ScheduledStationAETitle", "")

                //.AddKey("ScheduledProcedureStepSequence[0].ScheduledProcedureStepStartDate", "20150205-20150207")
                ////.AddKey("ScheduledProcedureStepSequence[0].ScheduledProcedureStepStartDate", "20150206")
                ////.AddKey("ScheduledProcedureStepSequence[0].ScheduledProcedureStepStartDate", "20150205")

                ////.AddKey("ScheduledProcedureStepSequence[0].ScheduledProcedureStepStartTime", "")
                .AddKey("ScheduledProcedureStepSequence[0].Modality", "")
                .AddKey("ScheduledProcedureStepSequence[0].ScheduledStationName", "")
                .AddKey("ReferringPhysicianName", "")
                .AddKey("PatientBirthDate", "")
                .AddKey("PatientSex", "")
                .AddKey("RequestedProcedureID", "")
                .AddKey("RequestedProcedureDescription", "")
                .Build();

            // act
            request.Start();

            // assert
            request.Wait();
            Assert.That(request.HasErrors, Is.False, request.ErrorMessage);
            foreach (var result in request.Result)
            {
                Console.WriteLine("Result");
                foreach (var property in result.Properties)
                    Console.WriteLine("   {0}={1}", property.Name,property.Value);
            }
        }

        [Test]
        public void Can_get_error_messages()
        {
            // arrange
            var request = _dcmtk.Find("192.168.5.51", 5678)
                .Set(x => x.CallingAETitle, "DRSHD2") // wrong calling!
                .Set(x => x.CalledAETitle, "MedXChange")
                .AddKey("PatientName", "")
                .AddKey("PatientID", "")
                .AddKey("StudyInstanceUID", "*")
                .Build();

            // act
            request.Start();

            // assert
            request.Wait();
            Assert.That(request.HasErrors, Is.True);
            Assert.That(request.ErrorMessage, Is.Not.Null.Or.Empty);
        }
    }
}
