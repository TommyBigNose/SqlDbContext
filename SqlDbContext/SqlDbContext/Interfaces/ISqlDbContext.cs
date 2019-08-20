using System;
using System.Collections.Generic;
using System.Text;

namespace SqlDbContext.Interfaces
{
	public interface ISqlDbContext : IDisposable
	{
		void ClearParameters();
		void ExecuteQuery();
		bool GetBoolean(string pFieldName);
		double GetDouble(string pFieldName);
		float GetFloat(string pFieldName);
		int GetInt(string pFieldName);
		string GetString(string pFieldName);
		void PrepareNewQuery(string pQuery);
		void PrepareNewQuery(string pQuery, List<ICloneable> pParameters);
		bool RecordSetNotEmptyRead();
		void SetParamFloat(string pParameter, float pValue);
		void SetParamInt(string pParameter, int pValue);
		void SetParamString(string pParameter, string pValue);
	}
}
