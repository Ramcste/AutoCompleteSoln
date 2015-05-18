using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoCompleteSoln
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SuggestionListBox.Items.Clear();

            Suggestion.Items.Clear();
            LoadSuggestion();
            
        }

        private void LoadSuggestion()
        {
            string[] arrayname = {"Bithil","Rakib","Masphy","Niloy","Shyam"};

            for (int i = 0; i < arrayname.Length;i++)
                Suggestion.Items.Add(arrayname[i]);
        }

        
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           string search = SearchTextBox.Text;

           SearchTextBox.Focus();

           if (search == "")
           {
               SuggestionListBox.Items.Clear();
               MessageBox.Show("Please Enter a name to search", "Message");
               Suggestion.Items.Clear();
               LoadSuggestion();
           }

           else
           {

               SuggestionListBox.Items.Clear();

               Suggestion.Items.Clear();

               String ConnectionString = @"Server=Ram;Database=Data;Integrated Security=True";
               String query = string.Format("select Name from info where (Name Like '" + search + "%')");
               SqlConnection connection = new SqlConnection(ConnectionString);
               try
               {
                   connection.Open();
                   SqlCommand cmd = new SqlCommand(query, connection);
                   SqlDataReader reader = cmd.ExecuteReader();

                   while (reader.Read())
                   {
                       string sname = reader[0].ToString();

                       SuggestionListBox.Visibility =Visibility.Visible;
                       
                       SuggestionListBox.Items.Add(sname);

                       Suggestion.Items.Add(sname);
                   }

               }
               catch (Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }

           }


        }

        private void SuggestionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BithilEvent_Selected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello Bithil","Message");
        }
    }
}
