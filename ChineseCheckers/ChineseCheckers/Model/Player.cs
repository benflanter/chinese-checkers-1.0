using ChineseCheckers.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace ChineseCheckers
{
    public class Player
    {
        /// <summary>
        /// a dictionary that holds all the pieces of a player
        /// </summary>
        protected Dictionary<int, Piece> pieces;
        /// <summary>
        /// the side of the player on the board (true = up & false = down)
        /// </summary>
        public bool side;
        /// <summary>
        /// stores the piece that we want to find the moves for
        /// </summary>
        private Piece scannedPiece;
        /// <summary>
        /// the game board
        /// </summary>
        protected Board board;
        /// <summary>
        /// the last row (from up to down) of the upper player base
        /// </summary>
        protected int destinationThreshold = 4;
        /// <summary>
        /// the first row (from up to down) of the upper player base
        /// </summary>
        protected int firstDestinationRow = 0;
        /// <summary>
        /// the first col of both player bases
        /// </summary>
        protected int firstCol = 9;
        /// <summary>
        /// the last col of both player bases
        /// </summary>
        protected int lastCol = 15;
        /// <summary>
        /// the first row (from up to down) of the lower player base
        /// </summary>
        private int firstOriginRow = Board.HEIGHT - 4;


        public Dictionary<int, Piece> Pieces { get => pieces; }

        /// <summary>
        /// the constructor that initializes all of the pieces of the player, based on it's side.
        /// </summary>
        /// <param name="side">the side of the player. true is up and flase is down</param>
        /// <param name="board">the game board</param>
        public Player(bool side, Board board)
        {
            this.board = board;
            this.side = side;
            pieces = new Dictionary<int, Piece>();
            if (side) // up side
            {
                for (int i = firstDestinationRow; i < destinationThreshold; i++)
                    for (int j = firstCol; j <= lastCol; j++)
                    {
                        if (Board.initmat[i, j] == 2)
                            Pieces.Add(i * Board.WIDTH + j, new Piece(i, j, side));
                    }
            }
            else // down side
            {
                for (int i = firstOriginRow; i < Board.HEIGHT; i++)
                    for (int j = firstCol; j <= lastCol; j++)
                    {
                        if (Board.initmat[i, j] == 3)
                            Pieces.Add(i * Board.WIDTH + j, new Piece(i, j, side));
                    }
            }
        }

        /// <summary>
        /// a method that draws all the pieces of a player
        /// </summary>
        /// <param name="graphics">a graphics object</param>
        public void Draw(Graphics graphics)
        {
            foreach (Piece piece in Pieces.Values)
                piece.Draw(graphics);
        }

        /// <summary>
        /// the method recieves a location on the board an if the player has a piece in that location it returns it, 
        /// otherwise it returns Null.
        /// </summary>
        /// <param name="row">the row of the piece</param>
        /// <param name="col">the col of the piece</param>
        /// <returns>if the player holds a piece in the desired location, the piece will be returned.
        /// if the player hasn't got a piece there, the function will return Null.</returns>
        public Piece getPiece(int row, int col)
        {
            int key = row * Board.WIDTH + col;
            if (!Pieces.ContainsKey(key))
                return null;
            return Pieces[key];
        }

        /// <summary>
        /// the method returns the key of piece with the highest row.
        /// </summary>
        /// <returns> an integer with the value of the key of piece with the highest row</returns>
        protected int GetRearMostPiece()
        {
            int key = 0, row = 0;
            foreach (KeyValuePair<int, Piece> piece in Pieces)
            {
                if (piece.Value.row > row)
                {
                    row = piece.Value.row;
                    key = piece.Key;
                }
            }
            return key;
        }

        /// <summary>
        /// the method recieves a key that represents a location on the board. if the player has a piece in 
        /// that location it returns it, otherwise it returns Null.
        /// </summary>
        /// <param name="row">the row of the piece</param>
        /// <param name="col">the col of the piece</param>
        /// <returns>if the player holds a piece that has the same key, that piece will be returned.
        /// if the player hasn't got a piece with the same key, the function will return Null.</returns>
        public Piece GetPieceFromKey(int key)
        {
            if (!Pieces.ContainsKey(key))
                return null;
            return Pieces[key];
        }

        /// <summary>
        /// removes a piece from the pieces dictionary
        /// </summary>
        /// <param name="piece">the piece to be removed</param>
        public void removePiece(Piece piece)
        {
            int key = piece.row * Board.WIDTH + piece.col;
            Pieces.Remove(key);
        }

        /// <summary>
        /// addes a piece to the pieces dictionary
        /// </summary>
        /// <param name="rowDest">the row of the new piece</param>
        /// <param name="colDest">the col of the new piece</param>
        /// <param name="side">the side of the new piece</param>
        public void addPiece(int rowDest, int colDest, bool side)
        {
            int key = rowDest * Board.WIDTH + colDest;
            Pieces.Add(key, new Piece(rowDest, colDest, side));
        }

        /// <summary>
        /// the method checks if the player won the game.
        /// </summary>
        /// <returns>a boolean: true if the player won, or false if he didn't</returns>
        public bool CheckPlayerWin()
        {
            int count = 0;
            foreach (KeyValuePair<int, Piece> piece in Pieces)
            {
                Piece p = piece.Value;
                if (this.side)
                {
                    if (Board.initmat[p.row, p.col] == 3)
                        count++;
                    else
                        return false;
                }
                else
                {
                    if (Board.initmat[p.row, p.col] == 2)
                        count++;
                    else
                        return false;
                }
            }
            return count == Pieces.Count;
        }

        /// <summary>
        /// the method returns the possible moves of a player.
        /// </summary>
        /// <returns>a list of moves that contain all the possible moves</returns>
        public List<Move> GetMoves()
        {
            List<Move> moves = new List<Move>();
            foreach (KeyValuePair<int, Piece> piece in Pieces)
            {
                moves.AddRange(GetMovesForPiece(piece.Value));

            }
            return moves;
        }

        /// <summary>
        /// the method finds all the possibkle moves of a given piece.
        /// </summary>
        /// <param name="piece">the piece that the moves belong to</param>
        /// <returns>a list of moves that contains all the possible moves of a given piece</returns>
        public List<Move> GetMovesForPiece(Piece piece)
        {
            List<Move> moves = GetNearMoves(piece);
            moves.AddRange(GetFarMoves(piece));
            return moves;
        }

        /// <summary>
        /// the method finds all the free spaces the surrounds a given piece.
        /// </summary>
        /// <param name="piece">the piece that the moves belong to</param>
        /// <returns>a list of moves that contain all the near moves of a given piece</returns>
        public List<Move> GetNearMoves(Piece piece)
        {
            List<Move> moves = new List<Move>();
            for (int i = 0; i < board.directions.Length / 2; i++)
            {
                int row = piece.row + board.directions[i, 0];
                int col = piece.col + board.directions[i, 1];
                if (row >= 0 && row < Board.HEIGHT && col >= 0 && col < Board.WIDTH)
                {
                    if (Board.initmat[row, col] != 0 && board.getPiece(row, col) == null)
                        moves.Add(new Move(piece, row, col));
                }
            }
            return moves;
        }

        /// <summary>
        /// the method finds all the possible moves a given piece can preform by hopping over other pieces.
        /// </summary>
        /// <param name="piece">the piece that the moves belong to</param>
        /// <returns>a list of moves that contains all the moves a given piece can preform by hopping</returns>
        private List<Move> GetFarMoves(Piece piece)
        {
            List<Move> moves = new List<Move>();
            scannedPiece = piece;
            board.clearHelpMat();
            ScanFarMoves(piece, moves);
            return moves;
        }

        /// <summary>
        /// the method recieves an empty list and adds recursivly all the places a given piece can hop to from the original place.
        /// </summary>
        /// <param name="Piece">he piece that the moves belong to</param>
        /// <param name="moves">an empty list of moves</param>
        private void ScanFarMoves(Piece Piece, List<Move> moves)
        {
            if (board.helpmat[Piece.row, Piece.col] == 1 || Board.initmat[Piece.row, Piece.col] == 0)
                return;
            board.helpmat[Piece.row, Piece.col] = 1;
            for (int i = 0; i < board.directions.Length / 2; i++)
            {
                int row = Piece.row + board.directions[i, 0];
                int col = Piece.col + board.directions[i, 1];
                if (Islegal(row, col) && board.getPiece(row, col) != null)
                {
                    int nextRow = row + board.directions[i, 0];
                    int nextCol = col + board.directions[i, 1];
                    if (Islegal(nextRow, nextCol) && board.getPiece(nextRow, nextCol) == null)
                    {
                        Piece nextPiece = new Piece(nextRow, nextCol, Piece.side);
                        moves.Add(new Move(scannedPiece, nextRow, nextCol));
                        ScanFarMoves(nextPiece, moves);
                    }
                }
            }
        }

        /// <summary>
        /// the method checks if a given location is legal, in terms of the board.
        /// </summary>
        /// <param name="row">the row of the location</param>
        /// <param name="col">the col of the location</param>
        /// <returns>a boolean: true if legal, false if not</returns>
        public static bool Islegal(int row, int col)
        {
            return row >= 0 && row < Board.HEIGHT && col >= 0 && col < Board.WIDTH && Board.initmat[row, col] != 0;
        }
    }
}