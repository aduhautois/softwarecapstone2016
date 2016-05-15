using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.DataAccess
{
    /// <summary>
    /// Added by Sara Nanke on 03/22/2016
    /// This class contains the SQL for the blog object.
    /// </summary>
    public class BlogAccessor
    {
        public List<Blog> retrieveBlogs()
        {
            List<Blog> blogs = new List<Blog>();

            //blogs.Add(new Blog(1000, "THIS IS A BLOG NAME", "THIS IS SOME BLOG DATA", 1000, 1001, new DateTime(2015, 12, 12), true));
            //blogs.Add(new Blog(1001, "THIS IS A BLOG NAME 2", "THIS IS SOME BLOG DATA 2 aaaaaaaaa aaaaaaaaaaaaa aaaaaaaaaaaaaa aaaaaaaaaaaa aaaaaaaaaaa aaaaaaaaaaaaaa aaaaaaaaaaaa aaaaa a aaaaaaaaaaaaaaaaaaa a a a    a aaaaaaaaaaaaa aaaaaaaaaaa aaaaaaaaaaaa aaaaaaaaaaaa aaaa aaa aa a aaaaaaaaaaaaa aa a aaaaaaaaa aa aaaaaaaaaaaaaaaa a aaaaaaaaaaa aaaa a aaaa aaaaaaaaaa aaaaaaaaaaaaaaaaa a a", 1000, 1001, new DateTime(2015, 11, 12), true));
            //blogs.Add(new Blog(1002, "THIS IS A BLOG NAME 3", "THIS IS SOME BLOG DATA 3", 1000, 1001, new DateTime(2016, 1, 22), true));
            //blogs.Add(new Blog(1003, "THIS IS A BLOG NAME 4", "THIS IS SOME BLOG DATA 4", 1000, 1001, new DateTime(2015, 3, 4), true));
            //return blogs;

            var messages = new List<Message>();
            var conn = DBConnection.GetDBConnection();
            var query = @"Expert.spSelectBlogs";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Blog blog = new Blog()
                        {
                            BlogID = reader.GetInt32(0),
                            BlogTitle = reader.GetString(1),
                            BlogData = reader.GetString(2),
                            CreatedBy = reader.GetInt32(3),
                            DateCreated = reader.GetDateTime(4),
                            ModifiedBy = reader.GetInt32(5),
                            ModifiedDate = reader.GetDateTime(6),
                            Active = reader.GetBoolean(7)
                        };
                        blogs.Add(blog);
                    }
                }
                else
                {
                    throw new ApplicationException("Data not found");
                }
            }
            catch (Exception)
            {
                //there are no expert blogs, do nothing
            }
            finally
            {
                conn.Close();
            }
            return blogs;
        }
        /// <summary>
        /// Comments added by TRex
        /// This method allows a user to add a blog.
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        public static int InsertBlog(Blog blog)
        {
            int count = 0;
            var conn = DBConnection.GetDBConnection();
            var query = @"Expert.spInsertBlogEntry";
            var cmd = new SqlCommand(query, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@BlogData", blog.BlogData);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@CreatedBy", blog.CreatedBy);
            cmd.Parameters.AddWithValue("@CreatedDate", blog.DateCreated);
            cmd.Parameters.AddWithValue("@ModifiedBy", blog.CreatedBy);
            cmd.Parameters.AddWithValue("@ModifiedDate", blog.DateCreated);

            try
            {
                conn.Open();
                count = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }
    }
}
