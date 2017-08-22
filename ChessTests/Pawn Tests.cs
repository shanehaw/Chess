using System;
using Chess;
using Chess.Pieces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessTests
{
    [TestClass]
    public class PawnTests
    {        
        private static void setupWhiteEnPassantRight(Board b)
        {
            b.Select(6, 0);
            b.Move(4, 0);

            b.Select(1, 7);
            b.Move(3, 7);

            b.Select(4, 0);
            b.Move(3, 0);

            b.Select(1, 1);
            b.Move(3, 1);
        }

        private static void whiteTakeEnPassantRight(Board b)
        {
            b.Select(3, 0);
            b.Move(2, 1);
        }

        private static void setupWhiteEnPassantLeft(Board b)
        {
            b.Select(6, 1);
            b.Move(4, 1);

            b.Select(1, 7);
            b.Move(3, 7);

            b.Select(4, 1);
            b.Move(3, 1);

            b.Select(1, 0);
            b.Move(3, 0);
        }

        private static void whiteTakeEnPassantLeft(Board b)
        {
            b.Select(3, 1);
            b.Move(2, 0);
        }

        private static void Black3rdPawnUpOne(Board b)
        {
            b.Select(1, 5);
            b.Move(2, 5);
        }

        private static void White6thPawnUpOne(Board b)
        {
            b.Select(6, 6);
            b.Move(5, 6);
        }

        [TestMethod]
        public void canWhitePawnMoveOneSpaceForward()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(6, 0); //White 1st pawn
            b.Move(5, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void whitePawnCannotMoveBackwards()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(6, 0);
            b.Move(7, 0);
        }

        [TestMethod]
        public void canBlackPawnMoveOneSpaceForward()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(1, 0);
            b.Move(2, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void blackPawnCannotMoveBackwards()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(1, 0);
            b.Move(0, 0);
        }

        [TestMethod]
        public void whitePawnCanMoveTwoSpacesAtStart()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(6, 0);
            b.Move(4, 0);
        }

        [TestMethod]
        public void blackPawnCanMoveTwoSpacesAtStart()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(1, 0);
            b.Move(3, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void whitePawnCannotMoveTwoSpacesAfterStart()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(6, 0);
            b.Move(4, 0);
            b.Select(4, 0);
            b.Move(2, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void blackPawnCannotMoveTwoSpacesAfterStart()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(1, 0);
            b.Move(2, 0);
            b.Select(2, 0);
            b.Move(4, 0);
        }

        [TestMethod]
        public void whitePawnCanTakePieceRight()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(6, 0);
            b.Move(4, 0);
            b.Select(1, 1);
            b.Move(3, 1);
            b.Select(4, 0);
            b.Move(3, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void whitePawnCannotTakeEmptyPieceRight()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(6, 0);
            b.Move(5, 1);
        }

        [TestMethod]
        public void whitePawnCanTakePieceLeft()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(6, 1);
            b.Move(4, 1);

            b.Select(1, 0);
            b.Move(3, 0);

            b.Select(4, 1);
            b.Move(3, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void whitePawnCannotTakeEmptyPieceLeft()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(6, 1);
            b.Move(5, 0);
        }

        [TestMethod]
        public void blackPawnCanTakePieceRight()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(1, 7);
            b.Move(3, 7);

            b.Select(6, 6);
            b.Move(4, 6);

            b.Select(3, 7);
            b.Move(4, 6);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void blackPawnCannotTakeEmptyPieceRight()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(1, 7);
            b.Move(2, 6);
        }

        [TestMethod]
        public void blackPawnCanTakePieceLeft()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(1, 0);
            b.Move(3, 0);
            b.Select(6, 1);
            b.Move(4, 1);
            b.Select(3, 0);
            b.Move(4, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void blackCannotTakeEmptyPieceLeft()
        {            
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.Black, 1, 0);
            b.Select(1, 0);
            b.Move(2, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void whitePawnCannotTakeTwoRowsForwardAtStart()
        {
            Board b = TestBoard.createAndSetupBoard();

            b.Select(1, 0);
            b.Move(3, 0);

            b.Select(6, 1);
            b.Move(4, 1);

            b.Select(3, 0);
            b.Move(4, 1);

            b.Select(4, 1);
            b.Move(5, 1);

            b.Select(1, 1);
            b.Move(3, 1);

            b.Select(3, 1);
            b.Move(4, 1);

            b.Select(6, 0);
            b.Move(4, 1);
        }

        [TestMethod]
        public void canTakePawnEnPassantRight()
        {
            Board b = TestBoard.createAndSetupBoard();
            setupWhiteEnPassantRight(b);
            whiteTakeEnPassantRight(b);
        }

        [TestMethod]
        public void canTakePawnEnPassantLeft()
        {
            Board b = TestBoard.createAndSetupBoard();
            setupWhiteEnPassantLeft(b);
            whiteTakeEnPassantLeft(b);
        }

        [TestMethod]
        public void moveEnPassantRightRemovesPiece()
        {
            Board b = TestBoard.createAndSetupBoard();
            setupWhiteEnPassantRight(b);
            whiteTakeEnPassantRight(b);
            Assert.IsTrue(b.IsCellEmpty(3, 1));
        }

        [TestMethod]
        public void moveEnPassantLeftRemovesPiece()
        {
            Board b = TestBoard.createAndSetupBoard();
            setupWhiteEnPassantLeft(b);
            whiteTakeEnPassantLeft(b);
            Assert.IsTrue(b.IsCellEmpty(3, 0));
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void moveEnPassantRightNotImmediateThrowsError()
        {
            Board b = TestBoard.createAndSetupBoard();
            setupWhiteEnPassantRight(b);
            White6thPawnUpOne(b);
            Black3rdPawnUpOne(b);
            whiteTakeEnPassantRight(b);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void moveEnPassantLeftNotImmediateThrowError()
        {
            Board b = TestBoard.createAndSetupBoard();
            setupWhiteEnPassantLeft(b);
            White6thPawnUpOne(b);
            Black3rdPawnUpOne(b);
            whiteTakeEnPassantLeft(b);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotMoveEnPassantIfOtherPieceNotAPawn()
        {
            Board b = TestBoard.createAndSetupBoard();
            setupWhiteEnPassantRight(b);
            b[3, 1].Piece = new Bishop(Piece.PlayerColour.Black, b[3, 1], b);
            whiteTakeEnPassantRight(b);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotMove2AtStartIfPieceInTheWay()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(6, 7);
            b.Move(5, 7);

            b.Select(1, 0);
            b.Move(3, 0);

            b.Select(6, 6);
            b.Move(5, 6);

            b.Select(3, 0);
            b.Move(4, 0);

            b.Select(6, 0);
            b.Move(4, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotMove2AtStartIfPieceJustInFront()
        {
            Board b = TestBoard.createAndSetupBoard();
            b.Select(6, 7);
            b.Move(5, 7);

            b.Select(1, 0);
            b.Move(3, 0);

            b.Select(6, 6);
            b.Move(5, 6);

            b.Select(3, 0);
            b.Move(4, 0);

            b.Select(6, 5);
            b.Move(5, 5);

            b.Select(4, 0);
            b.Move(5, 0);

            b.Select(6, 0);
            b.Move(4, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotMoveOneForwardWhenPieceInTheWay()
        {
            Board b = TestBoard.createBoard();            
            b.Place<Pawn>(Piece.PlayerColour.White, 5, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 4, 3);                        
            b.Select(5, 3);
            b.Move(4, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotTakeOwnPieceDiagRight()
        {
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 3, 4);
            b.Select(4, 3);
            b.Move(3, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void cannotTakeOwnPieceDiagLeft()
        {
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.White, 3, 2);
            b.Select(4, 3);
            b.Move(3, 2);
        }

        [TestMethod]
        public void invalidMoveGivesCorrectPieceName()
        {
            try
            {
                Board b = TestBoard.createBoard();
                b.Place<Pawn>(Piece.PlayerColour.White, 4, 3);
                b.Select(4, 3);
                b.Move(4, 3);
            }
            catch (ApplicationException ex)
            {
                Assert.IsTrue(ex.Message.Contains("pawn"));
            }
        }

        [TestMethod]
        public void canGetPawnGetPossibleMovesOnTopRow()
        {
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.White, 0, 4);
            Pawn p = b.GetFirstPiece<Pawn>(Piece.PlayerColour.White);
            p.AtStartPosition = false;
            p.GetPossibleMoves();
        }

        //This is a temporary test for the simulator. This logic should probably be more in the simulator than in the pawn class but for now this is the easiest thing
        [TestMethod]
        public void whitePawnAutomaticallyPromotedToQueenWhenMovedToTopRow()
        {
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.White, 1, 2);
            b.GetFirstPiece<Pawn>(Piece.PlayerColour.White).AtStartPosition = false;
            b.Select(1, 2);
            b.Move(0, 2);
            Assert.IsInstanceOfType(b[0, 2].Piece, typeof(Queen));
        }

        [TestMethod]
        public void blackPawnAutomaticallyPromotedToQuenWhenMoveToBottomRow()
        {
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.Black, 6, 2);
            b.GetFirstPiece<Pawn>(Piece.PlayerColour.Black).AtStartPosition = false;
            b.Select(6, 2);
            b.Move(7, 2);
            Assert.IsInstanceOfType(b[7, 2].Piece, typeof(Queen));
        }

        [TestMethod]
        public void whitePawnAutoPromotionWithTakingPieceToTopRow()
        {
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.White, 1, 2);
            b.GetFirstPiece<Pawn>(Piece.PlayerColour.White).AtStartPosition = false;
            b.Place<Bishop>(Piece.PlayerColour.Black, 0, 3);
            b.Select(1, 2);
            b.Move(0, 3);
            Assert.IsInstanceOfType(b[0, 3].Piece, typeof(Queen));
        }
    }
}
