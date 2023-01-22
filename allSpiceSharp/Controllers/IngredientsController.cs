namespace allSpiceSharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
  private readonly IngredientsService _ingredientsService;
  private readonly Auth0Provider _auth0provider;
  public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0Provider)
  {
    _ingredientsService = ingredientsService;
    _auth0provider = auth0Provider;
  }

  [HttpGet("{id}")]
  public ActionResult<Ingredient> GetIngredientById(int id)
  {
    try
    {
      Ingredient ingredient = _ingredientsService.GetIngredientsById(id);
      return Ok(ingredient);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Ingredient>> CreateIngredient([FromBody] Ingredient ingredientData)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      Ingredient ingredient = _ingredientsService.CreateIngredient(ingredientData);
      return Ok(ingredient);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<string>> RemoveIngredient(int id)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _ingredientsService.RemoveIngredient(id, userInfo.Id);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
