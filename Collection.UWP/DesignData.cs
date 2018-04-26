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
        public ObservableCollection<Piece> Pieces { get; set; }

        public DesignData()
        {
            Pieces = new ObservableCollection<Piece>()
            {
                new Piece()
                {
                    PieceId = 451,
                    PieceTitle = "Fahrenheit 451",
                    PieceAuthor = "Bradbury, Ray",
                    PieceIsbn = "978-0-00-654606-1",
                    PieceDescription = "Guy Montag is a fireman. His job is to burn books, which are forbidden, being the source of all discord and unhappiness."
                },
                new Piece()
                {
                    PieceId = 7,
                    PieceTitle = "The Wise Man's Fear",
                    PieceAuthor = "Rothfuss, Patrick",
                    PieceIsbn = "978-0-575-08143-7",
                    PieceDescription = "My name is Kvothe. You may have heard of me."
                },
                new Piece()
                {
                    PieceId = 12,
                    PieceTitle = "Biomega Vol. 2",
                    PieceAuthor = "Nihei, Tsutomu",
                    PieceIsbn = "978-1-4215-3185-4",
                    PieceDescription = "In Tsutomu Nihei's nightmare vision of the future, the N5S virus has swept across the earth, turning most of the population into zombie-like drones."
                }
            };
        }
    }
}
