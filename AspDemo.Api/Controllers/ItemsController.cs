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
        private IItemsRepository Repository;
        public ItemsController(IItemsRepository _Repository)
        {
            this.Repository = _Repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDTO>?>> GetItemsAsync()
        {
            IEnumerable<ItemDTO>? Items =  (await Repository.GetItemsAsync())?
                                            .Select(Item => Item.AsDTO());

            return Ok(Items);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ItemDTO>> GetItemAsync(int Id)
        {
            Item? Item = await Repository.GetItemAsync(Id);

            return Item == null ? NotFound() : Ok(Item.AsDTO()); 
        }

        [HttpPost]
        public async Task<ActionResult<ItemDTO>> CreateItemAsync(CreateItemDTO ItemToCreateDTO)
        {
            Item Item = new Item()
            {
                Name = ItemToCreateDTO.Name,
                Price = ItemToCreateDTO.Price
            };

            await Repository.CreateItemAsync(Item);

            return new CreatedAtActionResult(actionName: nameof(GetItemAsync), controllerName: null, routeValues: new { Id = Item.Id }, value: Item.AsDTO());
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateItemAsync(int Id, UpdateItemDTO ItemToUpdateDTO)
        {
            Item? ItemToUpdate = await Repository.GetItemAsync(Id);

            if(ItemToUpdate == null)
                return NotFound();

            ItemToUpdate.Name = ItemToUpdateDTO.Name;
            ItemToUpdate.Price = ItemToUpdateDTO.Price;

            await Repository.UpdateItemAsync(ItemToUpdate);

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteItemAsync(int Id)
        {
            Item? ItemToDelete = await Repository.GetItemAsync(Id);
            
            if (ItemToDelete == null)
                return NotFound();
            
            await Repository.DeleteItemAsync(ItemToDelete);
            return NoContent();
        }
    }
}