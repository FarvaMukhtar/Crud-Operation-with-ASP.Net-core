using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CrudCoreProcUpd.Models;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace CrudCoreProcUpd.Service
{
    public class customerDAL
    {
        
        public SqlConnection conn = new SqlConnection("Data Source=192.168.0.201;Initial Catalog=farvaDb;User ID=xprt;Password=xprt");
        public List<SelectListItem> GetdropdownNames()
        {
            List<SelectListItem> names = new List<SelectListItem>();

            
                conn.Open();

                using (SqlCommand command = new SqlCommand("dropdown_name", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string customerId = reader["CustomerID"].ToString();
                            string customerName = reader["CustomerName"].ToString();

                            names.Add(new SelectListItem { Value = customerId, Text = customerName });
                        }
                    }

                }

            
            return names;
        }
        public bool ValidateLogin(string username, string password)
        {
            try
            {
                bool result = false;
                IDataReader rd = null;
                using (SqlCommand cmd = new SqlCommand("sp_customer_login", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerName ", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    conn.Open();

                    // Assuming your stored procedure returns 1 for successful login, 0 for failure
                    rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        result = true;
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (log it or throw it if necessary)
                Console.WriteLine($"Exception during login validation: {ex.Message}");
                throw;
            }
            finally
            {
                // Make sure to close the connection in all cases
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        //////////////////////////////////////////////////////////////////////////////////

        
        public List<customermodel> GetCustomers()
        {

            DataTable ds;
            SqlCommand cmd = new SqlCommand("customersDetails", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            ds = new DataTable();
            da.Fill(ds);
            List<customermodel> list = new List<customermodel>();
            foreach (DataRow dr in ds.Rows)
            {
                list.Add(new customermodel
                {
                    id = Convert.ToInt32(dr["CustomerID"]),
                    name = dr["CustomerName"].ToString(),
                    salary = dr["Salary"].ToString(),
                    email = dr["Email"].ToString(),
                    phoneno = dr["PhoneNo"].ToString(),
                    address = dr["Address"].ToString(),
                    password = dr["Password"].ToString(),
                    //nmtoid = Convert.ToInt32(dr["NametoID"]),

                });
                conn.Close();
            }
            return list;
        }
        public bool InsertCustomer(customermodel customer)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_customer_insert", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", customer.id);
                cmd.Parameters.AddWithValue("@CustomerName", customer.name);
                cmd.Parameters.AddWithValue("@Salary", customer.salary);
                cmd.Parameters.AddWithValue("@Email", customer.email);
                cmd.Parameters.AddWithValue("@PhoneNo", customer.phoneno);
                cmd.Parameters.AddWithValue("@Address", customer.address);
                cmd.Parameters.AddWithValue("@Password", customer.password);
                //conn.Open();
                int r = cmd.ExecuteNonQuery();
                if (r > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
               // conn.Close();
            }

            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                // Make sure to close the connection in all cases
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }


        public bool UpdateCustomer(customermodel customer)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_customer_update", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", customer.id);
                cmd.Parameters.AddWithValue("@CustomerName", customer.name);
                cmd.Parameters.AddWithValue("@Salary", customer.salary);
                cmd.Parameters.AddWithValue("@Email", customer.email);
                cmd.Parameters.AddWithValue("@PhoneNo", customer.phoneno);
                cmd.Parameters.AddWithValue("@Address", customer.address);
                cmd.Parameters.AddWithValue("@Password", customer.password);
                conn.Open();
                int r = cmd.ExecuteNonQuery();
                if (r > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int DeleteCustomer(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_customer_delete", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", id);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
