using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duplicator.BL;

namespace Duplicator.Autorization
{
    public class AutorizationPresenter
    {
        //форма авторизации
        IAutorizationView _authView;
        //id и token
        UserIdWithToken _Info;
        
        //конструктор
        public AutorizationPresenter(IAutorizationView authView, UserIdWithToken info)
        {
            _authView = authView;
            _Info = info;

            //подписка на события
            _authView.AutorizationFormLoad += _authView_AutorizationFormLoad;
            _authView.DocCompleted += _authView_DocCompleted;
        }

        //события
        void _authView_DocCompleted(object sender, EventArgs e)
        {
            string url = _authView.BrowserUrl.OriginalString;

            if (url.Contains("#"))
            {
                SiteAndPars snp = WebClient.SplitURL(url);

                _Info.token = snp.ParamsCollection["access_token"];
                _Info.user_id = snp.ParamsCollection["user_id"];
            }
        }

        void _authView_AutorizationFormLoad(object sender, EventArgs e)
        {
            CookieClean();//почистили куки

            _authView.BrowserUrl = new Uri(@"https://oauth.vk.com/authorize?client_id=6077874&display=page&redirect_uri=https://vk.com&scope=groups, wall&response_type=token&v=5.65");
        }

        //очистка куки
        private void CookieClean()
        {
            string Path = Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
            try
            {
                System.IO.Directory.Delete(Path, true);
            }
            catch (Exception)
            {
            }
        }
    }
}
