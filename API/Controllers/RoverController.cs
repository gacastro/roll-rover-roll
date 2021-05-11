using System;
using API.Helpers;
using API.Models;
using Main;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RoverController : ControllerBase
    {
        private readonly IParseCommands _commandParser;
        private readonly IAmRover _rover;

        public RoverController(IParseCommands commandParser, IAmRover rover)
        {
            _commandParser = commandParser;
            _rover = rover;
        }

        [HttpGet]
        public IActionResult Position()
        {
            return Ok(_rover.Position);
        }

        [HttpPost]
        public IActionResult Move(ExecutionPlan executionPlan)
        {
            try
            {
                var commands = _commandParser.Parse(executionPlan.Commands);

                if (commands.Count == 0)
                {
                    return BadRequest(new {errorMessage = "Input should be formatted as `command,command,command,...`"});
                }

                foreach (var command in commands)
                {
                    _rover.Execute(command);
                    if (_rover.ObstacleCoordinates !=null)
                    {
                        return ConflictResponse();
                    }
                }
            }
            catch (Exception exception)
            {
                return StatusCode(500, new {errorMessage = $"The following unexpected error occurred: {exception.Message}"});
            }

            return Ok(new {newRoverPosition = _rover.Position});
        }

        private IActionResult ConflictResponse()
        {
            //because rover is a singleton (readme) we need to reset this
            //in order to allow the rover to continue to move 
            var obstacleCoordinates = _rover.ObstacleCoordinates;
            _rover.ObstacleCoordinates = null;
            
            return Conflict(new {obstacleCoordinates});
        }
    }
}