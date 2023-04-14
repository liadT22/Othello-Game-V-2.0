namespace A23_Ex05_Liad_206837247_Omer_206729873
{
    public class GameData
    {
        private int m_BoardSize;

        public int BoardSize
        {
            get
            {
                return this.m_BoardSize;
            }
        }

        private eSquareStatuses[,] m_SquareStatusMatrix;

        public eSquareStatuses[,] SquareStatusMatrix
        {
            get { return this.m_SquareStatusMatrix; }
            set { this.m_SquareStatusMatrix = value; }
        }

        private bool m_IsGameOver;

        public bool IsGameOver
        {
            get { return this.m_IsGameOver; }
        }

        private int m_PlayerOneScore;

        public int PlayerOneScore
        {
            get { return this.m_PlayerOneScore; }
        }

        private int m_PlayerTwoScore;

        public int PlayerTwoScore
        {
            get { return this.m_PlayerTwoScore; }
        }

        private bool m_IsPlayerOneTurn;

        public bool IsPlayerOneTurn
        {
            get { return this.m_IsPlayerOneTurn; }
        }

        private bool m_IsPlayerTwoComputer;

        public bool IsPlayerTwoComputer
        {
            get { return this.m_IsPlayerTwoComputer; }
        }

        public GameData(int i_BoardSize, bool i_IsPlayerTwoComputer)
        {
            this.m_BoardSize = i_BoardSize;
            this.m_SquareStatusMatrix = initBoard(i_BoardSize);
            this.m_IsPlayerTwoComputer = i_IsPlayerTwoComputer;
            this.m_IsPlayerOneTurn = true;
        }

        public void ChangeTurn()
        {
            this.m_IsPlayerOneTurn = !this.m_IsPlayerOneTurn;
        }

        public void GameEnded(int i_PlayerOneSCore, int i_PlayerTwoScore)
        {
            this.m_IsGameOver = true;
            this.m_PlayerOneScore = i_PlayerOneSCore;
            this.m_PlayerTwoScore = i_PlayerTwoScore;
        }

        private static eSquareStatuses[,] initBoard(int i_BoardSize)
        {
            eSquareStatuses[,] boxStatusMatrix = new eSquareStatuses[i_BoardSize, i_BoardSize];
            boxStatusMatrix[(i_BoardSize / 2) - 1, (i_BoardSize / 2) - 1] = eSquareStatuses.PlayerOne;
            boxStatusMatrix[i_BoardSize / 2, i_BoardSize / 2] = eSquareStatuses.PlayerOne;
            boxStatusMatrix[(i_BoardSize / 2) - 1, i_BoardSize / 2] = eSquareStatuses.PlayerTwo;
            boxStatusMatrix[i_BoardSize / 2, (i_BoardSize / 2) - 1] = eSquareStatuses.PlayerTwo;
            return boxStatusMatrix;
        }
    }
}
