﻿using BagerMC.DTO.Action;
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
            Player player1;
            Player player2;
            if (GameAPI.IsFirstPlayer)
            {
                player1 = state.Player1;
                player2 = state.Player2;
            }
            else
            {
                player1 = state.Player2;
                player2 = state.Player1;
            }
            int score = 0;
            score += player1.Score;
            if (player1.Nectar >= 50 && player1.X == player1.HiveX && player1.Y == player1.HiveY)
                score += 1000;
            return score;
        }
    }
}
