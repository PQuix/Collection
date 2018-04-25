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
using Windows.Web.Http;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.ToString() == "Cancel")
            {
                App.RootFrame.Navigate(typeof(ShowPieces));

                return;
            }

            var newPiece = new Piece
            {
                PieceTitle = Title.Text,
                PieceAuthor = Author.Text,
                PieceIsbn = Isbn.Text,
                PieceDescription = Description.Text
            };

            using (var client = new HttpClient())
            {
                var content = JsonConvert.SerializeObject(newPiece);

                Task task = Task.Run(async () =>
                {
                    var data = new HttpFormUrlEncodedContent(
                        new Dictionary<string, string>
                        {
                            ["value"] = content
                        });
                    await client.PostAsync(App.BaseUri, data);
                });

                task.Wait();
            }

            App.RootFrame.Navigate(typeof(ShowPieces));
        }
    }
}
