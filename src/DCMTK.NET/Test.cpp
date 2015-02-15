#include "stdafx.h"
#include "Test.h"

#include "dcmtk/config/osconfig.h"

#define INCLUDE_CSTDLIB
#define INCLUDE_CSTDIO
#define INCLUDE_CSTRING
#define INCLUDE_CSTDARG
#include "dcmtk/ofstd/ofstdinc.h"

#include "dcmtk/dcmdata/dctk.h"
#include "dcmtk/dcmnet/dimse.h"
#include "dcmtk/dcmnet/diutil.h"
#include "dcmtk/dcmdata/dcfilefo.h"
#include "dcmtk/dcmdata/dcdict.h"
#include "dcmtk/dcmdata/dcuid.h"
#include "dcmtk/dcmdata/cmdlnarg.h"
#include "dcmtk/ofstd/ofconapp.h"
#include "dcmtk/dcmdata/dcuid.h"      /* for dcmtk version name */
#include "dcmtk/dcmnet/dfindscu.h"

#include "dcmtk/dcmdata/dcdict.h"

#include <iostream>

static OFLogger findscuLogger = OFLog::getLogger("dcmtk.net.test");

class DcmFindSCUmyCallback : public DcmFindSCUCallback
{
public:
	DcmFindSCUmyCallback(){

	}

	virtual ~DcmFindSCUmyCallback() {}

	virtual void callback(
		T_DIMSE_C_FindRQ *request,
		int responseCount,
		T_DIMSE_C_FindRSP *rsp,
		DcmDataset *responseIdentifiers)
	{
		int r;
		r = responseCount;
		std::cout << "responsecount:" << responseCount << "\n";
		OFString string;
		OFCondition cond = responseIdentifiers->findAndGetOFString(DCM_PatientName, string);
		
		OFBool result = cond.good();
		std::cout << "patientname=" << string.c_str();
	}
};

Test::Test()
{
	DcmFindSCU findscu;
	OFString temp_str;
	OFList<OFString> overrideKeys;
	OFList<OFString> fileNameList;

	OFCondition cond = findscu.initializeNetwork(30);
	if (cond.bad()) {
		OFLOG_ERROR(findscuLogger, DimseCondition::dump(temp_str, cond));
		return;
	}

	overrideKeys.push_back(OFString("PatientName="));

	DcmFindSCUmyCallback callback;

	// do the main work: negotiate network association, perform C-FIND transaction,
	// process results, and finally tear down the association.
	cond = findscu.performQuery(
		"pacs.medxchange.com",
		5678,
		"DRSHD",
		"MedXChange",
		UID_FINDModalityWorklistInformationModel,
		EXS_Unknown,
		DIMSE_BLOCKING,
		0,
		ASC_MAXIMUMPDUSIZE,
		OFFalse,
		OFFalse,
		1,
		OFFalse,
		-1,
		&overrideKeys,
		&callback,
		NULL);

	OFBool result = cond.bad();

	cond = findscu.dropNetwork();
}


int Test::TestFunction(std::string param)
{
	return 0;
}
