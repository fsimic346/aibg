using BagerMC.DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagerMC
{
    public static class MinMax
    {
        public static GameState MiniMax(GameState currentState, int depth, int alpha, int beta, bool maximizingPlayer)
        {
            if (depth == 0 || Simulator.IsGameFinished(currentState.State))
            {
                currentState.Evaluation = EvaluateState(currentState.State);
                return currentState;
            }
            if (maximizingPlayer)
            {
                GameState MaxEvaluatedState = new GameState();
                MaxEvaluatedState.Evaluation = -100000;
                foreach (var item in Simulator.GetPossibleStates(currentState.State, maximizingPlayer))
                {
                    GameState childState = MiniMax(item, depth - 1, alpha, beta, false);
                    if (childState.Evaluation > MaxEvaluatedState.Evaluation)
                    {
                        MaxEvaluatedState.Evaluation = childState.Evaluation;
                        MaxEvaluatedState.Action = item.Action;
                    }
                    alpha = Math.Max(alpha, childState.Evaluation);
                    if (beta <= alpha)
                        break;
                }
                return MaxEvaluatedState;
            }
            else
            {
                GameState MinEvaluatedState = new GameState();
                MinEvaluatedState.Evaluation = 100000;
                foreach (var item in Simulator.GetPossibleStates(currentState.State, maximizingPlayer))
                {
                    GameState childState = MiniMax(item, depth - 1, alpha, beta, true);
                    if (childState.Evaluation < MinEvaluatedState.Evaluation)
                    {
                        MinEvaluatedState.Evaluation = childState.Evaluation;
                        MinEvaluatedState.Action = item.Action;
                    }
                    beta = Math.Min(alpha, childState.Evaluation);
                    if (beta <= alpha)
                        break;
                }
                return MinEvaluatedState;
            }
        }

        private static int EvaluateState(Game state)
        {
            int score = 0;
            score += state.Player1.Score - state.Player2.Score;
            if (state.Player1.Nectar >= 50 && state.Player1.X == state.Player1.HiveX && state.Player1.Y == state.Player1.HiveY)
                score += 1000;
            return score;
        }
    }
}
