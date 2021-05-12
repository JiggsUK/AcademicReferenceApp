using RefCatalogue.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace RefCatalogue.Controllers
{
    internal class DataProcessor : IDataProcessor
    {
        private readonly string[] RefTypes = { "Book", "ConfPaper", "RFC", "Website", "Blog", "Journal" };

        public DataTable SelectAll(DataTable data)
        {
            using var con = new SqlConnection(Settings.Default.ConnectionString);
            con.Open();

            foreach (var type in RefTypes)
            {
                using var cmd = new SqlCommand($"dbo.{type}_Select_All", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using var da = new SqlDataAdapter(cmd);
                da.Fill(data);
            }

            return data;
        }

        public void DeleteReference(string id, string tableName)
        {
            using var con = new SqlConnection(Settings.Default.ConnectionString);
            con.Open();
            using var cmd = new SqlCommand("dbo.Delete", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@tableName", SqlDbType.VarChar).Value = tableName;

            cmd.ExecuteNonQuery();
        }
        
        public DataView RetrieveAllReferences()
        {
            var dt = new DataTable("allRefs");
            var allRefs = SelectAll(dt).DefaultView;
            dt.DefaultView.Sort = "Author1SN asc";
            return allRefs;
        }
        public int PushBookDetailsToDatabase(Dictionary<string, string> bookDetails, string sqlCom)
        {
            using var con = new SqlConnection(Settings.Default.ConnectionString);
            con.Open();
            using var cmd = new SqlCommand(sqlCom, con)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (sqlCom == "dbo.Update_Book")
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(bookDetails["id"]);
            }
            cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = bookDetails["title"];
            cmd.Parameters.Add("@author1FN", SqlDbType.VarChar).Value = bookDetails["au1first"];
            cmd.Parameters.Add("@author1SN", SqlDbType.VarChar).Value = bookDetails["au1last"];
            cmd.Parameters.Add("@author2FN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(bookDetails["au2first"]) ? DBNull.Value : bookDetails["au2first"];
            cmd.Parameters.Add("@author2SN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(bookDetails["au2last"]) ? DBNull.Value : bookDetails["au2last"];
            cmd.Parameters.Add("@author3FN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(bookDetails["au3first"]) ? DBNull.Value : bookDetails["au3first"];
            cmd.Parameters.Add("@author3SN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(bookDetails["au3last"]) ? DBNull.Value : bookDetails["au3last"];
            cmd.Parameters.Add("@author4FN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(bookDetails["au4first"]) ? DBNull.Value : bookDetails["au4first"];
            cmd.Parameters.Add("@author4SN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(bookDetails["au4last"]) ? DBNull.Value : bookDetails["au4last"];
            cmd.Parameters.Add("@bookYear", SqlDbType.Int).Value = Convert.ToInt32(bookDetails["year"]);
            cmd.Parameters.Add("@publisher", SqlDbType.VarChar).Value = bookDetails["publisher"];
            cmd.Parameters.Add("@publisherLoc", SqlDbType.VarChar).Value = bookDetails["publisherLoc"];
            cmd.Parameters.Add("@edition", SqlDbType.Int).Value = string.IsNullOrEmpty(bookDetails["edition"]) ? -1 : Convert.ToInt32(bookDetails["edition"]);

            cmd.ExecuteNonQuery();
            return 1;
        }

        public int PushConfPaperDetailsToDatabase(Dictionary<string, string> confPaperDetails, string sqlCom)
        {
            using var con = new SqlConnection(Settings.Default.ConnectionString);
            con.Open();
            using var cmd = new SqlCommand(sqlCom, con)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (sqlCom == "dbo.Update_ConfPaper")
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(confPaperDetails["id"]);
            }

            cmd.Parameters.Add("@paperTitle", SqlDbType.VarChar).Value = confPaperDetails["paperTitle"];
            cmd.Parameters.Add("@author1FN", SqlDbType.VarChar).Value = confPaperDetails["au1first"];
            cmd.Parameters.Add("@author1SN", SqlDbType.VarChar).Value = confPaperDetails["au1last"];
            cmd.Parameters.Add("@author2FN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(confPaperDetails["au2first"]) ? DBNull.Value : confPaperDetails["au2first"];
            cmd.Parameters.Add("@author2SN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(confPaperDetails["au2last"]) ? DBNull.Value : confPaperDetails["au2last"];
            cmd.Parameters.Add("@author3FN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(confPaperDetails["au3first"]) ? DBNull.Value : confPaperDetails["au3first"];
            cmd.Parameters.Add("@author3SN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(confPaperDetails["au3last"]) ? DBNull.Value : confPaperDetails["au3last"];
            cmd.Parameters.Add("@author4FN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(confPaperDetails["au4first"]) ? DBNull.Value : confPaperDetails["au4first"];
            cmd.Parameters.Add("@author4SN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(confPaperDetails["au4last"]) ? DBNull.Value : confPaperDetails["au4last"];
            cmd.Parameters.Add("@confPaperYear", SqlDbType.Int).Value = Convert.ToInt32(confPaperDetails["year"]);
            cmd.Parameters.Add("@confTitle", SqlDbType.VarChar).Value = confPaperDetails["confTitle"];
            cmd.Parameters.Add("@confSubTitle", SqlDbType.VarChar).Value = string.IsNullOrEmpty(confPaperDetails["confSubTitle"]) ? DBNull.Value : confPaperDetails["confSubTitle"];
            cmd.Parameters.Add("@confLoc", SqlDbType.VarChar).Value = confPaperDetails["confLoc"];
            cmd.Parameters.Add("@confDateFrom", SqlDbType.VarChar).Value = confPaperDetails["confDateFrom"];
            cmd.Parameters.Add("@confDateTo", SqlDbType.VarChar).Value = confPaperDetails["confDateTo"];
            cmd.Parameters.Add("@publisher", SqlDbType.VarChar).Value = confPaperDetails["publisher"];
            cmd.Parameters.Add("@publisherLoc", SqlDbType.VarChar).Value = confPaperDetails["publisherLoc"];
            cmd.Parameters.Add("@pageFrom", SqlDbType.Int).Value = Convert.ToInt32(confPaperDetails["pageFrom"]);
            cmd.Parameters.Add("@pageTo", SqlDbType.Int).Value = Convert.ToInt32(confPaperDetails["pageTo"]);

            cmd.ExecuteNonQuery();
            return 1;
        }

        public int PushJournalDetailsToDatabase(Dictionary<string, string> journalDetails, string sqlCom)
        {
            using var con = new SqlConnection(Settings.Default.ConnectionString);
            con.Open();
            using var cmd = new SqlCommand(sqlCom, con)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (sqlCom == "dbo.Update_Journal")
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(journalDetails["id"]);
            }

            cmd.Parameters.Add("@articletitle", SqlDbType.VarChar).Value = journalDetails["articletitle"];
            cmd.Parameters.Add("@journaltitle", SqlDbType.VarChar).Value = journalDetails["journaltitle"];
            cmd.Parameters.Add("@author1FN", SqlDbType.VarChar).Value = journalDetails["au1first"];
            cmd.Parameters.Add("@author1SN", SqlDbType.VarChar).Value = journalDetails["au1last"];
            cmd.Parameters.Add("@author2FN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(journalDetails["au2first"]) ? DBNull.Value : journalDetails["au2first"];
            cmd.Parameters.Add("@author2SN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(journalDetails["au2last"]) ? DBNull.Value : journalDetails["au2last"];
            cmd.Parameters.Add("@author3FN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(journalDetails["au3first"]) ? DBNull.Value : journalDetails["au3first"];
            cmd.Parameters.Add("@author3SN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(journalDetails["au3last"]) ? DBNull.Value : journalDetails["au3last"];
            cmd.Parameters.Add("@author4FN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(journalDetails["au4first"]) ? DBNull.Value : journalDetails["au4first"];
            cmd.Parameters.Add("@author4SN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(journalDetails["au4last"]) ? DBNull.Value : journalDetails["au4last"];
            cmd.Parameters.Add("@journalYear", SqlDbType.Int).Value = Convert.ToInt32(journalDetails["year"]);
            cmd.Parameters.Add("@pageFrom", SqlDbType.Int).Value = string.IsNullOrEmpty(journalDetails["pageFrom"]) ? -1 : Convert.ToInt32(journalDetails["pageFrom"]);
            cmd.Parameters.Add("@pageTo", SqlDbType.Int).Value = string.IsNullOrEmpty(journalDetails["pageTo"]) ? -1 : Convert.ToInt32(journalDetails["pageTo"]);
            cmd.Parameters.Add("@volume", SqlDbType.Int).Value = string.IsNullOrEmpty(journalDetails["volume"]) ? -1 : Convert.ToInt32(journalDetails["volume"]);
            cmd.Parameters.Add("@partNo", SqlDbType.Int).Value = string.IsNullOrEmpty(journalDetails["partNo"]) ? -1 : Convert.ToInt32(journalDetails["partNo"]);

            cmd.ExecuteNonQuery();
            return 1;
        }

        public int PushWebsiteDetailsToDatabase(Dictionary<string, string> webDetails, string sqlCom)
        {
            using var con = new SqlConnection(Settings.Default.ConnectionString);
            con.Open();
            using var cmd = new SqlCommand(sqlCom, con)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (sqlCom == "dbo.Update_Website")
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(webDetails["id"]);
            }

            cmd.Parameters.Add("@webpagetitle", SqlDbType.VarChar).Value = webDetails["webpagetitle"];
            cmd.Parameters.Add("@organisation", SqlDbType.VarChar).Value = string.IsNullOrEmpty(webDetails["web1first"]) ? DBNull.Value : webDetails["web1first"];
            cmd.Parameters.Add("@author1FN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(webDetails["web1first"]) ? DBNull.Value : webDetails["web1first"];
            cmd.Parameters.Add("@author1SN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(webDetails["web1last"]) ? DBNull.Value : webDetails["web1last"];
            cmd.Parameters.Add("@author2FN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(webDetails["web2first"]) ? DBNull.Value : webDetails["web2first"];
            cmd.Parameters.Add("@author2SN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(webDetails["web2last"]) ? DBNull.Value : webDetails["web2last"];
            cmd.Parameters.Add("@author3FN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(webDetails["web3first"]) ? DBNull.Value : webDetails["web3first"];
            cmd.Parameters.Add("@author3SN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(webDetails["web3last"]) ? DBNull.Value : webDetails["web3last"];
            cmd.Parameters.Add("@websiteYear", SqlDbType.Int).Value = Convert.ToInt32(webDetails["year"]);
            cmd.Parameters.Add("@webURL", SqlDbType.VarChar).Value = webDetails["webURL"];
            cmd.Parameters.Add("@accessDate", SqlDbType.VarChar).Value = webDetails["accessDate"];

            cmd.ExecuteNonQuery();
            return 1;
        }

        public int PushBlogDetailsToDatabase(Dictionary<string, string> blogDetails, string sqlCom)
        {
            using var con = new SqlConnection(Settings.Default.ConnectionString);
            con.Open();
            using var cmd = new SqlCommand(sqlCom, con)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (sqlCom == "dbo.Update_Blog")
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(blogDetails["id"]);
            }

            cmd.Parameters.Add("@articleTitle", SqlDbType.VarChar).Value = blogDetails["articleTitle"];
            cmd.Parameters.Add("@author1FN", SqlDbType.VarChar).Value = blogDetails["web1first"];
            cmd.Parameters.Add("@author1SN", SqlDbType.VarChar).Value = blogDetails["web1last"];
            cmd.Parameters.Add("@author2FN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(blogDetails["web2first"]) ? DBNull.Value : blogDetails["web2first"];
            cmd.Parameters.Add("@author2SN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(blogDetails["web2last"]) ? DBNull.Value : blogDetails["web2last"];
            cmd.Parameters.Add("@author3FN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(blogDetails["web3first"]) ? DBNull.Value : blogDetails["web3first"];
            cmd.Parameters.Add("@author3SN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(blogDetails["web3last"]) ? DBNull.Value : blogDetails["web3last"];
            cmd.Parameters.Add("@websiteYear", SqlDbType.Int).Value = Convert.ToInt32(blogDetails["year"]);
            cmd.Parameters.Add("@webURL", SqlDbType.VarChar).Value = blogDetails["webURL"];
            cmd.Parameters.Add("@accessDate", SqlDbType.VarChar).Value = blogDetails["accessDate"];
            cmd.Parameters.Add("@websiteName", SqlDbType.VarChar).Value = blogDetails["websiteName"];
            cmd.Parameters.Add("@postedDate", SqlDbType.VarChar).Value = blogDetails["postedDate"];

            cmd.ExecuteNonQuery();
            return 1;
        }

        public int PushRfcDetailsToDatabase(Dictionary<string, string> rfcDetails, string sqlCom)
        {
            using var con = new SqlConnection(Settings.Default.ConnectionString);
            con.Open();
            using var cmd = new SqlCommand(sqlCom, con)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (sqlCom == "dbo.Update_RFC")
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(rfcDetails["id"]);
            }

            cmd.Parameters.Add("@RFCTitle", SqlDbType.VarChar).Value = rfcDetails["RFCTitle"];
            cmd.Parameters.Add("@author1FN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(rfcDetails["rfc1first"]) ? DBNull.Value : rfcDetails["rfc1first"];
            cmd.Parameters.Add("@author1SN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(rfcDetails["rfc1last"]) ? DBNull.Value : rfcDetails["rfc1last"];
            cmd.Parameters.Add("@author2FN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(rfcDetails["rfc2first"]) ? DBNull.Value : rfcDetails["rfc2first"];
            cmd.Parameters.Add("@author2SN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(rfcDetails["rfc2last"]) ? DBNull.Value : rfcDetails["rfc2last"];
            cmd.Parameters.Add("@author3FN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(rfcDetails["rfc3first"]) ? DBNull.Value : rfcDetails["rfc3first"];
            cmd.Parameters.Add("@author3SN", SqlDbType.VarChar).Value = string.IsNullOrEmpty(rfcDetails["rfc3last"]) ? DBNull.Value : rfcDetails["rfc3last"];
            cmd.Parameters.Add("@RFCYear", SqlDbType.Int).Value = Convert.ToInt32(rfcDetails["year"]);
            cmd.Parameters.Add("@RFCDocNumber", SqlDbType.Int).Value = Convert.ToInt32(rfcDetails["RFCDocNumber"]);
            cmd.Parameters.Add("@webURL", SqlDbType.VarChar).Value = rfcDetails["webURL"];
            cmd.Parameters.Add("@accessDate", SqlDbType.VarChar).Value = rfcDetails["accessDate"];

            cmd.ExecuteNonQuery();
            return 1;
        }
    }
}