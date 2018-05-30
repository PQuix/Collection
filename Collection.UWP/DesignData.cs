using Collection.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection.UWP
{
    public class DesignData
    {
        /// <summary>
        /// Gets or sets the pieces.
        /// </summary>
        /// <value>
        /// The pieces.
        /// </value>
        public ObservableCollection<Piece> Pieces { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DesignData" /> class.
        /// Creates "sample data" for use in Design mode.
        /// </summary>
        public DesignData()
        {
            Pieces = new ObservableCollection<Piece>()
            {
                new Piece()
                {
                    PieceId = 451,
                    Title = "Fahrenheit 451",
                    AuthorLName = "Bradbury",
                    AuthorFName ="Ray",
                    Isbn = "978-0-00-654606-1",
                    Description = "Guy Montag is a fireman. His job is to burn books, which are forbidden, being the source of all discord and unhappiness.",
                    Cover = "http://covers.openlibrary.org/b/isbn/9780006546061-M.jpg"
                },
                new Piece()
                {
                    PieceId = 7,
                    Title = "The Wise Man's Fear",
                    AuthorLName = "Rothfuss",
                    AuthorFName ="Patrick",
                    Isbn = "978-0-575-08143-7",
                    Description = "My name is Kvothe. You may have heard of me.",
                    Cover = "http://covers.openlibrary.org/b/isbn/9780575081437-M.jpg"
                }
            };
        }
    }
}
