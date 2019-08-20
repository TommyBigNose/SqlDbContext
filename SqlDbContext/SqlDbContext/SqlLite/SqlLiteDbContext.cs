using SqlDbContext.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SqlDbContext.SqlLite
{
	public class SqlLiteDbContext : ISqlDbContext
	{
		public SQLiteConnection Connection { get; set; }
		public string ConnectionString { get; set; }
		public SQLiteCommand Query { get; set; }
		public SQLiteDataReader Reader { get; set; }
		public List<SQLiteParameter> Parameters { get; set; }

		public SqlLiteDbContext(string pConnectionString, string pQueryString)
		{
			ConnectionString = pConnectionString;
			Connection = new SQLiteConnection();

			Query = new SQLiteCommand(pQueryString);
			Parameters = new List<SQLiteParameter>();

			Connection.ConnectionString = ConnectionString;
			Query.Connection = Connection;

			Connection.Open();
		}

		public void PrepareNewQuery(string pQuery)
		{
			ClearParameters();
			Query.CommandText = pQuery;
		}

		public void PrepareNewQuery(string pQuery, List<ICloneable> pParameters)
		{
			ClearParameters();
			Query.CommandText = pQuery;
			Query.Parameters.AddRange(pParameters.ToArray());
		}

		// Opens Connection and then executes SQL statement
		public void ExecuteQuery()
		{
			//Connection.Open();
			if (Parameters.Count > 0)
			{
				Query.Parameters.AddRange(Parameters.ToArray());
			}

			Reader = Query.ExecuteReader();
		}

		//	Checks to see if there are rows, and then reads the next row if possible.
		public Boolean RecordSetNotEmptyRead()
		{
			if (Reader.HasRows)
			{
				if (Reader.Read()) return true;
			}

			return false;
		}


		/******************************************************************
			Settings for preparing Parameters in a SQL statement
		******************************************************************/
		public void SetParamString(string pParameter, string pValue)
		{
			SQLiteParameter param = new SQLiteParameter(pParameter, System.Data.DbType.String);
			param.Value = pValue;
			Parameters.Add(param);
		}

		public void SetParamInt(string pParameter, int pValue)
		{
			SQLiteParameter param = new SQLiteParameter(pParameter, System.Data.DbType.Int32);
			param.Value = pValue;
			Parameters.Add(param);
		}

		public void SetParamFloat(string pParameter, float pValue)
		{
			SQLiteParameter param = new SQLiteParameter(pParameter, System.Data.DbType.Double);
			param.Value = pValue;
			Parameters.Add(param);
		}


		/******************************************************************
			Getters for various types of fields while you're looping through the record set
		******************************************************************/
		public string GetString(string pFieldName)
		{
			if (Reader.IsDBNull(Reader.GetOrdinal(pFieldName)) == false)
			{
				return Reader.GetString(Reader.GetOrdinal(pFieldName));
			}
			return string.Empty;
		}

		public int GetInt(string pFieldName)
		{
			if (Reader.IsDBNull(Reader.GetOrdinal(pFieldName)) == false)
			{
				return Reader.GetInt32(Reader.GetOrdinal(pFieldName));
			}
			return 0;
		}

		public float GetFloat(string pFieldName)
		{
			if (Reader.IsDBNull(Reader.GetOrdinal(pFieldName)) == false)
			{
				return Reader.GetFloat(Reader.GetOrdinal(pFieldName));
			}
			return 0.0f;
		}

		public double GetDouble(string pFieldName)
		{
			if (Reader.IsDBNull(Reader.GetOrdinal(pFieldName)) == false)
			{
				return Reader.GetDouble(Reader.GetOrdinal(pFieldName));
			}
			return 0.0;
		}

		public bool GetBoolean(string pFieldName)
		{
			if (Reader.IsDBNull(Reader.GetOrdinal(pFieldName)) == false)
			{
				return Reader.GetBoolean(Reader.GetOrdinal(pFieldName));
			}
			return false;
		}


		/******************************************************************
			Misc functions
		******************************************************************/
		public void ClearParameters()
		{
			Parameters.Clear();
			Query.Parameters.Clear();
		}

		public void Dispose()
		{
			Parameters.Clear();
			Query.Dispose();
			Reader.Close();
			Connection.Close();
		}
	}
}
