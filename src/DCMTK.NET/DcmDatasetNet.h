#pragma once

#include "DcmTagKeyNet.h"
#include "OFConditionNet.h"

#include "dcmtk/config/osconfig.h" /* make sure OS specific configuration is included first */
#include "dcmtk/dcmdata/dcdatset.h"

public ref class DcmDatasetNet
{
public:

	OFConditionNet^ FindAndGetString(DcmTagKeyNet^ tagKey, System::String^% result) 
	{
		result = nullptr;

		OFString string;
		OFCondition cond = _dataSet->findAndGetOFString(tagKey->GetUnmanagedType(), string);
		if (cond.good())
		{
			result = gcnew System::String(string.c_str());
		}

		return gcnew OFConditionNet(cond);
	}

internal:
	DcmDatasetNet(DcmDataset* dataSet)
		:_dataSet(NULL)
	{
		_dataSet = dataSet;
	}
private:
	DcmDataset* _dataSet;

	~DcmDatasetNet()
	{
		this->!DcmDatasetNet();
	}

	!DcmDatasetNet()
	{
		
	}
};