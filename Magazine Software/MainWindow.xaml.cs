using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Magazine_Software
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString;
        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        /// <summary>
        /// Execute query1 - select request with seleceted Manager and selected Item software
        /// Result query into DataGrid with DataReader and DataTable
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
                string sql = "select R.ID_request, R.data_request, LS.name_software from[dbo].[Request] R join[dbo].[Manager] M " +
                             "on R.manager = M.ID_manager join[dbo].[Personal_info] P " +
                             "on M.personal_info = P.ID_personal_info join[dbo].[Price_software] PRS " +
                             "on R.article_software = PRS.article join[dbo].[List_software] LS " +
                             "on PRS.software = LS.ID_software " +
                             "where P.last_name = 'Пупкін' and P.first_name = 'Іван' and LS.name_software = 'SQL Server Enterprise Edition'";

                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader dataReader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                dataGrid.ItemsSource = dataTable.DefaultView;
                dataReader.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                connection.Close();
            }
        }
        /// <summary>
        /// Create new Window for operation Update table List_software
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Window1 AddSoftware = new Window1();
            AddSoftware.Show();
        }
        /// <summary>
        /// Execute Query 3 - select Item software, what no sale last mounth
        /// Result query into DataGrid with DataAdapter and DataSet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                string sql = "select distinct PRS.article, LS.name_software, PRS.item " +
                             "from[dbo].[Price_software] PRS join[dbo].[List_software] LS " +
                             "on PRS.software = LS.ID_software left join[dbo].[Request] R on PRS.article = R.article_software " +
                             "where NOT(R.data_request between '2016-10-01' and '2016-10-30') or R.data_request is null";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "No sale");
                dataGrid.ItemsSource = dataSet.Tables["No sale"].DefaultView;
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
        /// <summary>
        /// Execute Query 2 - select Request from selected City
        /// Result query into DataGrid in NewWindow with DataReader
        /// Generation templete DataGrid
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
                string sql = "select R.ID_request, R.data_request, CF.name_firm, P.last_name, P.first_name, P.phone, P.e_mail " +
                                "from [dbo].[Request] R left join [dbo].[Client_firm] CF on R.client_firm = CF.ID_firm left join [dbo].[Client] C " +
                                "on R.client = C.ID_client join[dbo].[Address] A on CF.address_firm = A.ID_address or C.address_client = A.ID_address join [dbo].[Personal_info] P " +
                                "on P.ID_personal_info = CF.personal_info_confidant or P.ID_personal_info = C.personal_info where A.city = 'Київ'";

                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader dataReader = command.ExecuteReader();
                           
                if (dataReader.HasRows)
                {
                    WindowQuery windowQuery = new WindowQuery();
                    // Generation template DataGrid and binding with fields object Query2
                    List<string> listNameColumn = new List<string> { "ID", "DataRequest", "FirmName", "LastName", "FirstName", "Phone", "EMail" };
                    
                    for (int i = 0; i < listNameColumn.Count; i++)
                    {
                        DataGridTextColumn col = new DataGridTextColumn();
                        col.Header = listNameColumn[i].ToString();
                        col.Binding = new Binding(listNameColumn[i].ToString());
                        windowQuery.dataGridQuery.Columns.Add(col);
                    }
                    while (dataReader.Read())
                    {
                        Query2 query = new Query2();
                        query.ID = dataReader.GetInt32(0);
                        query.DataRequest = dataReader.GetValue(1).ToString();
                        query.FirmName = dataReader[2].ToString();
                        query.LastName = dataReader.GetString(3);
                        query.FirstName = dataReader.GetString(4);
                        query.Phone = dataReader.GetString(5);
                        query.EMail = dataReader.GetString(6);
                        windowQuery.dataGridQuery.Items.Add(query);
                    }
                    windowQuery.Show();
                }

                dataReader.Close();
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
