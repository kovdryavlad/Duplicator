using Duplicator.Autorization;
using Duplicator.BL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Duplicator
{
    class MainPresenter
    {
        //имя файла с основной информацией - токен и id
        string _infoFileName= "access_info.xml";

        //главная форма
        IMainForm _mainForm;
        //сообщения
        IMessageService _messageService;
        //Бизнес-логика
        IDuplicatEditor _duplicator;

        //id Пользователя и токен
        UserIdWithToken _info = new UserIdWithToken();
        //Сериализатор для них
        Serializer<UserIdWithToken> _serializer = new Serializer<UserIdWithToken>();

        //веб-клиент полученый из фабрики
        WebClient _webclient;

        //массив групп пользователя
        Group[] _userGroupsList;

        //секция ПУБЛИКАЦИИ
        //пост для копирования - КОНТЕКТ
        PostResponse _post;

        //ГДЕ И КОГДА ПУБЛИКОВАТЬ
        List<IdAndTimeToPublish> _publishList = new List<IdAndTimeToPublish>();

        //флаг была ли ошибка в таблице
        bool _wasErrorInTable = false;

        //для хранения разницы во времени ПК и сервера
        int _hourTimeDifferent;

        //конструктор
        public MainPresenter(IMainForm mainForm, IMessageService messageService, IDuplicatEditor duplicatEditor)
        {
            _mainForm = mainForm;
            _messageService = messageService;
            _duplicator = duplicatEditor;
            
            //получение нужного веб-клиента с фабрики
            _webclient = _duplicator.GetWebClientForVk();
            
            //подписка на события
            _mainForm.ViewLoad += _mainForm_ViewLoad;
            _mainForm.AnalizeClick += _mainForm_AnalizeClick;
            _mainForm.SaveTemplateClick += _mainForm_SaveTemplateClick;
            _mainForm.LoadTemplateClick += _mainForm_LoadTemplateClick;
            _mainForm.AutorizationClick += _mainForm_AutorizationClick;
            _mainForm.PublishClick += _mainForm_PublishClick;
            _mainForm.ClearFormClick += _mainForm_ClearFormClick;
        }

        //Очистить форму
        void _mainForm_ClearFormClick(object sender, EventArgs e)
        {
            _mainForm.ClearForm();
        }

        void _mainForm_PublishClick(object sender, EventArgs e)
        {
            //проходим по всему списку групп для публикации
            for (int i = 0; i < _publishList.Count; i++)
            {
                a:; //метка - вернемся сюда, если на нужное время уже запланирована публикация
                JToken jt = _webclient.GetResponse(VkQueriesSites.WallPost, VkQueriesParams.GatparamsFor_WallPost(_info, _post, _publishList[i]));
                
                Error er;
                try
                {
                    //если ошибок не возникло (удалось опубликовать пост) сл.строка выкинет ошибку
                    er = jt["error"].ToObject<Error>();
               
                    //уже запланирован запись
                    if (er.error_code == 214)
                    {
                        //добавляем интервал с формы и пробуем еще раз (переходим на метку)
                        _publishList[i].publishDateTime = _publishList[i].publishDateTime.AddMinutes(_mainForm.Interval);
                        goto a;
                    }
                    
                    else 
                    {
                        //неизвесная ошибка при публикации
                        string message = String.Format("В {} ошибка: код:{} сообщение:{}", 
                            _publishList[i].FullGroupLink, er.error_code, er.error_msg);
                        
                        _mainForm.AddMessageToLogger(message);
                    }
                }
                catch (Exception)
                {
                    //успешная публикация
                    //время возвращается к такому как у пользователя на ПК
                    string message = String.Format("В {0} успешно в {1}", 
                          _publishList[i].FullGroupLink, _publishList[i].publishDateTime.AddHours(-_hourTimeDifferent));
                    
                    _mainForm.AddMessageToLogger(message);

                    _mainForm.DeActivatePublishPutton();
                }
            }

        }

        void _mainForm_LoadTemplateClick(object sender, EventArgs e)
        {
            Serializer<List<PostInUIList>> sers = new Serializer<List<PostInUIList>>();

            //переменная для хранения имени файла
            string templatename = "";
 
            //вызываем диалоговое окно сохранения
            if (_duplicator.GetOpenFileName(out templatename, "Xml файлы|*.xml|Все файлы|*.*"))
            {
                //список для десериализации
                List<PostInUIList> PostListFromTemplate=null;
                try
                {
                    //получаем список постов из шаблона
                    PostListFromTemplate = sers.Deserialize(templatename);
                }
                catch 
                {
                    //выводим ошибку
                    _messageService.ShowError("Шаблон не может быть открыт по причине несовместимости");

                    return;
                }
                //переносим список в  таблицу
                _mainForm.PostList = PostListFromTemplate;

                //информируем пользователя
                _mainForm.AddMessageToLogger("Шаблон загружен");
            }
        }

        void _mainForm_SaveTemplateClick(object sender, EventArgs e)
        {
            //созлаем сериазизатор
            Serializer<List<PostInUIList>> sers = new Serializer<List<PostInUIList>>();

            //переменная для хранения имени файла
            string templatename = "";
 
            //вызываем диалоговое окно сохранения
            if(_duplicator.GetSaveFileName(out templatename, ".xml"))
            {
                //сериализуем
                sers.Serialize(templatename, _mainForm.PostList);
                //вывод сообщения в отладчик
                _mainForm.AddMessageToLogger("Шаблон сохранен.");
            }

            
        }

        //Анализ
        void _mainForm_AnalizeClick(object sender, EventArgs e)
        {
            //очистка мусора с предыдущего анализа
            _post = null;
            _publishList.Clear();
            _mainForm.AboutPost = "";
            _mainForm.ClearLogger();
            _wasErrorInTable = false;

            //id поста
            string postid="";
            //попытка отделить id из ссылки
            if (_duplicator.GetpostidFromFullLink(_mainForm.InputPostLink, out postid))
            {
                _mainForm.AddMessageToLogger("Входная ссылка на пост корректна");

                //вывод информации о посте
                ShowPostInformation(postid);
            }
            else 
            {
                //ошибка
                _mainForm.AddMessageToLogger("Ссылка на пост не корректна");
            }

            //анализ таблицы
            AnalizeTable();
            //анализ ошибок, если таковые были
            ErrorAnalize();
        }

        private void ErrorAnalize()
        {
            if (_post!=null&&!_wasErrorInTable&&_publishList.Count>0)
            {
                _mainForm.AddMessageToLogger("Анализ прошел успешно. Публикация возможна");
                _mainForm.ActivatePublishPutton();
            }
            if (_post==null)
            {
                _mainForm.AddMessageToLogger("Пост для копирования не прогружен");
                
            }
            if (_wasErrorInTable)
            {
                _mainForm.AddMessageToLogger("В таблице найдены ошибки");
            }
            if (_publishList.Count==0&&_wasErrorInTable==false)
            {
                    _mainForm.AddMessageToLogger("Таблица пуста"); 
            }

        }

        //анализируем таблицу и попутно сохраняем значения из нее, приводя к нужному виду
        private void AnalizeTable()
        {
            //получаем список - (ссылка на группу - время)
            List<PostInUIList> UIpostList = _mainForm.PostList;

            //получаем время с сервера
            DateTime ServerDateTime = GetServerTime();
            //вычилсяем разницу с текущим временем на ПК
            _hourTimeDifferent = ServerDateTime.Hour - DateTime.Now.Hour;
            
            //берем день, указанный на форме
            DateTime DateFromForm = _mainForm.PublicationDate;

            for (int i = 0; i < UIpostList.Count; i++)
            {
                try
                { 
                    //берем время со второго столбца таблицы
                    DateTime HoursAndMinutes = Convert.ToDateTime(UIpostList[i].PublicationTime);
                    //соеденяем день, с формы, с временем, которое считали
                    DateTime correctDateTimeToPublish = new DateTime(DateFromForm.Year, DateFromForm.Month, DateFromForm.Day, HoursAndMinutes.Hour, HoursAndMinutes.Minute, 0);
                    
                    //если время уже прошло выкидываем ошибку
                    if (correctDateTimeToPublish<DateTime.Now.AddMinutes(3))
	                    throw new Exception();

                    //ищем введенную группу в группах пользователя
                    Group group = _userGroupsList.First(x=>x.screen_name == UIpostList[i].FullGroupLink.Trim().Split(new[]{'\\','/'}).LastElement());
                    
                    //если дошли сюда и не наткнулись на ошибку, заполняем список публикации - полная ссылка для вывода сообшений пользователю
                    _publishList.Add(new IdAndTimeToPublish(group.id, correctDateTimeToPublish.AddHours(_hourTimeDifferent), UIpostList[i].FullGroupLink));
                    //покрасили строку таблицы в зеленый, показав, что в ней все хорошо
                    _mainForm.SetGreenColorOnRow(i);
                }
                catch 
                {
                    //сообщениия для пользователя
                    _mainForm.AddMessageToLogger("Не найдена группа или ошибка времени");
                    _mainForm.SetYellowColorOnRow(i);


                    //установика флага в значение "таблице есть ошибка"
                    _wasErrorInTable = true;
                }
            }


        }

        //получение времени с сервера
        private DateTime GetServerTime()
        {
            //Получаем время с сервера
            JToken jtime = _webclient.GetResponse(VkQueriesSites.ServerTime, VkQueriesParams.GetParamsFor_GroupsGet(_info));
            int ServerUnixtime = jtime["response"].ToObject<int>();

            //получаем формат времени с которым удобнее работать
            DateTime dateTime = TimeManager.UnixTimeStampToDateTime(ServerUnixtime);
            return dateTime;
        }

        //вывод информации о посте в место на форме
        private void ShowPostInformation(string postId)
        {
            postId=postId.Trim();
            //загружаем пост
            JToken jpost = _webclient.GetResponse(VkQueriesSites.WallGetById, VkQueriesParams.GatparamsFor_WallGetById(_info, postId));

            //переводим его в подготовленный формат ["response"] - разворачивает ответ (он приходит в родительском элементе response)
            //[0] тут из-за того, что метод возвращает список, а мы отправляли всего один id
            //а значит на выходе всего один элемент - он же нулевой
            _post = jpost["response"][0].ToObject<PostResponse>();

            
            //Берем нужные поля из поста
            string text = _post.text;                                //текст
            List<Attachment> attachments = _post.attachments;        //все вложения

            //маленькая лямбда для упрощения вывода на инфо-панель
            Action<string> logToAbout = t => _mainForm.AboutPost += t;

            //Вывод начала такста
            if (text.Length==0)
                logToAbout("(Текст отсутствует)\n\n");
            else if (text.Length > 150)
                logToAbout(text.Substring(0, 150) + "...\n\n");
            else
                logToAbout(text + "\n\n");

            if (attachments != null)
            {
                //выводим информацию о наличии вложений
                logToAbout("Фото: " + attachments.Count(x => x.type == "photo") + " шт.    ");
                logToAbout("Видео: " + attachments.Count(x => x.type == "video") + " шт.    ");
                logToAbout("Аудио: " + attachments.Count(x => x.type == "audio") + " шт.    ");
                logToAbout("Документы: " + attachments.Count(x => x.type == "doc") + " шт.    ");
            }
            else
            {
                logToAbout("Вложений нет"); 
            }
        }

        //форма загружена (запуск программы)
        void _mainForm_ViewLoad(object sender, EventArgs e)
        {
            //деактивируем кнопку
            _mainForm.DeActivatePublishPutton();

            //десериазизуем токен и id, если файл существует
            if (System.IO.File.Exists(_infoFileName))
            {
                _info = _serializer.Deserialize(_infoFileName);
                
                //переходим к авторизации и заодно
                //проверка действителен ли еще токен
                UserAutorizationWithToken();
            }
            else      
                _mainForm_AutorizationClick(this, EventArgs.Empty); //вызов окна авторизации
        }

        //ф-я авторизации - тут же подгрузка групп
        private void UserAutorizationWithToken()
        {
            //пробуем получить группы пользователя  (где filter=moder)
            JToken response = _webclient.GetResponse(VkQueriesSites.GroupsGet, VkQueriesParams.GetParamsFor_GroupsGet(_info));

            try
            {
                //если группы подгрузились следующая строка выкинет ошибку
                Error err = response["error"].ToObject<Error>();

                //5 - авторизация не удалась
                if(err.error_code==5)
                    _mainForm_AutorizationClick(this, EventArgs.Empty); //вызов окна авторизации
                    
            }
            catch
            {
                //превратили ответ сервера в массив групп
                _userGroupsList = (response["response"])["items"].Select(x => x.ToObject<Group>()).ToArray();

                //Указали на форме, что вход выполнен
                _mainForm.statusText = "Вход выполнен";
                _mainForm.SetColorOfStatusText(System.Drawing.Color.Green);

                _mainForm.AddMessageToLogger("Группы подгружены");
            }

        }

        //нажатие на кнопку авторизиция
        void _mainForm_AutorizationClick(object sender, EventArgs e)
        {
            //создаем форму авторизации и ее презентер
            AutorizationForm authform = new AutorizationForm();
            AutorizationPresenter AuthPresenter = new AutorizationPresenter(authform, _info);
            
            //показываем форму
            authform.ShowDialog();

            //проверка на прохождение авторизации
            if (_info.token == "" && _info.user_id == "")
                _messageService.ShowError("Вход не выполнен\n Приложение нуждается в перезапуске");
            else 
            {
                //засериализовали токен и id
                _serializer.Serialize(_infoFileName, _info);
                
            }

            //переходим к авторизации
            UserAutorizationWithToken();
        }
    }
}
