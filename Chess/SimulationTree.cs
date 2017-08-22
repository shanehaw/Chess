using System.Collections.Generic;

namespace Chess
{
    public class SimulationTree
    {
        public class SimNode
        {
            public Board Board { get; set; }
            public int NumOfMoves { get; set; }
            public List<SimNode> NextMoves { get; set; }
            public Cell FromCell { get; set; }
            public Cell ToCell { get; set; }

            public SimNode(Board board, int numOfMoves)
            {
                this.Board = board;
                this.NumOfMoves = numOfMoves;
                this.NextMoves = new List<SimNode>();
            }

            public SimNode(int numOfMoves, Cell fromCell, Cell toCell)
            {
                this.NumOfMoves = numOfMoves;
                this.FromCell = fromCell;
                this.ToCell = toCell;
            }
        }

        public SimNode Base { get; set; }

        public List<Board> GetPossibilitesAtNumMovesAhead(int numMovesAhead)
        {
            return GetBoardsAtNumMoves(Base, numMovesAhead);
        }        

        private List<Board> GetBoardsAtNumMoves(SimNode node, int numMovesAhead)
        {
            List<Board> boards = new List<Board>();
            if (node.NumOfMoves == numMovesAhead)
            {
                boards.Add(node.Board);
            }
            else
            {
                foreach (SimNode nextMoveNode in node.NextMoves)
                {
                    boards.AddRange(GetBoardsAtNumMoves(nextMoveNode, numMovesAhead));
                }
            }
            return boards;
        }
    }
}
