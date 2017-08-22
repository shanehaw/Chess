using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Piece
    {
        public enum PlayerColour
        {
            Black,
            White,
            None
        }

        public const char Rook = 'R';
        public const char Pawn = 'P';
        public const char Knight = 'K';
        public const char Bishop = 'B';
        public const char Queen = 'Q';
        public const char King = 'I';
        public const char Empty = '.';

        private static int counter;

        public virtual char Type
        {
            get
            {
                return Piece.Empty;
            }
        }        

        public int ID
        {
            get;
            private set;
        }

        public PlayerColour Colour
        {
            get;
            private set;
        }

        public bool AtStartPosition
        {
            get;
            set;
        }

        public Cell ParentCell
        {
            get;
            set;
        }

        protected int OneRowForward
        {
            get
            {
                return isWhite() ? ParentCell.Row - 1 : ParentCell.Row + 1;
            }
        }

        protected int TwoRowsForward
        {
            get
            {
                return isWhite() ? ParentCell.Row - 2 : ParentCell.Row + 2;
            }
        }

        protected int OneColRight
        {
            get
            {
                return isWhite() ? ParentCell.Col + 1 : ParentCell.Col - 1;
            }
        }

        protected int OneColLeft
        {
            get
            {
                return isWhite() ? ParentCell.Col - 1 : ParentCell.Col + 1;
            }
        }

        protected int TwoColsRight
        {
            get
            {
                return isWhite() ? ParentCell.Col + 2 : ParentCell.Col - 2;
            }
        }

        protected int OneRowDown
        {
            get
            {
                return isWhite() ? ParentCell.Row + 1 : ParentCell.Row - 1;
            }
        }

        protected int TwoRowsDown
        {
            get
            {
                return isWhite() ? ParentCell.Row + 2 : ParentCell.Row - 2;
            }
        }

        protected int TwoColsLeft
        {
            get
            {
                return isWhite() ? ParentCell.Col - 2 : ParentCell.Col + 2;
            }
        }

        protected Cell OneCellForward
        {
            get
            {
                return new Cell(OneRowForward, ParentCell.Col);
            }
        }

        protected Cell CellDiagonallyRight
        {
            get
            {
                return new Cell(OneRowForward, OneColRight);
            }
        }

        protected Cell CellDiagonallyLeft
        {
            get
            {
                return new Cell(OneRowForward, OneColLeft);
            }
        }

        protected Cell CellOneColRight
        {
            get
            {
                return new Cell(ParentCell.Row, OneColRight);
            }
        }

        protected Cell CellOneColLeft
        {
            get
            {
                return new Cell(ParentCell.Row, OneColLeft);
            }
        }

        public Board gameBoard;               

        public Piece(PlayerColour colour, Cell parentCell, Board gameBoard)
        {
            this.ID = counter++;
            this.Colour = colour;
            this.ParentCell = parentCell;
            this.gameBoard = gameBoard;
            this.AtStartPosition = true;
            this.VulnerableCheck = false;
        }              

        public bool isWhite()
        {
            return (this.Colour == Piece.PlayerColour.White);
        }

        protected bool isEmpty(Cell cell)
        {
            return isEmpty(cell.Row, cell.Col);
        }

        protected bool isEmpty(int row, int col)
        {
            return gameBoard.IsCellEmpty(row, col);
        }

        protected bool isPieceOpposingColour(Cell cell)
        {
            return isPieceOpposingColour(cell.Row, cell.Col);
        }

        protected bool isPieceOpposingColour(int row, int col)
        {
            return gameBoard[row, col].Piece.Colour == OpposingColour;
        }

        protected virtual bool canMoveToCell(Cell possibleCell)
        {
            return Board.InBounds(possibleCell) && (isEmpty(possibleCell) || isPieceOpposingColour(possibleCell));
        }        

        public PlayerColour OpposingColour
        {
            get
            {
                return isWhite() ? PlayerColour.Black : PlayerColour.White;
            }
        }

        protected virtual bool canCapture(Cell possibleCell)
        {
            return !gameBoard.IsCellEmpty(possibleCell);
        }

        protected delegate List<Cell> getPossibleMoves();

        #region Vertically Up
        protected List<Cell> GetVerticallyUp()
        {
            List<Cell> possibleMoves = new List<Cell>();
            for (int row = OneRowForward; IsVerticallyUpRowValid(row); IncrementVerticallyUpRow(ref row))
            {
                Cell possibleMove = getPossibleVerticalCell(row);
                possibleMoves.Add(possibleMove);
                if (canCapture(possibleMove))
                {
                    break;
                }
            }
            return possibleMoves;
        }

        protected bool willBeInCheck(GameCell possibleMove)
        {
            Board simBoard = gameBoard.SimulateMove(ParentCell, possibleMove);
            Chess.Pieces.King k = simBoard.GetFirstPiece<Chess.Pieces.King>(Colour);
            if (k != null)
            {
                return k.InCheck;
            }
            return false;
        }

        private bool IsVerticallyUpRowValid(int row)
        {
            Cell possibleCell = getPossibleVerticalCell(row);
            return isWhite()
                ? row >= 0 && canMoveToCell(possibleCell)
                : row < Board.NumOfRows && canMoveToCell(possibleCell);
        }

        private Cell getPossibleVerticalCell(int row)
        {
            return new Cell(row, ParentCell.Col);
        }

        private void IncrementVerticallyUpRow(ref int row)
        {
            row += isWhite() ? -1 : 1;
        } 
        #endregion

        #region Vertically Down
        protected List<Cell> GetVerticallyDown()
        {
            List<Cell> possibleMoves = new List<Cell>();
            int col = ParentCell.Col;
            for (int row = OneRowDown; IsVerticallyDownRowValid(row); IncrementVerticallyDownRow(ref row))
            {
                Cell possibleMove = getPossibleVerticalCell(row);
                possibleMoves.Add(possibleMove);
                if (canCapture(possibleMove))
                {
                    break;
                }
            }
            return possibleMoves;
        }

        private bool IsVerticallyDownRowValid(int row)
        {
            Cell possibleCell = getPossibleVerticalCell(row);
            return isWhite()
                ? row < Board.NumOfRows && canMoveToCell(possibleCell)
                : row >= 0 && canMoveToCell(possibleCell);
        }

        private void IncrementVerticallyDownRow(ref int row)
        {
            row += isWhite() ? 1 : -1;
        } 
        #endregion

        #region Horizontally Left
        protected List<Cell> GetHorizontallyLeft()
        {
            List<Cell> possibleMoves = new List<Cell>();
            for (int col = OneColLeft; IsHorizonatllyLeftColValid(col); IncrementHorizontallyLeftCol(ref col))
            {
                Cell possibleMove = getPossibleHorizontalCell(col);
                possibleMoves.Add(possibleMove);
                if (canCapture(possibleMove))
                {
                    break;
                }
            }
            return possibleMoves;
        }

        private bool IsHorizonatllyLeftColValid(int col)
        {
            Cell possibleCell = getPossibleHorizontalCell(col);
            return isWhite()
                ? col >= 0 && canMoveToCell(possibleCell)
                : col < Board.NumOfCols && canMoveToCell(possibleCell);
        }

        private Cell getPossibleHorizontalCell(int col)
        {
            return new Cell(ParentCell.Row, col);
        }

        private void IncrementHorizontallyLeftCol(ref int col)
        {
            col += isWhite() ? -1 : 1;
        } 
        #endregion

        #region Horizontally Right
        protected List<Cell> GetHorizontallyRight()
        {
            List<Cell> possibleMoves = new List<Cell>();
            for (int col = OneColRight; IsHorizontallyRightColValid(col); IncrementHorizontallyRightCol(ref col))
            {
                Cell possibleMove = getPossibleHorizontalCell(col);
                possibleMoves.Add(possibleMove);
                if (canCapture(possibleMove))
                {
                    break;
                }
            }
            return possibleMoves;
        }

        private bool IsHorizontallyRightColValid(int col)
        {
            Cell possibleCell = getPossibleHorizontalCell(col);
            return isWhite()
                ? col < Board.NumOfCols && canMoveToCell(possibleCell)
                : col >= 0 && canMoveToCell(possibleCell);
        }

        private void IncrementHorizontallyRightCol(ref int col)
        {
            col += isWhite() ? 1 : -1;
        } 
        #endregion

        #region Diagonally Up Right
        protected List<Cell> GetDiagonallyUpRight()
        {
            List<Cell> possibleMoves = new List<Cell>();
            for (int row = OneRowForward, col = OneColRight;
                areRowColValidDiagUpRight(row, col);
                iterateRowColDiagUpRight(ref row, ref col))
            {
                Cell possibleCell = new Cell(row, col);
                possibleMoves.Add(possibleCell);
                if (canCapture(possibleCell))
                {
                    break;
                }
            }
            return possibleMoves;
        }

        private bool areRowColValidDiagUpRight(int row, int col)
        {
            return isDiagUpRightRowValid(row) &&
                isDiagUpRightColValid(col) &&
                canMoveToCell(new Cell(row, col));
        }

        private bool isDiagUpRightRowValid(int row)
        {
            return (isWhite() ? row >= 0 : row < Board.NumOfRows);
        }

        private bool isDiagUpRightColValid(int col)
        {
            return (isWhite() ? col < Board.NumOfCols : col >= 0);
        }

        private void iterateRowColDiagUpRight(ref int row, ref int col)
        {
            row += isWhite() ? -1 : 1;
            col += isWhite() ? 1 : -1;
        } 
        #endregion

        #region Diagonally Up Left
        protected List<Cell> GetDiagonallyUpLeft()
        {
            List<Cell> possibleMoves = new List<Cell>();
            for (int row = OneRowForward, col = OneColLeft;
                areRowColValidDiagUpLeft(row, col);
                iterateRowColDiagUpLeft(ref row, ref col))
            {
                Cell possibleCell = new Cell(row, col);
                possibleMoves.Add(possibleCell);
                if (canCapture(possibleCell))
                {
                    break;
                }
            }
            return possibleMoves;
        }

        private bool areRowColValidDiagUpLeft(int row, int col)
        {
            return isDiagUpLeftRowValid(row) &&
                isDiagUpLeftColValid(col) &&
                canMoveToCell(new Cell(row, col));
        }

        private bool isDiagUpLeftRowValid(int row)
        {
            return isWhite() ? row >= 0 : row < Board.NumOfRows;
        }

        private bool isDiagUpLeftColValid(int col)
        {
            return isWhite() ? col >= 0 : col < Board.NumOfCols;
        }

        private void iterateRowColDiagUpLeft(ref int row, ref int col)
        {
            row += isWhite() ? -1 : 1;
            col += isWhite() ? -1 : 1;
        } 
        #endregion

        #region Diagonally Down Left
        protected List<Cell> GetDiagonallyDownLeft()
        {
            List<Cell> possibleMoves = new List<Cell>();
            for (int row = OneRowDown, col = OneColLeft;
                areRowColValidDiagDownLeft(row, col);
                iterateRowColDiagDownLeft(ref row, ref col))
            {
                Cell possibleCell = new Cell(row, col);
                possibleMoves.Add(possibleCell);
                if (canCapture(possibleCell))
                {
                    break;
                }
            }
            return possibleMoves;
        }

        private bool areRowColValidDiagDownLeft(int row, int col)
        {
            return isDiagDownLeftRowValid(row) &&
                isDiagDownLeftColValid(col) &&
                canMoveToCell(new Cell(row, col));
        }

        private bool isDiagDownLeftRowValid(int row)
        {
            return isWhite() ? row < Board.NumOfRows : row >= 0;
        }

        private bool isDiagDownLeftColValid(int col)
        {
            return isWhite() ? col >= 0 : col < Board.NumOfCols;
        }

        private void iterateRowColDiagDownLeft(ref int row, ref int col)
        {
            row += isWhite() ? 1 : -1;
            col += isWhite() ? -1 : 1;
        } 
        #endregion

        #region Diagonally Down Right
        protected List<Cell> GetDiagonallyDownRight()
        {
            List<Cell> possibleMoves = new List<Cell>();
            for (int row = OneRowDown, col = OneColRight;
                areRowColValidDiagDownRight(row, col);
                iterateRowColDiagDownRight(ref row, ref col))
            {
                Cell possibleCell = new Cell(row, col);
                possibleMoves.Add(possibleCell);
                if (canCapture(possibleCell))
                {
                    break;
                }
            }
            return possibleMoves;
        }

        private bool areRowColValidDiagDownRight(int row, int col)
        {
            return isDiagDownRightRowValid(row)
                && isDiagDownRightColValid(col)
                && canMoveToCell(new Cell(row, col));
        }

        private bool isDiagDownRightRowValid(int row)
        {
            return isWhite() ? row < Board.NumOfRows : row >= 0;
        }

        private bool isDiagDownRightColValid(int col)
        {
            return isWhite() ? col < Board.NumOfCols : col >= 0;
        }

        private void iterateRowColDiagDownRight(ref int row, ref int col)
        {
            row += isWhite() ? 1 : -1;
            col += isWhite() ? 1 : -1;
        } 
        #endregion

        public bool VulnerableCheck { get; set; }

        public bool CanMoveTo(Cell cell)
        {
            return GetPossibleMoves().Contains(cell);                               
        }

        public List<Cell> GetPossibleMoves()
        {
            List<Cell> checkedMoves = new List<Cell>();
            foreach (Cell c in GetMoves())
            {
                if (!willBeInCheck(gameBoard[c]))
                {
                    checkedMoves.Add(c);
                }
            }
            return checkedMoves;
        }

        public virtual List<Cell> GetMoves()
        {
            return new List<Cell>();
        }

        public virtual void RegisterMove(Cell toCell)
        {
            AtStartPosition = false;
            ParentCell = gameBoard[toCell];
        }

        public virtual void CopyToBoard(Board gameBoard)
        {
            //By Default does nothing
        }
    }
}
