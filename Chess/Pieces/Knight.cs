using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    [PawnPromotionCandidate]
    public class Knight: Piece
    {
        public override char Type
        {
            get 
            {
                return Piece.Knight;
            }
        }

        private Cell TopRightCell
        {
            get
            {
                return new Cell(TwoRowsForward, OneColRight);
            }
        }

        private Cell RightTopCell
        {
            get
            {
                return new Cell(OneRowForward, TwoColsRight);
            }
        }

        private Cell RightDownCell
        {
            get
            {
                return new Cell(OneRowDown, TwoColsRight);
            }
        }

        private Cell DownRightCell
        {
            get
            {
                return new Cell(TwoRowsDown, OneColRight);
            }
        }

        private Cell DownLeftCell
        {
            get
            {
                return new Cell(TwoRowsDown, OneColLeft);
            }
        }

        private Cell LeftDownCell
        {
            get
            {
                return new Cell(OneRowDown, TwoColsLeft);
            }
        }

        private Cell LeftUpCell
        {
            get
            {
                return new Cell(OneRowForward, TwoColsLeft);
            }
        }

        private Cell UpLeftCell
        {
            get
            {
                return new Cell(TwoRowsForward, OneColLeft);
            }
        }

        public List<Cell> Moves
        {
            get
            {
                return new List<Cell>()
                {
                    TopRightCell,
                    RightTopCell,
                    RightDownCell,
                    DownRightCell,
                    DownLeftCell,
                    LeftDownCell,
                    LeftUpCell,
                    UpLeftCell,
                };
            }
        }

        protected override bool canMoveToCell(Cell possibleCell)
        {
            return base.canMoveToCell(possibleCell);
        }

        public Knight(PlayerColour colour, Cell parentCell, Board gameBoard)
            : base(colour, parentCell, gameBoard)
        {
        }        

        public override List<Cell> GetMoves()
        {
            List<Cell> possibleMoves = new List<Cell>();
            foreach (Cell cell in Moves)
            {
                if (canMoveToCell(cell))
                {
                    possibleMoves.Add(cell);
                }
            }            
            return possibleMoves;
        }        

        public override string ToString()
        {
            return "knight";
        }

        public override void CopyToBoard(Board gameBoard)
        {
            gameBoard.Place<Knight>(Colour, ParentCell.Row, ParentCell.Col);
        }        
    }
}
