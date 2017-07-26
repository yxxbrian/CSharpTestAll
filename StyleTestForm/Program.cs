using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.IO;


namespace StyleTestForm
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            

            DevExpress.UserSkins.BonusSkins.Register();//进行皮肤组件注册
            DevExpress.UserSkins.OfficeSkins.Register();//进行皮肤组件注册
            DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.myskinnew).Assembly); //Register!
            DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.myskin).Assembly);
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            
            //Add
            if (!DevExpress.Skins.SkinManager.AllowFormSkins)
            {
                DevExpress.Skins.SkinManager.EnableFormSkins();
            }

            Application.Run(new Form1());
        }
    }
}
