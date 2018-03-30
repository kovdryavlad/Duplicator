using System;
using System.Windows.Forms;

namespace Duplicator.Autorization
{
    public interface IAutorizationView
    {
        Uri BrowserUrl { get; set; }
        
        //события
        event EventHandler AutorizationFormLoad;    //форма загружена
        event EventHandler DocCompleted;            //Документ браузера загружен
    }

    public partial class AutorizationForm : Form, IAutorizationView
    {
        public AutorizationForm()
        {
            InitializeComponent();

            this.Load += AutorizationForm_Load;
            Browser.DocumentCompleted += Browser_DocumentCompleted;
        }

        //реализация интерфейса IAutorizationForm
        public Uri BrowserUrl 
        { 
            get { return Browser.Url; }
            set { Browser.Url = value; } 
        }
    
        //проброс событий
        public event EventHandler AutorizationFormLoad;
        public event EventHandler DocCompleted;


        void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (DocCompleted != null)
                DocCompleted(this, EventArgs.Empty);
        }

        void AutorizationForm_Load(object sender, EventArgs e)
        {
            if (AutorizationFormLoad != null)
                AutorizationFormLoad(this, EventArgs.Empty);
        }

    
    }
}
