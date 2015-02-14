#include "stdafx.h"
#include "Test.h"

#include "dcmtk\config\osconfig.h"
#include "dcmtk\dcmdata\dcdict.h"

Test::Test()
{
	/* make sure data dictionary is loaded */
	OFBool result = dcmDataDict.isDictionaryLoaded();
	if (!result)
	{
		//OFLOG_WARN(echoscuLogger, "no data dictionary loaded, check environment variable: "
		//	<< DCM_DICT_ENVIRONMENT_VARIABLE);
	}
}
