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