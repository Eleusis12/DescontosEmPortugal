using DescontosEmPortugal.Library.Classes;
using DescontosEmPortugal.Library.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DescontosEmPortugal.Database
{
	public static class DataBaseAccessProcessing
	{
		private static class ConnectionToDatabase
		{
			public static string ConnectionString = "Data Source=DESKTOP-DGOMSEK\\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Initial Catalog=Produtos;";
		}

		public static DataTable GetAllUrl()
		{
			try
			{
				SqlConnection con = new SqlConnection(ConnectionToDatabase.ConnectionString);
				SqlDataAdapter ada = new SqlDataAdapter("USE [Produtos]\n" +
					"SELECT S.ID_Pesquisa" +
						  ",W.SiteURL" +
						  ",C.Nome" +
						  "\n" +

					 "FROM[dbo].[SitesAVerificar] S\n" +
					 "INNER JOIN [dbo].[Website]  W on S.ID_Website = W.ID_Website\n" +
					 "INNER JOIN [dbo].[Categoria] C on S.ID_Categoria = C.ID"
						, con);

				DataTable dt = new DataTable();
				ada.Fill(dt);

				return dt;
			}
			catch (SqlException ex)
			{
				LogDatabaseAccessError(ex);
				return null;
			}
		}

		public static bool InsertWebsiteDetailsIntoDataBase(WebsiteDetails websiteDetails)
		{
			using (SqlConnection conn = new SqlConnection(ConnectionToDatabase.ConnectionString))
			{
				// 1.  create a command object identifying the stored procedure
				SqlCommand cmd = new SqlCommand("InsertWebsiteToTrack", conn);

				// 2. set the command object so it knows to execute a stored procedure
				cmd.CommandType = CommandType.StoredProcedure;

				// 3. add parameter to command, which will be passed to the stored procedure
				cmd.Parameters.Add(new SqlParameter("@URL", websiteDetails.WebsiteUrl));
				cmd.Parameters.Add(new SqlParameter("@Category", websiteDetails.Category));

				try
				{
					conn.Open();
					cmd.ExecuteNonQuery();
				}
				catch (SqlException ex)
				{
					LogDatabaseAccessError(ex);
					return false;
				}
			}

			return true;
		}

		public static bool InsertProductDetailsIntoDataBase(ProductDetails productDetails, int categoryID, int searchID)
		{
			using (SqlConnection conn = new SqlConnection(ConnectionToDatabase.ConnectionString))
			{
				// 1.  create a command object identifying the stored procedure
				SqlCommand cmd = new SqlCommand("UpSertProduct", conn);

				// 2. set the command object so it knows to execute a stored procedure
				cmd.CommandType = CommandType.StoredProcedure;

				// 3. add parameter to command, which will be passed to the stored procedure
				cmd.Parameters.Add(new SqlParameter("@ID", productDetails.ProductId));
				cmd.Parameters.Add(new SqlParameter("@Name", productDetails.ProductName));
				cmd.Parameters.Add(new SqlParameter("@Brand", productDetails.Brand));
				cmd.Parameters.Add(new SqlParameter("@ImageLink", productDetails.ImageLink));
				cmd.Parameters.Add(new SqlParameter("@CurrentPrice", productDetails.CurrentPrice));
				cmd.Parameters.Add(new SqlParameter("@WebsiteURL", productDetails.WebsiteUrl));
				cmd.Parameters.Add(new SqlParameter("@Category", categoryID));
				cmd.Parameters.Add(new SqlParameter("@SearchID", searchID));
				cmd.Parameters.Add(new SqlParameter("@Popularity", productDetails.Popularity));

				try
				{
					conn.Open();
					cmd.ExecuteNonQuery();
				}
				catch (SqlException ex)
				{
					LogDatabaseAccessError(ex);
					return false;
				}

				return true;
			}
		}

		public static int GetCategoryID(int searchID)
		{
			using (SqlConnection conn = new SqlConnection(ConnectionToDatabase.ConnectionString))
			{
				// 1.  create a command object identifying the stored procedure
				SqlCommand cmd = new SqlCommand("GetCategoryID", conn);

				// 2. set the command object so it knows to execute a stored procedure
				cmd.CommandType = CommandType.StoredProcedure;

				// 3. add parameter to command, which will be passed to the stored procedure
				cmd.Parameters.Add(new SqlParameter("@SearchID", searchID));

				var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
				returnParameter.Direction = ParameterDirection.ReturnValue;

				try
				{
					conn.Open();
					cmd.ExecuteNonQuery();

					var result = returnParameter.Value;
					return (int)result;
				}
				catch (SqlException ex)
				{
					LogDatabaseAccessError(ex);
					return 0;
				}
			}
		}

		public static void LogDatabaseAccessError(SqlException ex)
		{
			StringBuilder errorMessages = new StringBuilder();
			for (int i = 0; i < ex.Errors.Count; i++)
			{
				errorMessages.Append("Index #" + i + "\n" +
					"Message: " + ex.Errors[i].Message + "\n" +
					"LineNumber: " + ex.Errors[i].LineNumber + "\n" +
					"Source: " + ex.Errors[i].Source + "\n" +
					"Procedure: " + ex.Errors[i].Procedure + "\n");
			}
			_ = new LogWriter("DatabaseConnectionLogs", errorMessages.ToString());
		}
	}
}