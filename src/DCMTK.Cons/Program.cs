using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCMTK.Cons
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Do();
            }
        }

        static void Do()
        {
            var o = DcmAssocNet.ASC_MAXIMUMPDUSIZE_NET;
            using (var find = new DcmFindSCUNet())
            {
                using (var initializeNetworkResult = find.InitializeNetwork(30))
                {
                    if (initializeNetworkResult.Good())
                    using (var queryResult = find.PerformQuery("192.168.2.110",
                            634, 
                            "PAULS",
                            "LEAD_MWL_SCP", 
                            DcmUIDNet.FINDModalityWorklistInformationModel,
                            TransferSyntaxNet.Unknown,
                            DIMSE_BlockingMode.DIMSE_BLOCKING, 
                            0,
                            DcmAssocNet.ASC_MAXIMUMPDUSIZE_NET, 
                            false,
                            false,
                            1,
                            false, 
                            -1, 
                            new List<string>
                            {
                                "ScheduledPerformingPhysicianName=",
                                "PatientName=",
                                "PatientID=",
                                "StudyInstanceUID=",
                                "AccessionNumber=",
                                "ScheduledProcedureStepSequence[0].ScheduledStationAETitle=",
                                "ScheduledProcedureStepSequence[0].ScheduledProcedureStepStartDate=",
                                "ScheduledProcedureStepSequence[0].ScheduledProcedureStepStartTime=",
                                "ScheduledProcedureStepSequence[0].Modality=",
                                "ScheduledProcedureStepSequence[0].ScheduledStationName=",
                                "ReferringPhysicianName=",
                                "PatientBirthDate=",
                                "PatientSex=",
                                "RequestedProcedureID=",
                                "RequestedProcedureDescription=",
                            }, (count, identifiers) =>
                            {
                                string performingPhysician;
                                var getresult = identifiers.FindAndGetString(DcmTagKeyNet.ScheduledPerformingPhysicianName, 0, true, out performingPhysician);
                                if (getresult.Good())
                                {
                                    Console.WriteLine("performingPhysician = " + performingPhysician);
                                }
                                else
                                {
                                    Console.WriteLine("performingPhysician error = " + getresult.Text());
                                }

                                string patientName;
                                getresult = identifiers.FindAndGetString(DcmTagKeyNet.PatientName, 0, true, out patientName);
                                if (getresult.Good())
                                {
                                    Console.WriteLine("patientName = " + patientName);
                                }
                                else
                                {
                                    Console.WriteLine("patientName error = " + getresult.Text());
                                }

                                string patientId;
                                getresult = identifiers.FindAndGetString(DcmTagKeyNet.PatientID, 0, true, out patientId);
                                if (getresult.Good())
                                {
                                    Console.WriteLine("patientId = " + patientId);
                                }
                                else
                                {
                                    Console.WriteLine("patientId error = " + getresult.Text());
                                }

                                string studyInstanceUID;
                                getresult = identifiers.FindAndGetString(DcmTagKeyNet.StudyInstanceUID, 0, true, out studyInstanceUID);
                                if (getresult.Good())
                                {
                                    Console.WriteLine("studyInstanceUID = " + studyInstanceUID);
                                }
                                else
                                {
                                    Console.WriteLine("studyInstanceUID error = " + getresult.Text());
                                }

                                string accessionNumber;
                                getresult = identifiers.FindAndGetString(DcmTagKeyNet.AccessionNumber, 0, true, out accessionNumber);
                                if (getresult.Good())
                                {
                                    Console.WriteLine("accessionNumber = " + accessionNumber);
                                }
                                else
                                {
                                    Console.WriteLine("accessionNumber error = " + getresult.Text());
                                }

                                string scheduledStationAETitle;
                                getresult = identifiers.FindAndGetString(DcmTagKeyNet.ScheduledStationAETitle, 0, true, out scheduledStationAETitle);
                                if (getresult.Good())
                                {
                                    Console.WriteLine("scheduledStationAETitle = " + scheduledStationAETitle);
                                }
                                else
                                {
                                    Console.WriteLine("scheduledStationAETitle error = " + getresult.Text());
                                }

                                string scheduledProcedureStepStartDate;
                                getresult = identifiers.FindAndGetString(DcmTagKeyNet.ScheduledProcedureStepStartDate, 0, true, out scheduledProcedureStepStartDate);
                                if (getresult.Good())
                                {
                                    Console.WriteLine("scheduledProcedureStepStartDate = " + scheduledProcedureStepStartDate);
                                }
                                else
                                {
                                    Console.WriteLine("scheduledProcedureStepStartDate error = " + getresult.Text());
                                }

                                string scheduledProcedureStepStartTime;
                                getresult = identifiers.FindAndGetString(DcmTagKeyNet.ScheduledProcedureStepStartTime, 0, true, out scheduledProcedureStepStartTime);
                                if (getresult.Good())
                                {
                                    Console.WriteLine("ScheduledProcedureStepStartTime = " + scheduledProcedureStepStartTime);
                                }
                                else
                                {
                                    Console.WriteLine("ScheduledProcedureStepStartTime error = " + getresult.Text());
                                }

                                string modality;
                                getresult = identifiers.FindAndGetString(DcmTagKeyNet.Modality, 0, true, out modality);
                                if (getresult.Good())
                                {
                                    Console.WriteLine("modality = " + modality);
                                }
                                else
                                {
                                    Console.WriteLine("modality error = " + getresult.Text());
                                }

                                string scheduledStationName;
                                getresult = identifiers.FindAndGetString(DcmTagKeyNet.ScheduledStationName, 0, true, out scheduledStationName);
                                if (getresult.Good())
                                {
                                    Console.WriteLine("scheduledStationName = " + scheduledStationName);
                                }
                                else
                                {
                                    Console.WriteLine("scheduledStationName error = " + getresult.Text());
                                }

                                string referringPhysicianName;
                                getresult = identifiers.FindAndGetString(DcmTagKeyNet.ReferringPhysicianName, 0, true, out referringPhysicianName);
                                if (getresult.Good())
                                {
                                    Console.WriteLine("referringPhysicianName = " + referringPhysicianName);
                                }
                                else
                                {
                                    Console.WriteLine("referringPhysicianName error = " + getresult.Text());
                                }

                                string patientBirthDate;
                                getresult = identifiers.FindAndGetString(DcmTagKeyNet.PatientBirthDate, 0, true, out patientBirthDate);
                                if (getresult.Good())
                                {
                                    Console.WriteLine("patientBirthDate = " + patientBirthDate);
                                }
                                else
                                {
                                    Console.WriteLine("patientBirthDate error = " + getresult.Text());
                                }

                                string patientSex;
                                getresult = identifiers.FindAndGetString(DcmTagKeyNet.PatientSex, 0, true, out patientSex);
                                if (getresult.Good())
                                {
                                    Console.WriteLine("patientSex = " + patientSex);
                                }
                                else
                                {
                                    Console.WriteLine("patientSex error = " + getresult.Text());
                                }

                                string requestedProcedureId;
                                getresult = identifiers.FindAndGetString(DcmTagKeyNet.RequestedProcedureID, 0, true, out requestedProcedureId);
                                if (getresult.Good())
                                {
                                    Console.WriteLine("requestedProcedureId = " + requestedProcedureId);
                                }
                                else
                                {
                                    Console.WriteLine("requestedProcedureId error = " + getresult.Text());
                                }

                                string requestedProcedureDescription;
                                getresult = identifiers.FindAndGetString(DcmTagKeyNet.RequestedProcedureDescription, 0, true, out requestedProcedureDescription);
                                if (getresult.Good())
                                {
                                    Console.WriteLine("requestedProcedureDescription = " + requestedProcedureDescription);
                                }
                                else
                                {
                                    Console.WriteLine("requestedProcedureDescription error = " + getresult.Text());
                                }
                            }, 
                            new List<string>()))
                    {
                        if (queryResult.Bad())
                        {
                            Console.WriteLine("Error querying.." + queryResult.Text());
                        }
                    }
                }
            }
        }
    }
}
