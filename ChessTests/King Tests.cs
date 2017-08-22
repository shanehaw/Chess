using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess;
using Chess.Pieces;

namespace ChessTests
{
    [TestClass]
    public class KingTests
    {
        [TestMethod]
        public void canMoveKingOneForward()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Select(4, 3);
            b.Move(3, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]        
        public void cannotTakeOwnPieceMovingForward()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 3, 3);
            b.Select(4, 3);
            b.Move(3, 3);
        }

        [TestMethod]        
        public void canTakeOppositionPieceMovingForward()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 3, 3);
            b.Select(4, 3);
            b.Move(3, 3);
        }

        [TestMethod]
        public void canMoveKingOneDown()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Select(4, 3);
            b.Move(5, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotTakeOwnPieceMovingDown()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 5, 3);
            b.Select(4, 3);
            b.Move(5, 3);
        }

        [TestMethod]
        public void canTakeOppositionPieceMovingDown()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 5, 3);
            b.Select(4, 3);
            b.Move(5, 3);
        }

        [TestMethod]
        public void canMoveKingUpRight()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Select(4, 3);
            b.Move(3, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotTakeOwnPieceMovingUpRight()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 3, 4);
            b.Select(4, 3);
            b.Move(3, 4);
        }

        [TestMethod]
        public void canTakeOppositionPieceMovingUpRight()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 3, 4);
            b.Select(4, 3);
            b.Move(3, 4);
        }

        [TestMethod]
        public void canMoveKingRight()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Select(4, 3);
            b.Move(4, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotTakeOwnPieceMovingRight()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 4, 4);
            b.Select(4, 3);
            b.Move(4, 4);
        }

        [TestMethod]
        public void canTakeOppositionPieceMovingRight()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 4, 4);
            b.Select(4, 3);
            b.Move(4, 4);
        }

        [TestMethod]
        public void canMoveKingDownRight()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Select(4, 3);
            b.Move(5, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotTakeOwnPieceMovingDownRight()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 5, 4);
            b.Select(4, 3);
            b.Move(5, 4);
        }

        [TestMethod]
        public void canTakeOppositionPieceMovingDownRight()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 5, 4);
            b.Select(4, 3);
            b.Move(5, 4);
        }

        [TestMethod]
        public void canMoveKingDownLeft()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Select(4, 3);
            b.Move(5, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotTakeOwnPieceMovingDownLeft()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 5, 2);
            b.Select(4, 3);
            b.Move(5, 2);
        }

        [TestMethod]
        public void canTakeOppositionPieceMovingDownLeft()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 5, 2);
            b.Select(4, 3);
            b.Move(5, 2);
        }

        [TestMethod]
        public void canMoveKingLeft()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Select(4, 3);
            b.Move(4, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotTakeOwnPieceMovingLeft()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 4, 2);
            b.Select(4, 3);
            b.Move(4, 2);
        }

        [TestMethod]
        public void canTakeOppositionPieceMovingLeft()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 4, 2);
            b.Select(4, 3);
            b.Move(4, 2);
        }

        [TestMethod]
        public void canMoveKingForwardLeft()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Select(4, 3);
            b.Move(3, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotTakeOwnPieceMovingForwardLeft()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 3, 2);
            b.Select(4, 3);
            b.Move(3, 2);
        }

        [TestMethod]
        public void canTakeOppositionPieceMovingForwardLeft()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 3, 2);
            b.Select(4, 3);
            b.Move(3, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]        
        public void cannotMoveIntoCheck()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 7, 3);
            b.Place<Rook>(Piece.PlayerColour.Black, 6, 1);
            b.Select(7, 3);
            b.Move(6, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotMoveSidewaysWhenInCheck()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 7, 3);
            b.Place<Rook>(Piece.PlayerColour.Black, 7, 0);
            b.Select(7, 3);
            b.Move(7, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotMoveKingIntoCheckWithPawn()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 7, 4);
            b.Place<Pawn>(Piece.PlayerColour.Black, 5, 6);
            b.Select(7, 4);
            b.Move(6, 5);
        }

        [TestMethod]        
        public void whiteCanCastle()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 7, 4);
            b.Place<Rook>(Piece.PlayerColour.White, 7, 7);            
            b.Select(7, 4);
            b.Move(7, 6);
        }

        [TestMethod]
        public void blackCanCastle()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.Black, 0, 4);
            b.Place<Rook>(Piece.PlayerColour.Black, 0, 7);
            b.Select(0, 4);
            b.Move(0, 6);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotCastleWhenKingHasMoved()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 7, 4);
            b.Place<Rook>(Piece.PlayerColour.White, 7, 7);
            b.Select(7, 4);
            b.Move(6, 4);
            b.Select(6, 4);
            b.Move(7, 4);
            b.Select(7, 4);
            b.Move(7, 6);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotCastleWhenCastleRookHasMoved()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 7, 4);
            b.Place<Rook>(Piece.PlayerColour.White, 7, 7);
            b.Select(7, 7);
            b.Move(6, 7);
            b.Select(6, 7);
            b.Move(7, 7);
            b.Select(7, 4);
            b.Move(7, 6);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotCastleOntoAPiece()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 7, 4);
            b.Place<Rook>(Piece.PlayerColour.White, 7, 7);
            b.Place<Knight>(Piece.PlayerColour.White, 7, 6);
            b.Select(7, 4);
            b.Move(7, 6);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotCastleWithPieceInTheWay()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 7, 4);
            b.Place<Rook>(Piece.PlayerColour.White, 7, 7);
            b.Place<Bishop>(Piece.PlayerColour.White, 7, 5);
            b.Select(7, 4);
            b.Move(7, 6);
        }

        [TestMethod]        
        public void whiteRookMovesWhenCastling()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.White, 7, 4);
            b.Place<Rook>(Piece.PlayerColour.White, 7, 7);
            Rook castle = b.GetFirstPiece<Rook>(Piece.PlayerColour.White);
            b.Select(7, 4);
            b.Move(7, 6);
            Assert.AreEqual<Cell>(new Cell(7, 5), castle.ParentCell);
        }

        [TestMethod]
        public void blackRookMovesWhenCastling()
        {
            Board b = TestBoard.createBoard();
            b.Place<King>(Piece.PlayerColour.Black, 0, 4);
            b.Place<Rook>(Piece.PlayerColour.Black, 0, 7);
            Rook castle = b.GetFirstPiece<Rook>(Piece.PlayerColour.Black);
            b.Select(0, 4);
            b.Move(0, 6);
            Assert.AreEqual<Cell>(new Cell(0, 5), castle.ParentCell);
        }        
    }
}
