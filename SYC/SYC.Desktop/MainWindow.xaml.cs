using SYC.Core.Storage;
using SYC.Core.Storage.Repositories;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SYC.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IPersonRepository _personRepository;
        public MainWindow()
        {
            InitializeComponent();
            _personRepository = new JsonPersonRepository();

            var people = _personRepository.GetAllPeopleAsync().GetAwaiter().GetResult();
            
            listview.ItemsSource = people;
        }

        private async void textboxsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            listview.IsEnabled = false;
            //Task.Run(() =>
            //{
            //    Thread.Sleep(2000); // delay
            //    this.Dispatcher.BeginInvoke(async () =>
            //    {
                    var people = await _personRepository.SearchPeopleAsync(textboxsearch.Text);
                    listview.ItemsSource = people;
                    listview.IsEnabled = true;
            //    });
            //});
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _personRepository.DisposeAsync();
            base.OnClosing(e);
        }
    }
}
