using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twitter.Application.Dto.Tweet;
using Twitter.Application.Services.Contract;
using Twitter.Infrastructure.Entities;
using Twitter.Infrastructure.Repository.Contract;

namespace Twitter.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class TweetController : ControllerBase
{
    private readonly ITweetService _tweetService;

    public TweetController(ITweetService tweetService)
    {
        _tweetService = tweetService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var res = await _tweetService.GetAll();

        return Ok(res);
    }

    [HttpGet("{id:Guid}", Name = "GetTweet")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var res = await _tweetService.Get(id);

        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTweetDto createTweet)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var res = await _tweetService.Create(createTweet);
        
        return CreatedAtRoute("GetTweet", new { id = res.Id }, res );
    }
}
