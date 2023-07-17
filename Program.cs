using System.Windows.Forms;

ApplicationConfiguration.Initialize();

var GameScreen = new Form();

GameScreen.FormBorderStyle = FormBorderStyle.None;
GameScreen.WindowState = FormWindowState.Maximized;

GameScreen.KeyDown += (s, e) =>
{
    if (e.KeyCode == Keys.Escape)
        Application.Exit();
};

Application.Run(GameScreen);