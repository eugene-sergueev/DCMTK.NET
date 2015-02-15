#pragma once

#include "dcmtk\dcmdata\dcdeftag.h"

public ref class DcmTagKeyNet
{
public:

	static property DcmTagKeyNet^ PatientName
	{
		DcmTagKeyNet^ get()
		{
			return gcnew DcmTagKeyNet(&DCM_PatientName);
		}
	}

	static property DcmTagKeyNet^ ScheduledPerformingPhysicianName
	{
		DcmTagKeyNet^ get()
		{
			return gcnew DcmTagKeyNet(&DCM_ScheduledPerformingPhysicianName);
		}
	}

	static property DcmTagKeyNet^ PatientID
	{
		DcmTagKeyNet^ get()
		{
			return gcnew DcmTagKeyNet(&DCM_PatientID);
		}
	}

	static property DcmTagKeyNet^ StudyInstanceUID
	{
		DcmTagKeyNet^ get()
		{
			return gcnew DcmTagKeyNet(&DCM_StudyInstanceUID);
		}
	}

	static property DcmTagKeyNet^ AccessionNumber
	{
		DcmTagKeyNet^ get()
		{
			return gcnew DcmTagKeyNet(&DCM_AccessionNumber);
		}
	}

	static property DcmTagKeyNet^ ScheduledStationAETitle
	{
		DcmTagKeyNet^ get()
		{
			return gcnew DcmTagKeyNet(&DCM_ScheduledStationAETitle);
		}
	}

	static property DcmTagKeyNet^ ScheduledProcedureStepStartDate
	{
		DcmTagKeyNet^ get()
		{
			return gcnew DcmTagKeyNet(&DCM_ScheduledProcedureStepStartDate);
		}
	}

	static property DcmTagKeyNet^ ScheduledProcedureStepStartTime
	{
		DcmTagKeyNet^ get()
		{
			return gcnew DcmTagKeyNet(&DCM_ScheduledProcedureStepStartTime);
		}
	}

	static property DcmTagKeyNet^ Modality
	{
		DcmTagKeyNet^ get()
		{
			return gcnew DcmTagKeyNet(&DCM_Modality);
		}
	}

	static property DcmTagKeyNet^ ScheduledStationName
	{
		DcmTagKeyNet^ get()
		{
			return gcnew DcmTagKeyNet(&DCM_ScheduledStationName);
		}
	}

	static property DcmTagKeyNet^ ReferringPhysicianName
	{
		DcmTagKeyNet^ get()
		{
			return gcnew DcmTagKeyNet(&DCM_ReferringPhysicianName);
		}
	}

	static property DcmTagKeyNet^ PatientBirthDate
	{
		DcmTagKeyNet^ get()
		{
			return gcnew DcmTagKeyNet(&DCM_PatientBirthDate);
		}
	}

	static property DcmTagKeyNet^ PatientSex
	{
		DcmTagKeyNet^ get()
		{
			return gcnew DcmTagKeyNet(&DCM_PatientSex);
		}
	}

	static property DcmTagKeyNet^ RequestedProcedureID
	{
		DcmTagKeyNet^ get()
		{
			return gcnew DcmTagKeyNet(&DCM_RequestedProcedureID);
		}
	}

	static property DcmTagKeyNet^ RequestedProcedureDescription
	{
		DcmTagKeyNet^ get()
		{
			return gcnew DcmTagKeyNet(&DCM_RequestedProcedureDescription);
		}
	}

	static property DcmTagKeyNet^ ScheduledProcedureStepSequence
	{
		DcmTagKeyNet^ get()
		{
			return gcnew DcmTagKeyNet(&DCM_ScheduledProcedureStepSequence);
		}
	}
	
internal:

	DcmTagKeyNet(DcmTagKey* tagKey)
		:_tagKey(NULL)
	{
		_tagKey = tagKey;
	}

	DcmTagKey GetUnmanagedType()
	{
		return *_tagKey;
	}

private:

	DcmTagKey* _tagKey;

	~DcmTagKeyNet()
	{
		this->!DcmTagKeyNet();
	}

	!DcmTagKeyNet()
	{

	}

};

