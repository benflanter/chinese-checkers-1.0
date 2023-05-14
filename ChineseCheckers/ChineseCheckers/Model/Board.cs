using ChineseCheckers.Model;
using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace ChineseCheckers
{
    public class Board
    {
        private int playerNum;
        public static int STARTX = 204;
        public static int STARTY = 72;
        public static int WIDTH = 25;
        public static int HEIGHT = 17;
        public Player player1, player2;
        public static byte[,] initmat =
        {
            { 0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,2,0,2,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,2,0,2,0,2,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,2,0,2,0,2,0,2,0,0,0,0,0,0,0,0,0 },
            { 1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1 },
            { 0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0 },
            { 0,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,0 },
            { 0,0,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,0,0 },
            { 0,0,0,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,0,0,0 },
            { 0,0,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,0,0 },
            { 0,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,0 },
            { 0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0 },
            { 1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1 },
            { 0,0,0,0,0,0,0,0,0,3,0,3,0,3,0,3,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,3,0,3,0,3,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,3,0,3,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0 },
        };
        public byte[,] helpmat =
        {
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }
        };

        public int[,] directions = { { -1, -1 }, { -1, 1 }, { 1, 1 }, { 1, -1 } };

        /// <summary>
        /// constructor of class board. it initializes the players depending on the number of players:
        /// if the number of players is 1, it will initialize 1 player and 1 computer player, 
        /// and if the number is 2, it will initialize 2 normal players
        /// </summary>
        /// <param name="totalNumberOfPlayers">the number of real players to be initialized</param>
        public Board(int totalNumberOfPlayers)
        {
            this.playerNum = totalNumberOfPlayers;
            if (totalNumberOfPlayers == 2)
            {
                player1 = new Player(true, this);
                player2 = new Player(false, this);
            }
            else if (totalNumberOfPlayers == 1)
            {
                player1 = new Player(true, this);
                player2 = new ComputerPlayer(false, this,player1);
            }
        }
        public void Draw(Graphics graphics)
        {
            player1.Draw(graphics);
            player2.Draw(graphics);
        }

        /// <summary>
        /// the method recieves a location on the board and checks to see if
        /// one of the players has a piece there.
        /// </summary>
        /// <param name="row">the row of the location</param>
        /// <param name="col">the col of the location</param>
        /// <returns>if one of the players has a piece in the given location it will be returnd, otherwise the method will return Null</returns>
        public Piece getPiece(int row, int col)
        {
            Piece piece = player2.getPiece(row, col);
            if (piece == null)
                piece = player1.getPiece(row, col);

            return piece;
        }

        /// <summary>
        /// the method recieves a key that represents a location on the board. if one of the players has a piece in 
        /// that location it returns it, otherwise it returns Null.
        /// </summary>
        /// <param name="row">the row of the piece</param>
        /// <param name="col">the col of the piece</param>
        /// <returns>one of the players holds a piece that has the same key, that piece will be returned.
        /// if one of the players hasn't got a piece with the same key, the function will return Null.</returns>
        public Piece GetPieceFromKey(int key)
        {
            Piece piece = player2.GetPieceFromKey(key);
            if (piece == null)
                piece = player1.GetPieceFromKey(key);

            return piece;
        }

        /// <summary>
        /// the method moves a piece from one location to the other, if the move is legal.
        /// </summary>
        /// <param name="piece">the piece that needs to be moved</param>
        /// <param name="rowDest">the destination row</param>
        /// <param name="colDest">the destination col</param>
        /// <returns>a player object that stores the player who made the move, 
        /// if the move is not valid the function will return null</returns>
        public Player Move(Piece piece, int rowDest, int colDest)
        {
            Player player = null;

            if (MoveAble(piece, rowDest, colDest))
            {
                player = piece.side ? player1 : player2;
                player.addPiece(rowDest, colDest, piece.side);
                player.removePiece(piece);
            }
            if (playerNum == 1)
            {
                (player2 as ComputerPlayer).RemoveDestination(rowDest, colDest);
                if (getPiece(piece.row, piece.col) == null && initmat[piece.row, piece.col] == 2)
                    (player2 as ComputerPlayer).AddDestination(piece.row, piece.col);
            }
            return player;
        }

        /// <summary>
        /// the method checks if agiven piece can move to the desired location by moving only one circle.
        /// </summary>
        /// <param name="piece">the piece</param>
        /// <param name="rowDest">the destination row</param>
        /// <param name="colDest">the destination col</param>
        /// <returns>a boolean: true is the action is possible, false if not</returns>
        private bool MoveValid(Piece piece, int rowDest, int colDest)
        {
            if (initmat[rowDest, colDest] != 0 && getPiece(rowDest, colDest) == null)
            {
                return (rowDest == piece.row + 1 || rowDest == piece.row - 1 || rowDest == piece.row) &&
                       (colDest == piece.col + 1 || colDest == piece.col - 1 || colDest == piece.col);
            }
            return false;
        }

        /// <summary>
        /// the method checks if a moves is legal
        /// </summary>
        /// <param name="piece">the piece to be moved</param>
        /// <param name="rowDest">the destination row</param>
        /// <param name="colDest">the destination col</param>
        /// <returns>a boolean: true if the move is legal, false if not</returns>
        public bool MoveAble(Piece piece, int rowDest, int colDest)
        {
            if (MoveValid(piece, rowDest, colDest))
                return true;
            else
                clearHelpMat();
            return HopValid(piece, rowDest, colDest);
        }

        /// <summary>
        /// the method clears the helpMat matrix from previous uses
        /// </summary>
        public void clearHelpMat()
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    helpmat[i, j] = 0;
                }
            }
        }

        /// <summary>
        /// the method checks if a given piece is legal, in terms of the game board.
        /// </summary>
        /// <param name="piece">the piece that will be checked</param>
        /// <returns>a boolean: true if legal, false if not</returns>
        public bool IsLegal(Piece piece)
        {
            return piece.row >= 0 && piece.row < HEIGHT && piece.col >= 0 && piece.col < WIDTH;
        }

        /// <summary>
        /// the method checks if a given piece can reach a destination location by hopping over other pieces.
        /// </summary>
        /// <param name="piece">the piece that will be checked</param>
        /// <param name="rowDest">the destination row</param>
        /// <param name="colDest">the destination col</param>
        /// <returns>a boolean: true if possible, false if not</returns>
        private bool HopValid(Piece piece, int rowDest, int colDest)
        {
            if (!IsLegal(piece) || initmat[piece.row, piece.col] == 0 || helpmat[piece.row, piece.col] == 1)
                return false;
            if (rowDest == piece.row && colDest == piece.col)
                return true;
            helpmat[piece.row, piece.col] = 1;
            for (int i = 0; i < directions.Length / 2; i++)
            {
                int rowN = piece.row + directions[i, 0];
                int colN = piece.col + directions[i, 1];
                int rowN2 = rowN + directions[i, 0];
                int colN2 = colN + directions[i, 1];
                if (getPiece(rowN, colN) != null)
                {
                    if (getPiece(rowN2, colN2) == null)
                    {
                        Piece pieceOther = new Piece(rowN2, colN2, piece.side);
                        if (HopValid(pieceOther, rowDest, colDest))
                            return true;
                    }
                }
            }
            return false;
        }
    }
}