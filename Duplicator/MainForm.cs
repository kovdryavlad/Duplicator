using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Duplicator.BL;

namespace Duplicator
{
    public interface IMainForm
    {
        //ввод
        int Interval { get; }                           //интервал при отложении публикации
        List<PostInUIList> PostList { get; set; }       //посты в таблице
        string InputPostLink { get; }                   //ссылка на входной пост
        DateTime PublicationDate { get; }               //дата публикации
        bool CopyRadio { get; }                         //Переключатель установлен в "Копия"
        bool RepostRadio { get; }                       //Переключатель установлен в "Репост"

        //вывод
        string AboutPost { set; get; }                  //место для вывода информации о посте
        string statusText { set; }                      //текст в строке состояния

        //методы
        void SetGreenColorOnRow(int rowNumber);         //установить цвет строки таблицы в зеленый
        void SetYellowColorOnRow(int rowNumber);        //установить цвет строки таблицы в желтый
        void SetColorOnRow(int rowNumber, Color color); //установить цвет строки таблицы в произвольный цвет.
        void SetColorOfStatusText(Color color);         //установить цвет текста в строке состояния
        
        void AddMessageToLogger(string message);        //добавить новое сообщение в логгер

        void ActivatePublishPutton();                   //активировать кнопку "Обупликовать"
        void DeActivatePublishPutton();                 //деактивировать кнопку "Обупликовать"
        void ClearLogger();                             //Очистить отладчик
        void ClearForm();                               //Очистить форму

        //события
        event EventHandler ViewLoad;                    //форма загружена
        event EventHandler AnalizeClick;                //Клик по Анализу
        event EventHandler PublishClick;                //Кнопка опубликовать  
        event EventHandler ClearFormClick;              //Клик по очистке формы
        event EventHandler AutorizationClick;           //Клик по авторизиции
        event EventHandler SaveTemplateClick;           //Сохранить шаблон
        event EventHandler LoadTemplateClick;           //Загрузить шаблон
    }

    public partial class MainForm : Form, IMainForm
    {
        List<PostInUIList> _postList = new List<PostInUIList>();

        public MainForm()
        {
            InitializeComponent();

            //добавление обработчиков событий
            Load += MainForm_Load;
            PublicButton.Click += PublicButton_Click;
            AutorizeButton.Click += AutorizeButton_Click;
            AnalizeButton.Click += MainForm_AnalizeClick; 
            FormClearButton.Click += FormClearButton_Click;
            SaveTemplateButton.Click += SaveTemplateButton_Click;
            LoadTemplateButton.Click += LoadTemplateButton_Click;
        }


        #region Реализация интерфейса IMainForm

        //ввод
        public int Interval
        {
            get { return (int)IntervalUpDown.Value; }
        }

        public List<PostInUIList> PostList
        {
            get 
            {
                _postList.Clear();
                FromTableToList();

                return _postList;
            }

            set 
            {
                foreach (var item in value)
                    PostsDataGridView.Rows.Add(item.FullGroupLink, item.PublicationTime);
            }
        }
        
        public string InputPostLink 
        {
            get { return LinkRichTextBox.Text; } 
        }   
      
        public DateTime PublicationDate 
        {
            get { return dateTimePickerOnForm.Value; } 
        }

        public bool CopyRadio { get { return CopyRadioButton.Checked; } }

        public bool RepostRadio { get { return RepostRadioButton.Checked; } }
        //вывод
        public string Logger 
        {
            set { ResultRichTextBox.Text = value; }
        }
        
        public string AboutPost 
        {
            set { DataAboutPostLabel.Text = value; }
            get { return DataAboutPostLabel.Text; }
        }
        
        public string statusText 
        {
            set { StatusLabel.Text = value; } 
        }

        //методы
        public void SetGreenColorOnRow(int rowNumber)
        {
            SetColorOnRow(rowNumber, Color.PaleGreen);  
        }

        public void SetYellowColorOnRow(int rowNumber)
        {
            SetColorOnRow(rowNumber, Color.PaleGoldenrod);  
        }

        public void SetColorOnRow(int rowNumber, Color color)
        {
            PostsDataGridView.Rows[rowNumber].Cells[0].Style.BackColor = color;

            PostsDataGridView.Rows[rowNumber].Cells[1].Style.BackColor = color;
        }

        public void SetColorOfStatusText(Color color)
        {
            StatusLabel.ForeColor = color;
        }

        public void AddMessageToLogger(string message)
        {
            ResultRichTextBox.Text += String.Format("{0}\n", message);
        }

        public void ActivatePublishPutton()
        {
            PublicButton.Enabled  = true;
        }
        public void DeActivatePublishPutton() 
        {
            PublicButton.Enabled = false; 
        }

        public void ClearForm()
        {
            ResultRichTextBox.Clear();
            DataAboutPostLabel.Text = "";
            PostsDataGridView.Rows.Clear();
            LinkRichTextBox.Clear();
        }

        public void ClearLogger() 
        {
            Logger = "";
        }

        #endregion

        #region Внутренние функции формы

        //заполняет поле _postList парами (ссылка на группу - время публикации)
        void FromTableToList()
        {
            for (int i = 0; i < PostsDataGridView.RowCount-1; i++)
            {
                string groupLink = PostsDataGridView.Rows[i].Cells[0].Value.ToString();

                string time = PostsDataGridView.Rows[i].Cells[1].Value.ToString();

                _postList.Add(new PostInUIList(groupLink, time));
            }
        }

        #endregion

        #region Проброс событий

        //инициализация
        public event EventHandler ViewLoad;
        public event EventHandler AnalizeClick;
        public event EventHandler PublishClick;                     
        public event EventHandler ClearFormClick;              
        public event EventHandler AutorizationClick;           
        public event EventHandler SaveTemplateClick;           
        public event EventHandler LoadTemplateClick;           

        //проброс

        void MainForm_Load(object sender, EventArgs e)
        {
            if (ViewLoad != null)
                ViewLoad(this, EventArgs.Empty);
        }

        void MainForm_AnalizeClick(object sender, EventArgs e)
        {
            if (AnalizeClick != null)
                AnalizeClick(this, EventArgs.Empty);
            
        }

        void PublicButton_Click(object sender, EventArgs e)
        {
            if (PublishClick != null)
                PublishClick(this, EventArgs.Empty);
        }

        void FormClearButton_Click(object sender, EventArgs e)
        {
            if (ClearFormClick != null)
                ClearFormClick(this, EventArgs.Empty);
        }

        void AutorizeButton_Click(object sender, EventArgs e)
        {
            if (AutorizationClick != null)
                AutorizationClick(this, EventArgs.Empty);
        }

        void LoadTemplateButton_Click(object sender, EventArgs e)
        {
            if (LoadTemplateClick != null)
                LoadTemplateClick(this, EventArgs.Empty);
        }

        void SaveTemplateButton_Click(object sender, EventArgs e)
        {

            if (SaveTemplateClick != null)
                SaveTemplateClick(this, EventArgs.Empty);
        }

       
        #endregion

    }

    
}
