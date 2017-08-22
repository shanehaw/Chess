using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess;
using Chess.Pieces;
using System.Collections.Generic;
using System.IO;

namespace ChessTests
{
    [TestClass]
    public class BoardSimulateTests
    {
        [TestMethod]
        public void simulationReturnsEmptyListForNoPieces()
        {
            Board b = TestBoard.createBoard();            
            List<Board> simBoards = b.Simulate(Piece.PlayerColour.White);
            Assert.AreEqual<int>(0, simBoards.Count);
        }

        [TestMethod]
        public void simulationReturnsTwoBoardsForSinglePawnAtStart()
        {
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.White, 6, 3);            
            List<Board> simBoards = b.Simulate(Piece.PlayerColour.White);
            Assert.AreEqual<int>(2, simBoards.Count);
            List<Cell> expectedPositions = new List<Cell>()
            {
                new Cell(5, 3),
                new Cell(4, 3)
            };
            foreach (Board simBoard in simBoards)
            {
                Pawn p = simBoard.GetFirstPiece<Pawn>(Piece.PlayerColour.White);
                Assert.IsNotNull(p);
                Assert.IsTrue(expectedPositions.Contains(p.ParentCell));
            }
        }

        [TestMethod]
        public void simulationReturnsCorrectSimulationBoardsForPawnAtStart()
        {
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.White, 6, 3);
            List<Board> simBoards = b.Simulate(Piece.PlayerColour.White);
            
            List<Cell> expectedPositions = new List<Cell>()
            {
                new Cell(5, 3), //One ahead
                new Cell(4, 3)  //Two ahead
            };

            foreach (Board simBoard in simBoards)
            {
                Pawn p = simBoard.GetFirstPiece<Pawn>(Piece.PlayerColour.White);
                Assert.IsNotNull(p);
                Assert.IsTrue(expectedPositions.Contains(p.ParentCell));
            }
        }

        [TestMethod]
        public void simulationReturnsCorrectNumberForQueen()
        {
            Board b = TestBoard.createBoard();
            b.Place<Queen>(Piece.PlayerColour.White, 4, 3);
            List<Board> simBoards = b.Simulate(Piece.PlayerColour.White);
            Assert.AreEqual<int>(27, simBoards.Count);
        }

        [TestMethod]
        public void simulationReturnsCorrectSimBoardsForQueen()
        {
            Board b = TestBoard.createBoard();
            b.Place<Queen>(Piece.PlayerColour.White, 4, 3);
            List<Board> simBoards = b.Simulate(Piece.PlayerColour.White);

            List<Cell> expectedPositions = new List<Cell>()
            {
                new Cell(3, 3), 
                new Cell(2, 3),
                new Cell(1, 3),
                new Cell(0, 3),

                new Cell(3, 4),
                new Cell(2, 5),
                new Cell(1, 6),
                new Cell(0, 7),

                new Cell(4, 4),
                new Cell(4, 5),
                new Cell(4, 6),
                new Cell(4, 7),

                new Cell(5, 4),
                new Cell(6, 5),
                new Cell(7, 6),

                new Cell(5, 3),
                new Cell(6, 3),
                new Cell(7, 3),

                new Cell(5, 2),
                new Cell(6, 1),
                new Cell(7, 0),

                new Cell(4, 2),
                new Cell(4, 1),
                new Cell(4, 0),

                new Cell(3, 2),
                new Cell(2, 1),
                new Cell(1, 0),
            };

            foreach (Board simBoard in simBoards)
            {
                Queen p = simBoard.GetFirstPiece<Queen>(Piece.PlayerColour.White);
                Assert.IsNotNull(p);
                Assert.IsTrue(expectedPositions.Contains(p.ParentCell), "{{{0}, {1}}} not found", p.ParentCell.Row, p.ParentCell.Col);
            }
        }

        [TestMethod]
        public void boardCloneMaintainsPawnAtStartProperty()
        {
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.White, 4, 3);
            b.GetFirstPiece<Pawn>(Piece.PlayerColour.White).AtStartPosition = false;
            Board newBoard = b.Clone() as Board;
            Assert.AreEqual<bool>(
                b.GetFirstPiece<Pawn>(Piece.PlayerColour.White).AtStartPosition, 
                newBoard.GetFirstPiece<Pawn>(Piece.PlayerColour.White).AtStartPosition);
        }

        [TestMethod]
        public void canSimulate2Moves()
        {
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 1, 1);
            b.Simulate(Piece.PlayerColour.White, 2);                       
        }

        [TestMethod]
        public void simulateReturnSimTree()
        {
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 1, 1);
            SimulationTree simTree = b.Simulate(Piece.PlayerColour.White, 2);
        }

        [TestMethod]
        public void ReturnedSimTreeCanGetBoardsAtNumMovesAhead()
        {
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 1, 1);
            SimulationTree simTree = b.Simulate(Piece.PlayerColour.White, 2);
            List<Board> twoMovesAheadBoards = simTree.GetPossibilitesAtNumMovesAhead(2);
        }

        [TestMethod]
        public void canSimulateOneMoveAhead()
        {
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 1, 1);
            Pawn whitePawn = b.GetFirstPiece<Pawn>(Piece.PlayerColour.White);
            whitePawn.AtStartPosition = false;
            SimulationTree simTree = b.Simulate(Piece.PlayerColour.White, 1);
            List<Board> oneMoveAheadBoards = simTree.GetPossibilitesAtNumMovesAhead(1);
            Assert.AreEqual<int>(1, oneMoveAheadBoards.Count);
            Pawn oneMoveAheadPawn = oneMoveAheadBoards[0].GetFirstPiece<Pawn>(Piece.PlayerColour.White);
            Assert.AreEqual<Cell>(new Cell(3, 3), oneMoveAheadPawn.ParentCell);
        }

        [TestMethod]        
        public void canSimTwoMovesAhead()
        {
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 2, 1);
            b.GetFirstPiece<Pawn>(Piece.PlayerColour.White).AtStartPosition = false;
            b.GetFirstPiece<Pawn>(Piece.PlayerColour.Black).AtStartPosition = false;
            SimulationTree simTree = b.Simulate(Piece.PlayerColour.White, 2);
            List<Board> twoMoveAheadBoards = simTree.GetPossibilitesAtNumMovesAhead(2);
            Assert.AreEqual<int>(1, twoMoveAheadBoards.Count);

            Pawn whiteOneMoveAheadPawn = twoMoveAheadBoards[0].GetFirstPiece<Pawn>(Piece.PlayerColour.White);
            Assert.AreEqual<Cell>(new Cell(3, 3), whiteOneMoveAheadPawn.ParentCell);

            Pawn blackTwoMovesAheadPawn = twoMoveAheadBoards[0].GetFirstPiece<Pawn>(Piece.PlayerColour.Black);
            Assert.AreEqual<Cell>(new Cell(3, 1), blackTwoMovesAheadPawn.ParentCell);
        }

        [TestMethod]
        public void canSimThreeMovesAhead()
        {
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 2, 1);
            b.GetFirstPiece<Pawn>(Piece.PlayerColour.White).AtStartPosition = false;
            b.GetFirstPiece<Pawn>(Piece.PlayerColour.Black).AtStartPosition = false;
            SimulationTree simTree = b.Simulate(Piece.PlayerColour.White, 3);
            List<Board> threeMovesAheadBoards = simTree.GetPossibilitesAtNumMovesAhead(3);
            Assert.AreEqual<int>(1, threeMovesAheadBoards.Count);

            Pawn whiteOneMoveAheadPawn = threeMovesAheadBoards[0].GetFirstPiece<Pawn>(Piece.PlayerColour.White);
            Assert.AreEqual<Cell>(new Cell(2, 3), whiteOneMoveAheadPawn.ParentCell);

            Pawn blackTwoMovesAheadPawn = threeMovesAheadBoards[0].GetFirstPiece<Pawn>(Piece.PlayerColour.Black);
            Assert.AreEqual<Cell>(new Cell(3, 1), blackTwoMovesAheadPawn.ParentCell);
        }

        [TestMethod]
        public void canSimFourMovesAhead()
        {
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 2, 1);
            b.GetFirstPiece<Pawn>(Piece.PlayerColour.White).AtStartPosition = false;
            b.GetFirstPiece<Pawn>(Piece.PlayerColour.Black).AtStartPosition = false;
            SimulationTree simTree = b.Simulate(Piece.PlayerColour.White, 4);
            List<Board> fourMovesAheadBoards = simTree.GetPossibilitesAtNumMovesAhead(4);
            Assert.AreEqual<int>(1, fourMovesAheadBoards.Count);

            Pawn whiteOneMoveAheadPawn = fourMovesAheadBoards[0].GetFirstPiece<Pawn>(Piece.PlayerColour.White);
            Assert.AreEqual<Cell>(new Cell(2, 3), whiteOneMoveAheadPawn.ParentCell);

            Pawn blackTwoMovesAheadPawn = fourMovesAheadBoards[0].GetFirstPiece<Pawn>(Piece.PlayerColour.Black);
            Assert.AreEqual<Cell>(new Cell(4, 1), blackTwoMovesAheadPawn.ParentCell);
        }

        [TestMethod]
        public void canSim1MoveWithOptions()
        {
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 3, 4);
            b.GetFirstPiece<Pawn>(Piece.PlayerColour.White).AtStartPosition = false;
            b.GetFirstPiece<Pawn>(Piece.PlayerColour.Black).AtStartPosition = false;
            SimulationTree simTree = b.Simulate(Piece.PlayerColour.White, 1);
            List<Board> twoMovesAheadBoards = simTree.GetPossibilitesAtNumMovesAhead(1);
            Assert.AreEqual<int>(2, twoMovesAheadBoards.Count);

            List<Cell> expectedPositions = new List<Cell>()
            {
                new Cell(3, 3),
                new Cell(3, 4),
            };

            foreach (Board possibleBoard in twoMovesAheadBoards)
            {
                Pawn whitePawn = possibleBoard.GetFirstPiece<Pawn>(Piece.PlayerColour.White);
                Assert.IsTrue(expectedPositions.Contains(whitePawn.ParentCell));
            }
        }

        [TestMethod]
        public void canSim2MovesAheadWithOptions()
        {
            Board b = TestBoard.createBoard();
            b.Place<Pawn>(Piece.PlayerColour.White, 4, 3);
            b.Place<Pawn>(Piece.PlayerColour.Black, 2, 4);
            b.GetFirstPiece<Pawn>(Piece.PlayerColour.White).AtStartPosition = false;
            b.GetFirstPiece<Pawn>(Piece.PlayerColour.Black).AtStartPosition = false;
            SimulationTree simTree = b.Simulate(Piece.PlayerColour.Black, 2);
            List<Board> twoMovesAheadBoards = simTree.GetPossibilitesAtNumMovesAhead(2);
            Assert.AreEqual<int>(2, twoMovesAheadBoards.Count);

            List<Cell> expectedPositions = new List<Cell>()
            {
                new Cell(3, 3),
                new Cell(3, 4),
            };

            foreach (Board possibleBoard in twoMovesAheadBoards)
            {
                Pawn whitePawn = possibleBoard.GetFirstPiece<Pawn>(Piece.PlayerColour.White);
                Assert.IsTrue(expectedPositions.Contains(whitePawn.ParentCell));
            }
        }

        [TestMethod]
        public void canSim2MovesWithWithMultiplePieces()
        {
            Board b = TestBoard.createBoard();
            b.Place<Knight>(Piece.PlayerColour.White, 7, 1);
            b.Place<Knight>(Piece.PlayerColour.Black, 0, 1);
            SimulationTree simTree = b.Simulate(Piece.PlayerColour.White, 2);
            List<Board> twoMovesAheadBoards = simTree.GetPossibilitesAtNumMovesAhead(2);            
            Assert.AreEqual<int>(9, twoMovesAheadBoards.Count);
        }
        
        [TestMethod]
        [Ignore]
        public void canSimEnumSetupBoardTo1MovesAhead()
        {
            Board b = TestBoard.createAndSetupBoard();
            Board.BoardWithHistory copyOfBoardWH = new Board.BoardWithHistory()
            {
                Board = b,
                BoardHistory = b.BoardString,
            };

            int simBoardsCount = 0;
            foreach (Board.BoardWithHistory simBoard in b.EnumSimTillNumMovesAhead(1, Piece.PlayerColour.White))
            {
                simBoardsCount++;
            }
            Assert.AreEqual<int>(20, simBoardsCount);
        }

        [TestMethod]
        [Ignore]
        public void canSimEnumSetupBoardTo2MovesAhead()
        {
            Board b = TestBoard.createAndSetupBoard();
            Board.BoardWithHistory copyOfBoardWH = new Board.BoardWithHistory()
            {
                Board = b,
                BoardHistory = b.BoardString,
            };

            int simBoardsCount = 0;
            foreach (Board.BoardWithHistory simBoard in b.EnumSimTillNumMovesAhead(2, Piece.PlayerColour.White))
            {
                simBoardsCount++;
            }
            Assert.AreEqual<int>(400, simBoardsCount);
        }

        [TestMethod]
        [Ignore]
        public void canSimEnumSetupBoardTo3MovesAhead()
        {
            Board b = TestBoard.createAndSetupBoard();
            Board.BoardWithHistory copyOfBoardWH = new Board.BoardWithHistory()
            {
                Board = b,
                BoardHistory = b.BoardString,
            };

            int simBoardsCount = 0;
            foreach (Board.BoardWithHistory simBoard in b.EnumSimTillNumMovesAhead(3, Piece.PlayerColour.White))
            {
                simBoardsCount++;
            }
            Assert.AreEqual<int>(8902, simBoardsCount);
        }

        [TestMethod]
        [Ignore]
        public void canSimEnumSetupBoardTo4MovesAhead()
        {
            Board b = TestBoard.createAndSetupBoard();
            Board.BoardWithHistory copyOfBoardWH = new Board.BoardWithHistory()
            {
                Board = b,
                BoardHistory = b.BoardString,
            };

            int simBoardsCount = 0;
            foreach (Board.BoardWithHistory simBoard in b.EnumSimTillNumMovesAhead(4, Piece.PlayerColour.White))
            {
                simBoardsCount++;
            }
            Assert.AreEqual<int>(197702, simBoardsCount);
        }

        [TestMethod]
        [Ignore]
        public void canSimEnumSetupBoardTo5MovesAhead()
        {
            Board b = TestBoard.createAndSetupBoard();
            Board.BoardWithHistory copyOfBoardWH = new Board.BoardWithHistory()
            {
                Board = b,
                BoardHistory = b.BoardString,
            };

            int simBoardsCount = 0;
            foreach (Board.BoardWithHistory simBoard in b.EnumSimTillNumMovesAhead(5, Piece.PlayerColour.White))
            {
                simBoardsCount++;
            }
            Assert.AreEqual<int>(4894368, simBoardsCount);
        }

        [TestMethod]
        [Ignore]
        public void canSimEnumSetupBoardTo6MovesAhead()
        {
            Board b = TestBoard.createAndSetupBoard();
            Board.BoardWithHistory copyOfBoardWH = new Board.BoardWithHistory()
            {
                Board = b,
                BoardHistory = b.BoardString,
            };

            int simBoardsCount = 0;
            foreach (Board.BoardWithHistory simBoard in b.EnumSimTillNumMovesAhead(6, Piece.PlayerColour.White))
            {
                simBoardsCount++;
            }
            Assert.AreEqual<int>(120736438, simBoardsCount);
        }

        [TestMethod]   
        [Ignore]  
        public void canSimTillCheckMate()
        {
            Board b = TestBoard.createAndSetupBoard();
            int checkMateCounter = 0;            

            foreach (Board.BoardWithHistory simBoard in b.EnumSimTill(Piece.PlayerColour.White, C =>
                {
                    if (C.MoveCount == 10)
                    {
                        return true;
                    }
                    else
                    {
                        King blackKing = C.Board.GetFirstPiece<King>(Piece.PlayerColour.Black);                        
                        return blackKing.InCheckMate;                        
                    }
                }))
            {
                King blackKing = simBoard.Board.GetFirstPiece<King>(Piece.PlayerColour.Black);
                if (blackKing.InCheckMate)
                {
                    checkMateCounter++;                    
                }
            }
            int x = 46;
        }
    }
}
