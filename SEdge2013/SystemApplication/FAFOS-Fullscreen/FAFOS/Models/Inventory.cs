using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Diagnostics;

namespace FAFOS
{
    class Inventory
    {
        SqlDataAdapter ServicesSQLDataAdapter;
        SqlDataAdapter ProductsSQLDataAdapter;
        DataTable servicesTable;
        DataTable productsTable;

        public bool set(String orderid, int userid)
        {
            String connString = Properties.Settings.Default.FAFOS;
            int[] quantity;
            int[] id;
            int size = 0;
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT COUNT(item_id) FROM Order_Items where quantity IS NOT NULL AND sales_order_id = " + orderid, con);

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
                size = Convert.ToInt32(reader[0]);

            con.Close();

            con.Open();
            quantity = new int[size];
            id = new int[size];
            command = new SqlCommand("SELECT item_id, quantity FROM Order_Items where quantity IS NOT NULL AND sales_order_id = " + orderid + " ORDER BY item_id ", con);


            reader = command.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                id[i] = Convert.ToInt32(reader[0]);
                quantity[i] = Convert.ToInt32(reader[1]);
                i++;
            }
            con.Close();

            con.Open();

            SqlCommand command2 = new SqlCommand("SELECT item_id, quantity FROM Franchisee_Item WHERE quantity IS NOT NULL ORDER BY item_id", con);
            SqlDataReader reader2 = command2.ExecuteReader();

            SqlCommand[] commandArray = new SqlCommand[size];
            i = 0;
            while (reader2.Read())
            {
                int idCheck = Convert.ToInt32(reader2[0]);
                for (int j = 0; j < size; j++)
                {
                    if (id[j] == idCheck)
                    {
                        if ((Convert.ToInt32(reader2[1]) - quantity[j]) >= 0)
                        {
                            commandArray[i] = new SqlCommand("UPDATE Franchisee_Item SET quantity =" + (Convert.ToInt32(reader2[1]) - quantity[j])
                                + "  WHERE item_id = " + id[j], con);
                            i++;
                        }
                        else
                        {
                            MessageBox.Show("You need an additional " + (quantity[j] - Convert.ToInt32(reader2[1])) + " quantities of "
                                + getName(id[j], userid) + " to complete this invoice");
                            return false;
                        }

                    }
                }
            }

            con.Close();

            con.Open();
            try
            {
                for (int j = 0; j < size; j++)
                {
                    commandArray[j].ExecuteNonQuery();
                }
            }
            catch (SqlException ef)
            {
                MessageBox.Show("Could not update the quantities for hte following items in the invoice form.");
            }

            con.Close();
            return true;
        }

        public String getName(int id, int userid)
        {
            String connString = Properties.Settings.Default.FAFOS;
            String name = "";
            // int size = 0;
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT  name FROM Franchisee_Item where item_id = " + id + " AND franchisee_id = " + new Users().getFranchiseeId(userid.ToString()), con);

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
                name = reader[0].ToString();

            con.Close();
            return name;
        }
        public string getDescription(String id)
        {
            String connString = Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT description FROM Franchisee_Item WHERE item_id = " + id, con);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            String description = reader[0].ToString();
            reader.Close();
            con.Close();
            return description;
        }
        public DataTable getProducts(String userid)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.FAFOS);
            con.Open();

            productsTable = new DataTable("Products");

            SqlCommand ProductsSelectSQLCommand = new SqlCommand();
            SqlCommand ProductsUpdateSQLCommand = new SqlCommand();
            SqlCommand ProductsInsertSQLCommand = new SqlCommand();

            ProductsSQLDataAdapter = new SqlDataAdapter();

            ProductsSelectSQLCommand.CommandType = CommandType.Text;
            ProductsSelectSQLCommand.CommandText = "SELECT i.item_id,i.name,i.description,i.price,i.cost, i.quantity,c.type,s.name AS supplier FROM Franchisee_Item i,Category c,Supplier s WHERE c.category_id = i.category_id AND s.supplier_id = i.supplier_id AND i.category_id !=2 AND franchisee_id =" + userid + " ORDER BY name";
            ProductsSelectSQLCommand.Connection = con;
            ProductsSQLDataAdapter.SelectCommand = ProductsSelectSQLCommand;

            ProductsUpdateSQLCommand.CommandType = CommandType.Text;
            ProductsUpdateSQLCommand.CommandText = "UPDATE Franchisee_Item Set price = @price,description = @description, quantity = @quantity WHERE item_id = @item_id";
            ProductsUpdateSQLCommand.Parameters.Add("@item_id", SqlDbType.SmallInt).SourceColumn = "item_id";
            ProductsUpdateSQLCommand.Parameters.Add("@quantity", SqlDbType.SmallInt).SourceColumn = "quantity";
            ProductsUpdateSQLCommand.Parameters.Add("@description", SqlDbType.VarChar, 50).SourceColumn = "description";
            ProductsUpdateSQLCommand.Parameters.Add("@price", SqlDbType.Float).SourceColumn = "price";
            ProductsUpdateSQLCommand.Connection = con;
            ProductsSQLDataAdapter.UpdateCommand = ProductsUpdateSQLCommand;

            DataSet ds = new DataSet();
            ProductsSQLDataAdapter.Fill(ds, "Franchisee_Item");
            productsTable = ds.Tables["Franchisee_Item"];
            con.Close();
            return productsTable;
        }
        public DataTable getProducts(String userid, String supplier)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.FAFOS);
            con.Open();
            DataTable supplierItemsTable = new DataTable();
            SqlCommand command = new SqlCommand("SELECT i.item_id,i.name,i.description,i.price, i.quantity,s.name AS supplier FROM Franchisee_Item i,Supplier s WHERE s.supplier_id = i.supplier_id AND i.category_id !=2 AND franchisee_id =" + userid + " AND i.supplier_id =" + supplier + " ORDER BY i.name", con);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds, "SupplierItems");
            supplierItemsTable = ds.Tables["SupplierItems"];
            con.Close();
            return supplierItemsTable;
        }
        public DataTable getServices(String userid)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.FAFOS);
            con.Open();

            servicesTable = new DataTable("Services");

            SqlCommand ServicesSelectSQLCommand = new SqlCommand();
            SqlCommand ServicesUpdateSQLCommand = new SqlCommand();
            SqlCommand ServicesInsertSQLCommand = new SqlCommand();
            ServicesSQLDataAdapter = new SqlDataAdapter();

            ServicesSelectSQLCommand.CommandType = CommandType.Text;
            ServicesSelectSQLCommand.CommandText = "SELECT item_id, name,description,price FROM Franchisee_Item WHERE category_id = 2 AND franchisee_id =" + userid + "ORDER BY name";
            ServicesSelectSQLCommand.Connection = con;
            ServicesSQLDataAdapter.SelectCommand = ServicesSelectSQLCommand;

            ServicesUpdateSQLCommand.CommandType = CommandType.Text;
            ServicesUpdateSQLCommand.CommandText = "UPDATE Franchisee_Item Set price = @price, description = @description WHERE item_id = @item_id";
            ServicesUpdateSQLCommand.Parameters.Add("@item_id", SqlDbType.SmallInt).SourceColumn = "item_id";
            ServicesUpdateSQLCommand.Parameters.Add("@description", SqlDbType.VarChar, 50).SourceColumn = "description";
            ServicesUpdateSQLCommand.Parameters.Add("@price", SqlDbType.Float).SourceColumn = "price";
            ServicesUpdateSQLCommand.Connection = con;
            ServicesSQLDataAdapter.UpdateCommand = ServicesUpdateSQLCommand;

            DataSet ds = new DataSet();
            ServicesSQLDataAdapter.Fill(ds, "Franchisee_Item");
            servicesTable = ds.Tables["Franchisee_Item"];
            con.Close();
            Debug.Write("1");
            return servicesTable;

        }
        public void deleteService(String id)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.FAFOS);
            con.Open();
            SqlCommand ServicesDeleteSQLCommand = new SqlCommand();
            ServicesDeleteSQLCommand.CommandType = CommandType.Text;
            ServicesDeleteSQLCommand.CommandText = "DELETE FROM franchisee_item WHERE item_id =" + id;
            ServicesDeleteSQLCommand.Connection = con;
            ServicesDeleteSQLCommand.ExecuteNonQuery();
            con.Close();
        }
        public void deleteProduct(String id)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.FAFOS);
            con.Open();
            SqlCommand ProductsDeleteSQLCommand = new SqlCommand();
            ProductsDeleteSQLCommand.CommandType = CommandType.Text;
            ProductsDeleteSQLCommand.CommandText = "DELETE FROM franchisee_item WHERE item_id =" + id;
            ProductsDeleteSQLCommand.Connection = con;
            ProductsDeleteSQLCommand.ExecuteNonQuery();
            con.Close();
        }
        public void updateServices()
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.FAFOS);
            con.Open();
            ServicesSQLDataAdapter.Update(servicesTable);
            con.Close();
        }
        public void updateProducts()
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.FAFOS);
            con.Open();
            ProductsSQLDataAdapter.Update(productsTable);
            con.Close();
        }
        public void insertProduct(String franchisee_id, String name, String description, String price, String category_id, String supplier_id)
        {
            float floatcheck;
            SqlConnection con = new SqlConnection(Properties.Settings.Default.FAFOS);
            con.Open();
            if (name == "")
            {
                MessageBox.Show("Please enter a name");
                return;
            }
            if (description == "")
            {
                MessageBox.Show("Please enter a description");
                return;
            }
            if (price == "")
            {
                MessageBox.Show("Please enter a price");
                return;
            }
            if (category_id == "")
            {
                MessageBox.Show("Please select a category");
                return;
            }
            if (supplier_id == "")
            {
                MessageBox.Show("Please select a supplier");
                return;
            }
            try
            {
                floatcheck = Convert.ToSingle(price);
                if (floatcheck < 0)
                {
                    MessageBox.Show("Please enter a valid number for price");
                    return;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid number for price");
                return;
            }

            SqlCommand command = new SqlCommand("INSERT INTO franchisee_item (franchisee_id, name, description, price, cost, quantity,category_id,supplier_id) VALUES (@franchisee_id, @name, @description, @price, 0,0, @category_id,@supplier_id)", con);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@description", description);
            command.Parameters.AddWithValue("@price", price);
            command.Parameters.AddWithValue("@category_id", category_id);
            command.Parameters.AddWithValue("@supplier_id", supplier_id);
            command.Parameters.AddWithValue("@franchisee_id", franchisee_id);
            command.ExecuteNonQuery();
            con.Close();

        }
        public void insertService(String franchisee_id, String name, String description, String price)
        {
            float floatcheck;
            int check;
            SqlConnection con = new SqlConnection(Properties.Settings.Default.FAFOS);
            con.Open();
            if (name == "")
            {
                MessageBox.Show("Please enter a name");
                return;
            }
            if (description == "")
            {
                MessageBox.Show("Please enter a description");
                return;
            }
            if (price == "")
            {
                MessageBox.Show("Please enter a price");
                return;
            }
         

            try
            {
                floatcheck = Convert.ToSingle(price);
                if (floatcheck < 0)
                {
                    MessageBox.Show("Please enter a valid number for price");
                    return;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid number for price");
                return;
            }
            
            SqlCommand command = new SqlCommand("INSERT INTO franchisee_item (franchisee_id, name, description, price, category_id) VALUES (@franchisee_id, @name, @description, @price,2)", con);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@description", description);
            command.Parameters.AddWithValue("@price", price);
            command.Parameters.AddWithValue("@franchisee_id", franchisee_id);
            command.ExecuteNonQuery();
            con.Close();
        }
        public void updateQuantity(String item_id, String quantity)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.FAFOS);
            con.Open();
            SqlCommand command = new SqlCommand("SELECT quantity FROM franchisee_item WHERE item_id=@item_id", con);
            command.Parameters.AddWithValue("@item_id", item_id);
            command.Parameters.AddWithValue("@quantity", quantity);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            String quantity1 = reader[0].ToString();
            reader.Close();
            quantity = (Convert.ToInt32(quantity) + Convert.ToInt32(quantity1)).ToString();
            command = new SqlCommand("UPDATE franchisee_item SET quantity=@quantity WHERE item_id = @item_id", con);
            command.Parameters.AddWithValue("@item_id", item_id);
            command.Parameters.AddWithValue("@quantity", quantity);
            command.ExecuteNonQuery();
            con.Close();
        }
        public void updateCost(String item_id, String cost)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.FAFOS);
            con.Open();
            SqlCommand command = new SqlCommand("SELECT cost FROM franchisee_item WHERE item_id=@item_id", con);
            command.Parameters.AddWithValue("@item_id", item_id);
            command.Parameters.AddWithValue("@cost", cost);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            String cost1 = reader[0].ToString();
            reader.Close();
            cost = ((Convert.ToInt32(cost1) + Convert.ToInt32(cost)) / 2).ToString();
            command = new SqlCommand("UPDATE franchisee_item SET cost=@cost WHERE item_id = @item_id", con);
            command.Parameters.AddWithValue("@item_id", item_id);
            command.Parameters.AddWithValue("@cost", cost);
            command.ExecuteNonQuery();
            con.Close();
        }
    }
}
