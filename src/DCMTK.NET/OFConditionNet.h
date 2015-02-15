#pragma once

#include "dcmtk/ofstd/ofcond.h"

public ref class OFConditionNet
{
public:

	inline unsigned short Module()
	{
		return _condition->module();
	}

	inline unsigned short Code()
	{
		return _condition->code();
	}

	inline System::String^ Text()
	{
		return gcnew System::String(_condition->text());
	}

	inline OFBool Good()
	{
		OFStatus s = _condition->status();
		return (s == OF_ok);
	}

	inline OFBool Bad()
	{
		OFStatus s = _condition->status();
		return (s != OF_ok);
	}

internal:

	OFCondition* _condition;

	OFConditionNet(OFCondition condition) 
		:_condition(NULL) 
	{
		_condition = new OFCondition(condition);
	}

	~OFConditionNet() 
	{
		this->!OFConditionNet();
	}   

	!OFConditionNet() 
	{
		if (_condition)
		{
			delete _condition;
			_condition = NULL;
		}
	}
};