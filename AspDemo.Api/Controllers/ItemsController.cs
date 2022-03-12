using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspDemo.Api.DTOs;
using AspDemo.Api.Models;
using AspDemo.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AspDemo.Api.Controllers
{
    [ApiController]
    [Route("Items")]
    public class ItemsController : ControllerBase
    {
        private IItemsRepository repository;
        public ItemsController(IItemsRepository _repository)
        {
            this.repository = _repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDTO>?>> GetItemsAsync()
        {
            IEnumerable<ItemDTO>? items =  (await repository.GetItemsAsync())?
                                            .Select(item => item.AsDTO());

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItemAsync(int id)
        {
            Item? item = await repository.GetItemAsync(id);

            return item == null ? NotFound() : Ok(item.AsDTO()); 
        }

        [HttpPost]
        public async Task<ActionResult<ItemDTO>> CreateItemAsync(CreateItemDTO createItemDTO)
        {
            Item item = new Item()
            {
                Name = createItemDTO.Name,
                Price = createItemDTO.Price
            };

            await repository.CreateItemAsync(item);

            return new CreatedAtActionResult(actionName: nameof(GetItemAsync), controllerName: null, routeValues: new { id = item.Id }, value: item.AsDTO());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(int id, UpdateItemDTO updateItemDTO)
        {
            Item? existingItem = await repository.GetItemAsync(id);

            if(existingItem == null)
                return NotFound();

            Item updatedItem = new Item
            {
                Id = existingItem.Id,
                Name = updateItemDTO.Name,
                Price = updateItemDTO.Price,
                CreatedTime = existingItem.CreatedTime
            };

            await repository.UpdateItemAsync(updatedItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(int id)
        {
            Item? deletingItem = await repository.GetItemAsync(id);
            
            if (deletingItem == null)
                return NotFound();
            
            await repository.DeleteItemAsync(id);
            return NoContent();
        }
    }
}