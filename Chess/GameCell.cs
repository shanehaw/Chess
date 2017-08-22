using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class GameCell: Cell
    {
        public Piece Piece
        {
            get;
            set;
        }

        public Color Colour
        {
            get;
            set;
        }

        public bool IsSelected
        {
            get;
            set;
        }

        public GameCell(int row, int col, Board gameBoard)
            : base(row, col)
        {
            Piece = new Piece(Piece.PlayerColour.None, this, gameBoard);            
            Colour = Color.Empty;            
        }
    }
}
