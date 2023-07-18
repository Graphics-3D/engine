using System.Drawing;
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

Camera cameraPlayer = null!;

GameScreen.Load += (s, e) =>
{
    var cameraPoint = new Point3D(GameScreen.Width / 2, GameScreen.Height / 2, 1);
    cameraPlayer = new Camera(cameraPoint, 10, 0, 0);
    var cameraSize = 100;
    var cameraBlock = new Panel()
    {
        Width = cameraSize,
        Height = cameraSize,
        BackColor = Color.Purple,
        Location = new Point(GameScreen.Width / 2 - cameraSize / 2, GameScreen.Height / 2 - cameraSize / 2)
    };

    GameScreen.Controls.Add(cameraBlock);
    var squarePoint = new Point3D(GameScreen.Width / 2 + 100, GameScreen.Height / 2 + 200, 1);
    var flag = cameraPlayer.ShouldRender(squarePoint, 1000);

    MessageBox.Show(flag.ToString());
};

Application.Run(GameScreen);