using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    [PawnPromotionCandidate]
    public class Rook: Piece
    {
        protected Cell CastlingCell
        {
            get
            {
                return new Cell(ParentCell.Row, ParentCell.Col - 2);
            }
        }

        public override char Type
        {
            get 
            {
                return Piece.Rook;
            }
        }

        protected List<getPossibleMoves> Moves
        {
            get
            {
                return new List<getPossibleMoves>()
                {
                    GetVerticallyUp,
                    GetVerticallyDown,
                    GetHorizontallyLeft,
                    GetHorizontallyRight,
                };
            }
        }

        public Rook(PlayerColour colour, Cell parentCell, Board gameBoard)
            : base(colour, parentCell, gameBoard)
        {
        }

        public override List<Cell> GetMoves()
        {
            List<Cell> possibleMoves = new List<Cell>();
            foreach (getPossibleMoves possibleMove in Moves)
            {
                possibleMoves.AddRange(possibleMove());
            }
            return possibleMoves;
        }

        public override string ToString()
        {
            return "rook";
        }

        public override void CopyToBoard(Board gameBoard)
        {
            gameBoard[ParentCell].Piece = new Rook(Colour, ParentCell, gameBoard)
            {
                AtStartPosition = this.AtStartPosition,
            };
        }

        public void PerformCastlingMovement()
        {
            gameBoard.MovePiece(ParentCell, CastlingCell);
        }        
    }
}
