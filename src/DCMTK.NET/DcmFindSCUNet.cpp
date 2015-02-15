#include "stdafx.h"
#include "DcmFindSCUNet.h"
#include <vcclr.h>

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
	E_TransferSyntax preferredTransferSyntax,
	T_DIMSE_BlockingMode blockMode,
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
		}
	}

	if (fileNameList != nullptr)
	{
		for each (System::String^ fileName in fileNameList)
		{
			System::IntPtr ptrToNativeString = System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(fileName);
			dcmtkFileNameList.push_back(static_cast<char*>(ptrToNativeString.ToPointer()));
		}
	}

	OFCondition result = _findScu->performQuery(
		"pacs.medxchange.com",
		5678,
		"DRSHD",
		"MedXChange",
		UID_FINDModalityWorklistInformationModel,
		EXS_Unknown,
		DIMSE_BLOCKING,
		0,
		ASC_MAXIMUMPDUSIZE,
		OFFalse,
		OFFalse,
		1,
		OFFalse,
		-1,
		&dcmtkOverrideKeys,
		&dcmtkCallback,
		&dcmtkFileNameList);

	return gcnew OFConditionNet(result);
}