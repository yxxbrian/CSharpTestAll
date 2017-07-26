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
    public class structGl
    {
        //public 
        public struct GLSvertex
        {
            public float[] veXYZ;

        }

        public struct GLSnormal
        {
            public float[] norXYZ;
        }

        public struct GLSfacet
        {
            public GLSvertex[] vertices;
            public GLSnormal normals;			//法线数组

        }

        public struct GLSgroup
        {
            public string name;
            public string filename;
            public int groupFlag;
            public uint numVertices;			//顶点个数
            public uint numFacets;           //面元个数
            public uint numNormals;			//法线个数

            //public GLSvertex[] vertices;		//顶点数组
            //public GLSnormal verNormals ;		//顶点法线 
            public GLSfacet[] facetIndices;		//面元索引           
            //public GLSnormal[] normals ;			//法线数组
            public GLSvertex axis;				//转动的轴心 

            public float[] RotXYZ;
            public float Angle;
            public float[] TransXYZ;
            public float[] color;


        }


        public struct GLSmodel
        {
            public int numGroups;			//组个数
            public GLSgroup[] groups;
        }

        public const float PI = 3.1415926f;

        //const GLS_NONE		(0)				/* render with only vertices */
        //const GLS_FLAT		(1 << 0)		/* render with facet normals */
        //const GLS_SMOOTH		(1 << 1)		/* render with vertex normals */
        //const GLS_COLOR		(1 << 3)		/* render with colors */

        public const int FLAG_NONE = 0;
        public const int FLAG_FRONT_GEAR = 1;
        public const int FLAG_BACK_GEAR = 2;
        public const int FLAG_DECELERATE = 3;
        public const int FLAG_LEFT_AILERON = 4;
        public const int FLAG_RIGHT_AILERON = 5;
        public const int FLAG_RUDDER1 = 6;
        public const int FLAG_LEFT_PINGWEI = 7;
        public const int FLAG_RIGHT_PINGWEI = 8;
        public const int FLAG_MISSILE = 9;
        public const int FLAG_HEAD = 10;
        public const int FLAG_COCKPIT = 11;
        //public const int STLEXP_Object01        =11;
        public const int FLAG_TAIL = 12;
        public const int FLAG_LEFT_FRONTJIN = 13;
        public const int FLAG_RIGHT_FRONTJIN = 14;
        public const int FLAG_LEFT_BACKJIN = 15;
        public const int FLAG_RIGHT_BACKJIN = 16;
        public const int FLAG_FRONT_WHEEL = 17;
        public const int FLAG_BACK_WHEEL = 18;
        public const int FLAG_PINGWEI = 19;

    }
}
