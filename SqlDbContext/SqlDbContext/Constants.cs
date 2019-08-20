using System;
using System.Collections.Generic;
using System.Text;

namespace SqlDbContext
{
	public class Constants
	{
		public class SqlLite
		{
			public class ConnectionStrings
			{
				public const string TagFullPath = "@@FULLPATH";
				public const string Template = @"Data Source=" + TagFullPath + ";Version=3;";

			}

			public class DefaultQueries
			{
				public const string Select = "SELECT * FROM sqlite_master";
			}
		}
	}
}
