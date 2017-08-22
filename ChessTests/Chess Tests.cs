using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess;
using System.Drawing;
using Chess.Pieces;

namespace ChessTests
{
    [TestClass]
    public class ChessTests
    {
        private static Board createBoard()
        {
            Board b = new Board();
            b.Setup();
            return b;
        }

        private static bool isEven(int col)
        {
            return (col % 2 == 0);
        }        

        [TestMethod]
        public void arePawnsSetup()
        {
            Board b = TestBoard.createAndSetupBoard();
            for (int i = 0; i < Board.NumOfCols; i++)
            {
                Assert.AreEqual<char>(Piece.Pawn, b[1, i].Piece.Type);
                Assert.AreEqual<char>(Piece.Pawn, b[6, i].Piece.Type);
            }
        }

        [TestMethod]
        public void areRooksSetup()
        {
            Board b = TestBoard.createAndSetupBoard();
            Assert.AreEqual<char>(Piece.Rook, b[0, 0].Piece.Type);
            Assert.AreEqual<char>(Piece.Rook, b[0, 7].Piece.Type);
            Assert.AreEqual<char>(Piece.Rook, b[7, 0].Piece.Type);
            Assert.AreEqual<char>(Piece.Rook, b[7, 7].Piece.Type);
        }

        [TestMethod]        
        public void areKnightsSetup()
        {
            Board b = TestBoard.createAndSetupBoard();
            Assert.AreEqual<char>(Piece.Knight, b[0, 1].Piece.Type);
            Assert.AreEqual<char>(Piece.Knight, b[0, 6].Piece.Type);
            Assert.AreEqual<char>(Piece.Knight, b[7, 1].Piece.Type);
            Assert.AreEqual<char>(Piece.Knight, b[7, 6].Piece.Type);
        }

        [TestMethod]
        public void areBishopsSetup()
        {
            Board b = TestBoard.createAndSetupBoard();
            Assert.AreEqual<char>(Piece.Bishop, b[0, 2].Piece.Type);
            Assert.AreEqual<char>(Piece.Bishop, b[0, 5].Piece.Type);
            Assert.AreEqual<char>(Piece.Bishop, b[7, 2].Piece.Type);
            Assert.AreEqual<char>(Piece.Bishop, b[7, 5].Piece.Type);
        }

        [TestMethod]
        public void areQueensSetup()
        {
            Board b = TestBoard.createAndSetupBoard();
            Assert.AreEqual<char>(Piece.Queen, b[0, 3].Piece.Type);
            Assert.AreEqual<char>(Piece.Queen, b[7, 3].Piece.Type);
        }

        [TestMethod]
        public void areKingsSetup()
        {
            Board b = TestBoard.createAndSetupBoard();
            Assert.AreEqual<char>(Piece.King, b[0, 4].Piece.Type);
            Assert.AreEqual<char>(Piece.King, b[7, 4].Piece.Type);
        }

        [TestMethod]
        public void areMiddleCellsEmpty()
        {
            const int EmptyRowsStart = 2;
            const int EmptyRowsEnd = 6;

            Board b = TestBoard.createAndSetupBoard();
            for (int row = EmptyRowsStart; row < EmptyRowsEnd; row++)
            {
                for (int col = 0; col < Board.NumOfCols; col++)
                {
                    Assert.AreEqual<char>(Piece.Empty, b[row, col].Piece.Type);
                }
            }
        }

        [TestMethod]        
        public void areEvenRowColoursSetup()
        {
            Board b = TestBoard.createAndSetupBoard();
            for (int row = 0; row < Board.NumOfRows; row += 2)
            {
                for (int col = 0; col < Board.NumOfCols; col++)
                {
                    Color expected = isEven(col) ? Color.White : Color.Black;
                    Assert.AreEqual<Color>(expected, b[row, col].Colour);
                }
            }
        }

        [TestMethod]
        public void areOddRowColoursSetup()
        {
            Board b = TestBoard.createAndSetupBoard();
            for (int row = 1; row < Board.NumOfRows; row += 2)
            {
                for (int col = 0; col < Board.NumOfCols; col++)
                {
                    Color expected = isEven(col) ? Color.Black : Color.White;
                    Assert.AreEqual<Color>(expected, b[row, col].Colour);
                }
            }
        }

        [TestMethod]
        public void canSelectCell()
        {
            Board b = TestBoard.createAndSetupBoard();  
            b.Select(0, 1);
        }

        [TestMethod]        
        public void canGetSelectedCell()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(0, 1);
            Assert.AreEqual<Cell>(b[0, 1], b.SelectedCell);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]        
        public void getExceptionWhenSelectCellNegativeRow()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(-1, 0);            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]        
        public void getExceptionWhenSelectCellNegativeCol()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]        
        public void getExceptionWhenSelectCellRowTooLarge()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(Board.NumOfRows + 1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]        
        public void getExceptionWhenSelectCellColTooLarge()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(0, Board.NumOfCols + 1);
        }        

        [TestMethod]        
        public void pieceMovesWithMove()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(6, 0); //white 1st pawn
            Piece selectedPiece = b.SelectedCell.Piece;
            b.Move(5, 0);
            Assert.AreEqual<Piece>(selectedPiece, b[5, 0].Piece);
        }

        [TestMethod]
        public void isCellEmptyWhenMovedOff()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(6, 0); //white 1st pawn
            GameCell selectedCell = b.SelectedCell;
            b.Move(5, 0);
            Assert.AreEqual<char>(selectedCell.Piece.Type, Piece.Empty);
        }

        [TestMethod]
        public void selectedCellIsNullAfterMove()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(6, 0); //white 1st pawn            
            b.Move(5, 0);
            Assert.IsNull(b.SelectedCell);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]         
        public void exceptionWhenMoveToNegativeRow()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(6, 0);
            b.Move(-1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]        
        public void exceptionWhenMoveTooLargeRow()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(6, 0);
            b.Move(Board.NumOfRows + 1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]        
        public void exceptionWhenMoveToNegativeCol()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(6, 0);
            b.Move(0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]        
        public void exceptionWhenMoveTooLargeCol()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(6, 0);
            b.Move(0, Board.NumOfCols + 1);
        }

        [TestMethod]
        public void areTop2RowsBlack()
        {
            Board b = TestBoard.createAndSetupBoard();
            for (int row = 0; row < 2; row++)
            {
                for (int col = 0; col < Board.NumOfCols; col++)
                {
                    Assert.AreEqual<Piece.PlayerColour>(Piece.PlayerColour.Black, b[row, col].Piece.Colour);
                }
            }
        }

        [TestMethod]
        public void areBottom2RowsWhite()
        {
            Board b = TestBoard.createAndSetupBoard();
            for (int row = 6; row < 8; row++)
            {
                for (int col = 0; col < Board.NumOfCols; col++)
                {
                    Assert.AreEqual<Piece.PlayerColour>(Piece.PlayerColour.White, b[row, col].Piece.Colour);
                }
            }
        }

        [TestMethod]
        public void areMiddleRowsPlayerColourNone()
        {
            Board b = TestBoard.createAndSetupBoard();
            for (int row = 2; row < 6; row++)
            {
                for (int col = 0; col < Board.NumOfCols; col++)
                {
                    Assert.AreEqual<Piece.PlayerColour>(Piece.PlayerColour.None, b[row, col].Piece.Colour);
                }
            }
        }

        [TestMethod]
        public void pieceHasNoValidMoves()
        {
            Board b = TestBoard.createBoard();
            b.Place<Piece>(Piece.PlayerColour.None, 4, 3);
            b.Select(4, 3);
            Assert.AreEqual<int>(0, b.SelectedCell.Piece.GetPossibleMoves().Count);
        }

        [TestMethod]
        public void nullCellNeverEqual()
        {
            Cell c = null;
            Assert.IsFalse(c == new Cell(4, 3));
        }

        [TestMethod]
        public void queensSetupOnCorrectColour()
        {
            Board b = TestBoard.createAndSetupBoard();
            Assert.AreEqual<Color>(Color.White, b[0, 0].Colour);
            Assert.AreEqual<Color>(Color.White, b[7, 3].Colour);
            Assert.AreEqual<Color>(Color.Black, b[0, 3].Colour);
            Assert.AreEqual<Color>(Color.Black, b[7, 0].Colour);
        }

        [TestMethod]
        public void canEnumeratePlayerPieces()
        {
            Board b = TestBoard.createAndSetupBoard();
            
            int kingCount = 0,
                queenCount = 0,
                rookCount = 0,
                bishopCount = 0,
                knightCount = 0,
                pawnCount = 0;

            foreach (Piece p in b.EnumeratePieces(Piece.PlayerColour.White))
            {
                switch (p.Type)
                {
                    case Piece.King:
                        kingCount++;
                        break;
                    case Piece.Queen:
                        queenCount++;
                        break;
                    case Piece.Rook:
                        rookCount++;
                        break;
                    case Piece.Bishop:
                        bishopCount++;
                        break;
                    case Piece.Knight:
                        knightCount++;
                        break;
                    case Piece.Pawn:
                        pawnCount++;
                        break;
                }
            }

            Assert.AreEqual<int>(1, kingCount);
            Assert.AreEqual<int>(1, queenCount);
            Assert.AreEqual<int>(2, rookCount);
            Assert.AreEqual<int>(2, bishopCount);
            Assert.AreEqual<int>(2, knightCount);
            Assert.AreEqual<int>(8, pawnCount);
        }

        [TestMethod]
        public void emptyPieceDoesNothingWhenCopyingToBoard()
        {
            Board b = TestBoard.createBoard();
            Board b2 = TestBoard.createBoard();
            Piece p = new Piece(Piece.PlayerColour.None, new Cell(4, 3), b);
            p.CopyToBoard(b2);
            for (int row = 0; row < Board.NumOfRows; row++)
            {
                for (int col = 0; col < Board.NumOfCols; col++)
                {
                    Assert.AreEqual<char>(b[row, col].Piece.Type, b2[row, col].Piece.Type);
                }
            }
        }
    }
}
