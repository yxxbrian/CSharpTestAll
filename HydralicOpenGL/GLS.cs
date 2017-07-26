using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpGL;
using System.IO;

namespace planeGL
{
    public class GLS : structGl
    {

        OpenGL gl = new OpenGL();

        public GLSgroup GLSAddGroup(GLSgroup pGroup, string filename)
        {

            int n = 0;

            //GLSgroup pGroup = model.groups[n];

            FileStream fs = new FileStream(filename, FileMode.Open);
            BinaryReader fr = new BinaryReader(fs);
            if (!File.Exists(filename))
            {
                MessageBox.Show("打开STL文件失败!\n", "错误");

            }

            pGroup.name = null;
            pGroup.filename = null;

            pGroup.numVertices = 0;
            pGroup.numFacets = 0;
            pGroup.numNormals = 0;

            pGroup.RotXYZ = new float[3];
            pGroup.TransXYZ = new float[3];
            pGroup.axis = new GLSvertex();
            pGroup.axis.veXYZ = new float[3];
            pGroup.color = new float[3];
            for (int j = 0; j < 3; j++)
            {
                pGroup.RotXYZ[j] = pGroup.TransXYZ[j] = 0;
                pGroup.axis.veXYZ[j] = 0;
                pGroup.color[j] = 0;
            }
            pGroup.Angle = 0.0f;
            pGroup.groupFlag = FLAG_NONE;

            byte[] buf = new byte[80];

            buf = fr.ReadBytes(80);
            string strbuf = Encoding.Default.GetString(buf);
            string strbuf1 = strbuf.ToUpper();

            pGroup.name = string.Copy(strbuf1);
            pGroup.filename = pGroup.name + ".stl";

            if (string.Compare(pGroup.name, "FRONT_GEAR") == 0) { pGroup.groupFlag = FLAG_FRONT_GEAR; GLSColorSet(pGroup, 0.64, 0.64, 0.69); }
            else if (string.Compare(pGroup.name, "BACK_GEAR") == 0) { pGroup.groupFlag = FLAG_BACK_GEAR; GLSColorSet(pGroup, 0.64, 0.64, 0.69); }
            else if (string.Compare(pGroup.name, "DECELERATE") == 0) { pGroup.groupFlag = FLAG_DECELERATE; GLSColorSet(pGroup, 0.4824, 0.4824, 0.4824); }
            else if (string.Compare(pGroup.name, "LEFT_AILERON") == 0) { pGroup.groupFlag = FLAG_LEFT_AILERON; GLSColorSet(pGroup, 0.4824, 0.4824, 0.4824); }
            else if (string.Compare(pGroup.name, "RIGHT_AILERON") == 0) { pGroup.groupFlag = FLAG_RIGHT_AILERON; GLSColorSet(pGroup, 0.4824, 0.4824, 0.4824); }
            else if (string.Compare(pGroup.name, "LEFT_PINGWEI") == 0) { pGroup.groupFlag = FLAG_LEFT_PINGWEI; GLSColorSet(pGroup, 0.4824, 0.4824, 0.4824); }
            else if (string.Compare(pGroup.name, "RIGHT_PINGWEI") == 0) { pGroup.groupFlag = FLAG_RIGHT_PINGWEI; GLSColorSet(pGroup, 0.4824, 0.4824, 0.4824); }
            else if (string.Compare(pGroup.name, "MISSILE") == 0) { pGroup.groupFlag = FLAG_MISSILE; GLSColorSet(pGroup, 0.4824, 0.4824, 0.4824); }
            else if (string.Compare(pGroup.name, "HEAD") == 0) { pGroup.groupFlag = FLAG_HEAD; GLSColorSet(pGroup, 0.886, 0.984, 0.992); }//0.598
            //else if(string.Compare(pGroup.name,"COCKPIT") == 0){pGroup.groupFlag = FLAG_COCKPIT;GLSColorSet(pGroup,0.886,0.984,0.992);}
            else if (string.Compare(pGroup.name, "STLEXP OBJECT01") == 0) { pGroup.groupFlag = FLAG_COCKPIT; GLSColorSet(pGroup, 0.598, 0.598, 0.598); }//0.886,0.984,0.992
            else if (string.Compare(pGroup.name, "TAIL") == 0) { pGroup.groupFlag = FLAG_TAIL; GLSColorSet(pGroup, 0.4824, 0.4824, 0.4824); }//0.6627,0.6627,0.729);}
            else if (string.Compare(pGroup.name, "FRONT_WHEEL") == 0) { pGroup.groupFlag = FLAG_FRONT_WHEEL; GLSColorSet(pGroup, 0.0, 0.0, 0.0); }
            else if (string.Compare(pGroup.name, "BACK_WHEEL") == 0) { pGroup.groupFlag = FLAG_BACK_WHEEL; GLSColorSet(pGroup, 0.0, 0.0, 0.0); }
            else if (string.Compare(pGroup.name, "LEFT_FRONTJIN") == 0) { pGroup.groupFlag = FLAG_LEFT_FRONTJIN; GLSColorSet(pGroup, 0.4824, 0.4824, 0.4824); }
            else if (string.Compare(pGroup.name, "RIGHT_FRONTJIN") == 0) { pGroup.groupFlag = FLAG_RIGHT_FRONTJIN; GLSColorSet(pGroup, 0.4824, 0.4824, 0.4824); }
            else if (string.Compare(pGroup.name, "LEFT_BACKJIN") == 0) { pGroup.groupFlag = FLAG_LEFT_BACKJIN; GLSColorSet(pGroup, 0.4824, 0.4824, 0.4824); }
            else if (string.Compare(pGroup.name, "RIGHT_BACKJIN") == 0) { pGroup.groupFlag = FLAG_RIGHT_BACKJIN; GLSColorSet(pGroup, 0.4824, 0.4824, 0.4824); }
            else if (string.Compare(pGroup.name, "RUDDER1") == 0) { pGroup.groupFlag = FLAG_RUDDER1; GLSColorSet(pGroup, 0.4824, 0.4824, 0.4824); }
            else if (string.Compare(pGroup.name, "PINGWEI") == 0) { pGroup.groupFlag = FLAG_PINGWEI; GLSColorSet(pGroup, 0.4824, 0.4824, 0.4824); }
            else { pGroup.groupFlag = FLAG_NONE; GLSColorSet(pGroup, 0.494, 0.494, 0.494); };//else GLSColorSet(pGroup,0.2941,0.4824,0.6235);

            byte[] numface = new byte[4];
            //fs.Read(numface, 0, 4);
            numface = fr.ReadBytes(4);
            //int facecount = Convert.ToInt32(Encoding.Default.GetString(numface));
            uint facecount = BitConverter.ToUInt32(numface, 0);
            pGroup.numFacets = facecount;
            pGroup.facetIndices = new GLSfacet[facecount];//定义面元，facecount个

            pGroup.numNormals = pGroup.numFacets;
            pGroup.numVertices = pGroup.numFacets * 3;

            //pGroup.verNormals = new GLSnormal();
            //pGroup.vertices = new GLSvertex[pGroup.numVertices];
            //pGroup.normals = new GLSnormal[facecount];//这些在下面读的时候定义

            while (n < pGroup.numFacets)
            {
                byte[] vern = new byte[4];
                byte[] vert = new byte[12];

                pGroup.facetIndices[n].normals = new GLSnormal();
                pGroup.facetIndices[n].normals.norXYZ = new float[3];
                pGroup.facetIndices[n].vertices = new GLSvertex[3];
                pGroup.facetIndices[n].vertices[0].veXYZ = new float[3];
                pGroup.facetIndices[n].vertices[1].veXYZ = new float[3];
                pGroup.facetIndices[n].vertices[2].veXYZ = new float[3];

                for (int j = 0; j < 3; j++)
                {
                    vern = fr.ReadBytes(4);
                    pGroup.facetIndices[n].normals.norXYZ[j] = BitConverter.ToSingle(vern, 0);
                }
                for (int i = 0; i < 3; i++)
                {
                    vert = fr.ReadBytes(12);
                    //pGroup.facetIndices[n].vertices[i].veXYZ = new float[3];
                    pGroup.facetIndices[n].vertices[i].veXYZ[0] = BitConverter.ToSingle(vert, 0);
                    pGroup.facetIndices[n].vertices[i].veXYZ[1] = BitConverter.ToSingle(vert, 4);
                    pGroup.facetIndices[n].vertices[i].veXYZ[2] = BitConverter.ToSingle(vert, 8);

                }
                byte[] property = fr.ReadBytes(2);
                n++;

            }
            if (n != pGroup.numFacets)
                MessageBox.Show("fail close STL file!\n", "error");

            return pGroup;

        }


        //public void GLSRenderModel(GLSmodel model, bool mode)
        // {

        //         GLSgroup pGroup;

        //         int n, i;
        //         //n = pGroup.numFacets;
        //         for (i = 0; i < model.numGroups; i++)
        //         {
        //             pGroup = model.groups[i];

        //             if (pGroup.groupFlag != 0)
        //             {

        //                 gl.PushMatrix();

        //                 gl.Translate(pGroup.TransXYZ[0], pGroup.TransXYZ[1], pGroup.TransXYZ[2]);
        //                 if (pGroup.Angle != 0.0)
        //                     gl.Rotate(pGroup.Angle, pGroup.RotXYZ[0], pGroup.RotXYZ[1], pGroup.RotXYZ[2]);
        //                 gl.Translate(-pGroup.TransXYZ[0], -pGroup.TransXYZ[1], -pGroup.TransXYZ[2]);

        //                 gl.Color(pGroup.color);
        //                 //gl.Enable(OpenGL.GL_NORMALIZE);
        //                 gl.Begin(OpenGL.GL_TRIANGLES);
        //                 for (n = 0; n < pGroup.numFacets; n++)
        //                 {
        //                     //if (!mode)
        //                     GLSNormalize(pGroup.facetIndices[n].normals);
        //                     gl.Normal(pGroup.facetIndices[n].normals.norXYZ);//glNormal —— 设置当前法线数组
        //                     for (int j = 0; j < 3; j++)
        //                     {
        //                         gl.Vertex(pGroup.facetIndices[n].vertices[j].veXYZ);
        //                     }

        //                 }
        //                 gl.End();

        //                 gl.PopMatrix();
        //             }

        //     }

        // }
        ///////////////////////////////////////////////////////////////////////////////
        /* GLSMax: returns the maximum of two floats */
        public float GLSMax(float a, float b)
        {
            if (b > a)
                return b;
            return a;
        }

        /* GLSAbs: returns the absolute value of a float */
        public float GLSAbs(float f)
        {
            if (f < 0)
                return -f;
            return f;
        }


        ///////////////////////////////////////////////////////////////////////////////
        public void GLSUnitize(GLSgroup pGroup)
        {
            uint i, j;
            float maxx, minx, maxy, miny, maxz, minz;
            float cx, cy, cz, w, h, d;
            float scale;

            //pGroup.facetIndices[0].vertices[0].veXYZ = new float[3];
            maxx = minx = pGroup.facetIndices[0].vertices[0].veXYZ[0];
            maxy = miny = pGroup.facetIndices[0].vertices[0].veXYZ[1];
            maxz = minz = pGroup.facetIndices[0].vertices[0].veXYZ[2];

            j = pGroup.numFacets;
            for (; j > 0; j--)
            {
                /* get the max/mins */
                for (i = 0; i < 3; i++)
                {
                    // pGroup.facetIndices[j].vertices[i].veXYZ = new float[3];

                    if (maxx < pGroup.facetIndices[j - 1].vertices[i].veXYZ[0])
                        maxx = pGroup.facetIndices[j - 1].vertices[i].veXYZ[0];
                    if (minx > pGroup.facetIndices[j - 1].vertices[i].veXYZ[0])
                        minx = pGroup.facetIndices[j - 1].vertices[i].veXYZ[0];

                    if (maxy < pGroup.facetIndices[j - 1].vertices[i].veXYZ[1])
                        maxy = pGroup.facetIndices[j - 1].vertices[i].veXYZ[1];
                    if (miny > pGroup.facetIndices[j - 1].vertices[i].veXYZ[1])
                        miny = pGroup.facetIndices[j - 1].vertices[i].veXYZ[1];

                    if (maxz < pGroup.facetIndices[j - 1].vertices[i].veXYZ[2])
                        maxz = pGroup.facetIndices[j - 1].vertices[i].veXYZ[2];
                    if (minz > pGroup.facetIndices[j - 1].vertices[i].veXYZ[2])
                        minz = pGroup.facetIndices[j - 1].vertices[i].veXYZ[2];
                }

            }


            /* calculate model width, height, and depth */
            w = GLSAbs(maxx) + GLSAbs(minx);
            h = GLSAbs(maxy) + GLSAbs(miny);
            d = GLSAbs(maxz) + GLSAbs(minz);

            /* calculate center of the model */
            cx = (maxx + minx) / 2.0f;
            cy = (maxy + miny) / 2.0f;
            cz = (maxz + minz) / 2.0f;

            /* calculate unitizing scale factor */
            scale = 2.0f / GLSMax(GLSMax(w, h), d);

            /* translate around center then scale */
            //pGroup = model.groups;
            //j = model.numGroups;
            //while(j--!=0)
            //{
            switch (pGroup.groupFlag)
            {
                case FLAG_NONE:
                    {
                        break;
                    }
                case FLAG_FRONT_GEAR:
                    {
                        GLSAxisSet(pGroup, 0.0, 178.18, 10.3343);
                        GLSRotXYZSet(pGroup, 1.0, 0.0, 0.0);
                        break;
                    }
                case FLAG_FRONT_WHEEL:
                    {
                        GLSAxisSet(pGroup, 0.0, 178.18, 10.3343);
                        GLSRotXYZSet(pGroup, 1.0, 0.0, 0.0);
                        break;
                    }
                case FLAG_BACK_GEAR:
                    {
                        GLSAxisSet(pGroup, 0.0, 210.937, 10.8025);
                        GLSRotXYZSet(pGroup, 1.0, 0.0, 0.0);
                        break;
                    }
                case FLAG_BACK_WHEEL:
                    {
                        GLSAxisSet(pGroup, 0.0, 210.937, 10.8025);
                        GLSRotXYZSet(pGroup, 1.0, 0.0, 0.0);
                        break;
                    }
                case FLAG_DECELERATE:
                    {
                        GLSAxisSet(pGroup, -15.778, 199.706, 20.1817);
                        GLSRotXYZSet(pGroup, -1.0, 0.0, 0.0);
                        break;
                    }
                case FLAG_LEFT_AILERON:
                    {
                        GLSAxisSet(pGroup, -38.8143, 236.903, 13.097);
                        GLSRotXYZSet(pGroup, 19.9827, -2.88, 0.0);
                        break;
                    }
                case FLAG_RIGHT_AILERON:
                    {
                        GLSAxisSet(pGroup, 38.7966, 236.903, 13.097);
                        GLSRotXYZSet(pGroup, 19.9827, 2.88, 0.0);
                        break;
                    }
                case FLAG_LEFT_PINGWEI:
                    {
                        GLSAxisSet(pGroup, -13.234, 279.002, 13.3023);
                        GLSRotXYZSet(pGroup, 18.906, -1.1, 5.069);
                        break;
                    }
                case FLAG_RIGHT_PINGWEI:
                    {
                        GLSAxisSet(pGroup, 13.234, 279.002, 13.3023);
                        GLSRotXYZSet(pGroup, 18.906, 1.1, -5.069);
                        break;
                    }
                case FLAG_LEFT_FRONTJIN:
                    {
                        GLSAxisSet(pGroup, -58.797, 231.169, 13.18);
                        GLSRotXYZSet(pGroup, -41.593, 27.2, 0.0);
                        break;
                    }
                case FLAG_RIGHT_FRONTJIN:
                    {
                        GLSAxisSet(pGroup, 58.7969, 231.169, 13.18);
                        GLSRotXYZSet(pGroup, -41.593, -27.2, 0.0);
                        break;
                    }
                case FLAG_LEFT_BACKJIN:
                    {
                        GLSAxisSet(pGroup, -17.204, 233.988, 13.097);
                        GLSRotXYZSet(pGroup, 19.9827, -2.88, 0.0);
                        break;
                    }
                case FLAG_RIGHT_BACKJIN:
                    {
                        GLSAxisSet(pGroup, 38.7966, 236.903, 13.097);
                        GLSRotXYZSet(pGroup, 19.9827, 2.88, 0.0);
                        break;
                    }
                case FLAG_RUDDER1:
                    {
                        GLSAxisSet(pGroup, -0.7953, 269.703, 28.0818);
                        GLSRotXYZSet(pGroup, 0.0, 10.31, 30.1652);
                        break;
                    }
                //case FLAG_RUDDER2:
                case FLAG_PINGWEI:
                    {
                        GLSAxisSet(pGroup, 16.1113, 31.5466, -5.9);
                        GLSRotXYZSet(pGroup, 11.2203, 2.68, 0.0);
                        break;
                    }
            }
            int k, l;
            for (k = 0; k < pGroup.numFacets; k++)
            {
                for (l = 0; l < 3; l++)
                {
                    {
                        pGroup.facetIndices[k].vertices[l].veXYZ[0] -= cx;
                        pGroup.facetIndices[k].vertices[l].veXYZ[1] -= cy;
                        pGroup.facetIndices[k].vertices[l].veXYZ[2] -= cz;
                        pGroup.facetIndices[k].vertices[l].veXYZ[0] *= scale;
                        pGroup.facetIndices[k].vertices[l].veXYZ[1] *= scale;
                        pGroup.facetIndices[k].vertices[l].veXYZ[2] *= scale;
                    }
                }
            }
            pGroup.TransXYZ[0] = (pGroup.axis.veXYZ[0] - cx) * scale;
            pGroup.TransXYZ[1] = (pGroup.axis.veXYZ[1] - cy) * scale;
            pGroup.TransXYZ[2] = (pGroup.axis.veXYZ[2] - cz) * scale;


            //}


            //return scale;
        }
        ////////////////////////////////////////////////////////////////////////////////

        //public void GLSVertexNormal(GLSgroup pGroup, float angle)
        //{
        //    GLSnode** members;
        //    GLSnode node;
        //    int i;
        //    int numNormals;
        //    float dot,cos_angle;
        //    byte pos;
        //    GLSnormal average;
        //    cos_angle = (float)Math.Cos ((angle * PI / 180.0f));

        //    if(pGroup)
        //    {
        //        members = new GLSnode*[pGroup.numVertices];
        //        pGroup.verNormals = new GLSnormal[3 * pGroup.numFacets];
        //        for(i = 0; i < pGroup.numVertices; i++)
        //        {
        //             int n;
        //            members[i] = NULL;
        //            node = NULL;
        //            numNormals = 1;
        //            average.normals[0] = average.normals[1] = average.normals[2] = 0;
        //            for(n = 0; n < pGroup.numFacets; n++)
        //            {
        //                if(pos = VerInFacet(pGroup.vertices[i], pGroup.facetIndices[n]))
        //                {
        //                    node = new GLSnode();
        //                    node.facetIndex = n;
        //                    node.pos = pos - 1;
        //                    node.next = members[i];
        //                    members[i] = node;
        //                }
        //            }

        //            while(node)
        //            {
        //                dot = GLSdot(&pGroup.normals[node.facetIndex], 
        //                    &pGroup.normals[members[i].facetIndex]);
        //                if(dot > cos_angle)
        //                {
        //                    node.averaged = GL_TRUE;
        //                    average.normals[0] += pGroup.normals[node.facetIndex].normals[0];
        //                    average.normals[1] += pGroup.normals[node.facetIndex].normals[1];
        //                    average.normals[2] += pGroup.normals[node.facetIndex].normals[2];
        //                }
        //                else node.averaged = GL_FALSE;
        //                node = node.next;

        //            }

        //            node = members[i];
        //            while(node)
        //            {
        //                if(node.averaged)
        //                {
        //                    pGroup.verNormals[3 * node.facetIndex + node.pos] = average;			
        //                }
        //                else 
        //                {
        //                    pGroup.verNormals[3 * node.facetIndex + node.pos] 
        //                    = pGroup.normals[node.facetIndex];
        //                }
        //                node = node.next;

        //            }
        //        }

        //        for(i = 0; i < pGroup.numVertices; i++)
        //        {
        //            while(node = members[i])
        //            {
        //                members[i] = node.next;
        //                delete node;
        //            }
        //        }
        //        delete []members;
        //    }
        //}


        public int VerInFacet(GLSvertex vertex, GLSfacet facet)
        {
            for (int i = 0; i < 3; i++)
            {
                if (vertex.veXYZ[0] == facet.vertices[i].veXYZ[0]
                    && vertex.veXYZ[1] == facet.vertices[i].veXYZ[1]
                    && vertex.veXYZ[2] == facet.vertices[i].veXYZ[2])
                    return i + 1;
            }
            return 0;
        }
        public void GLSNormalize(GLSnormal normals)          //法向量归一化
        {
            float l;
            l = normals.norXYZ[0] * normals.norXYZ[0]
                + normals.norXYZ[1] * normals.norXYZ[1]
                + normals.norXYZ[2] * normals.norXYZ[2];
            l = (float)Math.Sqrt(l);
            normals.norXYZ[0] /= l;
            normals.norXYZ[1] /= l;
            normals.norXYZ[2] /= l;
        }
        public float GLSdot(GLSnormal normal1, GLSnormal normal2)
        {
            GLSNormalize(normal1);
            GLSNormalize(normal2);
            return normal1.norXYZ[0] * normal2.norXYZ[0]
                + normal1.norXYZ[1] * normal2.norXYZ[1]
                + normal1.norXYZ[2] * normal2.norXYZ[2];

        }

        ////////////////////////////////////////////////////////////////////////////////


        public void GLSColorSet(GLSgroup group, double r, double g, double b)
        {
            group.color[0] = (float)r;
            group.color[1] = (float)g;
            group.color[2] = (float)b;
        }

        public void GLSAxisSet(GLSgroup group, double x, double y, double z)        //转动轴心
        {
            group.axis.veXYZ[0] = (float)x;
            group.axis.veXYZ[1] = (float)y;
            group.axis.veXYZ[2] = (float)z;
        }

        public void GLSRotXYZSet(GLSgroup group, double x, double y, double z)
        {
            group.RotXYZ[0] = (float)x;
            group.RotXYZ[1] = (float)y;
            group.RotXYZ[2] = (float)z;
        }
    }
}
