using Plunder.Setting.Data.Interfaces;
using Plunder.Setting.Models;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Plunder.Setting.ApiControllers
{
    [RoutePrefix("Domain")]
    public class DomainController : ApiController
    {

        IDomainQuery _domainQuery;

        IDomainRepository _domainRepository;

        public DomainController(IDomainQuery domainQuery, IDomainRepository domainRepository)
        {
            _domainQuery = domainQuery;
            _domainRepository = domainRepository;
        }

        /// <summary>
        /// 获取域列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<DomainEntity> Get()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 获取指定域
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<DomainEntity> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 分页获取域列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Paged")]
        public async Task<DomainEntity> PagedGet()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 分页获取域列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<DomainEntity> Post(DomainEntity value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 分页获取域列表
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<DomainEntity> Put(DomainEntity value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 分页获取域列表
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<DomainEntity> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
