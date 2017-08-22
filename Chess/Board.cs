using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Pieces;

namespace Chess
{
    public class Board : ICloneable
    {
        public const int NumOfRows = 8;
        public const int NumOfCols = 8;

        public static int NumOfCells
        {
            get
            {
                return NumOfRows * NumOfCols;
            }
        }

        public event EventHandler WhitePieceMoved = delegate { };
        public event EventHandler BlackPieceMoved = delegate { };

        public GameCell SelectedCell
        {
            get
            {
                for (int row = 0; row < NumOfRows; row++)
                {
                    for (int col = 0; col < NumOfCols; col++)
                    {
                        if (cells[row, col].IsSelected)
                        {
                            return cells[row, col];
                        }
                    }
                }
                return null;
            }
        }

        protected GameCell[,] cells;

        public Board()
        {
            InitializeCells();
        }

        private void InitializeCells()
        {
            cells = new GameCell[NumOfRows, NumOfCols];
            for (int row = 0; row < NumOfRows; row++)
            {
                for (int col = 0; col < NumOfCols; col++)
                {
                    cells[row, col] = new GameCell(row, col, this);
                }
            }
        }

        public GameCell this[int x, int y]
        {
            get
            {
                return cells[x, y];
            }
        }

        public GameCell this[Cell cell]
        {
            get
            {
                return cells[cell.Row, cell.Col];
            }
        }

        public void Setup()
        {
            for (int row = 0; row < NumOfRows; row++)
            {
                for (int col = 0; col < NumOfCols; col++)
                {
                    setColour(row, col);
                    setPiece(row, col);
                }
            }
        }

        private void setColour(int row, int col)
        {
            this[row, col].Colour = isEven(col + row) ? Color.White : Color.Black;
        }

        private static bool isEven(int num)
        {
            return (num % 2 == 0);
        }

        private void setPiece(int row, int col)
        {
            Piece.PlayerColour colour = getPlayerColour(row);
            if (OnBoundary(row) && OnBoundary(col))
            {
                Place<Rook>(colour, row, col);
            }
            else if (OnBoundary(row) && onKnightStart(col))
            {
                Place<Knight>(colour, row, col);
            }
            else if (OnBoundary(row) && onBishopStart(col))
            {
                Place<Bishop>(colour, row, col);
            }
            else if (OnBoundary(row) && onQueenStart(col))
            {
                Place<Queen>(colour, row, col);
            }
            else if (OnBoundary(row) && onKingStart(col))
            {
                Place<King>(colour, row, col);
            }
            else if (onPawnStart(row))
            {
                Place<Pawn>(colour, row, col);
            }
        }

        public static bool OnBoundary(int index)
        {
            return (index == 0 || index == 7);
        }

        private Piece.PlayerColour getPlayerColour(int row)
        {
            return (row < 2 ? Piece.PlayerColour.Black : Piece.PlayerColour.White);
        }

        public void Place<T>(Piece.PlayerColour colour, int row, int col)
            where T : Piece
        {
            this[row, col].Piece = (T)Activator.CreateInstance(typeof(T), colour, this[row, col], this);
        }

        private bool onKnightStart(int col)
        {
            return (col == 1 || col == 6);
        }

        private bool onBishopStart(int col)
        {
            return (col == 2 || col == 5);
        }

        private bool onQueenStart(int col)
        {
            return (col == 3);
        }

        private bool onKingStart(int col)
        {
            return (col == 4);
        }

        private bool onPawnStart(int row)
        {
            return (row == 1 || row == 6);
        }

        public void Select(int row, int col)
        {
            if (InBounds(row, col))
            {
                cells[row, col].IsSelected = true;
            }
            else
            {
                throw new ArgumentOutOfRangeException(String.Format("({0},{1}) not in {{8 > row >= 0; 8 > col >= 0}}", row, col));
            }
        }

        public static bool InBounds(int row, int col)
        {
            return InBounds(new Cell(row, col));
        }

        public static bool InBounds(Cell cell)
        {
            return (cell.Row >= 0 && cell.Col >= 0 && cell.Row < NumOfRows && cell.Col < NumOfCols);
        }

        public void Move(int row, int col)
        {
            if (InBounds(row, col))
            {
                AttemptMovePiece(this[row, col]);
            }
            else
            {
                throw new ArgumentOutOfRangeException(String.Format("({0},{1}) not in {{8 > row >= 0; 8 > col >= 0}}", row, col));
            }
        }

        private void AttemptMovePiece(GameCell toCell)
        {
            if (SelectedCell.Piece.CanMoveTo(toCell))
            {
                PerformMoveOnSelectedPiece(toCell);
            }
            else
            {
                throw new ApplicationException(String.Format("Cannot move {2} to ({0}, {1})", toCell.Row, toCell.Col, SelectedCell.Piece.ToString()));
            }
        }

        public void PerformMoveOnSelectedPiece(Cell toCell)
        {
            MovePiece(SelectedCell, toCell);
            RemoveSelection();
        }

        public void MovePiece(Cell fromCell, Cell toCell)
        {
            GameCell gameFromCell = this[fromCell] as GameCell;
            GameCell gameToCell = this[toCell] as GameCell;
            if (gameFromCell != null && gameToCell != null)
            {
                MovePiece(gameFromCell, gameToCell);
            }
        }

        private void MovePiece(GameCell fromCell, GameCell toCell)
        {
            fromCell.Piece.RegisterMove(toCell);
            MovePieceOnBoard(fromCell, toCell);            
            FirePieceMovedEvent(toCell);
        }

        private void MovePieceOnBoard(GameCell fromCell, GameCell toCell)
        {
            toCell.Piece = fromCell.Piece;
            EmptyCell(fromCell);
        }

        private void FirePieceMovedEvent(GameCell toCell)
        {
            if (toCell.Piece.isWhite())
            {
                WhitePieceMoved.Invoke(this, EventArgs.Empty);
            }
            else
            {
                BlackPieceMoved.Invoke(this, EventArgs.Empty);
            }
        }

        public void EmptyCell(Cell cell)
        {
            this[cell].Piece = new Piece(Piece.PlayerColour.None, cell, this);
        }

        private void RemoveSelection()
        {
            SelectedCell.IsSelected = false;
        }

        public bool IsCellEmpty(int row, int col)
        {
            return IsCellEmpty(new Cell(row, col));
        }

        public bool IsCellEmpty(Cell cell)
        {
            return (this[cell.Row, cell.Col].Piece.Type == Piece.Empty);
        }

        public bool isCellVulnerableAfterMove(GameCell fromCell, GameCell toCell, Piece.PlayerColour opposingColour)
        {
            Piece pieceToMove = fromCell.Piece;
            Piece pieceAtToCell = toCell.Piece;

            MovePieceOnBoard(fromCell, toCell);                                    
            
            bool isVulnerable = isCellVulnerable(toCell, opposingColour);

            fromCell.Piece = pieceToMove;
            toCell.Piece = pieceAtToCell;

            return isVulnerable;
        }

        public bool isCellVulnerable(Cell cell, Piece.PlayerColour opposingColour)
        {
            bool isVulnerable = false;
            foreach (Piece p in EnumeratePieces(opposingColour))
            {
                p.VulnerableCheck = true;

                if (p.GetMoves().Contains(cell))
                {
                    isVulnerable = true;
                    break;
                }

                p.VulnerableCheck = false;
            }
            return isVulnerable;
        }

        public IEnumerable<Piece> EnumeratePieces(Piece.PlayerColour playerColour)
        {
            for (int row = 0; row < NumOfRows; row++)
            {
                for (int col = 0; col < NumOfCols; col++)
                {
                    if (this[row, col].Piece.Colour == playerColour)
                    {
                        yield return this[row, col].Piece;
                    }
                }
            }
        }

        public T GetFirstPiece<T>(Piece.PlayerColour playerColour)
            where T : Piece
        {
            IEnumerable<T> pieces = EnumeratePieceType<T>(playerColour);
            if (pieces.Count() > 0)
            {
                return pieces.First();
            }
            return null;
        }

        public IEnumerable<T> EnumeratePieceType<T>(Piece.PlayerColour playerColour)
            where T : Piece
        {
            foreach (Piece p in EnumeratePieces(playerColour))
            {
                if (p is T)
                {
                    yield return (T)p;
                }
            }
        }        

        public SimulationTree Simulate(Piece.PlayerColour startColour, int numOfMoves)
        {
            return Simulate(startColour, Node => Node.NumOfMoves == numOfMoves);
        }

        public SimulationTree Simulate(Piece.PlayerColour startColour, Predicate<SimulationTree.SimNode> stopCase)
        {
            SimulationTree simTree = new SimulationTree();
            simTree.Base = new SimulationTree.SimNode(this, 0);
            BuildTree(simTree.Base, startColour, stopCase);
            return simTree;
        }

        private void BuildTree(SimulationTree.SimNode currentNode, Piece.PlayerColour toMove, Predicate<SimulationTree.SimNode> stopCase)
        {
            if (stopCase(currentNode))
            {
                return;
            }

            foreach (Board nextMoveBoard in currentNode.Board.Simulate(toMove))
            {
                SimulationTree.SimNode nextMoveNode = new SimulationTree.SimNode(nextMoveBoard, currentNode.NumOfMoves + 1);
                currentNode.NextMoves.Add(nextMoveNode);                                
            }

            currentNode.Board = null;            
            foreach (SimulationTree.SimNode node in currentNode.NextMoves)
            {
                Piece.PlayerColour nextColour = toMove == Piece.PlayerColour.White ? Piece.PlayerColour.Black : Piece.PlayerColour.White;
                BuildTree(node, nextColour, stopCase);
            }
        }

        public List<Board> Simulate(Piece.PlayerColour playerColour)
        {            
            List<Board> simBoards = new List<Board>();
            foreach (Piece p in EnumeratePieces(playerColour))
            {                
                foreach (Cell move in p.GetPossibleMoves())
                {
                    simBoards.Add(SimulateMove(p.ParentCell, move));
                }
            }
            return simBoards;
        }

        public Board SimulateMove(Cell fromCell, Cell toCell)
        {
            Board newBoard = Clone() as Board;
            newBoard.Select(fromCell.Row, fromCell.Col);
            newBoard.PerformMoveOnSelectedPiece(toCell);            
            return newBoard;
        }

        public object Clone()
        {
            Board b = new Board();
            ClonePieces(Piece.PlayerColour.White, b);
            ClonePieces(Piece.PlayerColour.Black, b);
            return b;
        }

        private void ClonePieces(Piece.PlayerColour playerColour, Board b)
        {
            foreach (Piece p in EnumeratePieces(playerColour))
            {
                p.CopyToBoard(b);
            }
        }

        public bool InStalemate(Piece.PlayerColour playerColour)
        {
            King k = GetFirstPiece<King>(playerColour);
            if (k != null)
            {
                return !k.InCheck && Simulate(Piece.PlayerColour.White).Count == 0;
            }
            return false;
        }

        public void Replace<T>(int row, int col)
            where T : Piece
        {
            if (isPawn(row, col) && isTypePawnPromotionCondidate(typeof(T)))
            {
                Piece.PlayerColour colour = this[row, col].Piece.Colour;
                Place<T>(colour, row, col);
            }
            else
            {
                throw new ApplicationException(String.Format("Cannot replace {0}.", this[row, col].Piece.ToString()));
            }
        }

        private bool isPawn(int row, int col)
        {
            return this[row, col].Piece.Type == Piece.Pawn;
        }

        private bool isTypePawnPromotionCondidate(System.Reflection.MemberInfo typeInfo)
        {
            bool candidate = false;
            foreach (object attribute in typeInfo.GetCustomAttributes(true))
            {
                if (attribute is PawnPromotionCandidateAttribute)
                {
                    candidate = true;
                    break;
                }
            }
            return candidate;
        }

        public string BoardString
        {
            get
            {
                string boardString = "";
                for (int i = 0; i < Board.NumOfCols; i++)
                {
                    for (int j = 0; j < Board.NumOfRows; j++)
                    {
                        boardString += ((this[i, j].Piece.Colour == Piece.PlayerColour.Black ? this[i, j].Piece.Type.ToString() : this[i, j].Piece.Type.ToString().ToLower()) + " ");
                    }
                    boardString += Environment.NewLine;
                }
                return boardString;
            }
        }

        public IEnumerable<BoardWithHistory> EnumSimTillNumMovesAhead(int numMovesAhead, Piece.PlayerColour startColour)
        {
            return EnumSimTill(startColour, A => A.MoveCount == numMovesAhead);
        }

        public IEnumerable<BoardWithHistory> EnumSimTill(Piece.PlayerColour startColour, Predicate<ConditionalStruct> condition)
        {
            Board copyOfBoard = this.Clone() as Board;
            BoardWithHistory copyOfBoardWH = new BoardWithHistory()
            {
                Board = copyOfBoard,
                BoardHistory = copyOfBoard.BoardString,
            };

            foreach (BoardWithHistory simBoard in EnumSim(copyOfBoardWH, startColour, 0, condition))
            {
                yield return simBoard;
            }
        }

        public struct ConditionalStruct
        {
            public Board Board { get; set; }
            public int MoveCount { get; set; }
        }

        public struct BoardWithHistory
        {
            public Board Board { get; set; }
            public string BoardHistory { get; set; }
        }

        private IEnumerable<BoardWithHistory> EnumSim(BoardWithHistory currentBoard, Piece.PlayerColour currentColour, int currentMoveNum, Predicate<ConditionalStruct> condition)
        {
            if (condition(new ConditionalStruct { Board = currentBoard.Board, MoveCount = currentMoveNum }))
            {
                yield return currentBoard;
            }
            else
            {
                Piece.PlayerColour nextColour = currentColour == Piece.PlayerColour.White ? Piece.PlayerColour.Black : Piece.PlayerColour.White;
                foreach (Piece p in currentBoard.Board.EnumeratePieces(currentColour))
                {
                    foreach (Cell move in p.GetPossibleMoves())
                    {
                        Board nextBoard = currentBoard.Board.SimulateMove(p.ParentCell, move);
                        BoardWithHistory nextBWH = new BoardWithHistory()
                        {
                            Board = nextBoard,
                            BoardHistory = currentBoard.BoardHistory + "\r\n\r\n" + nextBoard.BoardString,
                        };

                        foreach (BoardWithHistory returnedBoard in EnumSim(nextBWH, nextColour, currentMoveNum + 1, condition))
                        {
                            yield return returnedBoard;
                        }
                    }
                }
            }
        }        
    }
}