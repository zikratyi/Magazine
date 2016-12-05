using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Magazine_Software
{
    /// <summary>
    /// Interaction logic for Client.xaml
    /// </summary>
    public partial class Client : Window
    {
        string connectionString;
        public Client()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            LoadClientsInfo();

        }

        private void LoadClientsInfo()
        {
            SqlConnection connection = null;
            try
            {
                comboBox.Items.Clear();
                connection = new SqlConnection(connectionString);
                connection.Open();
                string sql = "ViewClients";
                SqlCommand command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader dataReader = command.ExecuteReader();
                //DataTable dataTable = new DataTable();
                //dataTable.Load(dataReader);
                //WindowQuery windowQuery = new WindowQuery();
                //windowQuery.dataGridQuery.ItemsSource = dataTable.DefaultView;
                //windowQuery.Show();
                //dataReader.Close();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        ClientInfo clientInfo = new ClientInfo();
                        clientInfo.ID = dataReader.GetInt32(0);
                        clientInfo.LastName = dataReader[1].ToString();
                        clientInfo.FirstName = dataReader[2].ToString();
                        clientInfo.Phone = dataReader[3].ToString();
                        clientInfo.Email = dataReader[4].ToString();
                        clientInfo.City = dataReader[5].ToString();
                        clientInfo.Street = dataReader[6].ToString();
                        clientInfo.House = dataReader[7].ToString();
                        clientInfo.Flat = (int)dataReader[8];
                        comboBox.Items.Add(clientInfo);

                    }
                }
                dataReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null ) connection.Close();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ClientInfo selectedClient = new ClientInfo();
            selectedClient = (ClientInfo)comboBox.SelectedItem;
            textID.Text = selectedClient.ID.ToString();
            textLastName.Text = selectedClient.LastName;
            textFirstName.Text = selectedClient.FirstName;
            textPhone.Text = selectedClient.Phone;
            textEmail.Text = selectedClient.Email;
            textCity.Text = selectedClient.City;
            textStreet.Text = selectedClient.Street;
            textHouse.Text = selectedClient.House;
            textFlat.Text = selectedClient.Flat.ToString();

        }

        private void butClear_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBox();
        }

        private void ClearTextBox()
        {
            List<TextBox> listBoxItem = new List<TextBox> { textID, textLastName, textFirstName, textPhone, textEmail,
                textCity, textStreet, textHouse, textFlat };
            foreach(TextBox txt in listBoxItem)
            {
                txt.Text = null;
            }
        }

        private void butUpdate_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                string sql = "UpdateClient";
                SqlCommand command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID_client", Convert.ToInt32(textID.Text));
                command.Parameters.AddWithValue("@last_name", textLastName.Text);
                command.Parameters.AddWithValue("@first_name", textFirstName.Text);
                command.Parameters.AddWithValue("@phone", textPhone.Text);
                command.Parameters.AddWithValue("@email", textEmail.Text);
                command.Parameters.AddWithValue("@city", textCity.Text);
                command.Parameters.AddWithValue("@street", textStreet.Text);
                command.Parameters.AddWithValue("@number_house", textHouse.Text);
                command.Parameters.AddWithValue("@number_flat", Convert.ToInt32(textFlat.Text));
                command.ExecuteNonQuery();
                MessageBox.Show("Update Ok");
                LoadClientsInfo();
                ClearTextBox();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }

        private void butDel_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                string sql = "DeleteClient";
                SqlCommand command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID_client", Convert.ToInt32(textID.Text));
                command.ExecuteNonQuery();
                MessageBox.Show("Delete Ok");
                LoadClientsInfo();
                ClearTextBox();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }

        private void butIns_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                string sql = "InsertClient";
                SqlCommand command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID_client", Convert.ToInt32(textID.Text));
                command.Parameters.AddWithValue("@last_name", textLastName.Text);
                command.Parameters.AddWithValue("@first_name", textFirstName.Text);
                command.Parameters.AddWithValue("@phone", textPhone.Text);
                command.Parameters.AddWithValue("@email", textEmail.Text);
                command.Parameters.AddWithValue("@city", textCity.Text);
                command.Parameters.AddWithValue("@street", textStreet.Text);
                command.Parameters.AddWithValue("@number_house", textHouse.Text);
                command.Parameters.AddWithValue("@number_flat", Convert.ToInt32(textFlat.Text));
                // Add output parametr "ID"
                SqlParameter idClient = new SqlParameter();
                idClient.ParameterName = "@ID";
                idClient.SqlDbType = SqlDbType.Int;
                idClient.Direction = ParameterDirection.Output;
                command.Parameters.Add(idClient);
                //Add output parametr "ID_Address"
                SqlParameter idAddress = new SqlParameter();
                idAddress.ParameterName = "@ID_address";
                idAddress.SqlDbType = SqlDbType.Int;
                idAddress.Direction = ParameterDirection.Output;
                command.Parameters.Add(idAddress);
                //Add output paramert "ID_personal_info"
                SqlParameter idPersonalInfo = new SqlParameter();
                idPersonalInfo.ParameterName = "@ID_personal_info";
                idPersonalInfo.SqlDbType = SqlDbType.Int;
                idPersonalInfo.Direction = ParameterDirection.Output;
                command.Parameters.Add(idPersonalInfo);
                command.ExecuteNonQuery();
                int id = (int)command.Parameters["@ID"].Value;
                int idA = (int)command.Parameters["@ID_address"].Value;
                int idP = (int)command.Parameters["@ID_personal_info"].Value;
                string message = "Client ID = " + id.ToString() + " with related rows in table Address (ID = " + idA.ToString() + 
                    "), in table Personal_info (ID = " + idP.ToString() + ")"; 
                MessageBox.Show(message);
                LoadClientsInfo();
                ClearTextBox();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }
    }
   
}
