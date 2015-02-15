#pragma once

#include "DcmTagKeyNet.h"
#include "OFConditionNet.h"

#include "dcmtk/config/osconfig.h" /* make sure OS specific configuration is included first */
#include "dcmtk/dcmdata/dcdatset.h"
#include "dcmtk/dcmnet/diutil.h"
#include "dcmtk/dcmdata/dcsequen.h"

public ref class DcmDatasetNet
{
public:

	OFConditionNet^ FindAndGetString(DcmTagKeyNet^ tagKey, unsigned long position, bool searchIntoSub, [System::Runtime::InteropServices::Out] System::String^% result)
	{
		result = nullptr;

		OFString string;
		OFCondition cond = _dataSet->findAndGetOFString(tagKey->GetUnmanagedType(), string, 0, searchIntoSub);
		if (cond.good())
		{
			result = gcnew System::String(string.c_str());
		}

		/*DcmSequenceOfItems* sequence;
		cond = _dataSet->findAndGetSequence(DCM_ScheduledProcedureStepSequence, sequence, true, false);
		if (cond.good())
		{
			DcmStack stack;
			while (sequence->nextObject(stack, true).good())
			{
				DcmObject* item = stack.top();
				if (item->getTag() == DCM_ScheduledProcedureStepStartDate)
				{
					DcmElement* element = (DcmElement*)item;
					element->getOFString(string, 0, true);
					result = gcnew System::String(string.c_str());
				}
			}
		}*/

		//DCMNET_WARN(DcmObject::PrintHelper(*_dataSet));
		/*if (!elementList->empty())
		{
			DcmObject *dO;
			elementList->seek(ELP_first);
			do {
				dO = elementList->get();
				dO->print(out, flags, level + 1, pixelFileName, pixelCounter);
			} while (elementList->seek(ELP_next));
		}*/

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