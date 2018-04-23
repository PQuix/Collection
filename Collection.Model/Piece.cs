using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection.Model
{
    public class Piece
    {
        public int PieceId { get; set; }

        public string PieceTitle { get; set; }

        public string PieceIsbn { get; set; }

        public virtual List<Author> Authors { get; set; }
    }
}
