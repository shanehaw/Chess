using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess;
using Chess.Pieces;

namespace ChessTests
{
    [TestClass]
    public class RookTests
    {
        [TestMethod]
        public void canMoveWhiteRookVerticallyUp()
        {
            int col = 3;
            for (int row = 3; row >= 0; row--)
            {
                Board b = TestBoard.createBoard();
                b.Place<Rook>(Piece.PlayerColour.White, 4, 3);
                b.Select(4, 3);
                b.Move(row, col);
            }
        }

        [TestMethod]        
        public void canMoveBlackRookVerticallyUp()
        {
            int col = 3;
            for (int row = 5; row < Board.NumOfRows; row++)
            {
                Board b = TestBoard.createBoard();
                b.Place<Rook>(Piece.PlayerColour.Black, 4, 3);
                b.Select(4, 3);
                b.Move(row, col);
            }
        }

        [TestMethod]
        public void canMoveWhiteRookVerticallyDown()
        {
            int col = 3;
            for (int row = 5; row < Board.NumOfRows; row++)
            {
                Board b = TestBoard.createBoard();
                b.Place<Rook>(Piece.PlayerColour.White, 4, 3);
                b.Select(4, 3);
                b.Move(row, col);
            }
        }

        [TestMethod]
        public void canMoveBlackRookVerticallyDown()
        {
            int col = 3;
            for (int row = 3; row >= 0; row--)
            {
                Board b = TestBoard.createBoard();
                b.Place<Rook>(Piece.PlayerColour.Black, 4, 3);
                b.Select(4, 3);
                b.Move(row, col);
            }
        }

        [TestMethod]
        public void canMoveWhiteRookHorizontallyLeft()
        {
            int row = 4;
            for (int col = 2; col >= 0; col--)
            {
                Board b = TestBoard.createBoard();
                b.Place<Rook>(Piece.PlayerColour.White, 4, 3);
                b.Select(4, 3);
                b.Move(row, col);
            }
        }

        [TestMethod]
        public void canMoveBlackRookHorizonallyLeft()
        {
            int row = 4;
            for (int col = 4; col < Board.NumOfCols; col++)
            {
                Board b = TestBoard.createBoard();
                b.Place<Rook>(Piece.PlayerColour.Black, 4, 3);
                b.Select(4, 3);
                b.Move(row, col);
            }
        }

        [TestMethod]
        public void canMoveWhiteRookHorizontallyRight()
        {
            int row = 4;
            for (int col = 4; col < Board.NumOfCols; col++)
            {
                Board b = TestBoard.createBoard();
                b.Place<Rook>(Piece.PlayerColour.White, 4, 3);
                b.Select(4, 3);
                b.Move(row, col);
            }
        }

        [TestMethod]
        public void canMoveBlackRookHorizontallyRight()
        {
            int row = 4;
            for (int col = 2; col >= 0; col--)
            {
                Board b = TestBoard.createBoard();
                b.Place<Rook>(Piece.PlayerColour.Black, 4, 3);
                b.Select(4, 3);
                b.Move(row, col);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotMoveRookPastCaptureVerticallyUp()
        {
            Board b = TestBoard.createBoard();
            b.Place<Rook>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 2, 3);
            b.Select(4, 3);
            b.Move(1, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotMoveRookPastCaptureVerticallyDown()
        {
            Board b = TestBoard.createBoard();
            b.Place<Rook>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 5, 3);
            b.Select(4, 3);
            b.Move(6, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotMoveRookPastCaptureHorizontallyLeft()
        {
            Board b = TestBoard.createBoard();
            b.Place<Rook>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 4, 2);
            b.Select(4, 3);
            b.Move(4, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotMoveRookPastCaptureHorizontallyRight()
        {
            Board b = TestBoard.createBoard();
            b.Place<Rook>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 4, 4);
            b.Select(4, 3);
            b.Move(4, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotCaptureOwnPieceVerticallyUp()
        {
            Board b = TestBoard.createBoard();
            b.Place<Rook>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 2, 3);
            b.Select(4, 3);
            b.Move(2, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotCaptureOwnPieceVerticallyDown()
        {
            Board b = TestBoard.createBoard();
            b.Place<Rook>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 6, 3);
            b.Select(4, 3);
            b.Move(6, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotCaptureOwnPieceHorizontallyRight()
        {
            Board b = TestBoard.createBoard();
            b.Place<Rook>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 4, 5);
            b.Select(4, 3);
            b.Move(4, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotCaptureOwnPieceHorizontallyLeft()
        {
            Board b = TestBoard.createBoard();
            b.Place<Rook>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 4, 1);
            b.Select(4, 3);
            b.Move(4, 1);
        }

        [TestMethod]
        public void invalidMoveGivesCorrectPieceName()
        {
            try
            {
                Board b = TestBoard.createBoard();
                b.Place<Rook>(Piece.PlayerColour.White, 4, 3);
                b.Select(4, 3);
                b.Move(4, 3);
            }
            catch (ApplicationException ex)
            {
                Assert.IsTrue(ex.Message.Contains("rook"));
            }
        }        
    }
}
