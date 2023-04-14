using System.Data.Common;

namespace A23_Ex05_Liad_206837247_Omer_206729873

{
    internal class GameDataControl
    {
        private GameData m_GameData;

        public GameData GameData
        {
            get { return this.m_GameData; }
        }

        private bool m_ClosedByExit = true;

        public bool ClosedByExit
        {
            get { return this.m_ClosedByExit; }
        }

        private bool m_WasExistingMovesChecked = false;
        private BoardForm m_BoardForm;

        public GameDataControl(GameData i_GameData)
        {
            this.m_GameData = i_GameData;
            this.amountOfPotentialMoves();
            this.m_BoardForm = new BoardForm(i_GameData.BoardSize, this.m_GameData);
            this.m_BoardForm.m_ReportBoardChangedDelegates += this.ReportBoardChanged;
            this.m_BoardForm.ShowDialog();
        }

        private GameData ReportBoardChanged(int i_Row, int i_Column)
        {
            this.EnterMoveToData(i_Row, i_Column);
            return this.m_GameData;
        }

        private void EnterMoveToData(int i_Row, int i_Column)
        {
            if (this.m_GameData.IsPlayerOneTurn)
            {
                this.m_GameData.SquareStatusMatrix[i_Row, i_Column] = eSquareStatuses.PlayerOne;
                this.convertLeft(i_Row, i_Column, eSquareStatuses.PlayerOne, eSquareStatuses.PlayerTwo);
                this.convertRight(i_Row, i_Column, eSquareStatuses.PlayerOne, eSquareStatuses.PlayerTwo);
                this.convertUp(i_Row, i_Column, eSquareStatuses.PlayerOne, eSquareStatuses.PlayerTwo);
                this.convertDown(i_Row, i_Column, eSquareStatuses.PlayerOne, eSquareStatuses.PlayerTwo);
            }
            else
            {
                this.m_GameData.SquareStatusMatrix[i_Row, i_Column] = eSquareStatuses.PlayerTwo;
                this.convertLeft(i_Row, i_Column, eSquareStatuses.PlayerTwo, eSquareStatuses.PlayerOne);
                this.convertRight(i_Row, i_Column, eSquareStatuses.PlayerTwo, eSquareStatuses.PlayerOne);
                this.convertUp(i_Row, i_Column, eSquareStatuses.PlayerTwo, eSquareStatuses.PlayerOne);
                this.convertDown(i_Row, i_Column, eSquareStatuses.PlayerTwo, eSquareStatuses.PlayerOne);
            }

            this.m_WasExistingMovesChecked = false;
            this.newTurn();
        }

        private void newTurn()
        {
            this.m_GameData.ChangeTurn();
            if (this.amountOfPotentialMoves() == 0)
            {
                this.m_GameData.ChangeTurn();
                if (this.amountOfPotentialMoves() == 0)
                {
                    this.CalcWinner();
                }
            }

            if (this.m_GameData.IsPlayerTwoComputer && !this.m_GameData.IsPlayerOneTurn)
            {
                this.ComputerTurn();
            }
        }

        private void resetPotentialMoves()
        {
            for (int i = 0; i < this.m_GameData.SquareStatusMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < this.m_GameData.SquareStatusMatrix.GetLength(0); j++)
                {
                    if (this.m_GameData.SquareStatusMatrix[i, j] == eSquareStatuses.PotentialTurn)
                    {
                        this.m_GameData.SquareStatusMatrix[i, j] = eSquareStatuses.Natural;
                    }
                }
            }

            this.m_WasExistingMovesChecked = false;
        }

        private int amountOfPotentialMoves()
        {
            this.resetPotentialMoves();
            if (!this.m_WasExistingMovesChecked)
            {
                this.CalcAllExistingMovesForCurrentPlayer();
                this.m_WasExistingMovesChecked = true;
            }

            return this.scanForPotentialMoves();
        }

        private void ComputerTurn()
        {
            int amountOfExistingMoves = this.amountOfPotentialMoves();
            Random random = new Random();
            if (amountOfExistingMoves > 0)
            {
                this.getRandomMove(random.Next(1, amountOfExistingMoves), out int row, out int column);
                this.EnterMoveToData(row, column);
            }
        }

        private void CalcWinner()
        {
            int playerOneScore = 0;
            int playerTwoScore = 0;
            foreach (eSquareStatuses currentBox in this.m_GameData.SquareStatusMatrix)
            {
                if (currentBox == eSquareStatuses.PlayerOne)
                {
                    playerOneScore++;
                }

                if (currentBox == eSquareStatuses.PlayerTwo)
                {
                    playerTwoScore++;
                }
            }

            this.m_ClosedByExit = false;
            this.m_GameData.GameEnded(playerOneScore, playerTwoScore);
        }

        private void getRandomMove(int i_PotentialMoveNumber, out int o_Row, out int o_Column)
        {
            o_Row = 0;
            o_Column = 0;
            for (int i = 0; i < this.m_GameData.SquareStatusMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < this.m_GameData.SquareStatusMatrix.GetLength(0); j++)
                {
                    if (this.m_GameData.SquareStatusMatrix[i, j] == eSquareStatuses.PotentialTurn)
                    {
                        i_PotentialMoveNumber--;
                    }

                    if (i_PotentialMoveNumber == 0)
                    {
                        i_PotentialMoveNumber = -1;
                        o_Row = i;
                        o_Column = j;
                    }
                }
            }
        }

        private int scanForPotentialMoves()
        {
            int amountOfPotentialMoves = 0;
            for (int i = 0; i < this.m_GameData.SquareStatusMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < this.m_GameData.SquareStatusMatrix.GetLength(0); j++)
                {
                    if (this.m_GameData.SquareStatusMatrix[i, j] == eSquareStatuses.PotentialTurn)
                    {
                        amountOfPotentialMoves++;
                    }
                }
            }

            return amountOfPotentialMoves;
        }

        private void convertLeft(int i_I, int i_K, eSquareStatuses i_CurrentPlayer, eSquareStatuses i_OtherPlayer)
        {
            if (i_K > 0)
            {
                int j = i_K;
                while (j > 1 && this.m_GameData.SquareStatusMatrix[i_I, j - 1] == i_OtherPlayer)
                {
                    j--;
                }

                if (this.m_GameData.SquareStatusMatrix[i_I, j - 1] == i_CurrentPlayer)
                {
                    while (j < i_K)
                    {
                        this.m_GameData.SquareStatusMatrix[i_I, j] = i_CurrentPlayer;
                        j++;
                    }
                }
            }
        }

        private void convertRight(int i_I, int i_K, eSquareStatuses i_CurrentPlayer, eSquareStatuses i_OtherPlayer)
        {
            if (i_K < this.m_GameData.BoardSize - 1)
            {
                int j = i_K;
                while (j < this.m_GameData.BoardSize - 2 && this.m_GameData.SquareStatusMatrix[i_I, j + 1] == i_OtherPlayer)
                {
                    j++;
                }

                if (this.m_GameData.SquareStatusMatrix[i_I, j + 1] == i_CurrentPlayer)
                {
                    while (j > i_K)
                    {
                        this.m_GameData.SquareStatusMatrix[i_I, j] = i_CurrentPlayer;
                        j--;
                    }
                }
            }
        }

        private void convertUp(int i_I, int i_K, eSquareStatuses i_CurrentPlayer, eSquareStatuses i_OtherPlayer)
        {
            if (i_I > 0)
            {
                int j = i_I;
                while (j > 1 && this.m_GameData.SquareStatusMatrix[j - 1, i_K] == i_OtherPlayer)
                {
                    j--;
                }

                if (this.m_GameData.SquareStatusMatrix[j - 1, i_K] == i_CurrentPlayer)
                {
                    while (j < i_I)
                    {
                        this.m_GameData.SquareStatusMatrix[j, i_K] = i_CurrentPlayer;
                        j++;
                    }
                }
            }
        }

        private void convertDown(int i_I, int i_K, eSquareStatuses i_CurrentPlayer, eSquareStatuses i_OtherPlayer)
        {
            if (i_I < this.m_GameData.BoardSize - 1)
            {
                int j = i_I;
                while (j < this.m_GameData.BoardSize - 2 && this.m_GameData.SquareStatusMatrix[j + 1, i_K] == i_OtherPlayer)
                {
                    j++;
                }

                if (this.m_GameData.SquareStatusMatrix[j + 1, i_K] == i_CurrentPlayer)
                {
                    while (j > i_I)
                    {
                        this.m_GameData.SquareStatusMatrix[j, i_K] = i_CurrentPlayer;
                        j--;
                    }
                }
            }
        }

        private void CalcAllExistingMovesForCurrentPlayer()
        {
            if (this.m_GameData.IsPlayerOneTurn)
            {
                this.allExistingMoveForCurrentPlayer(eSquareStatuses.PlayerOne, eSquareStatuses.PlayerTwo);
            }
            else
            {
                this.allExistingMoveForCurrentPlayer(eSquareStatuses.PlayerTwo, eSquareStatuses.PlayerOne);
            }
        }

        private void allExistingMoveForCurrentPlayer(eSquareStatuses i_CurrentPlayer, eSquareStatuses i_OtherPlayer)
        {
            for (int i = 0; i < this.m_GameData.BoardSize; i++)
            {
                for (int k = 0; k < this.m_GameData.BoardSize; k++)
                {
                    if (this.m_GameData.SquareStatusMatrix[i, k] == i_OtherPlayer)
                    {
                        if (this.isLeftValidMove(i, k, i_CurrentPlayer, i_OtherPlayer))
                        {
                            this.m_GameData.SquareStatusMatrix[i, k - 1] = eSquareStatuses.PotentialTurn;
                        }

                        if (this.isRightValidMove(i, k, i_CurrentPlayer, i_OtherPlayer))
                        {
                            this.m_GameData.SquareStatusMatrix[i, k + 1] = eSquareStatuses.PotentialTurn;
                        }

                        if (this.isUpValidMove(i, k, i_CurrentPlayer, i_OtherPlayer))
                        {
                            this.m_GameData.SquareStatusMatrix[i - 1, k] = eSquareStatuses.PotentialTurn;
                        }

                        if (this.isDownValidMove(i, k, i_CurrentPlayer, i_OtherPlayer))
                        {
                            this.m_GameData.SquareStatusMatrix[i + 1, k] = eSquareStatuses.PotentialTurn;
                        }
                    }
                }
            }
        }

        private string convertRowAndColumnToMoves(int i_I, int i_K)
        {
            char row, column;
            row = (char)('1' + i_I);
            column = (char)('A' + i_K);
            return column.ToString() + row.ToString();
        }

        private bool isLeftValidMove(int i_I, int i_K, eSquareStatuses i_CurrentPlayer, eSquareStatuses i_OtherPlayer)
        {
            bool isValidMove = false;
            if (i_K > 0)
            {
                if (this.m_GameData.SquareStatusMatrix[i_I, i_K - 1] == eSquareStatuses.Natural)
                {
                    while (i_K < this.m_GameData.BoardSize - 1 && this.m_GameData.SquareStatusMatrix[i_I, i_K] == i_OtherPlayer){
                        i_K++;
                    }

                    if (this.m_GameData.SquareStatusMatrix[i_I, i_K] == i_CurrentPlayer)
                    {
                        isValidMove = true;
                    }
                }
            }

            return isValidMove;
        }

        private bool isRightValidMove(int i_I, int i_K, eSquareStatuses i_CurrentPlayer, eSquareStatuses i_OtherPlayer)
        {
            bool isValidMove = false;
            if (i_K < this.m_GameData.BoardSize - 1)
            {
                if (this.m_GameData.SquareStatusMatrix[i_I, i_K + 1] == eSquareStatuses.Natural)
                {
                    while (i_K > 0 && this.m_GameData.SquareStatusMatrix[i_I, i_K] == i_OtherPlayer){
                        i_K--;
                    }

                    if (this.m_GameData.SquareStatusMatrix[i_I, i_K] == i_CurrentPlayer)
                    {
                        isValidMove = true;
                    }
                }
            }

            return isValidMove;
        }

        private bool isUpValidMove(int i_I, int i_K, eSquareStatuses i_CurrentPlayer, eSquareStatuses i_OtherPlayer)
        {
            bool isValidMove = false;
            if (i_I > 0)
            {
                if (this.m_GameData.SquareStatusMatrix[i_I - 1, i_K] == eSquareStatuses.Natural)
                {
                    while (i_I < this.m_GameData.BoardSize - 1 && this.m_GameData.SquareStatusMatrix[i_I, i_K] == i_OtherPlayer){
                        i_I++;
                    }

                    if (this.m_GameData.SquareStatusMatrix[i_I, i_K] == i_CurrentPlayer)
                    {
                        isValidMove = true;
                    }
                }
            }

            return isValidMove;
        }

        private bool isDownValidMove(int i_I, int i_K, eSquareStatuses i_CurrentPlayer, eSquareStatuses i_OtherPlayer)
        {
            bool isValidMove = false;
            if (i_I < this.m_GameData.BoardSize - 1)
            {
                if (this.m_GameData.SquareStatusMatrix[i_I + 1, i_K] == eSquareStatuses.Natural)
                {
                    while (i_I > 0 && this.m_GameData.SquareStatusMatrix[i_I, i_K] == i_OtherPlayer){
                        i_I--;
                    }

                    if (this.m_GameData.SquareStatusMatrix[i_I, i_K] == i_CurrentPlayer)
                    {
                        isValidMove = true;
                    }
                }
            }

            return isValidMove;
        }
    }
}
