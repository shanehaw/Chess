using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    [PawnPromotionCandidate]
    public class Bishop: Piece
    {
        public override char Type
        {
            get 
            {
                return Piece.Bishop;
            }
        }        

        protected List<getPossibleMoves> PossibleMoves
        {
            get
            {
                return new List<getPossibleMoves>()
                {
                    GetDiagonallyUpRight,
                    GetDiagonallyUpLeft,
                    GetDiagonallyDownLeft,
                    GetDiagonallyDownRight,
                };
            }
        }

        public Bishop(PlayerColour colour, GameCell parentCell, Board gameBoard)
            : base(colour, parentCell, gameBoard)
        {
        }

        public override List<Cell> GetMoves()
        {
            List<Cell> possibleMoves = new List<Cell>();
            foreach (getPossibleMoves possibleMove in PossibleMoves)
            {
                possibleMoves.AddRange(possibleMove());
            }
            return possibleMoves;
        }

        public override string ToString()
        {
            return "bishop";
        }

        public override void CopyToBoard(Board gameBoard)
        {
            gameBoard.Place<Bishop>(Colour, ParentCell.Row, ParentCell.Col);
        }
    }
}
