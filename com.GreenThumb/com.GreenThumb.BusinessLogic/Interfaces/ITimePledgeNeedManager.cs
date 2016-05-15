///<summary>
///Author: Chris Schwebach
///TimePledge Needs Interface for the Buisness Logic Layer
///</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.DataAccess;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.BusinessLogic.Interfaces
{
    public interface ITimePledgeNeedManager
    {
		///<summary>
		///Gets Time Needed List
		///@returns: TimeNeeded List
		///</summary>
		IEnumerable<TimePledgeNeeded> GetTimePledgeNeededList(string city, Group group);			
		 
		void AddTimePledgeNeeded(IEnumerable<TimePledge> timePledge, User user, Group group);
		
		///<summary>
		///Edits Time Needed
		///@returns: true/false
		///</summary>
		bool EditTimePledgeNeeded(IEnumerable<TimePledge> timePledge, User user, Group group);
	}
}