using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessLogic
{
    /// <summary>
    /// Added by Sara Nanke on 03/22/2016
    /// This Class manages blog objects to send to the view
    /// </summary>
    public class BlogManager
    {
        List<Blog> blogs;
        BlogAccessor blogAccessor = new BlogAccessor();

        public List<Blog> GetBlogs()
        {
            blogs = blogAccessor.retrieveBlogs();
            return blogs;
        }
        /// <summary>
        /// Comments added by TRex 4/19/16
        /// This method retrieves a specific blog.
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>

        public Blog GetBlogById(int blogId)
        {
            Blog blogReturn = null;
            blogs = blogAccessor.retrieveBlogs();
            foreach (Blog blog in blogs)
            {
                if (blog.BlogID == blogId)
                {
                    return blog;
                }
            }
            return blogReturn;
        }

        /// <summary>
        /// Comments added by TRex
        /// This method retrieves blogs by date.
        /// </summary>
        /// <returns></returns>
        public List<Blog> GetBlogByDate()
        {
            blogs = blogAccessor.retrieveBlogs();
            //sortByDate
            return blogs;
        }

        /// <summary>
        /// Comments added by TRex 4/19/16
        /// This method retrieves a blog by name.
        /// </summary>
        /// <returns></returns>

        public List<Blog> GetBlogByName()
        {
            //blogs = blogAccessor.fetchBlogsByName();

            return blogs;
        }

        /// <summary>
        /// Comments added by TRex 4/19/16
        /// This method creates a new blog.
        /// Method name changed by TRex 4/19/16
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        public bool AddBlog(Blog blog)
        {
            bool created;
            try
            {
                if (BlogAccessor.InsertBlog(blog) == 1)
                {
                    created = true;
                }
                else
                {
                    created = false;
                }
            }
            catch
            {
                created = false;
            }
            return created;
        }

    }
}
