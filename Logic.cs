using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test2
{
    public class Logic
    {
        public string CurrentPlayer { get; set; } = X;
        private const string X = "X";
        private const string O = "O";
        private string[,] Board = new string[3,3];
        
        public void SetNextPlayer()
        {
            if (CurrentPlayer == X)
            {
                CurrentPlayer = O;
            }
            else
            {
                CurrentPlayer = X;
            }
        }
        public bool PlayerWin()
        {
            //check row            
            for(var i=0;i<3;i++)
            {
                if (!String.IsNullOrWhiteSpace(Board[i, 0]))
                {
                    if (Board[i,0] == Board[i,1] && Board[i,0] == Board[i, 2])
                    {
                        return true;
                    }   
                }
            } 
            //Column
            for (var i = 0; i < 3; i++)
            {
                if (!String.IsNullOrWhiteSpace(Board[0, i]))
                {
                    if (Board[0, i] == Board[1, i] && Board[0, i] == Board[2, i])
                    {
                        return true;
                    }
                }
            }
            //diagonal

                if (!String.IsNullOrWhiteSpace(Board[1, 1]))
                {
                    if (Board[0, 0] == Board[1, 1] && Board[1, 1] == Board[2, 2])
                    {
                        return true;
                    }
                    if (Board[0, 2] == Board[1, 1] && Board[1, 1] == Board[2, 0])
                    {
                        return true;
                    }
                }
            
            return false;
        }
        public bool PlayerDraw()
        {

            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (String.IsNullOrWhiteSpace(Board[i, j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void UpdateBoard(Position position, string value) 
        {
            Board[position.x,position.y] = value;
        }

    }
}
