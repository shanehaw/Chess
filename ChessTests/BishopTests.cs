using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess;
using Chess.Pieces;

namespace ChessTests
{
    [TestClass]
    public class BishopTests
    {
        [TestMethod]
        public void canMoveWhiteBishopDiagonalUpRightNoObstructions()
        {            
            for (int row = 3, col = 4; row >= 0 && col < Board.NumOfCols; row--, col++)
            {                
                Board b = TestBoard.createBoard();
                b.Place<Bishop>(Piece.PlayerColour.White, 4, 3);
                b.Select(4, 3);
                b.Move(row, col);                
            }
        }

        [TestMethod]
        public void canMoveBlackBishipDiagonalUpRightNoObstructions()
        {
            for (int row = 5, col = 2; row < Board.NumOfRows && col >= 0; row++, col--)
            {
                Board b = TestBoard.createBoard();
                b.Place<Bishop>(Piece.PlayerColour.Black, 4, 3);
                b.Select(4, 3);
                b.Move(row, col);
            }
        }

        [TestMethod]        
        public void canMoveWhiteBishopDiagonalUpLeftNoObstructions()
        {
            for (int row = 3, col = 2; row >= 0 && col >= 0; row--, col--)
            {
                Board b = TestBoard.createBoard();
                b.Place<Bishop>(Piece.PlayerColour.White, 4, 3);
                b.Select(4, 3);
                b.Move(row, col);
            }
        }

        [TestMethod]        
        public void canMoveBlackBishopDiagonalUpLeftNoObstructions()
        {
            for (int row = 5, col = 4; row < Board.NumOfRows && col < Board.NumOfCols; row++, col++)
            {
                Board b = TestBoard.createBoard();
                b.Place<Bishop>(Piece.PlayerColour.Black, 4, 3);
                b.Select(4, 3);
                b.Move(row, col);
            }
        }

        [TestMethod]
        public void canMoveWhiteBishopDiagonallyDownLeftNoObstructions()
        {
            for (int row = 5, col = 2; row < Board.NumOfRows && col >= 0; row++, col--)
            {
                Board b = TestBoard.createBoard();
                b.Place<Bishop>(Piece.PlayerColour.White, 4, 3);
                b.Select(4, 3);
                b.Move(row, col);
            }
        }

        [TestMethod]        
        public void canMoveBlackBishopDiagonallyDownLeftNoObstructions()
        {
            for (int row = 3, col = 4; row >= 0 && col < Board.NumOfCols; row--, col++)
            {
                Board b = TestBoard.createBoard();
                b.Place<Bishop>(Piece.PlayerColour.Black, 4, 3);
                b.Select(4, 3);
                b.Move(row, col);
            }
        }

        [TestMethod]
        public void canMoveWhiteBishopDiagonallyDownRightNoObstructions()
        {
            for (int row = 5, col = 4; row < Board.NumOfRows && col < Board.NumOfCols; row++, col++)
            {
                Board b = TestBoard.createBoard();
                b.Place<Bishop>(Piece.PlayerColour.White, 4, 3);
                b.Select(4, 3);
                b.Move(row, col);
            }
        }

        [TestMethod]
        public void canMoveBlackBishopDiagonallyDownRightNoObstructions()
        {
            for (int row = 3, col = 2; row >= 0 && col >= 0; row--, col--)
            {
                Board b = TestBoard.createBoard();
                b.Place<Bishop>(Piece.PlayerColour.Black, 4, 3);
                b.Select(4, 3);
                b.Move(row, col);
            }
        }

        [TestMethod]
        public void canTakePiece()
        {
            Board b = TestBoard.createBoard();
            b.Place<Bishop>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 3, 4);
            b.Select(4, 3);
            b.Move(3, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotTakeOwnPieceDiagUpRight()
        {
            Board b = TestBoard.createBoard();
            b.Place<Bishop>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 3, 4);
            b.Select(4, 3);
            b.Move(3, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotTakeOwnPieceDiagDownRight()
        {
            Board b = TestBoard.createBoard();
            b.Place<Bishop>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 5, 4);
            b.Select(4, 3);
            b.Move(5, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotTakeOwnPieceDiagDownLeft()
        {
            Board b = TestBoard.createBoard();
            b.Place<Bishop>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 5, 2);
            b.Select(4, 3);
            b.Move(5, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotTakeOwnPieceDiagUpLeft()
        {
            Board b = TestBoard.createBoard();
            b.Place<Bishop>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 3, 2);
            b.Select(4, 3);
            b.Move(3, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotJumpOppositionDiagUpRight()
        {
            Board b = TestBoard.createBoard();
            b.Place<Bishop>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 3, 4);
            b.Select(4, 3);
            b.Move(2, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotJumpOppositionDiagUpLeft()
        {
            Board b = TestBoard.createBoard();
            b.Place<Bishop>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 3, 2);
            b.Select(4, 3);
            b.Move(2, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotJumpOppositionDiagDownLeft()
        {
            Board b = TestBoard.createBoard();
            b.Place<Bishop>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 5, 2);
            b.Select(4, 3);
            b.Move(6, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotJumpOppositionDiagDownRight()
        {
            Board b = TestBoard.createBoard();
            b.Place<Bishop>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 5, 4);
            b.Select(4, 3);
            b.Move(6, 5);
        }

        [TestMethod]
        public void invalidMoveGivesCorrectPieceName()
        {
            try
            {
                Board b = TestBoard.createBoard();
                b.Place<Bishop>(Piece.PlayerColour.White, 4, 3);
                b.Select(4, 3);
                b.Move(4, 3);
            }
            catch (ApplicationException ex)
            {
                Assert.IsTrue(ex.Message.Contains("bishop"));                       
            }
        }
    }
}
