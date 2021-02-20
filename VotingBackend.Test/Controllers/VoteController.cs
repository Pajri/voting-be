using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VotingBackend.Controllers;
using VotingBackend.Dtos;
using VotingBackend.Services;
using Xunit;

namespace VotingBackend.Test.Controllers
{
    public class VoteController
    {
        [Fact]
        public async Task Index                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          ()
        {
            //arrange
            var id = Guid.Parse("0160f336-6b40-4916-adf3-094fe7db265b");
            var mockService = new Mock<IVotingService>();
            mockService
                .Setup(service => service.GetVoteDetail(id))
                .ReturnsAsync(VotingTest(id));

            var controller = new VotingController(mockService.Object);

            //act
            var result = await controller.Index(id);

            //assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);

            var model = Assert.IsAssignableFrom<VotingDto>(actionResult.Value);
            Assert.Equal(id, model.ID);
            Assert.Equal("test name", model.Name);
            Assert.Equal("test description", model.Description);
            Assert.Equal(2, model.VotersCount);
            Assert.Equal("test category", model.Category);
        }

        private VotingDto VotingTest(Guid id)
        {
            return new VotingDto
            {
                ID = id,
                Name = "test name",
                Description = "test description",
                VotersCount = 2,
                Category = "test category"
            };
        }
    }
}
