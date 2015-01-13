using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DCMTK.Tests
{
    [TestFixture]
    public class EchoTests : TestBase
    {
        [Test]
        public void Can_performe_valid_echo()
        {
            // arrange
            var echoRequest = _dcmtk.Echo("pacs.medxchange.com", 5678)
                .Set(x => x.CallingAETitle, "DRSHD")
                .Set(x => x.CalledAETitle, "MedXChange")
                .Build();

            // act
            echoRequest.Start();

            // assert
            echoRequest.Wait();
            Assert.That(echoRequest.Result, Is.EqualTo(true));
        }

        [Test]
        public void Can_get_reason_for_failed_echo()
        {
            // arrange
            var echoRequest = _dcmtk.Echo("pacs.medxchange.com", 5678)
                .Set(x => x.CallingAETitle, "DRSHD2") // invalid!
                .Set(x => x.CalledAETitle, "MedXChange")
                .Build();

            // act
            echoRequest.Start();

            // assert
            echoRequest.Wait();
            Assert.That(echoRequest.Result, Is.EqualTo(false));
            Assert.That(echoRequest.Reason, Is.EqualTo("Calling AE Title Not Recognized"));
        }
    }
}
