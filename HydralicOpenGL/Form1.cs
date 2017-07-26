using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpGL;
using planeGL;

namespace HydralicOpenGL
{
    public partial class Form1 : Form
    {
        PlaneGL newgl = new PlaneGL();
        public float my_xRotation = 0.0f, my_yRotation = 0.0f, my_zRotation = 0.0f;              //定义旋转量
        public float m_xScaling = 1.0f, m_yScaling = 1.0f, m_zScaling = 1.0f;              //定义放缩量
        public float m_speedRotation = 0.6f;
        bool lMouseButtonDown;
        bool rMouseButtonDown;
        Point lButtonDownPos;
        Point rButtonDownPos;
        public float FW_Rotateangle = 0.0f;

        private SharpGL.SceneGraph.Texture texture;



        public Form1()
        {
            InitializeComponent();
            this.openGLControl.MouseWheel += this.myOnMouseWheel;
        }


        private void openGLControl_MouseClick(object sender, MouseEventArgs e)
        {
            //myOnMouseDown(object , System.Windows.Forms.Control.MouseButtons  );
            lMouseButtonDown = !lMouseButtonDown;
        }

        private void myOnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                lMouseButtonDown = true;       //定义为鼠标左键按下状态
                lButtonDownPos = e.Location;   //记录鼠标左键按下位置

            }
            if (e.Button == MouseButtons.Right)
            {
                rMouseButtonDown = true;       //定义为鼠标右键按下状态
                rButtonDownPos = e.Location;   //记录鼠标右键按下位置
            }
            this.Focus();
        }

        private void myOnMouseMove(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left)
            if (lMouseButtonDown)
            {
                my_xRotation -= (lButtonDownPos.Y - e.Location.Y) * m_speedRotation;
                my_yRotation -= (-lButtonDownPos.X + e.Location.X) * m_speedRotation;
                my_zRotation -= (lButtonDownPos.X - e.Location.X) * m_speedRotation;
                lButtonDownPos = e.Location;
                rButtonDownPos = e.Location;
                //lMouseButtonDown = false;
            }
        }

        public void myOnMouseWheel(object Sender, MouseEventArgs e)
        {
            m_xScaling += (e.Delta * 0.0001f);//放大缩小的单位
            m_yScaling += (e.Delta * 0.0001f);//放大缩小的单位
            m_zScaling += (e.Delta * 0.0001f);//放大缩小的单位
        }

        public void myOnMouseUp(object Sender, MouseEventArgs e)
        {
            lMouseButtonDown = false;
            rMouseButtonDown = false;
        }

        //OpenGLControl openGLControl;
        private void openGLControl1_OpenGLDraw(object sender, PaintEventArgs e)
        {
            //openGLControl = this.openGLControl;
            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            uint hdc = (uint)openGLControl.Handle;

            //  Clear the color and depth buffer.
            gl.Clear(OpenGL.COLOR_BUFFER_BIT | OpenGL.DEPTH_BUFFER_BIT);

            //  Load the identity matrix.
            gl.LoadIdentity();
            gl.Enable(OpenGL.DEPTH_TEST);

            //Enable back face culling, defaults to Clock wise vertices.
            //gl.Enable(OpenGL.GL_CULL_FACE);

            gl.Enable(OpenGL.COLOR_MATERIAL);

            gl.ColorMaterial(OpenGL.FRONT_AND_BACK, OpenGL.AMBIENT_AND_DIFFUSE);

            // Default mode
            gl.PolygonMode(OpenGL.FRONT_AND_BACK, OpenGL.FILL);
            gl.ShadeModel(OpenGL.SMOOTH);
            gl.Enable(OpenGL.NORMALIZE);
            gl.Enable(OpenGL.DEPTH);

            // Lights, material properties
            float[] ambientProperties = new float[] { 0.5f, 0.5f, 0.5f, 1.0f };
            float[] diffuseProperties = new float[] { 0.7f, 0.7f, 0.7f, 1.0f };
            float[] specularProperties = new float[] { 0.0f, 0.8f, 0.2f, 1.0f };
            //float[] position = new float[] { 0, 0, 10, 1 };

            gl.ClearDepth(1.0f);

            gl.Light(OpenGL.LIGHT0, OpenGL.AMBIENT, ambientProperties);
            gl.Light(OpenGL.LIGHT0, OpenGL.DIFFUSE, diffuseProperties);
            gl.Light(OpenGL.LIGHT0, OpenGL.SPECULAR, specularProperties);
            //gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, position);

            gl.LightModel(OpenGL.LIGHT_MODEL_TWO_SIDE, 1.0f);
            float[] ambient_lightModel = new float[] { 0.1f, 0.1f, 0.1f, 1.0f };
            gl.LightModel(OpenGL.LIGHT_MODEL_AMBIENT, ambient_lightModel);

            texture = new SharpGL.SceneGraph.Texture();
            //texture.Create(gl, Application.StartupPath + "\\texture\\橙色.jpg");
            //texture.Create(gl, Application.StartupPath + "\\texture\\橙色.jpg");

            // Default : lighting
            gl.Enable(OpenGL.TEXTURE_2D);
            gl.Enable(OpenGL.LIGHT0);
            gl.Enable(OpenGL.LIGHTING);

            gl.Enable(OpenGL.LINE_SMOOTH);
            gl.Enable(OpenGL.BLEND);
            gl.BlendFunc(OpenGL.SRC_ALPHA, OpenGL.ONE_MINUS_SRC_ALPHA);
            gl.Hint(OpenGL.LINE_SMOOTH_HINT, OpenGL.NICEST);
            //gl.LineWidth(5.0f);

            gl.Clear(OpenGL.COLOR_BUFFER_BIT | OpenGL.DEPTH_BUFFER_BIT);
            //gl.ClearColor(0.51f, 0.71f, 0.9f, 0.0f);
            gl.ClearColor(0f, 0.5f, 0.78f, 0.0f);


            //newgl.EnableFreeRotate(true);
            //newgl.freerotation();

            //newgl.RightFrontJinRotation(-45);
            //newgl.LeftFrontJinRotation(-45);
            //newgl.RightFrontJinRotation(0);
            //newgl.LeftFrontJinRotation(0);

            //newgl.LeftAileronRotation(45);
            //newgl.RightAileronRotation(45);
            //newgl.LeftAileronRotation(0);
            //newgl.RightAileronRotation(0);

            //newgl.RightPingweiRotation(45);
            //newgl.LeftPingweiRotation(45);
            //newgl.RightPingweiRotation(0);
            //newgl.LeftPingweiRotation(0);

            //newgl.BackJinRotation(45);
            //newgl.BackJinRotation(0);

            //newgl.Rudder1Rotation(45);
            //newgl.Rudder1Rotation(0);

            //newgl.GearUp();
            //newgl.GearDown();

            //newgl.DecelerateUp();
            //newgl.DecelerateDown();


            for (int i = 0; i < newgl.m_glsModel.numGroups; i++)
            {
                newgl.m_group = newgl.m_glsModel.groups[i];
                for (int m = 0; m < newgl.m_glsModel.numGroups; m++)
                {
                    newgl.GroupProcessing(ref newgl.m_glsModel.groups[m]);
                }

                gl.PushMatrix();
                gl.Rotate(my_xRotation, 1.0f, 0.0f, 0.0f);//旋转时绕x轴
                //gl.Rotate(my_yRotation, 0.0f, 1.0f, 0.0f);//旋转时绕y轴
                /*
                 * 
                 * 
                 * 
                 */
                gl.Translate(0,0,-1.5f);
                gl.Rotate(my_zRotation, 0.0f, 0.0f, 1.0f);
                gl.Scale(m_xScaling, m_yScaling, m_zScaling);//显示缩放量
                if (newgl.m_group.groupFlag != 0)
                {

                    if (newgl.m_group.groupFlag == 9)     //导弹
                    {
                        gl.Translate(0, 0.18, -0.09);
                    }
                    if (newgl.m_group.groupFlag == 2)  //后起落架
                    {
                        //gl.Translate(0, 0.12, -0.15);
                        gl.Translate(0, 0.12, -0.17);
                        //gl.Rotate(rotateAngle, 1, 0, 0);
                        //gl.Translate(0, 0, 0.075);
                        gl.Translate(newgl.m_group.TransXYZ[0], newgl.m_group.TransXYZ[1], newgl.m_group.TransXYZ[2]);
                        gl.Rotate(newgl.m_group.Angle, newgl.m_group.RotXYZ[0], newgl.m_group.RotXYZ[1], newgl.m_group.RotXYZ[2]);
                        gl.Translate(-newgl.m_group.TransXYZ[0], -newgl.m_group.TransXYZ[1], -newgl.m_group.TransXYZ[2]);
                    }
                    if (newgl.m_group.groupFlag == 18)  //后轮
                    {
                        //gl.Translate(0, 0.12, -0.18);
                        gl.Translate(0, 0.12, -0.20);
                        //gl.Rotate(45, 1, 1, 1);
                        gl.Translate(newgl.m_group.TransXYZ[0], newgl.m_group.TransXYZ[1], newgl.m_group.TransXYZ[2]);
                        gl.Rotate(newgl.m_group.Angle, newgl.m_group.RotXYZ[0], newgl.m_group.RotXYZ[1], newgl.m_group.RotXYZ[2]);
                        gl.Translate(-newgl.m_group.TransXYZ[0], -newgl.m_group.TransXYZ[1], -newgl.m_group.TransXYZ[2]);
                    }
                    if (newgl.m_group.groupFlag == 6)  //方向舵（大小不匹配）
                    {
                        gl.Translate(0, 0.452, 0.05);
                        gl.Translate(newgl.m_group.TransXYZ[0], newgl.m_group.TransXYZ[1], newgl.m_group.TransXYZ[2]);
                        gl.Rotate(newgl.m_group.Angle, newgl.m_group.RotXYZ[0], newgl.m_group.RotXYZ[1], newgl.m_group.RotXYZ[2]);
                        gl.Translate(-newgl.m_group.TransXYZ[0], -newgl.m_group.TransXYZ[1], -newgl.m_group.TransXYZ[2]);
                    }
                    if (newgl.m_group.groupFlag == 3)  //减速板
                    {
                        // gl.Rotate(-45, 1, 0, 0);//可增加条件判断是否需要旋转
                        gl.Translate(0, 0.08, -0.026);
                        // gl.Rotate(45, 1, 0, 0);
                        gl.Translate(newgl.m_group.TransXYZ[0], newgl.m_group.TransXYZ[1], newgl.m_group.TransXYZ[2]);
                        gl.Rotate(newgl.m_group.Angle, newgl.m_group.RotXYZ[0], newgl.m_group.RotXYZ[1], newgl.m_group.RotXYZ[2]);
                        gl.Translate(-newgl.m_group.TransXYZ[0], -newgl.m_group.TransXYZ[1], -newgl.m_group.TransXYZ[2]);
                    }
                    if (newgl.m_group.groupFlag == 7) //左平尾
                    {
                        gl.Translate(-0.1, 0.495, -0.095);
                        gl.Translate(newgl.m_group.TransXYZ[0], newgl.m_group.TransXYZ[1], newgl.m_group.TransXYZ[2]);
                        gl.Rotate(newgl.m_group.Angle, newgl.m_group.RotXYZ[0], newgl.m_group.RotXYZ[1], newgl.m_group.RotXYZ[2]);
                        gl.Translate(-newgl.m_group.TransXYZ[0], -newgl.m_group.TransXYZ[1], -newgl.m_group.TransXYZ[2]);
                    }
                    if (newgl.m_group.groupFlag == 8)   //右平尾
                    {
                        gl.Translate(0.1, 0.495, -0.095);
                        gl.Translate(newgl.m_group.TransXYZ[0], newgl.m_group.TransXYZ[1], newgl.m_group.TransXYZ[2]);
                        gl.Rotate(newgl.m_group.Angle, newgl.m_group.RotXYZ[0], newgl.m_group.RotXYZ[1], newgl.m_group.RotXYZ[2]);
                        gl.Translate(-newgl.m_group.TransXYZ[0], -newgl.m_group.TransXYZ[1], -newgl.m_group.TransXYZ[2]);
                    }
                    if (newgl.m_group.groupFlag == 1)  //前起落架
                    {
                        //gl.Translate(0, -0.1,-0.1399);
                        gl.Translate(0, -0.1, -0.13);
                        //gl.Translate(newgl.m_group.TransXYZ[0], newgl.m_group.TransXYZ[1], newgl.m_group.TransXYZ[2]);
                        gl.Rotate(newgl.m_group.Angle, newgl.m_group.RotXYZ[0], newgl.m_group.RotXYZ[1], newgl.m_group.RotXYZ[2]);
                        //gl.Translate(-newgl.m_group.TransXYZ[0], -newgl.m_group.TransXYZ[1], -newgl.m_group.TransXYZ[2]);
                        gl.Translate(0, 0, -0.05);
                    }
                    if (newgl.m_group.groupFlag == 17)  //前轮
                    {
                        //gl.Translate(0, -0.1, -0.18);
                        gl.Translate(0, -0.1, -0.22);
                        gl.Translate(newgl.m_group.TransXYZ[0], newgl.m_group.TransXYZ[1], newgl.m_group.TransXYZ[2]);
                        gl.Rotate(newgl.m_group.Angle, newgl.m_group.RotXYZ[0], newgl.m_group.RotXYZ[1], newgl.m_group.RotXYZ[2]);
                        gl.Translate(-newgl.m_group.TransXYZ[0], -newgl.m_group.TransXYZ[1], -newgl.m_group.TransXYZ[2]);

                        //gl.Translate(0, 0.01, -0.02);

                        gl.Rotate(newgl.getfrontwheelangle(), 1, 0, 1);
                    }
                    if (newgl.m_group.groupFlag == 10)  //座舱
                    {
                        gl.Translate(0, -0.14, -0.04);
                    }
                    if (newgl.m_group.groupFlag == 12)  //机尾（大小不匹配）
                    {
                        gl.Translate(0, 0.48, -0.086);

                    }
                    if (newgl.m_group.groupFlag == 14)  //右侧前襟
                    {

                        gl.Translate(0.18, 0.11, -0.088);
                        // gl.Rotate(45, 0.15, 0.1, 0);
                        gl.Translate(newgl.m_group.TransXYZ[0], newgl.m_group.TransXYZ[1], newgl.m_group.TransXYZ[2]);
                        gl.Rotate(newgl.m_group.Angle, newgl.m_group.RotXYZ[0], newgl.m_group.RotXYZ[1], newgl.m_group.RotXYZ[2]);
                        gl.Translate(-newgl.m_group.TransXYZ[0], -newgl.m_group.TransXYZ[1], -newgl.m_group.TransXYZ[2]);

                    }
                    if (newgl.m_group.groupFlag == 13)  //左侧前襟
                    {
                        gl.Translate(-0.18, 0.11, -0.088);
                        //gl.Rotate(-45, -0.18, 0.12, 0);
                        gl.Translate(newgl.m_group.TransXYZ[0], newgl.m_group.TransXYZ[1], newgl.m_group.TransXYZ[2]);
                        gl.Rotate(newgl.m_group.Angle, newgl.m_group.RotXYZ[0], newgl.m_group.RotXYZ[1], newgl.m_group.RotXYZ[2]);
                        gl.Translate(-newgl.m_group.TransXYZ[0], -newgl.m_group.TransXYZ[1], -newgl.m_group.TransXYZ[2]);

                    }
                    if (newgl.m_group.groupFlag == 16)  //右后襟
                    {
                        gl.Translate(0.131, 0.235, -0.088);
                        gl.Translate(newgl.m_group.TransXYZ[0], newgl.m_group.TransXYZ[1], newgl.m_group.TransXYZ[2]);
                        gl.Rotate(newgl.m_group.Angle, newgl.m_group.RotXYZ[0], newgl.m_group.RotXYZ[1], newgl.m_group.RotXYZ[2]);
                        gl.Translate(-newgl.m_group.TransXYZ[0], -newgl.m_group.TransXYZ[1], -newgl.m_group.TransXYZ[2]);
                    }
                    if (newgl.m_group.groupFlag == 15)   //左后襟
                    {
                        gl.Translate(-0.131, 0.235, -0.088);
                        gl.Translate(newgl.m_group.TransXYZ[0], newgl.m_group.TransXYZ[1], newgl.m_group.TransXYZ[2]);
                        gl.Rotate(newgl.m_group.Angle, newgl.m_group.RotXYZ[0], newgl.m_group.RotXYZ[1], newgl.m_group.RotXYZ[2]);
                        gl.Translate(-newgl.m_group.TransXYZ[0], -newgl.m_group.TransXYZ[1], -newgl.m_group.TransXYZ[2]);
                    }
                    if (newgl.m_group.groupFlag == 5)   //右副翼
                    {
                        gl.Translate(0.234, 0.2438, -0.088);
                        gl.Translate(newgl.m_group.TransXYZ[0], newgl.m_group.TransXYZ[1], newgl.m_group.TransXYZ[2]);
                        gl.Rotate(newgl.m_group.Angle, newgl.m_group.RotXYZ[0], newgl.m_group.RotXYZ[1], newgl.m_group.RotXYZ[2]);
                        gl.Translate(-newgl.m_group.TransXYZ[0], -newgl.m_group.TransXYZ[1], -newgl.m_group.TransXYZ[2]);

                    }
                    if (newgl.m_group.groupFlag == 4)   //左副翼
                    {
                        gl.Translate(-0.234, 0.2438, -0.088);
                        gl.Translate(newgl.m_group.TransXYZ[0], newgl.m_group.TransXYZ[1], newgl.m_group.TransXYZ[2]);
                        gl.Rotate(newgl.m_group.Angle, newgl.m_group.RotXYZ[0], newgl.m_group.RotXYZ[1], newgl.m_group.RotXYZ[2]);
                        gl.Translate(-newgl.m_group.TransXYZ[0], -newgl.m_group.TransXYZ[1], -newgl.m_group.TransXYZ[2]);
                    }
                    if (newgl.m_group.groupFlag == 19)   //平尾
                    {
                        gl.Translate(0, 0.45, -0.09);
                        //gl.Translate(newgl.m_group.TransXYZ[0], newgl.m_group.TransXYZ[1], newgl.m_group.TransXYZ[2]);
                        gl.Rotate(newgl.m_group.Angle, newgl.m_group.RotXYZ[0], newgl.m_group.RotXYZ[1], newgl.m_group.RotXYZ[2]);
                        //gl.Translate(-newgl.m_group.TransXYZ[0], -newgl.m_group.TransXYZ[1], -newgl.m_group.TransXYZ[2]);
                    }
                    //else
                    //{
                    //    gl.Translate(0, -0.5, 0.3);
                    //    //gl.Translate(newgl.m_group.TransXYZ[0], newgl.m_group.TransXYZ[1], newgl.m_group.TransXYZ[2]);
                    //    //if (newgl.m_group.Angle != 0.0)
                    //    //    gl.Rotate(newgl.m_group.Angle, newgl.m_group.RotXYZ[0], newgl.m_group.RotXYZ[1], newgl.m_group.RotXYZ[2]);
                    //    //gl.Translate(-newgl.m_group.TransXYZ[0], -newgl.m_group.TransXYZ[1], -newgl.m_group.TransXYZ[2]);
                    //}

                    gl.Color(newgl.m_group.color);
                    gl.Enable(OpenGL.NORMALIZE);
                    gl.Begin(OpenGL.TRIANGLES);
                    for (int n = 0; n < newgl.m_group.numFacets; n++)
                    {
                        //if (!mode)
                        // GLSNormalize(pGroup.facetIndices[n].normals);
                        gl.Normal(newgl.m_group.facetIndices[n].normals.norXYZ);//glNormal —— 设置当前法线数组
                        for (int j = 0; j < 3; j++)
                        {
                            gl.Vertex(newgl.m_group.facetIndices[n].vertices[j].veXYZ);
                        }
                    }
                    gl.End();


                    gl.PopMatrix();
                }

            }
        }
    }
}
