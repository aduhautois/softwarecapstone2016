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
    /// template manager
    /// </summary>
   public class TemplateManager
   {
       public bool uploadTemplate(Template newTemplate)
        {
            try
            {
                bool myBool = TemplateAccessor.UploadTemplate(newTemplate);
                return myBool;
            }
            catch (Exception)
            {

                throw;
            }
        }


       public Template retriveTemplate(int TemplateId)
        {
            Template myTemplate = new Template();

            try
            {
                myTemplate = TemplateAccessor.RetrieveTemplateById(TemplateId);
                return myTemplate;
            }
            catch (Exception)
            {

                throw;
            }

        }

       public List<Template> retriveAllTemplate()
       {
           List<Template> TemplateList;

           try
           {
               TemplateList = TemplateAccessor.RetrieveAllTemplate();
               return TemplateList;
           }
           catch (Exception)
           {               
               throw;
           }

       }

    }
}
