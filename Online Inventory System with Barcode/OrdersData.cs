using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;

namespace NibsInventoryManagementSystem
{
    internal class OrdersData
    {
        MySqlConnection connect = new MySqlConnection (Form1.gateway);


        public int ID { get; set; }
        public string Item { set; get; }
        public string ProductID { set; get; }
        public string ProductName { set; get; }
        public string Category { set; get; }
        public string QTY { set; get; }
        public string Unit {  set; get; }
        public string Date { set; get; }

        public List<OrdersData> allOrdersData()
        {
            List<OrdersData> listData = new List<OrdersData>();

            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();

                    MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                    direct.ExecuteNonQuery();


                    
                    int custID = 0;
                    string selectCustData = "SELECT MAX(transac_id) FROM orders";

                    using (MySqlCommand cmd = new MySqlCommand(selectCustData, connect))
                    {
                        object result = cmd.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            int temp = Convert.ToInt32(result);

                            if (temp == 0)
                            {
                                custID = 1;
                            }
                            else
                            {
                                custID = temp;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error ID");
                        }
                    }

                    string selectData = "SELECT * FROM orders ";

                    using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                    {

                        MySqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            OrdersData oData = new OrdersData();

                            oData.ID = (int)reader["id"];
                            oData.Item = reader["transac_id"].ToString();
                            oData.ProductID = reader["prod_id"].ToString();
                            oData.ProductName = reader["prod_name"].ToString();
                            oData.Category = reader["category"].ToString();
                            oData.QTY = reader["qty"].ToString();
                            oData.Unit = reader["unit"].ToString();
                            oData.Date = reader["order_date"].ToString();

                            listData.Add(oData);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection Error: " + ex);
                }
                finally
                {
                    connect.Close();
                }
            }
            return listData;
        }
    }
}
