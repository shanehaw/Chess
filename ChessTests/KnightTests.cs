using System;
using Chess;
using Chess.Pieces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessTests
{
    [TestClass]
    public class KnightTests
    {
        private Board SetupBoardForKnightTest(int row, int col, Piece.PlayerColour colour)
        {
            Board b = TestBoard.createBoard();
            b.Place<Knight>(colour, row, col);
            return b;
        }

        private bool isAPossibleMove(Board b, int row, int col)
        {
            return getTestKnight(b)
                .GetPossibleMoves()
                .Contains(createCell(row, col));
        }

        private  Knight getTestKnight(Board b)
        {
            return (Knight) b[4, 3].Piece;
        }

        private  Cell createCell(int row, int col)
        {
            return new Cell(row, col);
        }

        private int getNumOfPossibleMoves(Board b)
        {
            return getTestKnight(b).GetPossibleMoves().Count;
        }

        private int getNumExceptionsMoveToEveryCellOnBoard(Board b)
        {
            int exceptionsCaught = 0;
            for (int row = 0; row < Board.NumOfRows; row++)
            {
                for (int col = 0; col < Board.NumOfCols; col++)
                {
                    try
                    {
                        if (!isAPossibleMove(b, row, col))
                        {
                            b.Select(4, 3);
                            b.Move(row, col);
                        }
                    }
                    catch (Exception)
                    {
                        exceptionsCaught++;
                    }
                }
            }
            return exceptionsCaught;
        }

        [TestMethod]
        public void canMoveWhiteKnightUpRight()
        {
            Board b = SetupBoardForKnightTest(4, 3, Piece.PlayerColour.White);
            b.Select(4, 3);
            b.Move(2, 4);
        }

        [TestMethod]
        public void canMoveBlackKnightUpRight()
        {
            Board b = SetupBoardForKnightTest(4, 3, Piece.PlayerColour.Black);
            b.Select(4, 3);
            b.Move(6, 2);
        }

        [TestMethod]
        public void canMoveWhiteKnightRightUp()
        {
            Board b = SetupBoardForKnightTest(4, 3, Piece.PlayerColour.White);
            b.Select(4, 3);
            b.Move(3, 5);
        }

        [TestMethod]
        public void canMoveBlackKnightRightUp()
        {
            Board b = SetupBoardForKnightTest(4, 3, Piece.PlayerColour.Black);
            b.Select(4, 3);
            b.Move(5, 1);
        }

        [TestMethod]
        public void canMoveWhiteKnightRightDown()
        {
            Board b = SetupBoardForKnightTest(4, 3, Piece.PlayerColour.White);
            b.Select(4, 3);
            b.Move(5, 5);
        }

        [TestMethod]
        public void canMoveBlackKnightRightDown()
        {
            Board b = SetupBoardForKnightTest(4, 3, Piece.PlayerColour.Black);
            b.Select(4, 3);
            b.Move(3, 1);
        }

        [TestMethod]
        public void canMoveWhiteKnightDownRight()
        {
            Board b = SetupBoardForKnightTest(4, 3, Piece.PlayerColour.White);
            b.Select(4, 3);
            b.Move(6, 4);
        }

        [TestMethod]
        public void canMoveBlackKnightDownRight()
        {
            Board b = SetupBoardForKnightTest(4, 3, Piece.PlayerColour.Black);
            b.Select(4, 3);
            b.Move(2, 2);
        }

        [TestMethod]
        public void canMoveWhiteKnightDownLeft()
        {
            Board b = SetupBoardForKnightTest(4, 3, Piece.PlayerColour.White);
            b.Select(4, 3);
            b.Move(6, 2);
        }

        [TestMethod]
        public void canMoveBlackKnightDownLeft()
        {
            Board b = SetupBoardForKnightTest(4, 3, Piece.PlayerColour.Black);
            b.Select(4, 3);
            b.Move(2, 4);
        }

        [TestMethod]
        public void canMoveWhiteKnightLeftDown()
        {
            Board b = SetupBoardForKnightTest(4, 3, Piece.PlayerColour.White);
            b.Select(4, 3);
            b.Move(5, 1);
        }

        [TestMethod]
        public void canMoveBlackKnightLeftDown()
        {
            Board b = SetupBoardForKnightTest(4, 3, Piece.PlayerColour.Black);
            b.Select(4, 3);
            b.Move(3, 5);
        }

        [TestMethod]
        public void canMoveWhiteKnightLeftUp()
        {
            Board b = SetupBoardForKnightTest(4, 3, Piece.PlayerColour.White);
            b.Select(4, 3);
            b.Move(3, 1);
        }

        [TestMethod]
        public void canMoveBlackKnightLeftUp()
        {
            Board b = SetupBoardForKnightTest(4, 3, Piece.PlayerColour.Black);
            b.Select(4, 3);
            b.Move(5, 5);
        }

        [TestMethod]
        public void canMoveWhiteKnightUpLeft()
        {
            Board b = SetupBoardForKnightTest(4, 3, Piece.PlayerColour.White);
            b.Select(4, 3);
            b.Move(2, 2);
        }

        [TestMethod]
        public void canMoveBlackKnightUpLeft()
        {
            Board b = SetupBoardForKnightTest(4, 3, Piece.PlayerColour.Black);
            b.Select(4, 3);
            b.Move(6, 4);
        }

        [TestMethod]        
        public void cannotMoveWhiteKnightAnywayElse()
        {
            Board b = SetupBoardForKnightTest(4, 3, Piece.PlayerColour.White);
            int exceptionsCaught = getNumExceptionsMoveToEveryCellOnBoard(b);        
            int numOfPossibleMoves = getNumOfPossibleMoves(b);
            int expected = Board.NumOfCells - numOfPossibleMoves;
            Assert.AreEqual<int>(expected, exceptionsCaught);
        }

        [TestMethod]
        public void cannotMoveBlackKnightAnywayElse()
        {
            Board b = SetupBoardForKnightTest(4, 3, Piece.PlayerColour.Black);
            int exceptionsCaught = getNumExceptionsMoveToEveryCellOnBoard(b);
            int numOfPossibleMoves = getNumOfPossibleMoves(b);
            int expected = Board.NumOfCells - numOfPossibleMoves;
            Assert.AreEqual<int>(expected, exceptionsCaught);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]        
        public void cannotCaptureOwnPieceTopRight()
        {
            Board b = TestBoard.createBoard();
            b.Place<Knight>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 2, 4);
            b.Select(4, 3);
            b.Move(2, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotCaptureOwnPieceRightTop()
        {
            Board b = TestBoard.createBoard();
            b.Place<Knight>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 3, 5);
            b.Select(4, 3);
            b.Move(3, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotCaptureOwnPieceRightBottom()
        {
            Board b = TestBoard.createBoard();
            b.Place<Knight>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 5, 5);
            b.Select(4, 3);
            b.Move(5, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotCaptureOwnPieceBottomRight()
        {
            Board b = TestBoard.createBoard();
            b.Place<Knight>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 6, 4);
            b.Select(4, 3);
            b.Move(6, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotCaptureOwnPieceBottomLeft()
        {
            Board b = TestBoard.createBoard();
            b.Place<Knight>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 6, 2);
            b.Select(4, 3);
            b.Move(6, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotCaptureOwnPieceLeftBottom()
        {
            Board b = TestBoard.createBoard();
            b.Place<Knight>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 5, 1);
            b.Select(4, 3);
            b.Move(5, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotCaptureOwnPieceLeftTop()
        {
            Board b = TestBoard.createBoard();
            b.Place<Knight>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 3, 1);
            b.Select(4, 3);
            b.Move(3, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotCaptureOwnPieceTopLeft()
        {
            Board b = TestBoard.createBoard();
            b.Place<Knight>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 2, 2);
            b.Select(4, 3);
            b.Move(2, 2);
        }

        [TestMethod]
        public void invalidMoveGivesCorrectPieceName()
        {
            try
            {
                Board b = TestBoard.createBoard();
                b.Place<Knight>(Piece.PlayerColour.White, 4, 3);
                b.Select(4, 3);
                b.Move(4, 3);
            }
            catch (ApplicationException ex)
            {
                Assert.IsTrue(ex.Message.Contains("knight"));
            }
        }

        [TestMethod]
        public void noErrorWhenOnBottomRight()
        {
            Board b = TestBoard.createBoard();
            b.Place<Knight>(Piece.PlayerColour.White, 7, 7);
            b.Select(7, 7);
            b.Move(6, 5);
        }

        [TestMethod]
        public void noErrorWhenOnBottomLeft()
        {
            Board b = TestBoard.createBoard();
            b.Place<Knight>(Piece.PlayerColour.White, 7, 0);
            b.Select(7, 0);
            b.Move(5, 1);
        }

        [TestMethod]
        public void noErrorOnTopLeft()
        {
            Board b = TestBoard.createBoard();
            b.Place<Knight>(Piece.PlayerColour.White, 0, 0);
            b.Select(0, 0);
            b.Move(1, 2);
        }

        [TestMethod]
        public void noErrorOnTopRight()
        {
            Board b = TestBoard.createBoard();
            b.Place<Knight>(Piece.PlayerColour.White, 0, 7);
            b.Select(0, 7);
            b.Move(1, 5);
        }
    }
}
