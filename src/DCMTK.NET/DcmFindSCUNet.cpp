#include "stdafx.h"
#include "DcmFindSCUNet.h"
#include <vcclr.h>

namespace DCMTK
{
	class DcmFindSCUCallbackNet : public DcmFindSCUCallback
	{
	public:
		DcmFindSCUCallbackNet(DcmFindSCUCallbackDelegate^ del)
		{
			_del = del;
		}

		virtual ~DcmFindSCUCallbackNet() {}

		virtual void callback(
			T_DIMSE_C_FindRQ *request,
			int responseCount,
			T_DIMSE_C_FindRSP *rsp,
			DcmDataset *responseIdentifiers)
		{
			_del->Invoke(responseCount, gcnew DcmDatasetNet(responseIdentifiers));
		}
	private:
		gcroot<DcmFindSCUCallbackDelegate^> _del;
	};

	OFConditionNet^ DcmFindSCUNet::InitializeNetwork(int acse_timeout)
	{
		return gcnew OFConditionNet(_findScu->initializeNetwork(acse_timeout));
	}

	OFConditionNet^ DcmFindSCUNet::PerformQuery(
		System::String^ peer,
		unsigned int port,
		System::String^ ourTitle,
		System::String^ peerTitle,
		System::String^ abstractSyntax,
		TransferSyntaxNet preferredTransferSyntax,
		DIMSE_BlockingMode blockMode,
		int dimse_timeout,
		Uint32 maxReceivePDULength,
		OFBool secureConnection,
		OFBool abortAssociation,
		unsigned int repeatCount,
		OFBool extractResponsesToFile,
		int cancelAfterNResponses,
		System::Collections::Generic::List<System::String^>^ overrideKeys,
		DcmFindSCUCallbackDelegate^ callback,
		System::Collections::Generic::List<System::String^>^ fileNameList)
	{
		OFList<OFString> dcmtkOverrideKeys;
		OFList<OFString> dcmtkFileNameList;
		DcmFindSCUCallbackNet dcmtkCallback(callback);

		if (overrideKeys != nullptr)
		{
			for each (System::String^ key in overrideKeys)
			{
				System::IntPtr ptrToNativeString = System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(key);
				dcmtkOverrideKeys.push_back(static_cast<char*>(ptrToNativeString.ToPointer()));
				System::Runtime::InteropServices::Marshal::FreeHGlobal(ptrToNativeString);
			}
		}

		if (fileNameList != nullptr)
		{
			for each (System::String^ fileName in fileNameList)
			{
				System::IntPtr ptrToNativeString = System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(fileName);
				dcmtkFileNameList.push_back(static_cast<char*>(ptrToNativeString.ToPointer()));
				System::Runtime::InteropServices::Marshal::FreeHGlobal(ptrToNativeString);
			}
		}

		System::IntPtr peerPtr = System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(peer);
		System::IntPtr ourTitlePtr = System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(ourTitle);
		System::IntPtr peerTitlePtr = System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(peerTitle);
		System::IntPtr abstractSyntaxPtr = System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(abstractSyntax);

		OFCondition result = _findScu->performQuery(
			static_cast<char*>(peerPtr.ToPointer()),
			port,
			static_cast<char*>(ourTitlePtr.ToPointer()),
			static_cast<char*>(peerTitlePtr.ToPointer()),
			static_cast<char*>(abstractSyntaxPtr.ToPointer()),
			(E_TransferSyntax)preferredTransferSyntax,
			(T_DIMSE_BlockingMode)blockMode,
			0,
			maxReceivePDULength,
			secureConnection,
			abortAssociation,
			repeatCount,
			extractResponsesToFile,
			cancelAfterNResponses,
			&dcmtkOverrideKeys,
			&dcmtkCallback,
			&dcmtkFileNameList);

		System::Runtime::InteropServices::Marshal::FreeHGlobal(peerPtr);
		System::Runtime::InteropServices::Marshal::FreeHGlobal(ourTitlePtr);
		System::Runtime::InteropServices::Marshal::FreeHGlobal(peerTitlePtr);
		System::Runtime::InteropServices::Marshal::FreeHGlobal(abstractSyntaxPtr);

		return gcnew OFConditionNet(result);
	}

	class SpikeDcmFindSCUCallbackNet : public DcmFindSCUCallback
	{
	public:
		SpikeDcmFindSCUCallbackNet()
		{
			Result = gcnew System::Collections::Generic::List<System::Collections::Generic::Dictionary<System::String^, System::String^>^>();
		}

		virtual ~SpikeDcmFindSCUCallbackNet() {}

		virtual void callback(
			T_DIMSE_C_FindRQ *request,
			int responseCount,
			T_DIMSE_C_FindRSP *rsp,
			DcmDataset *responseIdentifiers)
		{
			System::Collections::Generic::Dictionary<System::String^, System::String^>^ entry = gcnew System::Collections::Generic::Dictionary<System::String^, System::String^>();
			OFString tmpString;

		/*	.AddKey("ScheduledPerformingPhysicianName", "")
				.AddKey("PatientName", "")
				.AddKey("PatientID", patientId)
				.AddKey("StudyInstanceUID", "")
				.AddKey("AccessionNumber", "")
				.AddKey("ScheduledProcedureStepSequence[0].ScheduledStationAETitle", filteringAeTitle)
				.AddKey("ScheduledProcedureStepSequence[0].ScheduledProcedureStepStartDate", dateFilter)
				.AddKey("ScheduledProcedureStepSequence[0].ScheduledProcedureStepStartTime", "")
				.AddKey("ScheduledProcedureStepSequence[0].Modality", modality)
				.AddKey("ScheduledProcedureStepSequence[0].ScheduledStationName", "")
				.AddKey("ReferringPhysicianName", "")
				.AddKey("PatientBirthDate", "")
				.AddKey("PatientSex", "")
				.AddKey("RequestedProcedureID", "")
				.AddKey("RequestedProcedureDescription", "")*/

			OFCondition cond = responseIdentifiers->findAndGetOFString(DCM_ScheduledPerformingPhysicianName, tmpString, 0, true);
			if (cond.good())
			{
				entry->Add("ScheduledPerformingPhysicianName", gcnew System::String(tmpString.c_str()));
			}

			cond = responseIdentifiers->findAndGetOFString(DCM_PatientName, tmpString, 0, true);
			if (cond.good())
			{
				entry->Add("PatientName", gcnew System::String(tmpString.c_str()));
			}

			cond = responseIdentifiers->findAndGetOFString(DCM_PatientID, tmpString, 0, true);
			if (cond.good())
			{
				entry->Add("PatientID", gcnew System::String(tmpString.c_str()));
			}

			cond = responseIdentifiers->findAndGetOFString(DCM_StudyInstanceUID, tmpString, 0, true);
			if (cond.good())
			{
				entry->Add("StudyInstanceUID", gcnew System::String(tmpString.c_str()));
			}

			cond = responseIdentifiers->findAndGetOFString(DCM_AccessionNumber, tmpString, 0, true);
			if (cond.good())
			{
				entry->Add("AccessionNumber", gcnew System::String(tmpString.c_str()));
			}

			cond = responseIdentifiers->findAndGetOFString(DCM_ScheduledStationAETitle, tmpString, 0, true);
			if (cond.good())
			{
				entry->Add("ScheduledStationAETitle", gcnew System::String(tmpString.c_str()));
			}

			cond = responseIdentifiers->findAndGetOFString(DCM_ScheduledProcedureStepStartDate, tmpString, 0, true);
			if (cond.good())
			{
				entry->Add("ScheduledProcedureStepStartDate", gcnew System::String(tmpString.c_str()));
			}

			cond = responseIdentifiers->findAndGetOFString(DCM_ScheduledProcedureStepStartTime, tmpString, 0, true);
			if (cond.good())
			{
				entry->Add("ScheduledProcedureStepStartTime", gcnew System::String(tmpString.c_str()));
			}

			cond = responseIdentifiers->findAndGetOFString(DCM_Modality, tmpString, 0, true);
			if (cond.good())
			{
				entry->Add("Modality", gcnew System::String(tmpString.c_str()));
			}

			cond = responseIdentifiers->findAndGetOFString(DCM_ScheduledStationName, tmpString, 0, true);
			if (cond.good())
			{
				entry->Add("ScheduledStationName", gcnew System::String(tmpString.c_str()));
			}

			cond = responseIdentifiers->findAndGetOFString(DCM_ReferringPhysicianName, tmpString, 0, true);
			if (cond.good())
			{
				entry->Add("ReferringPhysicianName", gcnew System::String(tmpString.c_str()));
			}

			cond = responseIdentifiers->findAndGetOFString(DCM_PatientBirthDate, tmpString, 0, true);
			if (cond.good())
			{
				entry->Add("PatientBirthDate", gcnew System::String(tmpString.c_str()));
			}

			cond = responseIdentifiers->findAndGetOFString(DCM_PatientSex, tmpString, 0, true);
			if (cond.good())
			{
				entry->Add("PatientSex", gcnew System::String(tmpString.c_str()));
			}

			cond = responseIdentifiers->findAndGetOFString(DCM_RequestedProcedureID, tmpString, 0, true);
			if (cond.good())
			{
				entry->Add("RequestedProcedureID", gcnew System::String(tmpString.c_str()));
			}

			cond = responseIdentifiers->findAndGetOFString(DCM_RequestedProcedureDescription, tmpString, 0, true);
			if (cond.good())
			{
				entry->Add("RequestedProcedureDescription", gcnew System::String(tmpString.c_str()));
			}

			Result->Add(entry);
		}
	public:
		gcroot<System::Collections::Generic::List<System::Collections::Generic::Dictionary<System::String^, System::String^>^>^> Result;
	};

	System::Collections::Generic::List<System::Collections::Generic::Dictionary<System::String^, System::String^>^>^ DcmFindSCUNet::QueryWorklist(System::String^ host, int port, System::String^ callingAE, System::String^ calledAE, System::String^ patientId, System::String^ modality, System::String^ scheduledAETitle, System::String^ startDate)
	{
		OFList<OFString> dcmtkOverrideKeys;
		SpikeDcmFindSCUCallbackNet dcmtkCallback;

		patientId = "PatientID=" + patientId;
		modality = "ScheduledProcedureStepSequence[0].Modality=" + modality;
		startDate = "ScheduledProcedureStepSequence[0].ScheduledProcedureStepStartDate=" + startDate;
		scheduledAETitle = "ScheduledProcedureStepSequence[0].ScheduledStationAETitle=" + scheduledAETitle;

		System::IntPtr patientIdPtr = System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(patientId);
		System::IntPtr modalityPtr = System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(modality);
		System::IntPtr startDatePtr = System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(startDate);
		System::IntPtr scheduledAETitlePtr = System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(scheduledAETitle);

		dcmtkOverrideKeys.push_back("ScheduledPerformingPhysicianName");
		dcmtkOverrideKeys.push_back("PatientName");
		dcmtkOverrideKeys.push_back(static_cast<char*>(patientIdPtr.ToPointer()));
		dcmtkOverrideKeys.push_back("StudyInstanceUID");
		dcmtkOverrideKeys.push_back("AccessionNumber");
		dcmtkOverrideKeys.push_back(static_cast<char*>(scheduledAETitlePtr.ToPointer()));
		dcmtkOverrideKeys.push_back(static_cast<char*>(startDatePtr.ToPointer()));
		dcmtkOverrideKeys.push_back("ScheduledProcedureStepSequence[0].ScheduledProcedureStepStartTime");
		dcmtkOverrideKeys.push_back(static_cast<char*>(modalityPtr.ToPointer()));
		dcmtkOverrideKeys.push_back("ScheduledProcedureStepSequence[0].ScheduledStationName");
		dcmtkOverrideKeys.push_back("ReferringPhysicianName");
		dcmtkOverrideKeys.push_back("PatientBirthDate");
		dcmtkOverrideKeys.push_back("PatientSex");
		dcmtkOverrideKeys.push_back("RequestedProcedureID");
		dcmtkOverrideKeys.push_back("RequestedProcedureDescription");
	
		System::IntPtr hostPtr = System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(host);
		System::IntPtr callingAEPtr = System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(callingAE);
		System::IntPtr calledAEPtr = System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(calledAE);

		OFCondition result = _findScu->performQuery(
			static_cast<char*>(hostPtr.ToPointer()),
			port,
			static_cast<char*>(callingAEPtr.ToPointer()),
			static_cast<char*>(calledAEPtr.ToPointer()),
			UID_FINDModalityWorklistInformationModel,
			EXS_Unknown,
			DIMSE_BLOCKING,
			0,
			ASC_MAXIMUMPDUSIZE,
			false,
			false,
			1,
			false,
			-1,
			&dcmtkOverrideKeys,
			&dcmtkCallback,
			NULL);

		System::Runtime::InteropServices::Marshal::FreeHGlobal(patientIdPtr);
		System::Runtime::InteropServices::Marshal::FreeHGlobal(modalityPtr);
		System::Runtime::InteropServices::Marshal::FreeHGlobal(startDatePtr);
		System::Runtime::InteropServices::Marshal::FreeHGlobal(scheduledAETitlePtr);

		System::Runtime::InteropServices::Marshal::FreeHGlobal(hostPtr);
		System::Runtime::InteropServices::Marshal::FreeHGlobal(callingAEPtr);
		System::Runtime::InteropServices::Marshal::FreeHGlobal(calledAEPtr);

		if (result.bad())
		{
			throw gcnew System::Exception(gcnew System::String(result.text()));
		}

		return dcmtkCallback.Result;
	}
}