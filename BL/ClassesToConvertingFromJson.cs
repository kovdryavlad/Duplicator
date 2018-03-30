using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duplicator.BL
{
    #region Для ошибок
    public class RequestParam
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class Error
    {
        public int error_code { get; set; }
        public string error_msg { get; set; }
        public List<RequestParam> request_params { get; set; }
    }

    #endregion

    #region Для поста (при получении) в дальнейшем нужно расширять

    public class MainAttach 
    {
        public int id { get; set; }
        public int owner_id { get; set; }
    }

    public class Photo:MainAttach
    {
        public int album_id { get; set; }
        public int user_id { get; set; }
        public string photo_75 { get; set; }
        public string photo_130 { get; set; }
        public string photo_604 { get; set; }
        public string photo_807 { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string text { get; set; }
        public int date { get; set; }
        public string access_key { get; set; }
    }

    public class Video : MainAttach
    {
        public string title { get; set; }
        public int duration { get; set; }
        public string description { get; set; }
        public int date { get; set; }
        public int comments { get; set; }
        public int views { get; set; }
        public string photo_130 { get; set; }
        public string photo_320 { get; set; }
        public string access_key { get; set; }
        public int can_edit { get; set; }
        public int can_add { get; set; }
    }

    public class Doc:MainAttach
    {
        public string title { get; set; }
        public int size { get; set; }
        public string ext { get; set; }
        public string url { get; set; }
        public int date { get; set; }
        public int type { get; set; }
        public string access_key { get; set; }
    }

    public class Audio : MainAttach
    {
        public string artist { get; set; }
        public string title { get; set; }
        public int duration { get; set; }
        public int date { get; set; }
        public int content_restricted { get; set; }
        public string url { get; set; }
        public int genre_id { get; set; }
    }

    public class Attachment
    {
        public string type { get; set; }
        public Photo photo { get; set; }
        public Video video { get; set; }
        public Doc doc { get; set; }
        public Audio audio { get; set; }
    }

    public class Place
    {
        public int id { get; set; }
        public string title { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int created { get; set; }
        public string icon { get; set; }
        public string country { get; set; }
        public string city { get; set; }
    }

    public class Geo
    {
        public string type { get; set; }
        public string coordinates { get; set; }
        public Place place { get; set; }
        public int showmap { get; set; }
    }

    public class PostSource
    {
        public string type { get; set; }
    }

    public class Comments
    {
        public int count { get; set; }
        public int can_post { get; set; }
    }

    public class Likes
    {
        public int count { get; set; }
        public int user_likes { get; set; }
        public int can_like { get; set; }
        public int can_publish { get; set; }
    }

    public class Reposts
    {
        public int count { get; set; }
        public int user_reposted { get; set; }
    }

    public class Views
    {
        public int count { get; set; }
    }

    public class PostResponse
    {
        public int id { get; set; }
        public int from_id { get; set; }
        public int owner_id { get; set; }
        public int date { get; set; }
        public int marked_as_ads { get; set; }
        public string post_type { get; set; }
        public string text { get; set; }
        public int can_edit { get; set; }
        public int created_by { get; set; }
        public int can_delete { get; set; }
        public int can_pin { get; set; }
        public List<Attachment> attachments { get; set; }
        public Geo geo { get; set; }
        public PostSource post_source { get; set; }
        public Comments comments { get; set; }
        public Likes likes { get; set; }
        public Reposts reposts { get; set; }
        public Views views { get; set; }
    }
    #endregion

    #region Для групп
    
    public class Group
    {
        public int id { get; set; }
        public string name { get; set; }
        public string screen_name { get; set; }
        public int is_closed { get; set; }
        public string type { get; set; }
        public int is_admin { get; set; }
        public int admin_level { get; set; }
        public int is_member { get; set; }
        public string photo_50 { get; set; }
        public string photo_100 { get; set; }
        public string photo_200 { get; set; }
    }

    #endregion
}
