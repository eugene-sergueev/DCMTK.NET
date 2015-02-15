#pragma once

#include "DcmTagKeyNet.h"
#include "OFConditionNet.h"

#include "dcmtk/config/osconfig.h" /* make sure OS specific configuration is included first */
#include "dcmtk/dcmdata/dcdatset.h"
#include "dcmtk/dcmnet/diutil.h"
#include "dcmtk/dcmdata/dcsequen.h"

namespace DCMTK
{
	public ref class DcmDatasetNet
	{
	public:

		OFConditionNet^ FindAndGetString(DcmTagKeyNet^ tagKey, unsigned long position, bool searchIntoSub, [System::Runtime::InteropServices::Out] System::String^% result)
		{
			result = nullptr;
			OFString string;

			OFCondition cond = _dataSet->findAndGetOFString(tagKey->GetUnmanagedType(), string, 0, searchIntoSub);
			if (cond.good())
				result = gcnew System::String(string.c_str());

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
}