
//using API.Services;

using Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Application.APIClients;


namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProductsController : ControllerBase
{
    private readonly ProductsAPIClient _productsAPIClient;

   // public string authToken { get; private set; }

    public ProductsController(ProductsAPIClient productsAPIClient)
    {
        _productsAPIClient = productsAPIClient;
    }
    [HttpGet]
    public async Task<IActionResult> GetProductsList(string productCategoryID)
    {
        var productsList = await _productsAPIClient.GetProductsbyCategoriesID(productCategoryID);
        return Ok(productsList);
    }

    [HttpGet("product-Category")]
    public async Task<IActionResult> GetProductsCategories()
    {
        var productsDetails = await _productsAPIClient.GetProductsCategories();
        return Ok(productsDetails);
    }
}

