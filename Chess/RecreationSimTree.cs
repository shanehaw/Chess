using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess
{
    public class RecreationSimTree
    {
        public class SimNode
        {
            public string Move { get; set; }
            public int MoveNumber { get; set; }
            public List<SimNode> NextMoves { get; set; }

            public SimNode(string move, int moveNumber)
            {
                this.Move = move;
                this.MoveNumber = moveNumber;
                this.NextMoves = new List<SimNode>();
            }
        }

        protected Board gameBoard;

        public SimNode Base { get; set; }

        public RecreationSimTree(Board board)
        {
            gameBoard = board;
            Base = new SimNode("", 0);
        }

        public int CountPossibilitiesAtNumMovesAhead(int numMovesAhead)
        {
            return GetPossibilities(Base, numMovesAhead);
        }

        private int GetPossibilities(SimNode node, int numMovesAhead)
        {
            if (node.MoveNumber == numMovesAhead)
            {
                return 1;
            }
            else
            {
                int num = 0;
                foreach (SimNode childNode in node.NextMoves)
                {
                    num += GetPossibilities(childNode, numMovesAhead);
                }
                return num;
            }
        }

        public IEnumerable<Board> EnumerateBoards(int numMovesAhead)
        {
            Board newBoard = gameBoard.Clone() as Board;
            foreach (Board b in EnumerateBoardsFromNode(Base, newBoard, numMovesAhead))
            {
                yield return b;                
            }
        }

        private IEnumerable<Board> EnumerateBoardsFromNode(SimNode node, Board b, int numMovesAhead)
        {
            if (!String.IsNullOrEmpty(node.Move))
            {
                node.Move = node.Move.Replace("{", "");
                node.Move = node.Move.Replace("}", "");
                string[] nums = node.Move.Split(':');
                string[] fromCellParts = nums[0].Split(',');
                string[] toCellParts = nums[1].Split(',');
                foreach (string part in fromCellParts)
                {
                    part.Trim();
                }
                foreach (string part in toCellParts)
                {
                    part.Trim();
                }
                Cell fromCell = new Cell(int.Parse(fromCellParts[0]), int.Parse(fromCellParts[1]));
                Cell toCell = new Cell(int.Parse(toCellParts[0]), int.Parse(toCellParts[1]));
                b.MovePiece(fromCell, toCell);
            }

            if (node.MoveNumber == numMovesAhead)
            {                
                yield return b;
            }
            else
            {
                foreach (SimNode childNode in node.NextMoves)
                {
                    Board newBoard = b.Clone() as Board;
                    foreach (Board childBoard in EnumerateBoardsFromNode(childNode, newBoard, numMovesAhead))
                    {
                        yield return childBoard;
                    }
                }
            }
        }
    }
}
