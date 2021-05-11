using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using API;
using Main.Navigation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Tests.Integration
{
    public class RoverControllerTests
    {
        private readonly HttpClient _client;

        public RoverControllerTests()
        {
            var webHostBuilder = new WebHostBuilder();
            webHostBuilder.UseStartup<Startup>();

            var testServer = new TestServer(webHostBuilder);
            _client = testServer.CreateClient();
        }

        [Fact]
        public async Task returns_400_when_invalid_arguments()
        {
            var response = await _client.PostAsJsonAsync("/rover/move", new {commands = "sadf"});
            
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            
            var jsonString = await response.Content.ReadAsStringAsync();
            var newPosition = JObject.Parse(jsonString);

            Assert.Equal("Input should be formatted as `command,command,command,...`", newPosition["errorMessage"].Value<string>());
        }

        [Theory]
        [InlineData("F,F,F,R,R,L,L,L,B,B,B,F", 2, 3, Heading.West)]
        [InlineData("F,F,R,F,F", 2, 2, Heading.East)]
        [InlineData("B,L,F,R,F,R,F,L", 0, 0, Heading.North)] //wrapping
        public async Task can_move_rover(string commands, int x, int y, Heading heading)
        {
            var response = await _client.PostAsJsonAsync("/rover/move", new {commands});
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonString = await response.Content.ReadAsStringAsync();
            var newPosition = JObject.Parse(jsonString);

            Assert.Equal(x, newPosition["newRoverPosition"]["coordinates"]["x"].Value<int>());
            Assert.Equal(y, newPosition["newRoverPosition"]["coordinates"]["y"].Value<int>());
            Assert.Equal(heading, Enum.Parse<Heading>(newPosition["newRoverPosition"]["heading"].Value<string>()));
        }

        [Fact]
        public async Task can_detect_collisions()
        {
            var response = await _client.PostAsJsonAsync("/rover/move", new {commands = "L,F,F,F,R,F,F"});
            
            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);

            var jsonString = await response.Content.ReadAsStringAsync();
            var newPosition = JObject.Parse(jsonString);

            Assert.Equal(7, newPosition["obstacleCoordinates"]["x"].Value<int>());
            Assert.Equal(2, newPosition["obstacleCoordinates"]["y"].Value<int>());
        }
        
        [Fact]
        public async Task can_move_rover_after_detecting_collision()
        {
            await _client.PostAsJsonAsync("/rover/move", new {commands = "L,F,F,F,R,F,F"});
            var response = await _client.PostAsJsonAsync("/rover/move", new {commands = "L,F"});
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonString = await response.Content.ReadAsStringAsync();
            var newPosition = JObject.Parse(jsonString);

            Assert.Equal(6, newPosition["newRoverPosition"]["coordinates"]["x"].Value<int>());
            Assert.Equal(1, newPosition["newRoverPosition"]["coordinates"]["y"].Value<int>());
            Assert.Equal(Heading.West, Enum.Parse<Heading>(newPosition["newRoverPosition"]["heading"].Value<string>()));
        }

        [Fact]
        public async Task can_get_rover_position()
        {
            await _client.PostAsJsonAsync("/rover/move", new {commands = "F,F,R,F"});
            var response = await _client.GetAsync("/rover/position");
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonString = await response.Content.ReadAsStringAsync();
            var newPosition = JObject.Parse(jsonString);

            Assert.Equal(1, newPosition["coordinates"]["x"].Value<int>());
            Assert.Equal(2, newPosition["coordinates"]["y"].Value<int>());
            Assert.Equal(Heading.East, Enum.Parse<Heading>(newPosition["heading"].Value<string>()));
        }
    }
}