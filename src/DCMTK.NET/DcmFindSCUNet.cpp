#include "stdafx.h"
#include "DcmFindSCUNet.h"

OFConditionNet^ DcmFindSCUNet::InitializeNetwork(int acse_timeout)
{
	return gcnew OFConditionNet(_findScu->initializeNetwork(acse_timeout));
}