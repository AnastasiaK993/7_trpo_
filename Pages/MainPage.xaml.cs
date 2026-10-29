using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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

using Cars_Client_seven;
namespace Cars_Client_seven.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {

        private readonly HttpClient _http = new HttpClient { 
        
        BaseAddress =new Uri("https://localhost:7165/")
            }
        ;
        public ObservableCollection<Car> Cars;

        public Car? SelectedCars;
        public MainPage()
        {
            InitializeComponent();
            DataContext = this;
           
        }
           
        private void ClickPost(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PostPage(SelectedCars));
        }

        private async void ClickDelete(object sender, RoutedEventArgs e)
        {
            if (SelectedCars == null)
            {
                MessageBox.Show("Выберите запись!");
                return;
            }
            if (MessageBox.Show("Вы действительно хотите удалить запись?", "Удалить?",
            MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                HttpResponseMessage response = await _http.DeleteAsync($"api/v1/Cars/{SelectedCars.Id}"  );
                if (response.IsSuccessStatusCode)
                {
                    Cars.Remove(SelectedCars);
                    SelectedCars = null;
                    MessageBox.Show($"Товар успешно удален {response.StatusCode}"   );
                }
                else
                {
                    MessageBox.Show($"ошибка {response.StatusCode}");
                }
                
            }
        }

        private void ClickEdit(object sender, RoutedEventArgs e)
        {
            if (SelectedCars == null)
            {
               MessageBox.Show("Выберите машину");
                return;
            }else
            NavigationService.Navigate(new EditPage(SelectedCars));
        }
       
        private void Show(object sender, RoutedEventArgs e)
        {
            if (SelectedCars == null)
            {
                MessageBox.Show("Выберите машину");
                return;
            }
            NavigationService.Navigate(new CarPage(SelectedCars));
        }

        private void SortChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void ClickNormal(object sender, RoutedEventArgs e)
        {var list=await _http.getfromjson

        }
    }
}
