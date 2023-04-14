using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A23_Ex05_Liad_206837247_Omer_206729873
{
    public partial class BoardForm : Form
    {
        private SquarePictureBox[,] m_SquarePictureBoxesList;

        public event Func<int, int, GameData> m_ReportBoardChangedDelegates;

        public BoardForm(int i_BoardSize, GameData i_GameData)
        {
            this.InitializeComponent();
            this.initBoard(i_BoardSize);
            this.initStartingSquare(i_BoardSize);
            this.updateBoard(i_GameData.SquareStatusMatrix, i_GameData.IsPlayerOneTurn);
        }

        private void reportClicked(int i_Row, int i_Column)
        {
            this.doWhenBoardChanged(i_Row, i_Column);
        }

        private void doWhenBoardChanged(int i_Row, int i_Column)
        {
            this.resetPotentialMoves();
            this.notifyBoardChangedObservers(i_Row, i_Column);
        }

        private void notifyBoardChangedObservers(int i_Row, int i_Column)
        {
            if (this.m_ReportBoardChangedDelegates != null)
            {
                GameData gameData = this.m_ReportBoardChangedDelegates.Invoke(i_Row, i_Column);
                if (!gameData.IsGameOver)
                {
                    this.updateBoard(gameData.SquareStatusMatrix, gameData.IsPlayerOneTurn);
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void updateBoard(eSquareStatuses[,] i_PotentialMovesMatrix, bool isPlayerOneTurn)
        {
            for (int i = 0; i < i_PotentialMovesMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < i_PotentialMovesMatrix.GetLength(0); j++)
                {
                    if (i_PotentialMovesMatrix[i, j] == eSquareStatuses.PotentialTurn)
                    {
                        this.m_SquarePictureBoxesList[i, j].IsPotentialMove = true;
                    }
                    else if (i_PotentialMovesMatrix[i, j] == eSquareStatuses.PlayerOne)
                    {
                        this.m_SquarePictureBoxesList[i, j].SquareStatuses = eSquareStatuses.PlayerOne;
                    }
                    else if (i_PotentialMovesMatrix[i, j] == eSquareStatuses.PlayerTwo)
                    {
                        this.m_SquarePictureBoxesList[i, j].SquareStatuses = eSquareStatuses.PlayerTwo;
                    }
                    else
                    {
                        this.m_SquarePictureBoxesList[i, j].SquareStatuses = eSquareStatuses.Natural;
                    }
                }
            }

            this.changeTextAccordingToPlayerTurn(isPlayerOneTurn);
        }

        private void resetPotentialMoves()
        {
            for (int i = 0; i < this.m_SquarePictureBoxesList.GetLength(0); i++)
            {
                for (int j = 0; j < this.m_SquarePictureBoxesList.GetLength(0); j++)
                {
                    this.m_SquarePictureBoxesList[i, j].IsPotentialMove = false;
                }
            }
        }

        private void changeTextAccordingToPlayerTurn(bool i_IsPlayerOneTurn)
        {
            this.Text = i_IsPlayerOneTurn ? "Othello - Red's Turn" : "Othello - Yellow's turn";
        }

        private void initStartingSquare(int i_BoardSize)
        {
            this.m_SquarePictureBoxesList[(i_BoardSize / 2) - 1, (i_BoardSize / 2) - 1].SquareStatuses = eSquareStatuses.PlayerOne;
            this.m_SquarePictureBoxesList[i_BoardSize / 2, i_BoardSize / 2].SquareStatuses = eSquareStatuses.PlayerOne;
            this.m_SquarePictureBoxesList[(i_BoardSize / 2) - 1, i_BoardSize / 2].SquareStatuses = eSquareStatuses.PlayerTwo;
            this.m_SquarePictureBoxesList[i_BoardSize / 2, (i_BoardSize / 2) - 1].SquareStatuses = eSquareStatuses.PlayerTwo;
        }

        private void initBoard(int i_BoardSize)
        {
            this.m_SquarePictureBoxesList = new SquarePictureBox[i_BoardSize, i_BoardSize];
            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    this.m_SquarePictureBoxesList[i, j] = new SquarePictureBox(i, j, this.reportClicked);
                    this.m_SquarePictureBoxesList[i, j].Left = 30 + (j * this.m_SquarePictureBoxesList[i, j].Width) + (j * 5);
                    this.m_SquarePictureBoxesList[i, j].Top = 30 + (i * this.m_SquarePictureBoxesList[i, j].Height) + (i * 5);
                    this.Controls.Add(this.m_SquarePictureBoxesList[i, j]);
                }
            }
        }

        private class SquarePictureBox : PictureBox
        {
            private eSquareStatuses m_SquareStatus = eSquareStatuses.Natural;
            private int m_Row;
            private int m_Column;

            public event Action<int, int> m_ReportClickDelegates;

            public eSquareStatuses SquareStatuses
            {
                get
                {
                    return this.m_SquareStatus;
                }

                set
                {
                    this.m_SquareStatus = value;
                    this.ChangeSquare();
                }
            }

            private bool m_IsPotentialMove;

            public bool IsPotentialMove
            {
                set
                {
                    if (value)
                    {
                        this.m_IsPotentialMove = true;
                        this.BackColor = Color.Green;
                        this.Click += SquarePictureBox_Click;
                    }
                    else
                    {
                        this.m_IsPotentialMove = false;
                        this.BackColor = Color.Gray;
                        this.Click -= SquarePictureBox_Click;
                    }
                }
            }

            public SquarePictureBox(int i_Row, int i_Column, Action<int, int> i_DoWhenClicked)
            {
                this.BackColor = Color.Gray;
                this.Width = 60;
                this.Height = 60;
                this.m_Row = i_Row;
                this.m_Column = i_Column;
                this.m_ReportClickDelegates = i_DoWhenClicked;
            }

            public void ChangeSquare()
            {
                if (this.m_SquareStatus == eSquareStatuses.PlayerOne)
                {
                    this.Load("CoinRed.png");
                }
                else if (this.m_SquareStatus == eSquareStatuses.PlayerTwo)
                {
                    this.Load("CoinYellow.png");
                }
            }

            private void SquarePictureBox_Click(object? sender, EventArgs e)
            {
                this.notifyClickedObservers(this.m_Row, this.m_Column);
            }

            private void notifyClickedObservers(int i_Row, int i_Column)
            {
                this.m_ReportClickDelegates?.Invoke(i_Row, i_Column);
            }

            private void PotentialMove()
            {
                this.BackColor = Color.Green;
            }
        }
    }
}
