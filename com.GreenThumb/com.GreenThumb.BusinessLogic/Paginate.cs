using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.BusinessLogic
{
    /// <summary>
    /// Rhett Allen
    /// Created Date: 4/21/16
    /// </summary> Get list of page and page details
    /// <typeparam name="T">The type of object you want to page</typeparam>
    public class Paginate<T>
    {
        public List<T> GetList(PageDetails page, List<T> list)
        {
            List<T> newList = new List<T>();
            try
            {
                if (list.Count <= page.PerPage)
                {
                    newList = list;
                }
                else if (page.CurrentPage == page.MaxPages)
                {
                    int remainder = list.Count % page.PerPage;

                    if(remainder == 0)
                    {
                        newList = list.GetRange((page.CurrentPage - 1) * page.PerPage, page.PerPage);
                    }
                    else
                    {
                        newList = list.GetRange(list.Count - remainder, remainder);
                    }
                }
                else
                {
                    newList = list.GetRange((page.CurrentPage - 1) * page.PerPage, page.PerPage);
                }
            }
            catch(Exception)
            {
                newList = new List<T>();
            }

            return newList;
        }
    }
}
