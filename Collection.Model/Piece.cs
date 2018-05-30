using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection.Model
{
    public class Piece
    {
        /// <summary>
        /// Gets or sets the piece identifier.
        /// </summary>
        /// <value>
        /// The piece identifier.
        /// </value>
        public int PieceId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the first name of the author.
        /// </summary>
        /// <value>
        /// The first name of the author.
        /// </value>
        public string AuthorFName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the author.
        /// </summary>
        /// <value>
        /// The last name of the author.
        /// </value>
        public string AuthorLName { get; set; }

        /// <summary>
        /// Gets the name of the author.
        /// </summary>
        /// <value>
        /// The name of the author.
        /// </value>
        public string AuthorName { get => $"{AuthorLName}, {AuthorFName}"; }

        /// <summary>
        /// Gets or sets the isbn.
        /// </summary>
        /// <value>
        /// The isbn.
        /// </value>
        public string Isbn { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the cover.
        /// </summary>
        /// <value>
        /// The cover.
        /// </value>
        public string Cover { get; set; }
    }
}
