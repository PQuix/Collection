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
                PieceIsbn = "978-0-575-10468-6",
                Authors = new List<Author>() { author1 }
            };

            var piece2 = new Piece()
            {
                PieceId = 0002,
                PieceTitle = "Best Served Cold",
                PieceIsbn = "978-0-575-08248-9",
                Authors = new List<Author>() { author1 }
            };

            var piece3 = new Piece()
            {
                PieceId = 0003,
                PieceTitle = "Do Androids Dream of Electric Sheep?",
                PieceIsbn = "978-0-575-11676-4",
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
