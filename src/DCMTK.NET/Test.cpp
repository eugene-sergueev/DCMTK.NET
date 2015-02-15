#include "stdafx.h"
#include "Test.h"

#include "dcmtk/config/osconfig.h"

#define INCLUDE_CSTDLIB
#define INCLUDE_CSTDIO
#define INCLUDE_CSTRING
#define INCLUDE_CSTDARG
#include "dcmtk/ofstd/ofstdinc.h"

#include "dcmtk/dcmnet/dimse.h"
#include "dcmtk/dcmnet/diutil.h"
#include "dcmtk/dcmdata/dcfilefo.h"
#include "dcmtk/dcmdata/dcdict.h"
#include "dcmtk/dcmdata/dcuid.h"
#include "dcmtk/dcmdata/cmdlnarg.h"
#include "dcmtk/ofstd/ofconapp.h"
#include "dcmtk/dcmdata/dcuid.h"      /* for dcmtk version name */

#include "dcmtk\dcmdata\dcdict.h"

static OFLogger echoscuLogger = OFLog::getLogger("dcmtk.net.test");

Test::Test()
{
	OFString temp_str;
	T_ASC_Network *net;
	T_ASC_Parameters *params;
	DIC_NODENAME localHost;
	DIC_NODENAME peerHost;

	/* initialize network, i.e. create an instance of T_ASC_Network*. */
	OFCondition cond = ASC_initializeNetwork(NET_REQUESTOR, 0, 3, &net);
	if (cond.bad()) {
		OFLOG_FATAL(echoscuLogger, DimseCondition::dump(temp_str, cond));
		exit(1);
	}

	/* initialize asscociation parameters, i.e. create an instance of T_ASC_Parameters*. */
	cond = ASC_createAssociationParameters(&params, ASC_MAXIMUMPDUSIZE);
	if (cond.bad()) {
		OFLOG_FATAL(echoscuLogger, DimseCondition::dump(temp_str, cond));
		exit(1);
	}

	/* sets this application's title and the called application's title in the params */
	/* structure. The default values to be set here are "STORESCU" and "ANY-SCP". */
	ASC_setAPTitles(params, "DRSHD", "MedXChange", NULL);

	/* Set the transport layer type (type of network connection) in the params */
	/* strucutre. The default is an insecure connection; where OpenSSL is  */
	/* available the user is able to request an encrypted,secure connection. */
	cond = ASC_setTransportLayerType(params, false);
	if (cond.bad()) {
		OFLOG_FATAL(echoscuLogger, DimseCondition::dump(temp_str, cond));
		exit(1);
	}

	/* Figure out the presentation addresses and copy the */
	/* corresponding values into the association parameters.*/
	gethostname(localHost, sizeof(localHost) - 1);
	sprintf(peerHost, "%s:%d", "pacs.medxchange.com", OFstatic_cast(int, 5678));
	ASC_setPresentationAddresses(params, localHost, peerHost);

	const char* ts[] = { UID_LittleEndianImplicitTransferSyntax };

	// add presentation context to association request
	cond = ASC_addPresentationContext(params, 1, UID_VerificationSOPClass, ts, 1);
	if (cond.bad()) {
		OFLOG_FATAL(echoscuLogger, DimseCondition::dump(temp_str, cond));
		exit(1);
	}

	// request DICOM association
	T_ASC_Association *assoc;
	cond = ASC_requestAssociation(net, params, &assoc);
	if (cond.bad()) {
		OFLOG_FATAL(echoscuLogger, DimseCondition::dump(temp_str, cond));
		
		if (cond == DUL_ASSOCIATIONREJECTED)
		{
			T_ASC_RejectParameters rej;
			ASC_getRejectParameters(params, &rej);
			ASC_printRejectParameters(temp_str, &rej);
		}
		else {
			OFLOG_FATAL(echoscuLogger, "Association Request Failed: " << DimseCondition::dump(temp_str, cond));
			exit(1);
		}

	}

	ASC_releaseAssociation(assoc); // release association
	ASC_destroyAssociation(&assoc); // delete assoc structure
	ASC_dropNetwork(&net); // delete net structure
}
