using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessLogic
{
    /// <summary>
    /// Nicholas King
    /// </summary>
    public class GardenTemplateManager
    {
        public GardenTemplateManager()
        {

        }


        /// <summary>
        /// created by: Nicholas King
        /// Created: 3/15/2016
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <returns>true if it's succssufully added file name</returns>
        public bool AddTemplate(string filePath, AccessToken at, string fileName)
        {
            bool result = false;

            try
            {
                byte[] file;
                var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                var reader = new BinaryReader(stream);
                file = reader.ReadBytes((int)stream.Length);

                if (ExpertAccessor.CreateGardenTemplate(file, at.UserID, fileName) == 2)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// created by: Nicholas King
        /// Created: 3/15/2016
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>data if it's succssufully added file name</returns>
        public byte[] AddLoadTemplate(string fileName)
        {
            byte[] data = null;


            //put this in the load template page... pull png from database to memoryStream, then convert to BitmapImage. then put as image source
            data = ExpertAccessor.RetrieveGardenTemplate(fileName);


            return data;
        }
        /// <summary>
        /// created by: Nicholas King
        /// Created: 3/25/2016
        /// </summary>
        /// <returns>a list of garden templete</returns>
        public List<GardenTemplate> GetTemplateList()
        {
            return ExpertAccessor.RetrieveAllGardenTemplates();
        }

    }
}
