using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nest;
using PeopleSPA.Elasticsearch;
using PeopleSPA.Model;

namespace PeopleSPA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
       
         
        // GET api/values
        [HttpGet]
        public IActionResult  GetValues()
        {
            Value tempPerson = null;
            var values = ConnectionToES.ESClinet().Search<Value>(s => s
            .Index("people")
            .Type("person")
            .From(0)
            .Size(1000)
            .Query(q => q.MatchAll()));

            var persons = values.Hits.ToList();
            List<Value> personList = new List<Value>();
            foreach (var item in persons) {

                tempPerson = new Value
                {
                    ID = item.Source.ID,
                    Name = item.Source.Name

                };
                personList.Add(tempPerson);

            }
               
        

            return Ok(personList);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult GetValue(int id)
        {
            Value tempPerson = null;
          
            var response = ConnectionToES.ESClinet().Search<Value>(s => s
            .Index("people")
            .Type("person")
            .From(0)
            .Size(1000)
            .Query (q => q.Term(fld => fld.ID, id)));

            if (response != null)
            {
                var person = response.Hits.FirstOrDefault();
                tempPerson = new Value
                {
                    ID = person.Source.ID,
                    Name = person.Source.Name
                };
            }


            return Ok(tempPerson);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Value per)
        {

            
            Value person = new Value { ID = per.ID, Name = per.Name};
            var response =  ConnectionToES.ESClinet().IndexAsync<Value>(per, i => i
                                              .Index("people")
                                              .Type("person")
                                              );
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAsync(int id)
        {

            

            var response = ConnectionToES.ESClinet().Delete<Value>(id, d => d
             .Index("people")
             .Type("person"));


            return Ok();
        }
    }
}
