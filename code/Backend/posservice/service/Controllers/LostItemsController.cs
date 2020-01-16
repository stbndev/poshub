using AutoMapper;
using mrgvn.db;
using Newtonsoft.Json;
using posrepository;
using posrepository.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace service.Controllers
{
    public class LostItemsController : ApiController
    {
        private ILostItems ng;

        public LostItemsController(ILostItems lostItems) { this.ng = lostItems; }

        public string Put(int id, [FromBody]LostItemDTO value)
        {
            return executeaction(Action.UPDATE, id: id, value: value);
        }
        // GET: api/lostitems/id
        public string Delete(int id)
        {
            return executeaction(Action.DELETE, id: id);
        }
        // GET: api/lostitems
        public string Get()
        {
            return executeaction(Action.READALL);
        }
        // GET: api/lostitems/id
        public string Get(int id)
        {
            return executeaction(Action.READID, id: id);
        }

        // POST: api/sales
        public string Post([FromBody]LostItemDTO value)
        {
            return executeaction(Action.CREATE, value: value);
        }

        private string executeaction(Action action, int id = 0, LostItemDTO value = null)
        {
            ResponseModel rm = new ResponseModel();
            LOSTITEM result = new LOSTITEM();

            try
            {

                switch (action)
                {
                    case Action.CREATE:
                        result = ng.Create(value);
                        break;

                    case Action.READID:
                        result = ng.Read(id: id).First();
                        break;

                    case Action.READALL:
                        var results = ng.Read(all: true);
                        List<LostItemDTO> p = Mapper.Map<List<LostItemDTO>>(results);
                        rm.result = p;
                        rm.SetResponse(true);
                        break;

                    case Action.UPDATE:
                        result = ng.Update(value);
                        break;

                    case Action.DELETE:
                        bool flag = ng.Delete(id);
                        rm.SetResponse(flag);
                        break;
                    default:
                        break;
                }

                if (result.id > 0)
                {
                    LostItemDTO tmp = Mapper.Map<LostItemDTO>(result);
                    rm.result = tmp;
                    rm.SetResponse(true);
                }

                return JsonConvert.SerializeObject(rm);
            }
            catch (Exception ex)
            {
                rm.message = ex.Message;
            }

            return JsonConvert.SerializeObject(rm);

        }
    }
}
