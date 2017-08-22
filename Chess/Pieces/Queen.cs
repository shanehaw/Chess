using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    [PawnPromotionCandidate]
    public class Queen: Piece
    {
        public override char Type
        {
            get 
            {
                return Piece.Queen;
            }
        }

        protected List<getPossibleMoves> GetPossibleMoveFunctions
        {
            get
            {
                return new List<getPossibleMoves>()
                {
                    GetVerticallyUp,
                    GetVerticallyDown,
                    GetHorizontallyLeft,
                    GetHorizontallyRight,
                    GetDiagonallyUpRight,
                    GetDiagonallyDownRight,
                    GetDiagonallyDownLeft,
                    GetDiagonallyUpLeft,
                };
            }
        }

        public Queen(PlayerColour colour, Cell parentCell, Board gameBoard)
            : base(colour, parentCell, gameBoard)
        {
        }

        public override List<Cell> GetMoves()
        {
            List<Cell> possibleMoves = new List<Cell>();
            foreach (getPossibleMoves possibleMove in GetPossibleMoveFunctions)
            {
                possibleMoves.AddRange(possibleMove());
            }
            return possibleMoves;
        }

        public override string ToString()
        {
            return "queen";
        }

        public override void CopyToBoard(Board gameBoard)
        {
            gameBoard.Place<Queen>(Colour, ParentCell.Row, ParentCell.Col);
        }
    }
}
