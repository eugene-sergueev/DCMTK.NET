#pragma once

#include "OFConditionNet.h"

#include "dcmtk\dcmnet\dfindscu.h"

ref struct PrivateManaged;

public ref class DcmFindSCUNet
{
public:
	DcmFindSCUNet()
	{
		_findScu = new DcmFindSCU();
	}

	OFConditionNet^ InitializeNetwork(int acse_timeout);

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

