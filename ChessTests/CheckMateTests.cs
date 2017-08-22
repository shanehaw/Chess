using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess;
using Chess.Pieces;

namespace ChessTests
{
    [TestClass]
    public class CheckMateTests
    {
        [TestMethod]
        public void kingNotInCheckMateByDefault()
        {
            Board b = TestBoard.createAndSetupBoard();            
            King wk = b.GetFirstPiece<King>(Piece.PlayerColour.White);
            King bk = b.GetFirstPiece<King>(Piece.PlayerColour.Black);
            Assert.IsFalse(wk.InCheckMate);
            Assert.IsFalse(bk.InCheckMate);
        }

        [TestMethod]        
        public void kingInCheckMateWhenInCheckWithNoPossibleMoves()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 7, 3);
            b.Place<Rook>(Piece.PlayerColour.Black, 6, 0);
            b.Place<Queen>(Piece.PlayerColour.Black, 7, 0);
            King k = b.GetFirstPiece<King>(Piece.PlayerColour.White);
            Assert.IsTrue(k.InCheckMate);
        }

        [TestMethod]        
        public void kingNotInCheckMateWhenOtherPieceCanMoveInTheWay()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 7, 3);
            b.Place<Rook>(Piece.PlayerColour.Black, 6, 0);
            b.Place<Queen>(Piece.PlayerColour.Black, 7, 0);
            b.Place<Rook>(Piece.PlayerColour.White, 3, 2);
            King k = b.GetFirstPiece<King>(Piece.PlayerColour.White);            
            Assert.IsFalse(k.InCheckMate);
        }        
    }
}
