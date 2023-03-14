namespace SpaceShooter.gui
{
    public static class GameOverMessage
    {
        private static bool isPrinted = false;

        public static void Print(Control parent)
        {
            if (isPrinted)
                return;

            Label gameOverMessage = new Label();
            parent.Controls.Add(gameOverMessage);
            
            gameOverMessage.AutoSize = true;
            gameOverMessage.BackColor = Color.Transparent;
            gameOverMessage.Font = new Font(
                "Microsoft Sans Serif",
                40.0F, FontStyle.Bold | FontStyle.Italic,
                GraphicsUnit.Point
            );
            gameOverMessage.ForeColor = Color.DarkRed;
            gameOverMessage.Text = "GAME OVER!";
            gameOverMessage.TextAlign = ContentAlignment.MiddleCenter;
            gameOverMessage.UseCompatibleTextRendering = true;
            gameOverMessage.Location = new Point(
                parent.ClientRectangle.Width / 2 - gameOverMessage.Width / 2,
                parent.ClientRectangle.Height / 2 - gameOverMessage.Height / 2
            );

            isPrinted = true;
        }
    }
}
