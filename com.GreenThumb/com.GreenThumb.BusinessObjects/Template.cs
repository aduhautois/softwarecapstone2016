using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Template
    {



        /// <summary>
        /// Stenner Kvindlog
        /// Interaction logic for Blueprint.cs
        /// Template class 
        /// </summary>

        public int TemplateID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public int ModifiedBy { get; set; }
        public string FilePath { get; set; }


        public Template()
        {

        }

        public Template( string title, string description, DateTime dateCreated, int modifiedBy, string filePath)
        {
           
            this.Title = title;
            this.Description = description;
            this.DateCreated = dateCreated;
            this.ModifiedBy = modifiedBy;
            this.FilePath = filePath;
        }      
    }
}
