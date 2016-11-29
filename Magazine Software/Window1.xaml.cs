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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        string connectionString;
        public Window1()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            LoadListSoftware();
        }
        /// <summary>
        /// Isert new intem into table List Software
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                string sql = "INSERT List_software(name_software, description, provider) VALUES ('" + textName.Text +
                    "', '" + textDescription.Text + "', '" + textProvider.Text + "')";
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Insert Ok");
                LoadListSoftware();
                clearAllTextBox();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            
        }
        /// <summary>
        /// Create ListSoftware object from selected item ComboBox
        /// Fill TextBlocks from fields object ListSoftware
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, RoutedEventArgs e)
        {
             ListSoftware currentItem = (ListSoftware)comboBox.SelectedItem;
             textName.Text = currentItem.Name;
             textDescription.Text = currentItem.Description;
             textProvider.Text = currentItem.Provider;      
        }
        /// <summary>
        /// Load list software from table List_Software to ComboBox
        /// </summary>
        private void LoadListSoftware()
        {
            SqlConnection connection = null;
            try
            {
                comboBox.Items.Clear();
                connection = new SqlConnection(connectionString);
                connection.Open();
                string sql = "SELECT * FROM List_software";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        ListSoftware listSoftware = new ListSoftware();
                        listSoftware.ID = dataReader.GetInt32(0);
                        listSoftware.Name = dataReader.GetString(1);
                        listSoftware.Description = dataReader.GetString(2);
                        listSoftware.Provider = dataReader.GetString(3);
                        comboBox.Items.Add(listSoftware);
                        
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
                connection.Close();
            }
        }
        /// <summary>
        /// Update Table "List Software"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                ListSoftware currentItem = (ListSoftware)comboBox.SelectedItem;
                string sql = "UPDATE List_software SET name_software='" + textName.Text +
                    "', description='" + textDescription.Text + "', provider='" + textProvider.Text +
                    "' WHERE ID_software=" + currentItem.ID;
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Update Ok");
                LoadListSoftware();
                clearAllTextBox();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// Delete selected row from table List_Software
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                ListSoftware currentItem = (ListSoftware)comboBox.SelectedItem;
                string sql = "DELETE FROM List_software WHERE ID_software=" + currentItem.ID;
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Delete Ok");
                LoadListSoftware();
                textName.Text = null;
                textDescription.Text = null;
                textProvider.Text = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// Clear TextBox's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBox();
        }
        /// <summary>
        /// Clear all TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearAllTextBox ()
        {
            textName.Text = null;
            textDescription.Text = null;
            textProvider.Text = null;
        }
    }
}
