/**

	Web Service that identifies a triangle type based on the lengths of the 3 sides.
	
	Name: Jacob Zaidi
    Email: jacob@zaidi.nz
    Website: https://jacob.zaidi.nz/
    Mobile: +64 22 084 6961
	GitHub: https://github.io/jacobzaidi/

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Configuration;
using System.Web.Services;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

/// <summary>
/// Summary description for TriangleTool
/// </summary>
[WebService(Namespace = "https://jacob.zaidi.nz/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class TriangleTool : System.Web.Services.WebService
{
    public TriangleTool()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod]
    public List<bool> Check(string a, string b, string c)
    {
        List<bool> result = new List<bool>();
        if (Regex.IsMatch(a, @"^\d+(\.\d+)?$") && (Regex.IsMatch(b, @"^\d+(\.\d+)?$")) && (Regex.IsMatch(c, @"^\d+(\.\d+)?$")))
        {

            double[] vec = { Convert.ToDouble(a), Convert.ToDouble(b), Convert.ToDouble(c) };
            Array.Sort(vec);

            if (vec[0] <= 0) return result;
            

            if ((a.Replace(".", "").Length > 15 || b.Replace(".", "").Length > 15 || c.Replace(".", "").Length > 15)
                && (vec[0] < 7.9 * Math.Pow(10, 28)) && (vec[1] + vec[2] < 7.9 * Math.Pow(10, 28)))
            {
                decimal[] DecimalVec = { Convert.ToDecimal(a), Convert.ToDecimal(b), Convert.ToDecimal(c) };
                Array.Sort(DecimalVec);
                if (DecimalVec[0] + DecimalVec[1] <= DecimalVec[2]) return result;
                result.Add((DecimalVec[0] == DecimalVec[2]));                         // equilateral
                result.Add((DecimalVec[0] == DecimalVec[1]) || (DecimalVec[1] == DecimalVec[2]));   // isosceles 
                result.Add(!result.ElementAt(1));                       // scalene
                if (vec[2] > vec[1]) result.Add(DecimalVec[0] / (DecimalVec[2] - DecimalVec[1]) == (DecimalVec[1] + DecimalVec[2]) / DecimalVec[0]);
                else result.Add(false);

            }
            else
            {
                if (vec[0] + vec[1] <= vec[2]) return result;
                result.Add((vec[0] == vec[2]));                         // equilateral
                result.Add((vec[0] == vec[1]) || (vec[1] == vec[2]));   // isosceles 
                result.Add(!result.ElementAt(1));                       // scalene

                if (vec[2] > vec[1]) result.Add(vec[0] / (vec[2] - vec[1]) == (vec[1] + vec[2]) / vec[0]);
                else result.Add(false);                                 // right angle
            }
        }

        if (result.Count == 4)
        {



            string provider = ConfigurationManager.AppSettings["provider"];
            string connectionString = ConfigurationManager.AppSettings["connectionString"];





            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);

            using (DbConnection connection =
                factory.CreateConnection())
            {
                if (connectionString == null)
                {
                    Console.WriteLine("Connection error");
                    Console.ReadLine();
                    return new List<bool>();// result;
                }

                connection.ConnectionString = connectionString;
                connection.Open();
                DbCommand command = factory.CreateCommand();
                if (command == null)
                {
                    Console.WriteLine("Command error");
                    Console.ReadLine();
                    return new List<bool>();// result;
                }

                command.Connection = connection;
                command.CommandText = "SELECT * FROM TRIANGLES WHERE [Publish Time] = (SELECT MAX([Publish Time]) FROM Triangles)";
                using (DbDataReader dataReader = command.ExecuteReader())

                {
                    while (dataReader.Read())
                    {
                        if (   $"{dataReader["Length 1"]}" == a
                            && $"{dataReader["Length 2"]}" == b
                            && $"{dataReader["Length 3"]}" == c)
                        {
                            return result;
                        }
                        Console.WriteLine($"{dataReader["Length 1"]}");
                    }
                }


                    command.CommandText = "INSERT INTO dbo.Triangles " +
                        "([Length 1], [Length 2], [Length 3], [Equilateral], [Isosceles], [Scalene], [Right Angle], [Publish Time])" +
                        "VALUES (" + a + ", " + b + ", " + c + ", " +
                        Convert.ToInt16(result[0]) + ", " +
                        Convert.ToInt16(result[1]) + ", " +
                        Convert.ToInt16(result[2]) + ", " +
                        Convert.ToInt16(result[3]) + ", " +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'" + ");";
                command.ExecuteNonQuery();
            }

        }

        return result;
    }
    [WebMethod]
    public string Error(String a, String b, String c)
    {
        if (Regex.IsMatch(a, @"^\d+(\.\d+)?$") && (Regex.IsMatch(b, @"^\d+(\.\d+)?$")) && (Regex.IsMatch(c, @"^\d+(\.\d+)?$")))
        {
            double[] vec = { Convert.ToDouble(a), Convert.ToDouble(b), Convert.ToDouble(c) };
            Array.Sort(vec);
            if (vec[0] <= 0) return "Lengths must be greater than zero.";
            if (vec[0] + vec[1] <= vec[2]) return "This is not a valid triangle";
            return "Unknown Error";
        }
        else return "Not a valid input";
    }
    [WebMethod]
    public int GetPageCount()
    {
        try
        {
            string provider = ConfigurationManager.AppSettings["provider"];
            string connectionString = ConfigurationManager.AppSettings["connectionString"];

            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);

            using (DbConnection connection =
                factory.CreateConnection())
            {
                if (connectionString == null)
                {
                    Console.WriteLine("Connection error");
                    Console.ReadLine();
                    return 0;
                }

                connection.ConnectionString = connectionString;
                connection.Open();
                DbCommand command = factory.CreateCommand();
                if (command == null)
                {
                    Console.WriteLine("Command error");
                    Console.ReadLine();
                    return 0;
                }

                command.Connection = connection;
                command.CommandText = "SELECT COUNT(*) FROM Triangles";
                return (int)command.ExecuteScalar();
            }
        }
        catch { }

        return 0;
    }
    [WebMethod]
    public List<String> GetPage(int page)
    {
        List<String> result = new List<String>();
        try
        {
            string provider = ConfigurationManager.AppSettings["provider"];
            string connectionString = ConfigurationManager.AppSettings["connectionString"];

            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);

            using (DbConnection connection =
                factory.CreateConnection())
            {
                if (connectionString == null)
                {
                    Console.WriteLine("Connection error");
                    Console.ReadLine();
                    return result;
                }

                connection.ConnectionString = connectionString;
                connection.Open();
                DbCommand command = factory.CreateCommand();
                if (command == null)
                {
                    Console.WriteLine("Command error");
                    Console.ReadLine();
                    return result;
                }

                command.Connection = connection;
                command.CommandText = "SELECT TOP 1 * FROM(SELECT TOP " + page + " * FROM Triangles ORDER BY ID DESC) z ORDER BY ID";




                using (DbDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        result.Add($"{dataReader["Length 1"]}");
                        result.Add($"{dataReader["Length 2"]}");
                        result.Add($"{dataReader["Length 3"]}");

                        result.Add($"{dataReader["Equilateral"]}");
                        result.Add($"{dataReader["Isosceles"]}");
                        result.Add($"{dataReader["Scalene"]}");
                        result.Add($"{dataReader["Right Angle"]}");
                    }
                }
            }
            return result;
        }
        catch
        {
            List<string> catchResult = new List<string>();
            catchResult.Add("Fail");
            return catchResult;
        }
    }
}