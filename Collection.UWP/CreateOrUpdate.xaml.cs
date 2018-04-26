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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Collection.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateOrUpdate : Page
    {
        public CreateOrUpdate()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter as bool? == false)
            {
                var piece = App.Pieces;
                Id.Text = piece.PieceId.ToString();
                Title.Text = piece.PieceTitle;
                Author.Text = piece.PieceAuthor;
                Isbn.Text = piece.PieceIsbn;
                Description.Text = piece.PieceDescription;
            }
        }

        private async void Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.ToString() == "Cancel")
            {
                App.RootFrame.Navigate(typeof(ShowPieces));

                return;
            }

            var existingPiece = new Piece
            {
                PieceTitle = Title.Text,
                PieceAuthor = Author.Text,
                PieceIsbn = Isbn.Text,
                PieceDescription = Description.Text
            };

            var newPiece = new Piece
            {
                PieceTitle = Title.Text,
                PieceAuthor = Author.Text,
                PieceIsbn = Isbn.Text,
                PieceDescription = Description.Text
            };

            using (var client = new System.Net.Http.HttpClient())
            {
                var json = JsonConvert.SerializeObject(newPiece);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var result = client.PostAsync(App.BaseUri, byteContent).Result;
                /*var data = new System.Net.Http.FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["value"] = json
                });
                await client.PostAsync(App.BaseUri, data);*/

                /*var content = JsonConvert.SerializeObject(newPiece);

                Task task = Task.Run(async () =>
                {
                    var data = new System.Net.Http.HttpFormUrlEncodedContent(
                        new Dictionary<string, string>
                        {
                            ["value"] = content
                        });
                    await client.PostAsync(App.BaseUri, data);
                });

                task.Wait();*/
            }

            App.RootFrame.Navigate(typeof(ShowPieces));
        }
    }
}
