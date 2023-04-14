using System.Drawing.Printing;
using System.Media;
using System.Text;

namespace A23_Ex05_Liad_206837247_Omer_206729873
{
    internal class SystemControl
    {
        public static void Run()
        {
            GameSettingsForm gameSettingsForm = new GameSettingsForm();
            gameSettingsForm.ShowDialog();
            if (gameSettingsForm.ClosedByPlayAgainstComputer != eClosedByPlayer.Exit)
            {
                bool isPlayAgain = true;
                int playerOneOverallScore = 0;
                int playerTwoOverallScore = 0;
                while (isPlayAgain)
                {
                    GameData gameData = initData(gameSettingsForm);
                    GameDataControl gameDataControl = new GameDataControl(gameData);
                    if (!gameDataControl.ClosedByExit)
                    {
                        if (gameDataControl.GameData.PlayerOneScore > gameDataControl.GameData.PlayerTwoScore)
                        {
                            playerOneOverallScore++;
                        }
                        else if (gameDataControl.GameData.PlayerOneScore < gameDataControl.GameData.PlayerTwoScore)
                        {
                            playerTwoOverallScore++;
                        }

                        if (!IsPlayAgain(gameDataControl.GameData, playerOneOverallScore, playerTwoOverallScore))
                        {
                            isPlayAgain = false;
                        }
                    }
                    else
                    {
                        isPlayAgain = false;
                    }
                }
            }
        }

        private static bool IsPlayAgain(GameData i_GameData, int i_PlayerOneOverallScore, int i_PlayerTwoOverallScore)
        {
            bool isPlayAgain = false;
            StringBuilder scoreBoard = new StringBuilder();
            if (i_GameData.PlayerOneScore > i_GameData.PlayerTwoScore)
            {
                scoreBoard.AppendLine(string.Format("Red Won!! ({0}/{1}) ({2}/{3})", i_GameData.PlayerOneScore, i_GameData.PlayerTwoScore, i_PlayerOneOverallScore, i_PlayerTwoOverallScore));
            }
            else if (i_GameData.PlayerOneScore < i_GameData.PlayerTwoScore)
            {
                scoreBoard.AppendLine(string.Format("Yellow Won!! ({0}/{1}) ({2}/{3})", i_GameData.PlayerTwoScore, i_GameData.PlayerOneScore, i_PlayerTwoOverallScore, i_PlayerOneOverallScore));
            }
            else
            {
                scoreBoard.AppendLine(string.Format("It's a tie!! ({0}/{1}) ({2}/{3})", i_GameData.PlayerOneScore, i_GameData.PlayerTwoScore, i_PlayerOneOverallScore, i_PlayerTwoOverallScore));
            }

            scoreBoard.AppendLine("Would you like another round?");

            if (MessageBox.Show(scoreBoard.ToString(), "Othello", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                isPlayAgain = true;
            }

            return isPlayAgain;
        }

        private static GameData initData(GameSettingsForm i_GameSettingsForm)
        {
            GameData gameData;
            if (i_GameSettingsForm.ClosedByPlayAgainstComputer == eClosedByPlayer.PlayerAndComputer)
            {
                gameData = new GameData(i_GameSettingsForm.BoardSize, true);
            }
            else
            {
                gameData = new GameData(i_GameSettingsForm.BoardSize, false);
            }

            return gameData;
        }
    }
}
