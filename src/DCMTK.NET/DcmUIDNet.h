#pragma once

#include "dcmtk\dcmdata\dcuid.h"

namespace DCMTK
{
	public ref class DcmUIDNet
	{
	public:
		static property System::String^ FINDModalityWorklistInformationModel
		{
			System::String^ get()
			{
				return gcnew System::String(UID_FINDModalityWorklistInformationModel);
			}
		}
	};
}