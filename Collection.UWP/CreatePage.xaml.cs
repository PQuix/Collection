using Collection.DataSource;
using Collection.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class CreatePage : Page
    {
        /// <summary>
        /// Gets or sets the pieces.
        /// </summary>
        /// <value>
        /// The pieces.
        /// </value>
        public static ObservableCollection<Piece> Pieces { get; set; } = new ObservableCollection<Piece>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePage"/> class.
        /// </summary>
        public CreatePage()
        {
            displayPieces();
            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// Handles the retrieval of the Pieces to be displayed and displays them.
        /// </summary>
        /// <exception cref="JsonSerializationException">Can not deserialize object '{0}'</exception>
        private async void displayPieces()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(@"http://localhost:57994/api/pieces");
                    var json = await client.GetStringAsync("Pieces");

                    Piece[] pieces = JsonConvert.DeserializeObject<Piece[]>(json);
                    Pieces.Clear();
                    foreach (var piece in pieces)
                        Pieces.Add(piece);
                }
            }
            catch (JsonSerializationException e)
            {
                throw new JsonSerializationException("Can not deserialize object '{0}'", e);
            }
        }

        /// <summary>
        /// Depending on the button pressed: Creates a new Piece, Updates an existing Piece, or Cancels the operation. All events sends the user back to the Display Page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="HttpRequestException">Something went wrong '{0}'</exception>
        /// <exception cref="System.NullReferenceException">Object cannot be null '{0}'</exception>
        private async void Btn_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Content.ToString())
            {
                case "Add":
                    try
                    {
                        Piece piece = new Piece()
                        {
                            Title = TitleBox.Text,
                            AuthorLName = lNameBox.Text,
                            AuthorFName = fNameBox.Text,
                            Isbn = IsbnBox.Text,
                            Description = DescBox.Text,
                            Cover = "http://covers.openlibrary.org/b/isbn/" + IsbnBox.Text + "-M.jpg"
                        };

                        var result = await new PieceDS().CreatePiece(piece);

                        if(result)
                        {
                            var dialog = new ContentDialog()
                            {
                                Content = piece.Title + " by " + piece.AuthorName + " has been added!"
                            };

                            dialog.PrimaryButtonText = "OK";
                            await dialog.ShowAsync();
                        }
                    }

                    catch (HttpRequestException e1)
                    {
                        throw new HttpRequestException("Something went wrong '{0}'", e1);
                    }

                    App.RootFrame.Navigate(typeof(DisplayPage));
                    break;

                case "Edit":
                    try
                    {
                        Piece piece = (Piece)aeList.SelectedItem;
                        piece.Title = TitleBox.Text;
                        piece.AuthorLName = lNameBox.Text;
                        piece.AuthorFName = fNameBox.Text;
                        piece.Isbn = IsbnBox.Text;
                        piece.Description = DescBox.Text;

                        var result = await new PieceDS().UpdatePieceAsync(piece);

                        if (result)
                        {
                            var dialog = new ContentDialog()
                            {
                                Content = piece.Title + " by " + piece.AuthorName + " has been edited!"
                            };

                            dialog.PrimaryButtonText = "OK";
                            await dialog.ShowAsync();
                        }
                    }

                    catch (NullReferenceException e2)
                    {
                        throw new NullReferenceException("Object cannot be null '{0}'", e2);
                    }

                    App.RootFrame.Navigate(typeof(DisplayPage));
                    break;

                default:
                    App.RootFrame.Navigate(typeof(DisplayPage));

                    break;
            }
        }
    }
}
