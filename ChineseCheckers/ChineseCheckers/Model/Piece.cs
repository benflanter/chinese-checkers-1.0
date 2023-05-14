using System;
using System.Drawing;

namespace ChineseCheckers
{
    public class Piece
    {   
        public int row, col;
        public bool side;
        public const int X_STEP = 27;
        public const int Y_STEP = 45;
        public const int PieceSize = 55;
        public bool empty;

        /// <summary>
        ///  the constructor that initializes the piece and it's location on the board.
        /// </summary>
        /// <param name="row">the row of the (row,col) location</param>
        /// <param name="col">the col of the (row,col) location</param>
        /// <param name="side">the side of the piece: true is up and false is down</param>
        public Piece(int row, int col, bool side)
        {
            this.row = row;
            this.col = col;
            this.side = side;
            this.empty = false;
        }

        /// <summary>
        /// the constructor for an empty piece.
        /// </summary>
        /// <param name="row">the row of the (row,col) location</param>
        /// <param name="col">the col of the (row,col) location</param>
        public Piece(int row, int col)
        {
            this.row = row;
            this.col = col;
            this.side = false;
            this.empty = true;
        }

        /// <summary>
        /// a method that draws the piece on the board.
        /// </summary>
        /// <param name="graphics">graphics object</param>
        public void Draw(Graphics graphics)
        {
            Image img = side ? Properties.Resources.Black : Properties.Resources.Red;
            graphics.DrawImage(img, col * X_STEP + Board.STARTX - 10, row * Y_STEP + Board.STARTY,
                                         PieceSize + 6, PieceSize);

        }
    }
}