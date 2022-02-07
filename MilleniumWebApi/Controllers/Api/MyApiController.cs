using Microsoft.AspNetCore.Mvc;
using MilleniumWebApi.Models;
using MilleniumWebApi.Services.Interfaces;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace MilleniumWebApi.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyApiController : ControllerBase
    {
        private readonly IMyService _myService;

        public MyApiController(IMyService myService)
        {
            _myService = myService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string format)
        {
            var result = await _myService.GetAllPeopleAsync();

            if (result == null)
            {
                return BadRequest();
            }

            if (format.Equals("json"))
            {
                return Ok(result);
            }

            if (format.Equals("xml"))
            {
                return Ok(Serialize(result.ToList()));
            }
            else
            {
                return BadRequest("Unknown result format");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, string format)
        {
            try
            {
                var result = await _myService.GetPersonByIdAsync(id);

                if (result == null)
                {
                    return NotFound("Person with this id wasn't founded");
                }

                if (format.Equals("json"))
                {
                    return Ok(result);
                }

                if (format.Equals("xml"))
                {
                    return Ok(Serialize(result));
                }
                else
                {
                    return BadRequest("Unknown result format");
                }
            }
            catch
            {
                return BadRequest();
            }

            ;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person, string format)
        {
            var result = await _myService.AddPersonAsync(person);

            if (result == null)
            {
                return BadRequest();
            }

            if (format.Equals("json"))
            {
                return Ok(result);
            }

            if (format.Equals("xml"))
            {
                return Ok(Serialize(result));
            }
            else
            {
                return BadRequest("Unknown result format");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Person person, string format)
        {
            try
            {
                var result = await _myService.UpdatePerson(id, person);

                if (result == null)
                {
                    return NotFound("Person with this id wasn't founded");
                }

                if (format.Equals("json"))
                {
                    return Ok(result);
                }

                if (format.Equals("xml"))
                {
                    return Ok(Serialize(result));
                }
                else
                {
                    return BadRequest("Unknown result format");
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, string format)
        {
            try
            {
                var result = await _myService.DeletePersonAsync(id);

                if (result == null)
                {
                    return NotFound("Person with this id wasn't founded");
                }

                if (format.Equals("json"))
                {
                    return Ok(result);
                }

                if (format.Equals("xml"))
                {
                    return Ok(Serialize(result.ToList()));
                }
                else
                {
                    return BadRequest("Unknown result format");
                }
            }
            catch
            {
                return BadRequest();
            }

        }


        public static string Serialize<T>(T obj)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
            using (var sw = new StringWriter())
            {
                using (XmlTextWriter writer = new XmlTextWriter(sw) { Formatting = Formatting.Indented })
                {
                    xsSubmit.Serialize(writer, obj);
                    return sw.ToString();
                }
            }
        }
    }
}
