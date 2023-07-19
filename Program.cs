using Engine;
using System.Drawing;
using System.Linq;
using System.Numerics;
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

// Camera cameraPlayer = null!;
Graphics g = null!;

PictureBox pb = new PictureBox();
pb.Dock = DockStyle.Fill;
GameScreen.Controls.Add(pb);


var tm = new Timer()
{
    Interval = 1000,
};

var rec = Mesh.GenerateRectangle(new Point3D(400, 100, 0), new Point3D(400, 300, 0), new Point3D(0, 300, 400));

tm.Tick += delegate
{
    var points = rec.Faces.SelectMany(f =>
        new PointF[]
        {
            new PointF(f.p.X, f.p.Y),
            new PointF(f.q.X, f.q.Y),
            new PointF(f.r.X, f.r.Y)
        });
    g.DrawLines(Pens.Black, points.ToArray());
    pb.Refresh();
    g.Clear(Color.White);
    rec = rec
        .RotateZ(0.1f, 0.1f)
        .Translate(200, 200, 0);
};

GameScreen.Load += (s, e) =>
{
    // var size = 50;

    // var cameraPoint = new Point3D(GameScreen.Width / 2, GameScreen.Height / 2, 1);
    // var normal = new Vector3(-1, 0, 0);
    // cameraPlayer = new Camera(cameraPoint, normal, 200, 200, 10);
    // var cameraBlock = new Panel()
    // {
    //     Width = size,
    //     Height = size,
    //     BackColor = Color.Purple,
    //     Location = new Point(GameScreen.Width / 2 - size / 2, GameScreen.Height / 2 - size / 2)
    // };
    // GameScreen.Controls.Add(cameraBlock);

    // var squareX = GameScreen.Width / 2 - 200;
    // var squareY = GameScreen.Height / 2;
    // var squarePoint = new Point3D(squareX, squareY, 0);


    // var squarePanel = new Panel()
    // {
    //     Width = size,
    //     Height = size,
    //     BackColor = Color.Green,
    //     Location = new Point(squareX - size / 2, squareY - size / 2)
    // };
    // GameScreen.Controls.Add(squarePanel);

    var bmp = new Bitmap(pb.Width, pb.Height);
    g = Graphics.FromImage(bmp);

    pb.Image = bmp;
    tm.Start();
};


Application.Run(GameScreen);