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
        /// <summary>
        /// Seeds the database with a couple of Pieces. Used when using the API as Startup and the Database is empty.
        /// The default implementation does nothing.
        /// </summary>
        /// <param name="context">The context to seed.</param>
        protected override void Seed(CollectionContext context)
        {
            var piece1 = new Piece()
            {
                PieceId = 0001,
                Title = "Sharp Ends",
                AuthorLName = "Abercrombie",
                AuthorFName = "Joe",
                Description = "A collection of short stories, set in Joe Abercrombie's First Law universe.",
                Isbn = "978-0-575-10468-6",
                Cover = "http://covers.openlibrary.org/b/isbn/9780575104686-M.jpg"
            };

            var piece2 = new Piece()
            {
                PieceId = 0002,
                Title = "Best Served Cold",
                AuthorLName = "Abercrombie",
                AuthorFName = "Joe",
                Description = "The first book in the continuation of Abercrombie's First Law series.",
                Isbn = "978-0-575-08248-9",
                Cover = "http://covers.openlibrary.org/b/isbn/9780575082489-M.jpg"
            };

            var piece3 = new Piece()
            {
                PieceId = 0003,
                Title = "Do Androids Dream of Electric Sheep?",
                AuthorLName = "Dick",
                AuthorFName = "Philip K.",
                Description = "Sick Sci-Fi Shit, Yo!",
                Isbn = "978-0-575-11676-4",
                Cover = "http://covers.openlibrary.org/b/isbn/9780575116764-M.jpg"
            };

            context.Pieces.Add(piece1);
            context.Pieces.Add(piece2);
            context.Pieces.Add(piece3);

            base.Seed(context);
        }
    }
}
