using System;
using System.Collections.Generic;
using System.Windows.Forms;
using XLuSharpLibrary.DbAccess;

namespace BHair
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //DbConfig.DbConnection = @"Data Source=.;Initial Catalog=BHairDB;User ID=sa;Password=123456";
            //DbConfig.DbType = DBType.SQLServer;
            DbConfig.DbConnection = "Server=localhost;Database=bhairdb;Uid=root;Pwd=1qazXSW@;CharSet = utf8;";
            DbConfig.DbType = DBType.Mysql;
            //Application.Run(new frmLogin());
            Application.Run(new frmMain());
        }
    }
}
