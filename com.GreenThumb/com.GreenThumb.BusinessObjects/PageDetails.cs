using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    /// <summary>
    /// Rhett Allen
    /// Created Date: 4/21/16
    /// </summary>
    public class PageDetails
    {
        private int _currentPage = 1;
        private int _perPage = 1;
        private int _maxPages = 1;
        private int _count = 1;
        public int PerPage
        {
            get
            {
                return _perPage;
            }
            set
            {
                _perPage = value;
            }
        }
        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                if (value > _maxPages)
                {
                    _currentPage = _maxPages;
                }
                else if (value < 1)
                {
                    _currentPage = 1;
                }
                else
                {
                    _currentPage = value;
                }
            }
        }

        //public int CurrentPage { get; set; }
        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
            }
        }
        public int MaxPages
        {
            get
            {
                if (_count % _perPage != 0)
                {
                    _maxPages = (_count / _perPage) + 1;
                }
                else
                {
                    _maxPages = (_count / _perPage);
                }

                return _maxPages;
            }
            private set
            {
                if (_count % _perPage != 0)
                {
                    _maxPages = (_count / _perPage) + 1;
                }
                else
                {
                    _maxPages = (_count / _perPage);
                }
            }
        }
    }
}
