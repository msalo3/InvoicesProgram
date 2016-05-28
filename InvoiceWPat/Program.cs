using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceWPat
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }
    class User
    {
        public int id;
        public string name;
        public string password;

        public void StoreNewUserInfo(DBLoader loader)
        {
            string insert = $"INSERT ";
        }
    }
    class Invoice
    {
        public int id;
        public int userId;
        public DateTime date;
        public List<Itemization> itemizations;
        public bool paid;
    }
    class Itemization
    {
        public int id;
        public int itemId;
        public int quanitity;
    }
    class Item
    {
        public int id;
        public string name;
        public string description;
        public decimal price;
        
    }
    class ListOfItemsClass
    {
        public List<Item> itemList;

        public ListOfItemsClass()
        {
            itemList = new List<Item>();
        }

        public void DisplayItems()
        {
            foreach (Item thisItem in itemList)
            {
                Console.WriteLine(thisItem.id + ". " + thisItem.name + " " + thisItem.price + "\n\t" + thisItem.description);
            }
        }
    }
    class DBLoader
    {

        public SqlConnection connection;
        public SqlCommand command;

        public DBLoader()
        {
            connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDBFilename=\\mac\home\documents\visual studio 2015\Projects\InvoiceWPat\InvoiceWPat\CompanyDB.mdf;Integrated Security=True");
        }

        public void InsertData(string insert)
        {
            command = new SqlCommand(insert, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public int InsertDataAndGetId(string insert)
        {
            int newId = 0;
            command = new SqlCommand(insert, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    newId = Convert.ToInt16(reader["ID"]);
                }
            }
            reader.Close();
            connection.Close();
            return newId;
        }
        public int GetIdWithSelect(string select, string id)
        {
            int newId = 0;
            command = new SqlCommand(select, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    newId = Convert.ToInt16(reader[id]);
                }
            }
            reader.Close();
            connection.Close();
            return newId;
        }
        public void GetInventory(string select)
        {
            ListOfItemsClass listOfItemsClass = new ListOfItemsClass();

            command = new SqlCommand(select, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Item item1 = new Item();
                    item1.id = Convert.ToInt16(reader["id"]);
                    item1.description = reader["Description"].ToString();
                    item1.name = reader["ItemName"].ToString();
                    item1.price = Convert.ToInt16(reader["Price"]);
                    listOfItemsClass.itemList.Add(item1);
                }
            }
            reader.Close();
            connection.Close();
        }


    }
}
