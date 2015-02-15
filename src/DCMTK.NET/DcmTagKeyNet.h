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

