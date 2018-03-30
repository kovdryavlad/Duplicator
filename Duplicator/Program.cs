using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Duplicator.BL;

namespace Duplicator
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
            
            MessageService messageService = new MessageService();
            DuplicatEditor DuplctEditor = new DuplicatEditor();
            MainForm MForm = new MainForm();

            MainPresenter presenter = new MainPresenter(MForm, messageService, DuplctEditor);

            Application.Run(MForm);
        }
    }
}
