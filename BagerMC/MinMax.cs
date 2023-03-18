using BagerMC.DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagerMC
{
    public class MinMax
    {

        public int MiniMax(Game game, int depth, int alpha, int beta, bool maximizingPlayer)
        {
            //int P1Evaluation = game.Game.Player1.Score;
            //int P2Evaluation = game.Game.Player2.Score;
            if (depth == 0)
            {
                return 1;
            }
            if (maximizingPlayer)
            {
                int MaxEvaluation = -100000;
                foreach(var item in Test())
                {
                    int Evaluation = MiniMax(item, depth - 1, alpha, beta, false);
                    MaxEvaluation = Math.Max(MaxEvaluation, Evaluation);
                    alpha = Math.Max(alpha, Evaluation);
                    if(beta <= alpha)
                        break;
                }
                return MaxEvaluation;
            }
            else
            {
                int MinimumEvaluation = +100000;
                foreach(var item in Test())
                {
                    int Evaluation = MiniMax(item, depth - 1, alpha, beta, true);
                    MinimumEvaluation = Math.Min(MinimumEvaluation, Evaluation);
                    alpha = Math.Min(beta, Evaluation);
                    if (beta <= alpha)
                        break;
                }
                return MinimumEvaluation;
            }
        }

        public List<Game> Test()
        {
            List<Game> list = new List<Game>();
            return list;
        }
    }
}
