using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class PostThread
    {
        /// <summary>
        /// Added by Poonam Dubey on 02/27/2016

        /// </summary>
         public int PostID { get; set; }
        public string PostType { get; set; }
        public bool GroupCommnets { get; set; }
        public int? NoComments { get; set; }
        public bool ViewByAll { get; set; }
        public int UserID { get; set; }
        public int GroupID { get; set; }
        public DateTime PostDateTime { get; set; }
        public string PostContent { get; set; }
        public string PostTitle { get; set; }
        public bool Active { get; set; }
        //public int RoleID { get; set; }
        //public DateTime PostEdit { get; set; }

        public PostThread() { }
        public PostThread(int postID,
                           string postType,
                           bool groupComments,
                           int noComments,
                           bool viewByAll,
                           int userID,
                           int groupID,
                           DateTime postDateTime,
                           string postContent,
                           string postTitle,
                           bool active)
        {
            PostID = postID;
            PostType = postType;
            GroupCommnets = groupComments;
            NoComments = noComments;
            ViewByAll = viewByAll;
            UserID = userID;
            GroupID = groupID;
            PostDateTime = postDateTime;
            PostContent = postContent;
            PostTitle = postTitle;
            Active = active;
        }
    }
}
