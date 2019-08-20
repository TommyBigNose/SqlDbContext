using NUnit.Framework;
using SqlDbContext.SqlLite;
using System.IO;

namespace SqlDbContext.NUnit
{
	[TestFixture]
	public class DbContextTests
	{
		private string connectionString = Constants.SqlLite.ConnectionStrings.Template;

		[SetUp]
		public void SetUp()
		{
			string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
			string directory = Path.GetDirectoryName(location);

			connectionString = connectionString.Replace(Constants.SqlLite.ConnectionStrings.TagFullPath, directory + TestConstants.ConnectionStrings.Database);
		}

		[TearDown]
		public void TearDown()
		{

		}

		[Test]
		public void BasicSelect()
		{
			// Arrange
			string result = "";

			// Act
			using (SqlLiteDbContext dbContext = new SqlLiteDbContext(connectionString, Constants.SqlLite.DefaultQueries.Select))
			{
				dbContext.ExecuteQuery();
				while(dbContext.RecordSetNotEmptyRead())
				{
					result = dbContext.GetString("name");
				}
			}

			// Assert
			Assert.IsFalse(string.IsNullOrEmpty(result));
			System.Console.WriteLine(result);
		}
	}
}