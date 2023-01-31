using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataBusiness;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MasterDataAPI.Controllers
{
    [Route("api/RuleConditionOperationPopup")]
    public class RuleConditionOperationPopupController : Controller
    {



        [HttpPost("filterruleconditionoperationpopup")]
        public IActionResult filterruleconditionoperationpopup([FromBody]JObject body)
        {
            try
            {
                var service = new RuleConditionOperationService();
                var Models = new RuleConditionOperationViewModel();
                Models = JsonConvert.DeserializeObject<RuleConditionOperationViewModel>(body.ToString());
                var result = service.filterruleconditionoperationpopup(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
      }
    }