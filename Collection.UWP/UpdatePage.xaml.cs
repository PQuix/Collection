using Collection.DataSource;
using Collection.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Collection.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UpdatePage : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Piece piece = (Piece)Displa
            IdBox.Text = piece.PieceId.ToString();
            TitleBox.Text = piece.PieceTitle;
            AuthorBox.Text = piece.PieceAuthor;
            IsbnBox.Text = piece.PieceIsbn;
            DescBox.Text = piece.PieceDescription;
        }

        public UpdatePage()
        {
            this.InitializeComponent();
        }

        private async void Btn_Click(object sender, RoutedEventArgs e)
        {
            if((sender as Button).Content.ToString() == "Cancel")
            {
                App.RootFrame.Navigate(typeof(DisplayPage));
                return;
            }

            var existingPiece = new Piece
            {
                PieceTitle = TitleBox.Text,
                PieceAuthor = AuthorBox.Text,
                PieceIsbn = IsbnBox.Text,
                PieceDescription = DescBox.Text,
                PieceCover = "http://covers.openlibrary.org/b/isbn/" + IsbnBox.Text + "-M.jpg",
                PieceId = int.Parse(IdBox.Text)
            };

            using (var client = new System.Net.Http.HttpClient())
            {
                var content = JsonConvert.SerializeObject(existingPiece);
                Task task = Task.Run(async () =>
                {
                    var data = new FormUrlEncodedContent(
                    new Dictionary<string, string>
                    {
                        ["value"] = content,
                        ["id"] = App.Pieces.PieceId.ToString()
                    }
                    );
                    await client.PutAsync(App.BaseUri, data);
                });
                task.Wait();
            }

            App.RootFrame.Navigate(typeof(DisplayPage));
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            activateButton();
        }

        private void activateButton()
        {
            UpdateBtn.IsEnabled = (!string.IsNullOrWhiteSpace(TitleBox.Text) &&
                                !string.IsNullOrWhiteSpace(AuthorBox.Text) &&
                                !string.IsNullOrWhiteSpace(IsbnBox.Text) &&
                                !string.IsNullOrWhiteSpace(DescBox.Text));
        }
    }
}
