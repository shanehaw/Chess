using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Cell
    {
        public int Row
        {
            get;
            private set;
        }

        public int Col
        {
            get;
            private set;
        }        

        public Cell(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public override bool Equals(object obj)
        {
            Cell other = obj as Cell;
            if (other != null)
            {
                return this.Row == other.Row &&
                    this.Col == other.Col;
            }
            return false;
        }

        public static bool operator ==(Cell left, Cell right)
        {
            if (Object.ReferenceEquals(left, null) && Object.ReferenceEquals(right, null))
            {
                return true;
            }
            else if (Object.ReferenceEquals(left, null))
            {
                return false;
            }
            else
            {
                return left.Equals(right);
            }
        }

        public static bool operator !=(Cell left, Cell right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return String.Format("{{{0}, {1}}}", Row, Col);
        }
    }
}
