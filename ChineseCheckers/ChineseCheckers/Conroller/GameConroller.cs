using ChineseCheckers.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChineseCheckers
{
    class GameConroller
    {
        public Board board;
        public Player turn;
        private Piece piece_choose;
        private GameForm gameForm;
        public Player playerwin;

        /// <summary>
        /// the constructor that initializes the game board and thr turns.
        /// </summary>
        /// <param name="gameForm">the form where the game will be displayed</param>
        /// <param name="playernum">the number of players in the game</param>
        public GameConroller(GameForm gameForm, int playernum)
        {
            board = new Board(playernum);
            this.gameForm = gameForm;
            this.turn = board.player1;
            this.playerwin = null;
            gameForm.labelTurn.ForeColor = Color.Black;

        }

        /// <summary>
        /// the method checks if a given location is in the board's boundaries
        /// </summary>
        /// <param name="row">the location's row</param>
        /// <param name="col">the location's col</param>
        /// <returns>a boolean: true if legal, otherwise false</returns>
        private bool Islegal(int row, int col)
        {
            return row >= 0 && row < Board.HEIGHT && col >= 0 && col < Board.WIDTH;
        }

        /// <summary>
        /// a method that is responsible for controlling the player's clicks.
        /// </summary>
        /// <param name="row">the row of the click</param>
        /// <param name="col">the col if the click</param>
        public void Click(int row, int col)
        {
            if (!Islegal(row, col)) return;
            if (Board.initmat[row, col] == 0)
                col--;
            Piece piece = board.getPiece(row, col);
            if (piece != null)
                piece_choose = piece;
            else
            {
                if (piece_choose != null && piece_choose.side == turn.side && Board.initmat[row, col] != 0)
                {
                    Player player = board.Move(piece_choose, row, col);
                    if (player != null)
                    {  // Move is valid
                        turn = (turn == board.player1 ? board.player2 : board.player1);
                        gameForm.labelTurn.ForeColor = turn == board.player1 ? Color.Black : Color.Red;
                        if (player.CheckPlayerWin())
                            playerwin = player;
                        else
                        {
                            piece_choose = null;
                            if (board.player2 is ComputerPlayer)
                            {
                                turn = board.player2;
                                (board.player2 as ComputerPlayer).MakeMove();
                                if (board.player2.CheckPlayerWin())
                                    playerwin = board.player2;
                                else
                                    turn = board.player1;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// a method that shows a message for the player who won the game
        /// </summary>
        public void game_over()
        {
            if (playerwin != null)
            {
                // Game over
                this.gameForm.timer1.Enabled = false;
                string mes = playerwin.side ? "Gray player won" : "Red player won";
                MessageBox.Show(mes, "Game over", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.gameForm.Close();
            }
        }

        /// <summary>
        /// a method that draws the game board
        /// </summary>
        /// <param name="graphics">a graphics object</param>
        public void Draw(Graphics graphics)
        {
            board.Draw(graphics);
            if (piece_choose != null && piece_choose.side == turn.side)
            {
                graphics.DrawEllipse(new Pen(Color.Green, 5), piece_choose.col * Piece.X_STEP + Board.STARTX - 10,
                                                             piece_choose.row * Piece.Y_STEP + Board.STARTY,
                                                             Piece.PieceSize + 6, Piece.PieceSize);


                List<Move> moves = turn.GetMovesForPiece(piece_choose);
                foreach (var move in moves)
                {
                    int i = move.GetRow(), j = move.GetCol();
                    graphics.DrawEllipse(new Pen(Color.Yellow, 5), j * Piece.X_STEP + Board.STARTX - 10,
                                                                    i * Piece.Y_STEP + Board.STARTY,
                                                                     Piece.PieceSize + 6, Piece.PieceSize);

                }
            }
        }
    }
}