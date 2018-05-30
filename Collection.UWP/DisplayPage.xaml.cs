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
        /// <summary>
        /// Gets or sets the pieces.
        /// </summary>
        /// <value>
        /// The pieces.
        /// </value>
        public static ObservableCollection<Piece> Pieces { get; set; } = new ObservableCollection<Piece>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayPage" /> class.
        /// </summary>
        public DisplayPage()
        {
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
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative of the pending navigation that will load the current Page. Usually the most relevant property to examine is Parameter.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            displayPieces();
        }

        /// <summary>
        /// Depending on the button pressed: Sends the user to the Create/Edit page or deletes the selected Piece.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Content.ToString())
            {
                case "Add / Edit Piece":
                    App.RootFrame.Navigate(typeof(CreatePage));

                    break;

                case "Delete Piece":
                    Deletition();
                    App.RootFrame.Navigate(typeof(DisplayPage));

                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Deletes the selected Piece and send a confirmation dialog.
        /// </summary>
        /// <exception cref="System.NullReferenceException">Object cannot be null '{0}'</exception>
        private async void Deletition()
        {
            try
            {
                Piece piece = (Piece)ActGrid.SelectedItem;
                var result = await new PieceDS().DeletePieceAsync(piece.PieceId);

                if (result)
                {
                    var dialog = new ContentDialog()
                    {
                        Content = piece.Title + " by " + piece.AuthorName + " has been deleted!"
                    };
                    dialog.PrimaryButtonText = "OK";
                    await dialog.ShowAsync();
                }
            }

            catch (NullReferenceException e)
            {
                throw new NullReferenceException("Object cannot be null '{0}'", e);
            }
        }

        /// <summary>
        /// Displays the details of the selected Piece.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void ActGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Piece piece = (Piece)ActGrid.SelectedItem;

            if(piece != null)
            {
                Title.Text = piece.Title;
                Author.Text = piece.AuthorName;
                Isbn.Text = piece.Isbn;
                Description.Text = piece.Description;
                Cover.Source = new BitmapImage(new Uri(piece.Cover, UriKind.Absolute));

                IsbnText.Visibility = Visibility.Visible;
                Delete.Visibility = Visibility.Visible;
            }
        }
    }
}
