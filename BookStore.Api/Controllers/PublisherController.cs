using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [Route("/publisher")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        PublisherRepository _publisherRepository = new PublisherRepository();

        [HttpGet("getPublishers")]
        [ProducesResponseType(typeof(ListResponse<PublisherModel>), 200)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public IActionResult GetPublishers(int pageIndex = 1, int pageSize = 10, string? keyword = "")
        {
            var publishers = _publisherRepository.GetPublishers(pageIndex, pageSize, keyword);

            ListResponse<PublisherModel> listResponse = new ListResponse<PublisherModel>()
            {
                Results = publishers.Results.Select(c => new PublisherModel(c)).ToList(),
                Totalrecords = publishers.Totalrecords
            };

            return Ok(listResponse);
        }

        [HttpGet("getPublisher/{id}")]
        [ProducesResponseType(typeof(PublisherModel), 200)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public IActionResult AddPublisher(int id)
        {
            var publisher = _publisherRepository.GetPublisher(id);

            PublisherModel publisherModel = new PublisherModel(publisher);

            return Ok(publisherModel);
        }

        [HttpPost("addPublisher")]
        [ProducesResponseType(typeof(PublisherModel), 200)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public IActionResult AddPublisher(PublisherModel model)
        {
            if (model == null)
            {
                return BadRequest("No data found to be added");
            }
            Publisher publisher = new Publisher()
            {
                Id = model.Id,
                Name = model.Name,
                Address=model.Address,
                Contact=model.Contact
            };
            var entry = _publisherRepository.AddPublisher(publisher);

            PublisherModel publisherModel = new PublisherModel(entry);

            return Ok(publisherModel);
        }

        [HttpPut("updatePublisher")]
        [ProducesResponseType(typeof(PublisherModel), 200)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public IActionResult UpdatePublisher(PublisherModel model)
        {
            if (model == null)
            {
                return BadRequest("No data found to be added");
            }
            Publisher publisher = new Publisher()
            {
                Id = model.Id,
                Name = model.Name
            };
            var entry = _publisherRepository.UpdatePublisher(publisher);

            PublisherModel publisherModel = new PublisherModel(entry);

            return Ok(publisherModel);
        }

        [HttpDelete("deletePublisher/{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public IActionResult DeletePublisher(int id)
        {
            if (id == 0)
            {
                return BadRequest("Please enter valid Id");
            }
            var entry = _publisherRepository.DeletePublisher(id);
            return Ok(entry);
        }

    }
}
