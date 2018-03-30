using System;
using System.Linq;
using System.Collections.Specialized;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Reflection;

namespace Duplicator.BL
{
    public interface IDuplicatEditor 
    {
        bool GetOpenFileName(out string FileName);                              //Диалоговое окно открытия файла
        bool GetOpenFileName(out string FileName, string filter);               //Перегрузка с указанием фильтра

        bool GetSaveFileName(out string FileName);                              //диалоговое окно сохранение файла
        bool GetSaveFileName(out string FileName, string extension);            //перегрузка с указанием расширения
    
        WebClient GetWebClientForVk();                                          //фабрика готового веб-клиента
        bool GetpostidFromFullLink(string FullPostlink, out string postid);     //отделение id поста от ссылки
    }

    public class DuplicatEditor:IDuplicatEditor
    {
        //получить все вложения поста
        internal static string AllPostAttachment(PostResponse post)
        { 
            //результирующая строка
            string result="";

            //возьмем все вложения поста
            List<Attachment> attachements = post.attachments;

            //проверим на null
            if (attachements==null)
                result = "";

            else if (attachements.Count >= 0) //и если их больше одного
	        {
                for (int i = 0; i < attachements.Count; i++)
                {
                    //используя рефлексию
                    //Возьмем информацию о типе
                    Type t = typeof(Attachment);

                    //получим все свойства этого типа
                    PropertyInfo[] Finfo = t.GetProperties();

                    //найдем нужное свойство
                    //После запроса в type лежит тип вложения и в одноименном свойстве лежит нужная информация по конретнному вложению
                    PropertyInfo current = Finfo.First(x => x.Name == attachements[i].type);

                    //соеденяем owner_id и id
                    string id =((MainAttach)current.GetValue(attachements[i])).owner_id+"_"+((MainAttach)current.GetValue(attachements[i])).id;

                    //соеденяем полный id  полученый выше с типом
                    result += attachements[i].type + id;

                    //если вложение  не последнее добавляем запятую
                    if (i != attachements.Count - 1)
                        result += ",";
                }
	        }
            else
	            result = "";

            return result;
        
        }

        //диалог открытия файла
        /// <summary>
        /// Открывает диалоговое окно сохранения файла
        /// </summary>
        /// <returns>Возвращает какая кнопка была нажата на диалоговом окне. OK - true, Cancel - false</returns>
        public bool GetOpenFileName(out string FileName)
        {
            //создание окна
            OpenFileDialog ofd = new OpenFileDialog();

            //вызов окна
            return ShowDialogWindow(ofd, out FileName);
        }
        /// <summary>
        /// Открывает диалоговое окно открытия файла
        /// </summary>
        /// <param name="FileName">out переменная для записи пути к файлу</param>
        /// <param name="filter">Фильтр для диалогового окна. Пример для передачи: 'Все файлы|*.*'</param>
        /// <returns>Возвращает какая кнопка была нажата на диалоговом окне. OK - true, Cancel - false</returns>
        public bool GetOpenFileName(out string FileName, string filter)
        {
            //создаем окно
            OpenFileDialog ofd = new OpenFileDialog();

            //добавляем фильтр
            ofd.Filter = filter; 
            //вызов окна
            return ShowDialogWindow(ofd, out FileName);
        }

        //диалог сохранения файла
        /// <summary>
        /// Открывает диалоговое окно сохранения файла
        /// </summary>
        /// <param name="FileName">out переменная для записи пути к файлу</param>
        /// <returns>Возвращает какая кнопка была нажата на диалоговом окне. OK - true, Cancel - false</returns>
        public bool GetSaveFileName(out string FileName) 
        {
            //создаем окно
            SaveFileDialog sfl = new SaveFileDialog();

            //вызов окна
            return ShowDialogWindow(sfl, out FileName);
        }
        /// <summary>
        /// Открывает диалоговое окно сохранения файла
        /// </summary>
        /// <param name="FileName">out переменная для записи пути к файлу</param>
        /// <param name="extension">Расширение, которое будет дописываться, если пользователь не указал. Пример для передачи: '.xml'</param>
        /// <returns>Возвращает какая кнопка была нажата на диалоговом окне. OK - true, Cancel - false</returns>
        public bool GetSaveFileName(out string FileName, string extension)
        {
            //создаем окно
            SaveFileDialog sfl = new SaveFileDialog();

            //добавляем расширение
            sfl.AddExtension = true;
            sfl.DefaultExt = extension;

            //вызов окна
            return ShowDialogWindow(sfl, out FileName);
        }

        //общий метод для диалоговых окон openFile и SaveFile
        private static bool ShowDialogWindow(FileDialog dialogWindow, out string FileName)
        {
            if (dialogWindow.ShowDialog() == DialogResult.OK)
            {
                FileName = dialogWindow.FileName;
                return true;
            }
            else
            {
                FileName = "";
                return false;
            }
        }

        //для получения id поста из ссылки на него
        public bool GetpostidFromFullLink(string FullPostlink, out string postid)
        {
            //строка по которой определяем корректность ссылки и выполняем отделение id от полной ссылки
            string wall = "wall";
            
            if (!FullPostlink.Contains(wall))
            {
                postid = "";
                return false;
            }

            //номер первого символа подстроки
            int n = FullPostlink.IndexOf(wall);

            //копируем подстроку
            postid = FullPostlink.Substring(n+wall.Length);

            return true;
        }

        //фабрика нужного веб-клиента
        public WebClient GetWebClientForVk()
        {
            WebClient wc = new WebClient();

            wc.ErrorCatching += TryLater_ErrorCatching;

            return wc;
        }

        //подписчик события ErrorCatching для веб-клиента, который возвращает фабрика
        private void TryLater_ErrorCatching(object sender, WebClientEventArgs e)
        {
            //проверяем если в ответе ошибка
            try
            {
                //приведение к ошибке
                Error err = e.Response["error"].ToObject<Error>();

                //проверка на предусмотреные ошибки
                //1  - неизвесная ошибка
                //6  - слишком много запросов в секунду
                //10 - сервер попросил обратиться позже
                if (err.error_code == 1 || err.error_code == 6 || err.error_code == 10)
                {
                    System.Threading.Thread.Sleep(200);
                    e.Continue = true;
                    return;
                }
                else 
                {
                    //возникла непредвиденная ошибка
                    //записать в будущем логгером
                }
            }
            catch { return; } 
        }


    }
    
    //для работы со временем
    public class TimeManager
    {
         //для перевода в unix времени в DateTime
        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            System.DateTime dtDateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime;
        }
        
        //для перевода в DateTime времени в unix
        public static uint DateTimeToUnixTimeStamp(DateTime dateTime)
        {
            return (uint)(dateTime - new DateTime(1970, 1, 1)).TotalSeconds;
        }

    }

    //мой веб клиент с событием и статической ф-ей для разбития url
    public class WebClient 
    {
        //для разбития Url адреса на части
        public static SiteAndPars SplitURL(string url)
        {
            SiteAndPars ResultElement = new SiteAndPars();

            #region Отделение сайта
            
            string[] UrlSplitResult = url.Split(new[] { '?', '#' }, StringSplitOptions.RemoveEmptyEntries);
            //сайт отделился сразу
            ResultElement.Site = UrlSplitResult[0];
            
            #endregion

            #region Отделение параметров 

            string[] ParamsAfterSplit = UrlSplitResult[1].Split(new[] { '&', '=' });

            for (int i = 0; i < ParamsAfterSplit.Length; i = i + 2)
                ResultElement.ParamsCollection.Add(ParamsAfterSplit[i],ParamsAfterSplit[i + 1]);
            
            #endregion

            return ResultElement;
        }

        //для экземпляров
        
        //объявления события и аргументоа для него
        public event EventHandler<WebClientEventArgs> ErrorCatching;

        //для отправки запроса на сервер
        public JToken GetResponse(string siteUrl, NameValueCollection queryPars)
        {
            //для события. тут в WcEvArgs.response будет и резальтат 
            WebClientEventArgs WcEvArgs = new WebClientEventArgs();
       
             //Системный веб клинт для отправки пост запроса
            System.Net.WebClient webClientSystem = new System.Net.WebClient();

           //Trust all certificates
           System.Net.ServicePointManager.ServerCertificateValidationCallback =
                ((sender, certificate, chain, sslPolicyErrors) => true);
           
           do
            {
                //получаем ответ в байтах
                var byteResponse = webClientSystem.UploadValues(siteUrl, queryPars);
                //преобразуем в строку
                var stringResponse = Encoding.UTF8.GetString(byteResponse);
                //строку преобразуем в JToken
                WcEvArgs.Response = JToken.Parse(stringResponse);

                //уведомляем подписчиков события, если они есть
                if (ErrorCatching != null)
                    ErrorCatching(this, WcEvArgs);

            } while (WcEvArgs.Continue);//подписчики могут установить в True в случае, если сервер попросит обратиться позже

            //позвращает ответ
            return WcEvArgs.Response;
        }
    }

    //сайт и пары наборов (имя-значение)
    public class SiteAndPars
    {
        public string Site { get; set; }

        public NameValueCollection ParamsCollection = new NameValueCollection();
    }

    //Потомок от EventArgs для WebClient
    public class WebClientEventArgs : EventArgs 
    {
        //нужно ли продолжить цикл
        private bool _continue;

        public bool Continue 
        {   
            get { return _continue; }
            set { _continue = value; }
        }

        //ответ с сервера
        private JToken _response;

        public JToken Response
        {
            get { return _response; }
            set { _response = value;}
        }
    }

    //Обобщенный сериализатор Очень простой)
    public class Serializer<T>
    { 
        XmlSerializer formatter = new XmlSerializer(typeof(T));

        public void Serialize(string filename, T ClassObject)
        {
            using (StreamWriter str = new StreamWriter(filename))
                formatter.Serialize(str, ClassObject);
        }

        public T Deserialize(string filename)
        {
            T result;

            using (StreamReader read = new StreamReader(filename))
            {
                result = (T)formatter.Deserialize(read);
            }
            return result; 
        }
    }

    //для сериализации
    [Serializable]
    public class UserIdWithToken
    {
        public string user_id;

        public string token;
    }

    //хранилище параметров для запросов
    public static class VkQueriesParams
    {
        //переменная для версии, задается в статическом конструкторе
        static string version;
        
        //статический конструктор
        static VkQueriesParams()
        {
            //задаем версию
            version = "5.65";
        }
    
        //Фабрики
        //groups.get
        public static NameValueCollection GetParamsFor_GroupsGet(UserIdWithToken inform) 
        {
            //инициализация
            NameValueCollection ParamsForGroupsGet = new NameValueCollection();
            
            //параметры метода
            ParamsForGroupsGet.Add("v", version);
            ParamsForGroupsGet.Add("filter", "moder");
            ParamsForGroupsGet.Add("extended", "1");

            //id и  токен
            ParamsForGroupsGet.Add("user_id", inform.user_id);
            ParamsForGroupsGet.Add("access_token", inform.token);

            return ParamsForGroupsGet;
        }

        //wall.getByid
        public static NameValueCollection GatparamsFor_WallGetById(UserIdWithToken inform, string postId) 
        { 
            NameValueCollection ParamsForWallGetById = new NameValueCollection();
            
            ParamsForWallGetById.Add("v", version);
            ParamsForWallGetById.Add("posts", postId);
            ParamsForWallGetById.Add("access_token", inform.token);

            //access_token=" + _info.USER_TOKEN + "&v=5.65" + "&posts=" + _POST_ID
            return ParamsForWallGetById;
        }
        //wall.post
        public static NameValueCollection GatparamsFor_WallPost(UserIdWithToken inform, PostResponse post, IdAndTimeToPublish publishIdTime)
        {
            NameValueCollection ParamsForWallWallPost = new NameValueCollection();

            //&owner_id=-"GnTlist[i]._Id" + "&attachments=" + _wallPost.GetAttachementString() + "&publish_date=" + GnTlist[i]._unixTime;

            ParamsForWallWallPost.Add("v", version);
            ParamsForWallWallPost.Add("access_token", inform.token);
            
            ParamsForWallWallPost.Add("publish_date", TimeManager.DateTimeToUnixTimeStamp(publishIdTime.publishDateTime).ToString());
            ParamsForWallWallPost.Add("message", post.text);
            ParamsForWallWallPost.Add("from_group", "1");
            ParamsForWallWallPost.Add("owner_id", "-"+publishIdTime.id);
            ParamsForWallWallPost.Add("attachments", DuplicatEditor.AllPostAttachment(post));

            //если гео не пуста
            if (post.geo!=null)
            {
                var SplitGeo = post.geo.coordinates.Split(new []{' '});

                ParamsForWallWallPost.Add("lat", SplitGeo[0]);

                ParamsForWallWallPost.Add("long", SplitGeo[1]);
            }
            return ParamsForWallWallPost;
        }
        //wall.repost  
        public static NameValueCollection GatparamsFor_WallRepost(UserIdWithToken inform, IdAndTimeToPublish publishIdTime)
        {
            NameValueCollection ParamsForWallWallRepost = new NameValueCollection();

            ParamsForWallWallRepost.Add("v", version);
            ParamsForWallWallRepost.Add("access_token", inform.token);
            ParamsForWallWallRepost.Add("publish_date", TimeManager.DateTimeToUnixTimeStamp(publishIdTime.publishDateTime).ToString());
            

            return ParamsForWallWallRepost;
        }
    }

    //хранилище сайтов для запросов
    public static class VkQueriesSites
    {
        public static string GroupsGet   =  "https://api.vk.com/method/groups.get";
        public static string WallGetById =  "https://api.vk.com/method/wall.getById";
        public static string ServerTime  =  "https://api.vk.com/method/utils.getServerTime";
        public static string WallPost    =  "https://api.vk.com/method/wall.post";
        public static string WallRepost  =  "https://api.vk.com/method/wall.repost";
    }

    //хранение финальной стадии подготовки к публикации (id-время и полная ссылка)
    public class IdAndTimeToPublish
    {
        public int id { get; set; }                         //id группы
        public DateTime publishDateTime { get; set; }       //дата публикации
        public string FullGroupLink { get; set; }           //полная ссылка на группу для сообщений пользователю

        //конструктор
        public IdAndTimeToPublish(int id, DateTime dateTime, string fullGroupLink)
        {
            this.id = id;
            this.publishDateTime = dateTime;
            this.FullGroupLink = fullGroupLink;
        }
    }
    
    //для хранения постов полученных из пользовательского интерфейса
    [Serializable]
    public class PostInUIList
    {
        public string FullGroupLink { get; set; }

        public string PublicationTime { get; set; }

        //пустой конструктор для сериализации
        public PostInUIList() { }

        //нормальный конструктор
        public PostInUIList(string fullLink, string time)
        {
            FullGroupLink = fullLink;

            PublicationTime = time;
        }

    }
}
