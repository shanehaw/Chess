using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess;
using Chess.Pieces;

namespace ChessTests
{
    [TestClass]
    public class QueenTests
    {
        [TestMethod]
        public void canMoveQueenVerticallyUp()
        {
            int col = 3;
            for (int row = 3; row >= 0; row--)
            {
                Board b = TestBoard.createBoard();
                b.Place<Queen>(Piece.PlayerColour.White, 4, 3);
                b.Select(4, 3);
                b.Move(row, col);
            }
        }

        [TestMethod]
        public void canMoveQueenVerticallyDown()
        {
            int col = 3;
            for (int row = 5; row < Board.NumOfRows; row++)
            {
                Board b = TestBoard.createBoard();
                b.Place<Queen>(Piece.PlayerColour.White, 4, 3);
                b.Select(4, 3);
                b.Move(row, col);
            }
        }

        [TestMethod]
        public void canMoveQueenHorizontallyLeft()
        {
            int row = 4;
            for (int col = 2; col >= 0; col--)
            {
                Board b = TestBoard.createBoard();
                b.Place<Queen>(Piece.PlayerColour.White, 4, 3);
                b.Select(4, 3);
                b.Move(row, col);
            }
        }

        [TestMethod]
        public void canMoveQueenHorizontallyRight()
        {
            int row = 4;
            for (int col = 4; col < Board.NumOfCols; col++)
            {
                Board b = TestBoard.createBoard();
                b.Place<Queen>(Piece.PlayerColour.White, 4, 3);
                b.Select(4, 3);
                b.Move(row, col);
            }
        }

        [TestMethod]
        public void canMoveQueenDiagUpRight()
        {
            for (int row = 3, col = 4; row >= 0 && col < Board.NumOfCols; row--, col++)
            {
                Board b = TestBoard.createBoard();
                b.Place<Queen>(Piece.PlayerColour.White, 4, 3);
                b.Select(4, 3);
                b.Move(row, col);
            }
        }

        [TestMethod]
        public void canMoveQueenDiagDownRight()
        {
            for (int row = 5, col = 4; row < Board.NumOfRows && col < Board.NumOfCols; row++, col++)
            {
                Board b = TestBoard.createBoard();
                b.Place<Queen>(Piece.PlayerColour.White, 4, 3);
                b.Select(4, 3);
                b.Move(row, col);
            }
        }

        [TestMethod]
        public void canMoveQueenDiagDownLeft()
        {
            for (int row = 5, col = 2; row < Board.NumOfRows && col >= 0; row++, col--)
            {
                Board b = TestBoard.createBoard();
                b.Place<Queen>(Piece.PlayerColour.White, 4, 3);
                b.Select(4, 3);
                b.Move(row, col);
            }
        }

        [TestMethod]
        public void canMoveQueenDiagUpLeft()
        {
            for (int row = 3, col = 2; row >= 0 && col >= 0; row--, col--)
            {
                Board b = TestBoard.createBoard();
                b.Place<Queen>(Piece.PlayerColour.White, 4, 3);
                b.Select(4, 3);
                b.Move(row, col);
            }
        }

        [TestMethod]
        public void invalidMoveGivesCorrectPieceName()
        {
            try
            {
                Board b = TestBoard.createBoard();
                b.Place<Queen>(Piece.PlayerColour.White, 4, 3);
                b.Select(4, 3);
                b.Move(4, 3);
            }
            catch (ApplicationException ex)
            {
                Assert.IsTrue(ex.Message.Contains("queen"));
            }
        }
    }
}
