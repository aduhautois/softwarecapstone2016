using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;

namespace com.GreenThumb.BusinessLogic
{
    /// <summary>
    /// stenner kvindlog 
    /// blueprint manager
    /// </summary>
   public class BlueprintManager
    {
        public bool uploadBlueprint(Blueprint newBlueprint)
        {
            try
            {
                bool myBool = BlueprintAccessor.UploadBlueprint(newBlueprint);
                return myBool;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Blueprint retriveBlueprint(int blueprintId)
        {
            Blueprint myBlueprint = new Blueprint();

            try
            {
                myBlueprint = BlueprintAccessor.RetrieveBlueprintById(blueprintId);
                return myBlueprint;
            }
            catch (Exception)
            {
                throw;
            }
       
        }

        public List<Blueprint> retriveAllBlueprints()
        {
            List<Blueprint> blueprintList;

            try
            {
                blueprintList = BlueprintAccessor.RetrieveAllBlueprints();
                return blueprintList;
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
