///<summary>
///Author: Chris Schwebach
///Time Pledge Donations Interface for the Buisness Logic Layer
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
    public interface ITimePledgeDonationManager
    {
		///<summary>
		///Gets Time Pledge List
		///@returns: TimePledge List
		///</summary>
		IEnumerable<TimePledge> GetTimePledgeList(string city, User user);		 
		 
		void AddTimePledge(IEnumerable<TimePledge> timePledge, User user);
		
		///<summary>
		///Edits Time Pledge
		///@returns: true/false
		///</summary>
		bool EditTimePledge(IEnumerable<TimePledge>timePledge, User user);
	}
}