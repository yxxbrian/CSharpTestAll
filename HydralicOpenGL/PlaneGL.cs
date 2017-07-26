using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpGL;
//using SharpGL.WinForms;
using System.IO;

namespace planeGL
{
    public class PlaneGL : GLS
    {
        float m_fRudder2AngleDst;
        float m_fRudder1AngleDst;
        float m_fBJinAngleDst;
        float m_fRFJinAngleDst;
        float m_fLFJinAngleDst;
        float m_fRPingweiAngleDst;
        float m_fLPingweiAngleDst;
        float m_fRAileronAngleDst;
        float m_fLAileronAngleDst;
        int m_iDecelerateStat;
        bool m_bDecelerateDown;
        bool m_bDecelerateUp;
        int m_iGearStat;
        bool m_bGearUp;
        bool m_bGearDown;
        float m_fwheelAngleDst;  //前轮转向目标角度
        float m_fwheelAngle = 0.0f;     //前轮转向角度


        //void GroupProcessing();//旋转到目标位置处理函数
        //HDC m_hDC[NUMBER_OF_DEVICE];
        //OpenGLDevice m_OpenGLDevice[NUMBER_OF_DEVICE];

        float m_fTransX;
        float m_fTransY;
        float m_fTransZ;
        float m_fRotXDst;
        float m_fRotYDst;
        float m_fRotZDst;
        float m_zRotation;
        float m_yRotation;
        float m_xRotation;

        bool m_bSmooth;
        bool m_bFreeRotation;

        const int DRAW_FRAME = 1;
        const int STATE_GEAR_UP = 0;
        const int STATE_GEAR_DOWN = 1;
        const int STATE_DECELERATE_UP = 0;
        const int STATE_DECELERATE_DOWN = 1;
        OpenGL gl = new OpenGL();

        public GLSmodel m_glsModel = new GLSmodel();
        public GLSgroup m_group = new GLSgroup();

        public PlaneGL()
        {

            DataReset();
            DecelerateDown();
            InitGL();
        }

        public void DataReset()
        {
            m_fRudder2AngleDst = 0;
            m_fRudder1AngleDst = 0;
            m_fBJinAngleDst = 0;
            m_fRFJinAngleDst = 0;
            m_fLFJinAngleDst = 0;
            m_fRPingweiAngleDst = 0;
            m_fLPingweiAngleDst = 0;
            m_fRAileronAngleDst = 0;
            m_fLAileronAngleDst = 0;

            m_fwheelAngleDst = 0;

            m_iDecelerateStat = STATE_DECELERATE_UP;
            m_iGearStat = STATE_GEAR_DOWN;
            m_bDecelerateDown = false;
            m_bDecelerateUp = false;
            m_bGearUp = false;
            m_bGearDown = true;

            m_fTransX = 0;
            m_fTransY = 0;
            m_fTransZ = -1.5f;
            m_fRotXDst = 0;
            m_fRotYDst = 0;
            m_fRotZDst = 0;
            m_zRotation = 0;
            m_yRotation = 0;
            m_xRotation = 0;

            m_bSmooth = false;
            m_bFreeRotation = false;


        }
        public void InitGL()
        {
            m_glsModel.groups = new GLSgroup[19];
            m_glsModel.numGroups = 19;
            string[] stlFile = new string[19]{"\\plane\\导弹.stl","\\plane\\方向舵.stl","\\plane\\飞机.stl","\\plane\\飞机头.stl","\\plane\\后轮.stl","\\plane\\后起落架.stl",
			"\\plane\\减速板.stl","\\plane\\平尾.stl","\\plane\\前轮.stl","\\plane\\前起落架.stl","\\plane\\尾巴.stl","\\plane\\右侧前襟.stl","\\plane\\右副翼.stl",
			"\\plane\\右后襟.stl","\\plane\\右平尾.stl","\\plane\\左侧前襟.stl","\\plane\\左副翼.stl","\\plane\\左后襟.stl","\\plane\\左平尾.stl"};

            string path = Application.StartupPath;

            for (int j = 0; j < 19; j++)
            {
                stlFile[j] = path + stlFile[j];
            }

            for (int i = 0; i < 19; i++)
            {

                m_glsModel.groups[i] = GLSAddGroup(m_group, stlFile[i]);
                GLSUnitize(m_glsModel.groups[i]);
            }

            //SharpGLForm newSharpGLForm = new SharpGLForm();
            //newSharpGLForm.getgl(this);
            //Application.Run(newSharpGLForm);
            //ElectronicForm newSharpGLForm = new ElectronicForm();
            //newSharpGLForm.getgl(this);



        }

        public void RenderScene(uint index)                                                  //渲染、旋转
        //public  void RenderScene()
        {

            //SharpGLForm newSharpGLForm = new SharpGLForm();
            //newSharpGLForm.getgl(this);
            //Application.Run(newSharpGLForm);

            //gl.MatrixMode(OpenGL.GL_MODELVIEW);
            //gl.LoadIdentity();

            gl.Translate(m_fTransX, m_fTransY, m_fTransZ);
            gl.Rotate(-90, 0.0f, 1.0f, 0.0f);
            gl.Rotate(-90, 1.0f, 0.0f, 0.0f);
            //	glRotatef(90,0.0f,0.0f,1.0f);

            if (m_fRotXDst > 0) { m_xRotation++; if (--m_fRotXDst < 0) m_fRotXDst = 0.0f; }
            else if (m_fRotXDst < 0) { m_xRotation--; if (++m_fRotXDst > 0) m_fRotXDst = 0.0f; }
            if (m_fRotYDst > 0) { m_yRotation++; if (--m_fRotYDst < 0) m_fRotYDst = 0.0f; }
            else if (m_fRotYDst < 0) { m_yRotation--; if (++m_fRotYDst > 0) m_fRotYDst = 0.0f; }
            if (m_fRotZDst > 0) { m_zRotation++; if (--m_fRotZDst < 0) m_fRotZDst = 0.0f; }
            else if (m_fRotZDst < 0) { m_zRotation--; if (++m_fRotZDst > 0) m_fRotZDst = 0.0f; }

            gl.Rotate(m_xRotation, 1.0f, 0.0f, 0.0f);
            gl.Rotate(m_yRotation, 0.0f, 1.0f, 0.0f);
            gl.Rotate(m_zRotation, 0.0f, 0.0f, 1.0f);

            if (m_bFreeRotation)
            {
                m_xRotation += 2;
                m_yRotation++;
            }
            //gl.ClearColor(0.51f, 0.71f, 0.9f, 0.0f);
            //// Clear the screen and the depth buffer
            //gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            //gl.Color(0.5f, 0.5f, 0.5f);

            //if (m_glsModel.numGroups != 0)
            //{
            //    for (int i = 0; i < m_glsModel.numGroups; i++)
            //    {
            //        GroupProcessing(m_glsModel.groups[i]);
            //    }
            //    //GLSRenderModel(m_glsModel, m_bSmooth);
            //}
            //////////////////////////////////////////////////////////////////////////////////////    
            // Swap the virtual screens

            // Winapi.SwapBuffers(index);

        }



        public void GroupProcessing(ref GLSgroup group)
        {
            //GLSgroup group;
            //group = m_glsModel.groups[group.groupFlag];
            //while (group)
            //{
            switch (group.groupFlag)
            {
                case FLAG_NONE: break;
                case FLAG_FRONT_GEAR:                                       //前起落架
                    {
                        if (m_bGearUp && m_iGearStat == STATE_GEAR_DOWN)//
                        {
                            if (group.Angle < 90) group.Angle += 0.1f;
                            else
                            {
                                m_bGearUp = false;
                                m_iGearStat = STATE_GEAR_UP;
                            }
                        }
                        if (m_bGearDown && m_iGearStat == STATE_GEAR_UP)
                        {
                            if (group.Angle > 0) group.Angle -= 0.1f;
                            else
                            {
                                m_bGearDown = false;
                                m_iGearStat = STATE_GEAR_DOWN;
                            }
                        }
                        break;
                    }
                case FLAG_BACK_GEAR:                                         //后起落架
                    {
                        if (m_bGearUp && m_iGearStat == STATE_GEAR_DOWN)//
                        {
                            if (group.Angle < 90) group.Angle += 0.1f;
                            else
                            {
                                m_bGearUp = false;
                                m_iGearStat = STATE_GEAR_UP;
                            }
                        }
                        if (m_bGearDown && m_iGearStat == STATE_GEAR_UP)
                        {
                            if (group.Angle > 0) group.Angle -= 0.1f;
                            else
                            {
                                m_bGearDown = false;
                                m_iGearStat = STATE_GEAR_DOWN;
                            }
                        }
                        break;
                    }
                case FLAG_FRONT_WHEEL:                                      //前轮
                    {
                        if (m_bGearUp && m_iGearStat == STATE_GEAR_DOWN)//
                        {
                            if (group.Angle < 90) group.Angle += 0.1f;
                            else
                            {
                                m_bGearUp = false;
                                m_iGearStat = STATE_GEAR_UP;
                            }
                        }
                        if (m_bGearDown && m_iGearStat == STATE_GEAR_UP)
                        {
                            if (group.Angle > 0) group.Angle -= 0.1f;
                            else
                            {
                                m_bGearDown = false;
                                m_iGearStat = STATE_GEAR_DOWN;
                            }
                        }
                        if (Math.Abs(m_fwheelAngleDst - m_fwheelAngle) < 0.1f) m_fwheelAngle = m_fwheelAngleDst;    //前轮转向
                        else if (m_fwheelAngleDst > m_fwheelAngle) m_fwheelAngle += 0.1f;
                        else if (m_fwheelAngleDst < m_fwheelAngle) m_fwheelAngle -= 0.1f;
                        break;
                    }
                case FLAG_BACK_WHEEL:                                           //后轮
                    {
                        if (m_bGearUp && m_iGearStat == STATE_GEAR_DOWN)//
                        {
                            if (group.Angle < 90) group.Angle += 0.1f;
                            else
                            {
                                m_bGearUp = false;
                                m_iGearStat = STATE_GEAR_UP;
                            }
                        }
                        if (m_bGearDown && m_iGearStat == STATE_GEAR_UP)
                        {
                            if (group.Angle > 0) group.Angle -= 0.1f;
                            else
                            {
                                m_bGearDown = false;
                                m_iGearStat = STATE_GEAR_DOWN;
                            }
                        }
                        break;
                    }
                case FLAG_DECELERATE:                                                           //减速板
                    {
                        if (m_bDecelerateUp && (m_iDecelerateStat == STATE_DECELERATE_DOWN))
                        {
                            if (group.Angle > 0) group.Angle -= 0.1f;
                            else
                            {
                                m_bDecelerateUp = false;
                                m_iDecelerateStat = STATE_DECELERATE_UP;
                            }
                        }
                        if (m_bDecelerateDown && (m_iDecelerateStat == STATE_DECELERATE_UP))
                        {
                            if (group.Angle < 30) group.Angle += 0.1f;
                            else
                            {
                                m_bDecelerateDown = false;
                                m_iDecelerateStat = STATE_DECELERATE_DOWN;
                            }
                        }
                        break;
                    }
                case FLAG_LEFT_AILERON:                                                             //左副翼
                    {
                        if (Math.Abs(m_fLAileronAngleDst - group.Angle) < 0.1f) group.Angle = m_fLAileronAngleDst;
                        else if (m_fLAileronAngleDst > group.Angle) group.Angle += 0.1f;
                        else if (m_fLAileronAngleDst < group.Angle) group.Angle -= 0.1f;
                        break;
                    }
                case FLAG_RIGHT_AILERON:                                                            //右副翼
                    {
                        if (Math.Abs(m_fRAileronAngleDst - group.Angle) < 0.1f) group.Angle = m_fRAileronAngleDst;
                        else if (m_fRAileronAngleDst > group.Angle) group.Angle += 0.1f;
                        else if (m_fRAileronAngleDst < group.Angle) group.Angle -= 0.1f;
                        break;
                    }
                case FLAG_LEFT_PINGWEI:                                                              //左平尾
                    {
                        if (Math.Abs(m_fLPingweiAngleDst - group.Angle) < 0.1f) group.Angle = m_fLPingweiAngleDst;
                        else if (m_fLPingweiAngleDst > group.Angle) group.Angle += 0.1f;
                        else if (m_fLPingweiAngleDst < group.Angle) group.Angle -= 0.1f;
                        break;
                    }
                case FLAG_RIGHT_PINGWEI:                                                                //右平尾
                    {
                        if (Math.Abs(m_fRPingweiAngleDst - group.Angle) < 0.1f) group.Angle = m_fRPingweiAngleDst;
                        else if (m_fRPingweiAngleDst > group.Angle) group.Angle += 0.1f;
                        else if (m_fRPingweiAngleDst < group.Angle) group.Angle -= 0.1f;
                        break;
                    }
                case FLAG_LEFT_FRONTJIN:                                                                //左前襟
                    {
                        if (Math.Abs(m_fLFJinAngleDst - group.Angle) < 0.1f) group.Angle = m_fLFJinAngleDst;
                        else if (m_fLFJinAngleDst > group.Angle) group.Angle += 0.1f;
                        else if (m_fLFJinAngleDst < group.Angle) group.Angle -= 0.1f;
                        break;
                    }
                case FLAG_RIGHT_FRONTJIN:                                                               //右前襟
                    {
                        if (Math.Abs(m_fRFJinAngleDst - group.Angle) < 0.1f) group.Angle = m_fRFJinAngleDst;
                        else if (m_fRFJinAngleDst > group.Angle) group.Angle += 0.1f;
                        else if (m_fRFJinAngleDst < group.Angle) group.Angle -= 0.1f;
                        break;
                    }
                case FLAG_LEFT_BACKJIN:                                                                 //左后襟
                    {
                        if (Math.Abs(m_fBJinAngleDst - group.Angle) < 0.1f) group.Angle = m_fBJinAngleDst;
                        else if (m_fBJinAngleDst > group.Angle) group.Angle += 0.1f;
                        else if (m_fBJinAngleDst < group.Angle) group.Angle -= 0.1f;
                        break;
                    }
                case FLAG_RIGHT_BACKJIN:                                                                //右后襟
                    {
                        if (Math.Abs(m_fBJinAngleDst - group.Angle) < 0.1f) group.Angle = m_fBJinAngleDst;
                        else if (m_fBJinAngleDst > group.Angle) group.Angle += 0.1f;
                        else if (m_fBJinAngleDst < group.Angle) group.Angle -= 0.1f;
                        break;
                    }
                case FLAG_RUDDER1:                                                                      //方向舵
                    {
                        if (Math.Abs(m_fRudder1AngleDst - group.Angle) < 0.1f) group.Angle = m_fRudder1AngleDst;
                        else if (m_fRudder1AngleDst > group.Angle) group.Angle += 0.1f;
                        else if (m_fRudder1AngleDst < group.Angle) group.Angle -= 0.1f;
                        break;
                    }
                //case FLAG_RUDDER2:
                case FLAG_PINGWEI:                                                                     //平尾
                    {
                        if (Math.Abs(m_fRudder2AngleDst - group.Angle) < 2) group.Angle = m_fRudder2AngleDst;
                        else if (m_fRudder2AngleDst > group.Angle) group.Angle += 2;
                        else if (m_fRudder2AngleDst < group.Angle) group.Angle -= 2;
                        break;
                    }
                default: { break; }
            }

        }

        public void freerotation()
        {
            if (m_fRotXDst > 0) { m_xRotation++; if (--m_fRotXDst < 0) m_fRotXDst = 0.0f; }
            else if (m_fRotXDst < 0) { m_xRotation--; if (++m_fRotXDst > 0) m_fRotXDst = 0.0f; }
            if (m_fRotYDst > 0) { m_yRotation++; if (--m_fRotYDst < 0) m_fRotYDst = 0.0f; }
            else if (m_fRotYDst < 0) { m_yRotation--; if (++m_fRotYDst > 0) m_fRotYDst = 0.0f; }
            if (m_fRotZDst > 0) { m_zRotation++; if (--m_fRotZDst < 0) m_fRotZDst = 0.0f; }
            else if (m_fRotZDst < 0) { m_zRotation--; if (++m_fRotZDst > 0) m_fRotZDst = 0.0f; }

            gl.Rotate(m_xRotation, 1.0f, 0.0f, 0.0f);
            gl.Rotate(m_yRotation, 0.0f, 1.0f, 0.0f);
            gl.Rotate(m_zRotation, 0.0f, 0.0f, 1.0f);

            if (m_bFreeRotation)
            {
                m_xRotation += 2;
                m_yRotation++;
            }
        }

        public void GearDown()
        {
            if (m_iGearStat == STATE_GEAR_UP)
            {
                m_bGearDown = true;
                m_bGearUp = false;
            }
        }
        public void GearUp()
        {
            if (m_iGearStat == STATE_GEAR_DOWN)
            {
                m_bGearDown = false;
                m_bGearUp = true;
            }
        }

        public void DecelerateDown()
        {
            if (m_iDecelerateStat == STATE_DECELERATE_UP)
            {
                m_bDecelerateDown = true;
                m_bDecelerateUp = false;
            }
        }

        public void DecelerateUp()
        {
            if (m_iDecelerateStat == STATE_DECELERATE_DOWN)
            {
                m_bDecelerateDown = false;
                m_bDecelerateUp = true;
            }
        }

        public void LeftAileronRotation(float AngleDst)
        {
            m_fLAileronAngleDst = AngleDst;
        }

        public void RightAileronRotation(float AngleDst)
        {
            m_fRAileronAngleDst = AngleDst;
        }

        public void LeftPingweiRotation(float AngleDst)
        {
            m_fLPingweiAngleDst = AngleDst;
        }

        public void RightPingweiRotation(float AngleDst)
        {
            m_fRPingweiAngleDst = AngleDst;
        }

        public void EnableFreeRotate(bool FreeRotation)
        {
            m_bFreeRotation = FreeRotation;
        }

        public void Rotation(float x, float y, float z)
        {
            m_xRotation += x;
            m_yRotation += y;
            m_zRotation += z;
        }

        public void LeftFrontJinRotation(float AngleDst)
        {
            m_fLFJinAngleDst = AngleDst;
        }

        public void RightFrontJinRotation(float AngleDst)
        {
            m_fRFJinAngleDst = AngleDst;
        }

        public void BackJinRotation(float AngleDst)
        {
            m_fBJinAngleDst = AngleDst;
        }

        public void Rudder1Rotation(float AngleDst)
        {
            m_fRudder1AngleDst = AngleDst;
        }

        public void Rudder2Rotation(float AngleDst)
        {
            m_fRudder2AngleDst = AngleDst;
        }

        public void FrontWheelRotation(float AngleDst)//前轮转向
        {
            m_fwheelAngleDst = AngleDst;
        }

        public float getfrontwheelangle()
        {
            return m_fwheelAngle;
        }

        public void RotationDest(float x, float y, float z)
        {
            m_fRotXDst = x;
            m_fRotYDst = y;
            m_fRotZDst = z;
        }

        public void TranslateXYZ(float x, float y, float z)
        {
            m_fTransX += x;
            if ((m_fTransZ < -0.7 && z > 0) || (m_fTransZ > -5.8 && z < 0))
            { m_fTransZ += z; }
        }

        public void Center(int cx, int cy)
        {
            gl.Viewport(0, 0, cx, cy);
            gl.MatrixMode(OpenGL.PROJECTION);
            gl.LoadIdentity();

            // Calculate The Aspect Ratio Of The Window
            gl.Perspective(60.0f, (float)cx / (float)cy, 0.1f, 1000.0f);
            gl.MatrixMode(OpenGL.MODELVIEW);
            gl.LoadIdentity();
            gl.LookAt(
            0.0, 0.0, 2000.0, // eye location
            0.0, 0.0, 0.0, // center location
            0.0, 1.0, 0.0);// up vector
        }
    }
}
