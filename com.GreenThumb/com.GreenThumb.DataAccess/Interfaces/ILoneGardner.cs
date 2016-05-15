///<summary>
///Author: Nasr Mohammed
///Lone Gardner Interface for the Buisness Object Layer
///</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.DataAccess.Interfaces
{
    public interface ILoneGardner
    {
        ///<summary>
        ///Fetch list of advices
        ///@returns: advice 
        ///</summary>
        List<Advice> GetAdvice();

        ///<summary>
        ///Insert a question in question table
        ///@returns: rows affected 
        ///</summary>
        int PostQuestion(Question question);

        ///<summary>
        /// Design a Root  TopGarden *** HELPS ***
        ///@returns: a garden
        ///</summary>
        Garden DesignRootTopGarden(Garden gardenID);

        ///<summary>
        ///Design a Regulart Garden *** HELPS ***
        ///@returns: a garden
        ///</summary>		
        Garden DesignRegularGarden(Garden gardenID);

        ///<summary>
        ///Design a Vacant Lot Garden *** HELPS ***
        ///@returns: a garden
        ///</summary>
        Garden DesignVacantLotGarden(Garden gardenID);

        ///<summary>
        ///Design a Container Garden *** HELPS ***
        ///@returns: a garden
        ///</summary>
        Garden DesignContainerGarden(Garden gardenID);

        ///<summary>
        /// *** HELPS ***
        ///@returns: 
        ///</summary>
        string WhatToPlant(string plant);
    }
}
















