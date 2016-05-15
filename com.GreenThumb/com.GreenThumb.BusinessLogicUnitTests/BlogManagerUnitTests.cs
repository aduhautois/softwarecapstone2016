using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;
using System.Collections.Generic;

namespace com.GreenThumb.BusinessLogicUnitTests
{
    [TestClass]
    public class BlogManagerUnitTests
    {
        BlogManager blogManager = new BlogManager();

        /// <summary>
        /// Steve Hoover
        /// 5/6/16
        /// 
        /// Tests to verify a list of blogs returned is not null.
        /// </summary>
        [TestMethod]
        public void GetBlogsTest()
        {
            List<Blog> blogs = new List<Blog>();
            blogs = blogManager.GetBlogs();
            Assert.IsNotNull(blogs);
        }

        [TestMethod]
        public void GetBlogByIdTest()
        {
            Blog blogReturn = null;
            int blogId = 1000;

            blogReturn = blogManager.GetBlogById(blogId);

            Assert.IsNotNull(blogReturn);
        }

        [TestMethod]
        public void GetBlogByDateTest()
        {
            List<Blog> blogs = new List<Blog>();
            blogs = blogManager.GetBlogByDate();
            Assert.IsNotNull(blogs);
        }
    }
}
