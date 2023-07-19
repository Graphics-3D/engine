using Engine;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

ApplicationConfiguration.Initialize();

var GameScreen = new Form();

PictureBox pb = new PictureBox();
pb.Dock = DockStyle.Fill;
GameScreen.Controls.Add(pb);

var tm = new Timer()
{
    Interval = 1000,
};

Camera cam = null!;
Graphics g = null!;

#region GameScreen

GameScreen.FormBorderStyle = FormBorderStyle.None;
GameScreen.WindowState = FormWindowState.Maximized;

GameScreen.KeyDown += (s, e) =>
{
    if (e.KeyCode == Keys.Escape)
        Application.Exit();
};

GameScreen.Load += (s, e) =>
{
    var rec = Mesh.GenerateRectangle(
        new Point3D(10, 5, 0),
        new Point3D(15, 0, 5)
    );
    Scene.Create(rec);
    cam = new Camera(Point3D.Empty, new Vector3(1, 1, 0), new Vector3(0, 0, 1), pb.Width, pb.Height, 5, 1000);
    var bmp = new Bitmap(pb.Width, pb.Height);
    g = Graphics.FromImage(bmp);
    pb.Image = bmp;
    tm.Start();
};

#endregion

tm.Tick += delegate
{
    cam?.Render(Scene.Current);
    cam?.Draw(g);
    pb?.Refresh();
};

Application.Run(GameScreen);