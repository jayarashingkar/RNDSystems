using RNDSystems.API.SQLHelper;
using RNDSystems.Common.Constants;
using RNDSystems.Models;
using RNDSystems.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RNDSystems.API.Controllers
{
    public class UACListingController : UnSecuredController
    {
        /// <summary>
        /// Retrieve the UAC details
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(RNDMaterial material)
        {
            ApiViewModel VM = null;
            try
            {
                CurrentUser user = ApiUser;
                VM = new ApiViewModel();
                if (material != null && !string.IsNullOrEmpty(material.records))
             //  if (material.RecID > 0)

                {
                    AdoHelper ado = new AdoHelper();
                    //material.Comment is list assignmaterial primary key example 5;6
                    //SqlParameter param1 = new SqlParameter("@Ids", material.Comment);
                    SqlParameter param1 = new SqlParameter("@Ids", material.records);                    
                    SqlParameter param2 = new SqlParameter("@MillLotNo", material.MillLotNo);                
                    SqlParameter param3 = new SqlParameter("@WorkStudyID", material.WorkStudyID);
                    SqlParameter param4 = new SqlParameter("@EntryBy", user.UserName);

                    SqlParameter param5 = new SqlParameter("@SoNum",material.SoNum);
                   // SqlParameter param6 = new SqlParameter("@UACPart", material.UACPart);
                    SqlParameter param7 = new SqlParameter("@Alloy", material.Alloy);
                    SqlParameter param8 = new SqlParameter("@Temper", material.Temper);
                    SqlParameter param9 = new SqlParameter("@Hole", material.Hole);
                    SqlParameter param10 = new SqlParameter("@PieceNo", material.PieceNo);
                    SqlParameter param11 = new SqlParameter("@Comment", material.Comment);
                 
                    ado.ExecScalarProc("RNDUACPartListing_Insert", "RND", new object[] { param1, param2, param3, param4,
                    param5,
                   // param6,
                        param7,
                        param8,
                        param9, param10, param11 });
                    VM.Message = MessageConstants.Saved;
                    VM.Success = true;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return Serializer.ReturnContent(VM, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);
        }
    }
}
