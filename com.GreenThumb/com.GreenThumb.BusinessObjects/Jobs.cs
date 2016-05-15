// Created By Poonam Dubey on 02/27/2016
// Updated by Nasr Mohammed on 3/4/2016 and change the object name from task to job
//Update file in project

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Job
    {
        public int JobID { get; set; }
        public int GardenID { get; set; }
        public string Description { get; set; }
        public DateTime DateAssigned { get; set; }      
        public int AssignedTo { get; set; }
        public int AssignedFrom { get; set; }
        public string UserNotes { get; set; }
        public bool Active { get; set; }
        public DateTime DateCompleted { get; set; }


        public Job() { }
        public Job(int jobID, int gardenID,
                     string description,
                     DateTime dateAssigned,
                    int assignedTo, int assignedFrom, string userNotes, bool active, DateTime dateCompleted)
        {
            JobID = jobID;
            GardenID = gardenID;
            Description = description;
            DateAssigned = dateAssigned;      
            AssignedTo = assignedTo;
            AssignedFrom = assignedFrom;
            UserNotes = userNotes;
            Active = active;
            DateCompleted = dateCompleted;

        }
    }
}
