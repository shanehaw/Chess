using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    public class King: Piece
    {
        public override char Type
        {
            get 
            {
                return Piece.King;
            }
        }

        protected Cell OneCellDown
        {
            get
            {
                return new Cell(OneRowDown, ParentCell.Col);
            }
        }

        protected Cell OneCellRight
        {
            get
            {
                return new Cell(ParentCell.Row, OneColRight);
            }
        }

        protected Cell OneCellDownRight
        {
            get
            {
                return new Cell(OneRowDown, OneColRight);
            }
        }

        protected Cell OneCellDownLeft
        {
            get
            {
                return new Cell(OneRowDown, OneColLeft);
            }
        }

        protected Cell OneCellLeft
        {
            get
            {
                return new Cell(ParentCell.Row, OneColLeft);
            }
        }

        protected Cell OneCellForwardLeft
        {
            get
            {
                return new Cell(OneRowForward, OneColLeft);
            }
        }

        protected Cell CellTwoColsRight
        {
            get
            {
                return new Cell(ParentCell.Row, ParentCell.Col + 2);
            }
        }

        protected Cell CastleRookCell
        {
            get
            {
                return isWhite() ? new Cell(7, 7) : new Cell(0, 7);
            }
        }

        protected List<getPossibleMoves> Moves
        {
            get
            {
                return new List<getPossibleMoves>()
                {
                    getMovingForward,
                    getMovingDown,
                    getMovingForwardRight,
                    getMovingRight,
                    getMovingDownRight,
                    getMovingDownLeft,
                    getMovingLeft,
                    getMovingForwardLeft,
                    getCastling
                };
            }
        }

        public bool InCheck
        {
            get
            {
                return gameBoard.isCellVulnerable(ParentCell, OpposingColour);
            }
        }        

        public bool InCheckMate
        {
            get
            {
                bool isCheckMate = false;
                if (InCheck)
                {
                    isCheckMate = true;
                    foreach (Board b in gameBoard.Simulate(Colour))
                    {
                        King k = b.GetFirstPiece<King>(Colour);
                        if (!k.InCheck)
                        {
                            isCheckMate = false;
                            break;
                        }
                    }
                    return isCheckMate;
                }
                return false;                
            }
        }        

        public King(PlayerColour colour, Cell parentCell, Board gameBoard)
            : base(colour, parentCell, gameBoard)
        {            
        }

        protected override bool canMoveToCell(Cell possibleCell)
        {
            return base.canMoveToCell(possibleCell) && !willBeInCheck(possibleCell);
        }

        protected bool willBeInCheck(Cell possibleCell)
        {
            if (!VulnerableCheck)
            {
                return gameBoard.isCellVulnerableAfterMove(gameBoard[ParentCell], gameBoard[possibleCell], OpposingColour);
            }
            return false;
        }

        public override List<Cell> GetMoves()
        {
            List<Cell> possibleMoves = new List<Cell>();
            string boardString = this.gameBoard.BoardString;
            Piece.PlayerColour colour = this.Colour;
            foreach (getPossibleMoves getMove in Moves)
            {                
                possibleMoves.AddRange(getMove());
                boardString = this.gameBoard.BoardString;
            }
            return possibleMoves;            
        }        

        protected List<Cell> getMovingForward()
        {                        
            List<Cell> possibleMoves = new List<Cell>();
            if (canMoveToCell(OneCellForward))
            {
                possibleMoves.Add(OneCellForward);
            }
            return possibleMoves;
        }

        protected List<Cell> getMovingDown()
        {            
            List<Cell> possibleMoves = new List<Cell>();
            if (canMoveToCell(OneCellDown))
            {
                possibleMoves.Add(OneCellDown);
            }
            return possibleMoves;
        }

        protected List<Cell> getMovingForwardRight()
        {
            List<Cell> possibleMoves = new List<Cell>();
            if (canMoveToCell(CellDiagonallyRight))
            {
                possibleMoves.Add(CellDiagonallyRight);
            }
            return possibleMoves;
        }

        protected List<Cell> getMovingRight()
        {
            List<Cell> possibleMoves = new List<Cell>();
            if (canMoveToCell(OneCellRight))
            {
                possibleMoves.Add(OneCellRight);
            }
            return possibleMoves;
        }

        protected List<Cell> getMovingDownRight()
        {
            List<Cell> possibleMoves = new List<Cell>();
            if (canMoveToCell(OneCellDownRight))
            {
                possibleMoves.Add(OneCellDownRight);
            }
            return possibleMoves;
        }

        protected List<Cell> getMovingDownLeft()
        {            
            List<Cell> possibleMoves = new List<Cell>();
            if (canMoveToCell(OneCellDownLeft))
            {
                possibleMoves.Add(OneCellDownLeft);
            }
            return possibleMoves;            
        }

        protected List<Cell> getMovingLeft()
        {
            List<Cell> possibleMoves = new List<Cell>();
            if (canMoveToCell(OneCellLeft))
            {
                possibleMoves.Add(OneCellLeft);
            }
            return possibleMoves;
        }

        protected List<Cell> getMovingForwardLeft()
        {
            List<Cell> possibleMoves = new List<Cell>();
            if (canMoveToCell(OneCellForwardLeft))
            {
                possibleMoves.Add(OneCellForwardLeft);
            }
            return possibleMoves;
        }

        protected List<Cell> getCastling()
        {
            if (canCastle())
            {
                return new List<Cell>()
                {
                    CellTwoColsRight
                };
            }            
            return new List<Cell>();
        }

        private bool canCastle()
        {
            Rook castle = findCastleRook();
            return castle != null &&
                castle.AtStartPosition &&
                AtStartPosition &&
                isEmpty(CellTwoColsRight) &&
                isEmpty(CellOneColRight);
        }

        protected Rook findCastleRook()
        {            
            foreach (Rook possibleRook in gameBoard.EnumeratePieceType<Rook>(Colour))
            {
                if (possibleRook.ParentCell == CastleRookCell)
                {
                    return possibleRook;
                }                
            }
            return null;
        }

        public override void CopyToBoard(Board gameBoard)
        {
            gameBoard[ParentCell].Piece = new King(Colour, ParentCell, gameBoard)
            {
                AtStartPosition = this.AtStartPosition,
            };            
        }

        public override void RegisterMove(Cell toCell)
        {
            if (isCastlingMovement(toCell))
            {
                Rook castle = findCastleRook();                
                castle.PerformCastlingMovement();                
            }
            base.RegisterMove(toCell);
        }

        private bool isCastlingMovement(Cell toCell)
        {
            return toCell == CellTwoColsRight;
        }
    }
}
