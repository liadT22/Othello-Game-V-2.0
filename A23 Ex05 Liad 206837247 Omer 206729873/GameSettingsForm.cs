namespace A23_Ex05_Liad_206837247_Omer_206729873
{
    public partial class GameSettingsForm : Form
    {
        private int m_BoardSize = 6;

        public int BoardSize
        {
            get { return this.m_BoardSize; }
        }

        private eClosedByPlayer m_ClosedByPlayAgainstComputer = eClosedByPlayer.Exit;

        public eClosedByPlayer ClosedByPlayAgainstComputer
        {
            get { return this.m_ClosedByPlayAgainstComputer; }
        }


        public GameSettingsForm()
        {
            InitializeComponent();
        }

        private void buttonBoardSize_Click(object sender, EventArgs e)
        {
            if (this.m_BoardSize < 12)
            {
                this.m_BoardSize += 2;
            }
            else
            {
                this.m_BoardSize = 6;
            }

            this.adjustTextToCurrentBoardSize();
        }

        private void adjustTextToCurrentBoardSize()
        {
            this.buttonBoardSize.Text = string.Format("Board Size: {0}x{0} (click to {1})", this.m_BoardSize, this.m_BoardSize < 12 ? "Increase" : "reset");
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (sender == this.buttonPlayAgainstComputer)
            {
                this.m_ClosedByPlayAgainstComputer = eClosedByPlayer.PlayerAndComputer;
            }
            else if (sender == this.buttonPlayAgainstFriend)
            {
                this.m_ClosedByPlayAgainstComputer = eClosedByPlayer.TwoPlayers;
            }
            else
            {
                this.m_ClosedByPlayAgainstComputer = eClosedByPlayer.Exit;
            }

            this.Close();
        }
    }
}