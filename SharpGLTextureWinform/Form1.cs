using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpGL;

namespace SharpGLTextureWinform
{
    public partial class Form1 : Form
    {
        SharpGL.OpenGL gl;
        SharpGL.SceneGraph.Texture texture = new SharpGL.SceneGraph.Texture();
        //六面体位置 
        float x = 0f;
        float y = 0f;
        float z = -0.6f;
        //光源位置 
        float lx = 0f;
        float ly = 0f;
        float lz = -3f;

        //旋转法线
        float rx = 0f;
        float ry = 0f;
        float rz = 0f;
        float[] fLightPosition = new float[4];// 光源位置 
        float[] fLightAmbient = new float[4] { 1f, 1f, 1f, 1.0f };// 环境光参数 
        float[] fLightDiffuse = new float[4] { 1f, 1f, 1f, 1f };// 漫射光参数

        public Form1()
        {
            InitializeComponent();
            fLightPosition = new float[4] {lx,ly,lz, 1f};
            gl = this.openGLControl1.OpenGL;
            gl.Light(OpenGL.LIGHT0, OpenGL.AMBIENT, fLightAmbient);
            gl.Light(OpenGL.LIGHT0, OpenGL.DIFFUSE, fLightDiffuse);//漫射光源 
            gl.Light(OpenGL.LIGHT0, OpenGL.POSITION, fLightPosition);//光源位置 
            gl.Enable(OpenGL.LIGHT0);
           // gl.Enable(OpenGL.LIGHTING);
            
            
        }


        static int num = 0;
        private void openGLControl1_OpenGLDraw(object sender, PaintEventArgs e)
        {
            //this.Text = "OPENGL num:" + num--;
            num--;
            texture.Create(gl, @"D:\精美壁纸\88c6d4a5249f31a47fb0f28761c94142.jpg");
            gl.Clear(OpenGL.COLOR_BUFFER_BIT | OpenGL.DEPTH_BUFFER_BIT);
            gl.Enable(OpenGL.TEXTURE_2D);
            
            //清除深度缓存 
            gl.LoadIdentity();
            //重置模型观察矩阵，我认为其实就是重置了三维坐标轴的位置，将其初始化为原点 
            gl.Translate(-1.5f+x, 0f+y, -6f+z);
            gl.Rotate(180 * Math.Abs(num) / 100, rx, ry, rz);
            this.Text = "X:" + rx + " Y:" + ry;
            //gl.Translate(-1.5f, 0f, num/20f);
            gl.Begin(OpenGL.TRIANGLES);

            gl.Color(1.0, 0, 0);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0, 1.0f, 0);
            gl.Vertex(-1.0f, -1.0f, 1.0f);
            gl.Color(0, 0, 1.0f);
            gl.Vertex(1.0f, -1.0f, 1.0f);

            
            gl.Color(1.0, 0, 0);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0, 1.0f, 0);
            gl.Vertex(1.0f, -1.0f, 1.0f);
            gl.Color(0, 0, 1.0f);
            gl.Vertex(1.0f, -1.0f, -1.0f);

            gl.Color(1.0, 0, 0);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0, 1.0f, 0);
            gl.Vertex(1.0f, -1.0f, -1.0f);
            gl.Color(0, 0, 1.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);

            gl.Color(1.0, 0, 0);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0, 1.0f, 0);
            gl.Vertex(-1.0f, -1.0f, 1.0f);
            gl.Color(0, 0, 1.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);

            gl.End();



            //Quards
            texture.Create(gl, @"D:\精美壁纸\u=4262596037,2038920441&fm=21&gp=0.jpg");
            gl.LoadIdentity();
            gl.Translate(3f, 0f, -7f);
            gl.Rotate(180 * Math.Abs(num) / 100, -1f, 1.0f, 1f);

            gl.Begin(OpenGL.QUADS);
            gl.Color(1f, 1f, 1f);
            gl.TexCoord(0f, 1.0f);
            //gl.Color(1.0f, 0f, 0f);
            gl.Vertex(-1.0f, 1.0f, 1f);
            gl.TexCoord(1.0f, 1.0f);
            //gl.Color(0f, 1.0f, 0f);
            gl.Vertex(1.0f, 1.0f, 1f);
            gl.TexCoord(1.0f, 0f);
            //gl.Color(0f, 0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, 1f);
            gl.TexCoord(0f, 0f);
            //gl.Color(1.0f, 0f, 1.0f);
            gl.Vertex(-1.0f, -1.0f, 1f);
            
            gl.Color(1.0f, 0f, 0f);
            gl.Vertex(1.0f, 1.0f, 1.0f);
            gl.Color(0f, 1.0f, 0f);
            gl.Vertex(1.0f, 1.0f, -1.0f);
            gl.Color(0f, 0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, -1.0f);
            gl.Color(1.0f, 0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, 1.0f);

            gl.Color(1.0f, 0f, 0f);
            gl.Vertex(-1.0f, 1.0f, -1f);
            gl.Color(0f, 1.0f, 0f);
            gl.Vertex(-1.0f, -1.0f, -1f);
            gl.Color(0f, 0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, -1f);
            gl.Color(1.0f, 0f, 1.0f);
            gl.Vertex(1.0f, 1.0f, -1f);

            gl.Color(1.0f, 0f, 0f);
            gl.Vertex(-1.0f, -1.0f, 1.0f);
            gl.Color(0f, 1.0f, 0f);
            gl.Vertex(-1.0f, 1.0f, 1.0f);
            gl.Color(0f, 0f, 1.0f);
            gl.Vertex(-1.0f, 1.0f, -1.0f);
            gl.Color(1.0f, 0f, 1.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);

            gl.Color(1.0f, 0f, 0f);
            gl.Vertex(-1.0f, 1.0f, 1.0f);
            gl.Color(0f, 1.0f, 0f);
            gl.Vertex(-1.0f, 1.0f, -1.0f);
            gl.Color(0f, 0f, 1.0f);
            gl.Vertex(1.0f, 1.0f, -1.0f);
            gl.Color(1.0f, 0f, 1.0f);
            gl.Vertex(1.0f, 1.0f, 1.0f);

            gl.Color(1.0f, 0f, 0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);
            gl.Color(0f, 1.0f, 0f);
            gl.Vertex(1.0f, -1.0f, -1.0f);
            gl.Color(0f, 0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, 1f);
            gl.Color(1.0f, 0f, 1.0f);
            gl.Vertex(-1.0f, -1.0f, 1f);
            
            gl.End();
            gl.Flush();


        }
        bool light = true;
        private void openGLControl1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) 
            {
                case Keys.W:
                    y += 0.1f;break;
                case Keys.S:
                    y -= 0.1f;break;
                case Keys.A:
                    x -= 0.1f;break;
                case Keys.D:
                    x += 0.1f;break;
                case Keys.Q:
                    z += 0.1f;break;
                case Keys.E:
                    z -= 0.1f;break;
                case Keys.Space:
                    if (light)
                    {
                        this.openGLControl1.OpenGL.Disable(OpenGL.LIGHT0);
                    }
                    else
                    {
                        this.openGLControl1.OpenGL.Enable(OpenGL.LIGHT0);
                    }
                    light = !light;
                    break; 
                default:
                    break;
            }

        }

        static int num1 = 0;
        Point oldPosition = new Point();
        private void openGLControl1_MouseMove(object sender, MouseEventArgs e)
        {
            //this.Text = "X:" + Cursor.Position.X + " Y:" + Cursor.Position.Y;
            if (e.Button == MouseButtons.Right) 
            {
                PointF offset = new PointF();
                if (Math.Pow((Cursor.Position.X - oldPosition.X), 2) + Math.Pow((Cursor.Position.Y - oldPosition.Y), 2) > 100) 
                {
                    offset.X = Cursor.Position.X - oldPosition.X;
                    offset.Y = Cursor.Position.Y - oldPosition.Y;
                    oldPosition = Cursor.Position;
                    double angle = -Math.Atan2(offset.Y, offset.X) + Math.PI*0.5f;
                    //rx = offset.X / (float)(Math.Sqrt(offset.X * offset.X + offset.Y * offset.Y));
                    //ry = offset.Y / (float)(Math.Sqrt(offset.X * offset.X + offset.Y * offset.Y));
                    //rz = 0;
                    rx = 1f * (float)Math.Cos(angle);
                    ry = 1f * (float)Math.Sin(angle);
                }
            }
        }

        private void openGLControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
                oldPosition = Cursor.Position;
        }



    }
}
