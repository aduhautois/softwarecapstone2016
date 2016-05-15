using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace com.GreenThumb.BusinessObjects
{
    public class Task
    {
        /// <summary>
        /// Author: Poonam
        /// Data Transfer Object to represent a Task from the
        /// Database
        /// 
        /// Comments Added 3/3 By Trevor Glisch
        /// </summary>
        public int TaskID { get; set; }
        [DisplayName("Task Description ")]
        [Required(ErrorMessage="Please enter the task description")]
        public string TaskDescription { get; set; }

        [DisplayName("Created/Assigned On")]
        public string AssignedOn { get; set; }
        [DisplayName("Completed On")]
        public string CompletedOn { get; set; }

        [DisplayName("Created By")]
        public string AssignedBy { get; set; }

        [DisplayName("Assigned To")]
        public string AssignedTo { get; set; }

        [DisplayName("Notes")]
        [Required(ErrorMessage="Please enter the notes.")]
        public string UserNotes { get; set; }

        public bool Active { get; set; }

        public int AssignedToUserID { get; set; }
        //public DateTime TaskLastRevision { get; set; }

        public Task() { }
    }
}
