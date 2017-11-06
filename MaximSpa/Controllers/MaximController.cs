using MaximSpa.Models;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MaximSpa.Controllers
{
    [RoutePrefix("maxim")]
    [EnableCors("*", "*", "*")]
    public class MaximController : ApiController
    {
        [Route("")]
        public List<MaximEntity> Get()
        {
            var dao = new MaximSpaDAO();
            var query = new TableQuery<MaximEntity>();
            var result = dao.MaximTable.ExecuteQuery(query).ToList();
            return result;
        }

        [Route("{id}")]
        public MaximEntity Get(string id)
        {
            var dao = new MaximSpaDAO();
            var op = TableOperation.Retrieve<MaximEntity>("PartitionKey", id);
            var opResult = dao.MaximTable.Execute(op);
            var result = (MaximEntity)opResult.Result;

            if (result == null)
            {
                var msg = new HttpResponseMessage();
                msg.StatusCode = HttpStatusCode.NotFound;
                msg.Content = new StringContent("id not found in maxim db");
                throw new HttpResponseException(msg);
            }

            return result;
        }

        [Route("")]
        public MaximEntity Post([FromBody]MaximEntity inMaxim)
        {
            if (inMaxim == null)
            {
                var msg = new HttpResponseMessage();
                msg.StatusCode = HttpStatusCode.BadRequest;
                msg.Content = new StringContent("inMaxim is null, bad request, bad json");
                throw new HttpResponseException(msg);
            }

            var dao = new MaximSpaDAO();
            var newMaxim = new MaximEntity();
            newMaxim.Name = inMaxim.Name;
            newMaxim.Content = inMaxim.Content;
            var op = TableOperation.InsertOrReplace(newMaxim);
            dao.MaximTable.Execute(op);
            return newMaxim;
        }

        [Route("")]
        public async Task Delete()
        {
            var dao = new MaximSpaDAO();
            var batchOp = new TableBatchOperation();
            var query = new TableQuery<MaximEntity>();

            foreach(var item in dao.MaximTable.ExecuteQuery(query))
            {
                var delOp = TableOperation.Delete(item);
                batchOp.Add(delOp);
            }

            var res = dao.MaximTable.ExecuteBatch(batchOp);
        }
    }
}
