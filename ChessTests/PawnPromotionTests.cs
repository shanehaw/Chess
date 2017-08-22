using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess;
using Chess.Pieces;

namespace ChessTests
{
    [TestClass]
    public class PawnPromotionTests
    {
        /*
         * Notes:
         *      1.) Must be on the boundary
         *      2.) Must be a pawn
         *      3.) Can only be replaced by Queen, Knight, Rook, Bishop
         *      4.) Possible final solution:
         *          -   Create Interface Replaceable
         *          -   Have Queen, Knight, Rook, and Bishop Implement it
         *          -   Method is:
         *          -
         *          -   void Replace(int row, int col)
         *          -   {
         *          -       gameBoard.Place<T>(Colour, row, col);
         *          -   }
         *          -
         *          -   Then Board.Replace<T>(int row, int col) where T: Replaceable
         *          -
         *          -   Provides the necessary restrictions and moves the responsibility of Piece placement to the actual piece         
         */
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotReplaceRook()
        {
            Board b = TestBoard.createBoard();
            b.Place<Rook>(Piece.PlayerColour.White, 0, 1);
            b.Replace<Queen>(0, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotReplaceKnight()
        {
            Board b = TestBoard.createBoard();
            b.Place<Knight>(Piece.PlayerColour.White, 0, 1);
            b.Replace<Queen>(0, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotReplaceBishop()
        {
            Board b = TestBoard.createBoard();
            b.Place<Bishop>(Piece.PlayerColour.White, 0, 1);
            b.Replace<Queen>(0, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotReplaceQueen()
        {
            Board b = TestBoard.createBoard();
            b.Place<Queen>(Piece.PlayerColour.White, 0, 1);
            b.Replace<Queen>(0, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotReplaceKing()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 0, 1);
            b.Replace<Queen>(0, 1);
        }

        [TestMethod]
        public void boardCanReplacePieceWithQueen()
        {
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.White, 0, 3);
            b.Replace<Queen>(0, 3);
            Assert.AreEqual<char>(Piece.Queen, b[0, 3].Piece.Type);
        }

        [TestMethod]        
        public void canReplaceWithBishop()
        {
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.White, 0, 1);
            b.Replace<Bishop>(0, 1);
        }

        [TestMethod]
        public void canReplaceWithKnight()
        {
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.White, 0, 1);
            b.Replace<Knight>(0, 1);
        }

        [TestMethod]
        public void canReplaceWithRook()
        {
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.White, 0, 1);
            b.Replace<Rook>(0, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]        
        public void cannotReplaceWithKing()
        {
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.White, 0, 1);
            b.Replace<King>(0, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotReplaceWithPawn()
        {
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.White, 0, 1);
            b.Replace<Pawn>(0, 1);
        }
    }
}
