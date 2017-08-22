using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess.Pieces;
using Chess;

namespace ChessTests
{
    [TestClass]
    public class CheckTests
    {
        [TestMethod]
        public void kingDefaultNotInCheck()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            King k = b[4, 3].Piece as King;
            Assert.IsFalse(k.InCheck);
        }

        [TestMethod]        
        public void kingInCheckWhenPieceCanTakeIt()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Place<Queen>(Piece.PlayerColour.Black, 2, 3);
            King k = b[4, 3].Piece as King;
            Assert.IsTrue(k.InCheck);
        }

        [TestMethod]
        public void kingNotInCheckWhenOwnPieceBlockingOppositionPiece()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Place<Queen>(Piece.PlayerColour.Black, 2, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 3, 3);
            King k = b[4, 3].Piece as King;
            Assert.IsFalse(k.InCheck);
        }        

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]        
        public void cannotMoveIntoCheckWithRook()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 7, 3);
            b.Place<Rook>(Piece.PlayerColour.Black, 7, 0);
            b.Place<Queen>(Piece.PlayerColour.Black, 6, 0);
            b.Place<Rook>(Piece.PlayerColour.White, 7, 2);
            King k = b.GetFirstPiece<King>(Piece.PlayerColour.White);
            b.Select(7, 2);
            b.Move(4, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotMoveIntoCheckWithKnight()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 7, 3);
            b.Place<Rook>(Piece.PlayerColour.Black, 7, 0);
            b.Place<Queen>(Piece.PlayerColour.Black, 6, 0);
            b.Place<Knight>(Piece.PlayerColour.White, 7, 2);
            King k = b.GetFirstPiece<King>(Piece.PlayerColour.White);
            b.Select(7, 2);
            b.Move(5, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]        
        public void cannotMoveIntoCheckWithPawn()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 7, 3);
            b.Place<Rook>(Piece.PlayerColour.Black, 7, 0);
            b.Place<Queen>(Piece.PlayerColour.Black, 6, 0);
            b.Place<Pawn>(Piece.PlayerColour.White, 7, 2);
            King k = b.GetFirstPiece<King>(Piece.PlayerColour.White);
            b.Select(7, 2);
            b.Move(6, 2);
        }

        


        [TestMethod]
        public void colourNotInStaleManteByDefault()
        {
            Board b = TestBoard.createAndSetupBoard();
            Assert.IsFalse(b.InStalemate(Piece.PlayerColour.White));
        }

        [TestMethod]
        public void colourInStalemateWhenHasNoPossibleMoves()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 7, 7);
            b.Place<Queen>(Piece.PlayerColour.Black, 0, 6);
            b.Place<Rook>(Piece.PlayerColour.Black, 6, 0);
            Assert.IsTrue(b.InStalemate(Piece.PlayerColour.White));                        
        }

        [TestMethod]
        public void colourNotInStalemateWhenNoPossibleMovesAndInCheck()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 7, 7);
            b.Place<Queen>(Piece.PlayerColour.Black, 0, 6);
            b.Place<Rook>(Piece.PlayerColour.Black, 6, 0);
            b.Place<Rook>(Piece.PlayerColour.Black, 7, 0);
            Assert.IsFalse(b.InStalemate(Piece.PlayerColour.White));            
        }

        [TestMethod]
        public void colourNotInStalemateWhenNoKing()
        {
            Board b = TestBoard.createBoard();
            Assert.IsFalse(b.InStalemate(Piece.PlayerColour.White));
        }        

        [TestMethod]
        public void kingCheckInfiniteLoopStackOverflowBug()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 7, 4);
            b.Place<King>(Piece.PlayerColour.Black, 0, 4);
            b.Select(7, 4);
            b.Move(6, 4);
        }
    }
}
