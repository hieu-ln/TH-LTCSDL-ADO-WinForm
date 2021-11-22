using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

using LTCSDL.DTO;

namespace LTCSDL.DAL
{
    public class CategoryDAL
    {
        private SqlConnection cnn;
        private SqlCommand cmd;

        public CategoryDAL()
        {
            string cnStr = "Server = localhost; Database = Northwind; User id = sa; password = Password123;";
            cnn = new SqlConnection(cnStr);
        }

        public List<Category> GetAll()
        {
            string sqlStr = "select CategoryID, CategoryName, [Description]  from Categories";
            List<Category> lst = new List<Category>();
            try
            {
                cnn.Open();
                cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = sqlStr;
                cmd.CommandType = CommandType.Text;

                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    Category cat = new Category();
                    cat.CategoryID = int.Parse(reader["CategoryID"].ToString());
                    cat.CategoryName = reader["CategoryName"].ToString();
                    cat.Description = reader["Description"].ToString();
                    lst.Add(cat);
                }
                reader.Close();
                cnn.Close();
            }
            catch (Exception e)
            {
                lst = null;
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return lst;
        }

        public int Insert(Category cat)
        {
            StringBuilder sb = new StringBuilder("Insert into Categories(CategoryName, [Description]) ");
            sb.AppendFormat("values(N'{0}',N'{1}'); ", cat.CategoryName, cat.Description);
            sb.Append("select cast(@@IDENTITY as int)");
            int res = 0;
            try
            {
                cnn.Open();
                cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = sb.ToString();
                cmd.CommandType = CommandType.Text;
                res = (int)cmd.ExecuteScalar();
                cnn.Close();
            }
            catch(Exception e)
            {
                res = 0;
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return res;
        }

        public int Update(Category cat)
        {
            StringBuilder sb = new StringBuilder("Update Categories set ");
            sb.AppendFormat(" CategoryName = N'{0}', [Description] = N'{1}' ", cat.CategoryName, cat.Description);
            sb.AppendFormat(" Where CategoryID = {0}", cat.CategoryID);
            int res = 0;
            try
            {
                cnn.Open();
                cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = sb.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                res = 1;
                cnn.Close();
            }
            catch (Exception e)
            {
                res = 0;
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return res;
        }

        public int Delete(int id)
        {
            StringBuilder sb = new StringBuilder("DELETE Categories ");           
            sb.AppendFormat(" Where CategoryID = {0}", id);
            int res = 0;
            try
            {
                cnn.Open();
                cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = sb.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                res = 1;
                cnn.Close();
            }
            catch (Exception e)
            {
                res = 0;
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return res;
        }
    }
}
