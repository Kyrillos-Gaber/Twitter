﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Twitter.Application.Dto.Tweet;
using Twitter.Application.Services.Contract;

namespace Twitter.Api.Controllers;

[Authorize]
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

    [HttpGet("{id:int}", Name = "GetTweet")]
    public async Task<IActionResult> GetById(int id)
    {
        var res = await _tweetService.Get(id);

        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTweetDto createTweet)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var res = await _tweetService.Create(createTweet, User);

        return CreatedAtRoute("GetTweet", new { id = res.Id }, res);
    }

    [HttpPost("{mainTweetId:int}")]
    public async Task<IActionResult> CreateSubTweet(
        [FromRoute] int mainTweetId,
        [FromBody] CreateTweetDto createTweet)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var res = await _tweetService.CreateSubTweet(mainTweetId, createTweet, User);

        return CreatedAtRoute("GetTweet", new { id = mainTweetId }, res);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        _tweetService.Delete(id);

        return NoContent();
    }

    [HttpPut]
    public IActionResult Update([FromBody] ReadTweetDto readTweet)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var tweet = _tweetService.Update(readTweet);
        return Ok(tweet);
    }
}
