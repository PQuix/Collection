using Collection.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection.DataAccess
{
    public class CollectionDBInitializer : DropCreateDatabaseIfModelChanges<CollectionContext>
    {
        protected override void Seed(CollectionContext context)
        {
            var author1 = new Author()
            {
                AuthorId = 10,
                FirstName = "Joe",
                LastName = "Abercrombie"
            };

            var author2 = new Author()
            {
                AuthorId = 11,
                FirstName = "Philip K.",
                LastName = "Dick"
            };

            var piece1 = new Piece()
            {
                PieceId = 0001,
                PieceTitle = "Sharp Ends",
                PieceAuthor = "Abercrombie, Joe",
                PieceDescription = "A collection of short stories, set in Joe Abercrombie's First Law universe.",
                PieceIsbn = "978-0-575-10468-6",
                PieceCover = "http://covers.openlibrary.org/b/isbn/9780575104686-M.jpg",
                Authors = new List<Author>() { author1 }
            };

            var piece2 = new Piece()
            {
                PieceId = 0002,
                PieceTitle = "Best Served Cold",
                PieceAuthor = "Abercrombie, Joe",
                PieceDescription = "The first book in the continuation of Abercrombie's First Law series.",
                PieceIsbn = "978-0-575-08248-9",
                PieceCover = "http://covers.openlibrary.org/b/isbn/9780575082489-M.jpg",
                Authors = new List<Author>() { author1 }
            };

            var piece3 = new Piece()
            {
                PieceId = 0003,
                PieceTitle = "Do Androids Dream of Electric Sheep?",
                PieceAuthor = "Dick, Philip K.",
                PieceDescription = "Sick Sci-Fi Shit, Yo!",
                PieceIsbn = "978-0-575-11676-4",
                PieceCover = "http://covers.openlibrary.org/b/isbn/9780575116764-M.jpg",
                Authors = new List<Author>() { author2 }
            };

            context.Authors.Add(author1);
            context.Authors.Add(author2);

            context.Pieces.Add(piece1);
            context.Pieces.Add(piece2);
            context.Pieces.Add(piece3);

            base.Seed(context);
        }
    }
}
