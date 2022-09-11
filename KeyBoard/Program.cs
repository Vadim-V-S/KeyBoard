using KeyBoard.Model;
using KeyBoard.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyBoard
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm mainForm = new MainForm();
            EnKeyBoardBL keyBoard = new EnKeyBoardBL();
            MessegeService messegeService = new MessegeService();

            MainPresenter mainPresenter = new MainPresenter(mainForm, keyBoard, messegeService);

            Application.Run(mainForm);
        }
    }
}
