using KeyVaultDemo.Business;
using KeyVaultDemo.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace KeyVaultDemo.Controllers
{
    [RoutePrefix("api/secret")]
    public class SecretController : ApiController
    {
        #region Properties

        private IAzureKeyVaultClient _kvOperator;

        #endregion


        #region Constructor

        public SecretController(IAzureKeyVaultClient kvOperator)
        {
            _kvOperator = kvOperator;
        }

        #endregion


        #region Methods

        [HttpPost]
        [Route("add")]
        public async Task<IHttpActionResult> AddBotSecretAsync([FromBody]SecretDto input)
        {
            var response = await _kvOperator.SetSecretAsyn(input.Name, input.Secret);
            return Ok(response);
        }


        [HttpGet]
        [Route("get/{id}")]
        public async Task<IHttpActionResult> GetBotSecretAsync(string id)
        {
            var secret = await _kvOperator.GetSecretAsync(id);
            return Ok(secret);
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IHttpActionResult> DelBotSecretAsync([FromBody]string id)
        {
            var response = await _kvOperator.DeleteSecretAsyn(id);
            return Ok(response);
        }

        #endregion
    }
}