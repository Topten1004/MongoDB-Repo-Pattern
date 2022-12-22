using FixWithCustomSerialization.Models;
using FixWithCustomSerialization.Services;
using Microsoft.AspNetCore.Mvc;

namespace FixWithCustomSerialization.Controllers;

/// <summary>
/// User MediaFile Controller
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class MediaController : ControllerBase
{
    private readonly ILogger<MediaController> _logger;
    private readonly MediafileService _db;

    public MediaController(ILogger<MediaController> logger, MediafileService db)
    {
        _db = db;
        _logger = logger;
    }

    /// <summary>
    /// Update a MediaFile record
    /// </summary>
    /// <param name="record">MediaFile</param>
    /// <returns>MediaFile</returns>
    /// <response code="200"></response>
    /// <response code="404">Not Found</response>
    [HttpPost()]
    [Route("UpdateMedia")]
    [ProducesResponseType(typeof(MediaFile), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateMedia([FromBody] MediaFile media)
    {
        try
        {
            _db.CreateAsync(media);
            return Ok(media);
        }
        catch (Exception ex)
        {
            var msg = $"Method: GetUsers, Exception: {ex.Message}";

            _logger.LogError(msg);

            return Problem(title: "/User/GetUsers", detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }

        /* todo: Implement the method
         * 
         * 1. media will be contained in the ImageDataB64 prop. It should have been already saved to the webroots folder by the "read" method of MediaFileJsonConverter
         * 2. Save the media object in Mongo db
         * 3. return the MediaFile object
         * 4. We expect the "write" method of MediaFileJsonConverter to create the public URL
         * 
         * */
    }

    /// <summary>
    /// Gets a MediaFile record based in the Name
    /// </summary>
    /// <param name="Name">Primary Key</param>
    /// <returns>MediaFile</returns>
    /// <response code="200">MediaFile</response>
    /// <response code="404">Record not found</response>
    [HttpGet()]
    [Route("GetMedia/{Name:Guid}")]
    [ProducesResponseType(typeof(MediaFile), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMedia(string Name)
    {
        MediaFile media = new MediaFile();
        try
        {
            return Ok(media);
        }
        catch (Exception ex)
        {
            var msg = $"Method: GetUsers, Exception: {ex.Message}";

            _logger.LogError(msg);

            return Problem(title: "/User/GetUsers", detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }


        /*
         * return _mongoDb.getcollection<MediaFile>.Find(m=>m.Name==Name).SingleAsync();
         * 
         * 1. We expect the "write" method of MediaFileJsonConverter to create the public URL
         * 
         */

    }
}