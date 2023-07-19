using Engine;
using System;
using System.Drawing;
using System.Linq;
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

    var angle = 0.1f;
    var cos = MathF.Cos(angle);
    var sin = MathF.Sin(angle);

    rec = rec.RotateX(cos, sin);
};

GameScreen.Load += (s, e) =>
{
    var bmp = new Bitmap(pb.Width, pb.Height);
    g = Graphics.FromImage(bmp);

    pb.Image = bmp;
    tm.Start();
};

Application.Run(GameScreen);