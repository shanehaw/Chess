using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    public class Pawn : Piece
    {
        public override char Type
        {
            get
            {
                return Piece.Pawn;
            }
        }        

        protected List<getPossibleMoves> PossibleMoves
        {
            get
            {
                return new List<getPossibleMoves>()
                {
                    CheckMoveOneRowForward,
                    CheckMove2RowsForward,
                    CheckMoveDiagonallyRight,
                    CheckMoveDiagonallyLeft,
                    CheckMoveEnPassantRight,
                    CheckMoveEnPassantLeft,
                };
            }
        }        

        protected bool isEnPassantVulnerable;

        public Pawn(PlayerColour colour, Cell parentCell, Board gameBoard)
            : base(colour, parentCell, gameBoard)
        {
            isEnPassantVulnerable = false;
        }        

        public override List<Cell> GetMoves()
        {
            List<Cell> possibleMoves = new List<Cell>();
            foreach (getPossibleMoves getMove in PossibleMoves)
            {
                possibleMoves.AddRange(getMove());
            }
            return possibleMoves;
        }

        private List<Cell> CheckMoveOneRowForward()
        {
            List<Cell> possibleMoves = new List<Cell>();
            if (canMoveOneRowForward())
            {
                possibleMoves.Add(OneCellForward);
            }
            return possibleMoves;
        }

        private bool canMoveOneRowForward()
        {
            return Board.InBounds(OneCellForward) && gameBoard.IsCellEmpty(OneCellForward);
        }              

        protected List<Cell> CheckMove2RowsForward()
        {
            List<Cell> possibleMoves = new List<Cell>();
            if (canMoveTwoRowsForward())
            {
                possibleMoves.Add(getCellTwoRowsAhead());
            }
            return possibleMoves;
        }

        private bool canMoveTwoRowsForward()
        {
            return AtStartPosition &&
                Board.InBounds(getCellTwoRowsAhead()) &&
                gameBoard.IsCellEmpty(OneCellForward) &&
                gameBoard.IsCellEmpty(getCellTwoRowsAhead());
        }

        private Cell getCellTwoRowsAhead()
        {
            return new Cell(TwoRowsForward, ParentCell.Col);
        }

        protected  List<Cell> CheckMoveDiagonallyRight()
        {        
            List<Cell> possibleMoves = new List<Cell>();
            if (canCapture(CellDiagonallyRight))
            {
                possibleMoves.Add(CellDiagonallyRight);
            }
            return possibleMoves;
        }

        protected override bool canCapture(Cell cell)
        {
            return Board.InBounds(cell) &&
                !gameBoard.IsCellEmpty(cell) &&
                isPieceOpposingColour(cell);
        }               

        protected List<Cell> CheckMoveDiagonallyLeft()
        {
            List<Cell> possibleMoves = new List<Cell>();
            if(canCapture(CellDiagonallyLeft))
            {
                possibleMoves.Add(CellDiagonallyLeft);
            }
            return possibleMoves;
        }

        protected List<Cell> CheckMoveEnPassantRight()
        {
            List<Cell> possibleMoves = new List<Cell>();
            if (canMoveEnPassantRight())
            {
                possibleMoves.Add(CellDiagonallyRight);
            }
            return possibleMoves;
        }

        protected bool canMoveEnPassantRight()
        {
            return canMoveEnPassantAgainstCell(CellOneColRight);
        }

        protected bool canMoveEnPassantAgainstCell(Cell cell)
        {
            return (Board.InBounds(cell) &&
                !isEmpty(cell) &&
                isPieceOpposingColour(cell) &&                
                gameBoard[cell].Piece is Pawn &&
                (gameBoard[cell].Piece as Pawn).isEnPassantVulnerable);
        }       

        protected List<Cell> CheckMoveEnPassantLeft()
        {
            List<Cell> possibleMoves = new List<Cell>();
            if (canMoveEnPassantLeft())
            {
                possibleMoves.Add(CellDiagonallyLeft);
            }
            return possibleMoves;
        }

        protected bool canMoveEnPassantLeft()
        {
            return canMoveEnPassantAgainstCell(CellOneColLeft);
        }               

        public override void RegisterMove(Cell toCell)
        {
            if (isEnPassantRightCell(toCell) && canMoveEnPassantRight())
            {
                gameBoard.EmptyCell(CellOneColRight);                    
            }
            else if (isEnPassantLeftCell(toCell) && canMoveEnPassantLeft())
            {
                gameBoard.EmptyCell(CellOneColLeft);
            }
            else if (isTwoRowsAheadCell(toCell))
            {
                isEnPassantVulnerable = true;
                setEnPassantEventHandler();
            }
            else if (Board.OnBoundary(toCell.Row))
            {
                setPawnPromotionEventHandler(toCell);
            }
            base.RegisterMove(toCell);
        }                                 

        private bool isEnPassantRightCell(Cell toCell)
        {
            return CellDiagonallyRight == toCell;
        }

        private bool isEnPassantLeftCell(Cell toCell)
        {
            return CellDiagonallyLeft == toCell;
        }

        private bool isTwoRowsAheadCell(Cell toCell)
        {
            return toCell == getCellTwoRowsAhead();
        }

        private void setEnPassantEventHandler()
        {
            EventHandler ev = null;
            if (isWhite())
            {
                ev = (sender, args) =>
                    {
                        isEnPassantVulnerable = false;
                        gameBoard.BlackPieceMoved -= ev;
                    };
                gameBoard.BlackPieceMoved += ev;
            }
            else
            {
                ev = (sender, args) =>
                    {
                        isEnPassantVulnerable = false;
                        gameBoard.WhitePieceMoved -= ev;
                    };
                gameBoard.WhitePieceMoved += ev;
            }
        }

        private void setPawnPromotionEventHandler(Cell toCell)
        {
            EventHandler ev = null;
            if (isWhite())
            {
                ev = (sender, args) =>
                    {
                        gameBoard.Replace<Queen>(toCell.Row, toCell.Col);
                        gameBoard.WhitePieceMoved -= ev;
                    };
                gameBoard.WhitePieceMoved += ev;
            }
            else
            {
                ev = (sender, args) =>
                    {
                        gameBoard.Replace<Queen>(toCell.Row, toCell.Col);
                        gameBoard.BlackPieceMoved -= ev;
                    };
                gameBoard.BlackPieceMoved += ev;
            }
        }     

        public override string ToString()
        {
            return "pawn";
        }

        public override void CopyToBoard(Board gameBoard)
        {            
            gameBoard[ParentCell].Piece = new Pawn(Colour, ParentCell, gameBoard)
            {
                AtStartPosition = this.AtStartPosition
            };
        }
    }
}
