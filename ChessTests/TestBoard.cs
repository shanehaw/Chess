using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess;
using Chess.Pieces;

namespace ChessTests
{
    public static class TestBoard
    {
        public static Board createBoard()
        {
            Board b = new Board();
            return b;
        }

        public static Board createAndSetupBoard()
        {
            Board b = createBoard();
            b.Setup();
            return b;
        }

        /// <summary>
        /// [TestMethod]        
        //  public void canSimStuckBoard()
        //  {
        //      Board b = TestBoard.parse(
        //          ". R B Q I B K R", 
        //          "P P p P P P P P",
        //          "K . . . . . . .",
        //          "p . . . . . . .",
        //          ". . . . . . . .",
        //          ". . . . . . . .",
        //          ". . p p p p p p",
        //          "r k b q i b k r"
        //      );
        //      b.Simulate(Piece.PlayerColour.White);
        //  }
        /// </summary>
        /// <param name="boardString"></param>
        /// <returns></returns>
        public static Board parse(params string[] boardString)
        {
            Board b = new Board();            
            int i = 0;
            foreach (string line in boardString)
            {
                string[] cells = line.Trim().Split(' ');
                int j = 0;
                foreach (string cell in cells)
                {
                    switch (cell)
                    {
                        case "R":
                            b[i, j].Piece = new Rook(Piece.PlayerColour.Black, b[i, j], b);
                            break;
                        case "r":
                            b[i, j].Piece = new Rook(Piece.PlayerColour.White, b[i, j], b);
                            break;
                        case "K":
                            b[i, j].Piece = new Knight(Piece.PlayerColour.Black, b[i, j], b);
                            break;
                        case "k":
                            b[i, j].Piece = new Knight(Piece.PlayerColour.White, b[i, j], b);
                            break;
                        case "B":
                            b[i, j].Piece = new Bishop(Piece.PlayerColour.Black, b[i, j], b);
                            break;
                        case "b":
                            b[i, j].Piece = new Bishop(Piece.PlayerColour.White, b[i, j], b);
                            break;
                        case "Q":
                            b[i, j].Piece = new Queen(Piece.PlayerColour.Black, b[i, j], b);
                            break;
                        case "q":
                            b[i, j].Piece = new Queen(Piece.PlayerColour.White, b[i, j], b);
                            break;
                        case "I":
                            b[i, j].Piece = new King(Piece.PlayerColour.Black, b[i, j], b)
                            {
                                AtStartPosition = false,
                            };
                            break;
                        case "i":
                            b[i, j].Piece = new King(Piece.PlayerColour.White, b[i, j], b);
                            break;
                        case "P":
                            b[i, j].Piece = new Pawn(Piece.PlayerColour.Black, b[i, j], b)
                            {
                                AtStartPosition = false,
                            };
                            break;
                        case "p":
                            b[i, j].Piece = new Pawn(Piece.PlayerColour.White, b[i, j], b)
                            {
                                AtStartPosition = false,
                            };
                            break;
                        default:
                            b[i, j].Piece = new Piece(Piece.PlayerColour.None, b[i, j], b);
                            break;
                    }
                    j++;
                }
                i++;
            }
            return b;
        }
    }
}
