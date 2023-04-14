namespace A23_Ex05_Liad_206837247_Omer_206729873
{
    partial class GameSettingsForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonBoardSize = new System.Windows.Forms.Button();
            this.buttonPlayAgainstComputer = new System.Windows.Forms.Button();
            this.buttonPlayAgainstFriend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonBoardSize
            // 
            this.buttonBoardSize.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonBoardSize.Location = new System.Drawing.Point(34, 43);
            this.buttonBoardSize.Margin = new System.Windows.Forms.Padding(4);
            this.buttonBoardSize.Name = "buttonBoardSize";
            this.buttonBoardSize.Size = new System.Drawing.Size(752, 108);
            this.buttonBoardSize.TabIndex = 0;
            this.buttonBoardSize.Text = "Board Size : 6x6 (click to increase)";
            this.buttonBoardSize.UseVisualStyleBackColor = true;
            this.buttonBoardSize.Click += new System.EventHandler(this.buttonBoardSize_Click);
            // 
            // buttonPlayAgainstComputer
            // 
            this.buttonPlayAgainstComputer.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonPlayAgainstComputer.Location = new System.Drawing.Point(34, 186);
            this.buttonPlayAgainstComputer.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPlayAgainstComputer.Name = "buttonPlayAgainstComputer";
            this.buttonPlayAgainstComputer.Size = new System.Drawing.Size(372, 108);
            this.buttonPlayAgainstComputer.TabIndex = 2;
            this.buttonPlayAgainstComputer.Text = "Play against the computer";
            this.buttonPlayAgainstComputer.UseVisualStyleBackColor = true;
            this.buttonPlayAgainstComputer.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonPlayAgainstFriend
            // 
            this.buttonPlayAgainstFriend.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonPlayAgainstFriend.Location = new System.Drawing.Point(414, 186);
            this.buttonPlayAgainstFriend.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPlayAgainstFriend.Name = "buttonPlayAgainstFriend";
            this.buttonPlayAgainstFriend.Size = new System.Drawing.Size(372, 108);
            this.buttonPlayAgainstFriend.TabIndex = 3;
            this.buttonPlayAgainstFriend.Text = "Play against your friend";
            this.buttonPlayAgainstFriend.UseVisualStyleBackColor = true;
            this.buttonPlayAgainstFriend.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // GameSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 327);
            this.Controls.Add(this.buttonPlayAgainstFriend);
            this.Controls.Add(this.buttonPlayAgainstComputer);
            this.Controls.Add(this.buttonBoardSize);
            this.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "GameSettingsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Othello - Game Settings";
            this.ResumeLayout(false);

        }

        #endregion

        private Button buttonBoardSize;
        private Button buttonPlayAgainstComputer;
        private Button buttonPlayAgainstFriend;
    }
}