using Collection.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Collection.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DisplayPage : Page
    {
        public DisplayPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            using (var client = new HttpClient())
            {
                var response = "";

                Task task = Task.Run(async () =>
                {
                    response = await client.GetStringAsync(App.BaseUri);
                });

                task.Wait();

                ActGrid.ItemsSource = JsonConvert.DeserializeObject<List<Piece>>(response);

                App.Pieces = JsonConvert.DeserializeObject<List<Piece>>(response)[0];
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Content.ToString())
            {
                case "Delete Piece":
                    App.RootFrame.Navigate(typeof(DeletePage));

                    break;

                case "Update Piece":
                    App.RootFrame.Navigate(typeof(UpdatePage));

                    break;

                default:
                    break;
            }
        }

        private void ActGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = ((sender as GridView).SelectedItem as Piece);

            App.Pieces = item;

            Title.Text = item.PieceTitle;
            Author.Text = item.PieceAuthor;
            Isbn.Text = item.PieceIsbn;
            Description.Text = item.PieceDescription;
            Cover.Source = new BitmapImage(new Uri(item.PieceCover, UriKind.Absolute));
        }
    }
}
