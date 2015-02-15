#pragma once

#include "OFConditionNet.h"
#include "DcmDatasetNet.h"
#include "DcmTransferSyntaxNet.h"
#include "DcmDimseNet.h"
#include "dcmtk\dcmnet\dfindscu.h"

public delegate void DcmFindSCUCallbackDelegate(int responseCount, DcmDatasetNet^ responseIdentifiers);

public ref class DcmFindSCUNet
{
public:
	DcmFindSCUNet()
	{
		_findScu = new DcmFindSCU();
	}

	OFConditionNet^ InitializeNetwork(int acse_timeout);

	OFConditionNet^ PerformQuery(
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
		System::Collections::Generic::List<System::String^>^ fileNameList);

private:
	DcmFindSCU* _findScu;

	~DcmFindSCUNet()
	{
		this->!DcmFindSCUNet();
	}

	!DcmFindSCUNet()
	{
		if (_findScu)
		{
			delete _findScu;
			_findScu = NULL;
		}
	}
};

