using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Forum.Application.MainTopics;
using Forum.Application.MainUsers;
using Forum.Domain.user;
using Forum.Application.Exceptions;
using Forum.Domain.topic;

namespace ForumManagement.Application.Tests
{
    public class TopicServiceTests
    {
        private readonly Mock<IUserStore<User>> _userStore;
        private readonly Mock<IOptions<IdentityOptions>> _identityOptions;
        private readonly Mock<IPasswordHasher<User>> _passwordHasher;
        private readonly List<IUserValidator<User>> _userValidators;
        private readonly List<IPasswordValidator<User>> _passwordValidators;
        private readonly Mock<ILookupNormalizer> _lookupNormalizer;
        private readonly Mock<IdentityErrorDescriber> _identityErrorDescriber;
        private readonly Mock<IServiceProvider> _serviceProvider;
        private readonly Mock<ILogger<UserManager<User>>> _logger;
        private readonly Mock<UserManager<User>> _userManager;
        private readonly Mock<IUserRepository> _userRepo;
        private readonly Mock<ITopicRepository> _topicRepo;
        private readonly TopicService _topicService;

        public TopicServiceTests()
        {
            // Mock the required dependencies for UserManager
            _userStore = new Mock<IUserStore<User>>();
            _identityOptions = new Mock<IOptions<IdentityOptions>>();
            _passwordHasher = new Mock<IPasswordHasher<User>>();
            _userValidators = new List<IUserValidator<User>> { new Mock<IUserValidator<User>>().Object };
            _passwordValidators = new List<IPasswordValidator<User>> { new Mock<IPasswordValidator<User>>().Object };
            _lookupNormalizer = new Mock<ILookupNormalizer>();
            _identityErrorDescriber = new Mock<IdentityErrorDescriber>();
            _serviceProvider = new Mock<IServiceProvider>();
            _logger = new Mock<ILogger<UserManager<User>>>();

            // Initialize the UserManager with mocked dependencies
            _userManager = new Mock<UserManager<User>>(
                _userStore.Object,
                _identityOptions.Object,
                _passwordHasher.Object,
                _userValidators,
                _passwordValidators,
                _lookupNormalizer.Object,
                _identityErrorDescriber.Object,
                _serviceProvider.Object,
                _logger.Object
            );

            // Initialize the other mocks and TopicService
            _topicRepo = new Mock<ITopicRepository>();
            _userRepo = new Mock<IUserRepository>();

            _topicService = new TopicService(
                _topicRepo.Object,
                _userManager.Object,
                _userRepo.Object
            );
        }

        [Fact]
        public async Task CreateTopicAsync_WhenUserNotFound_ShouldThrowUserNotFoundException()
        {
            // Arrange
            var authorId = 1; // Assuming an existing author ID
            var topicCreateModel = new TopicCreateModel(); // Provide necessary topic data for creation

            _userManager.Setup(x => x.FindByIdAsync(authorId.ToString()))
                        .ReturnsAsync((User)null); // Simulate user not found

            // Act
            var task = async () => await _topicService.CreateTopicAsync(CancellationToken.None, topicCreateModel, authorId);

            // Assert
            var exception = await Assert.ThrowsAsync<UserNotFoundException>(task);
            Assert.NotNull(exception);
        }

        [Fact]
        public async Task DeleteTopicAsync_WhenTopicNotFound_ShouldThrowTopicNotFoundException()
        {
            // Arrange
            var topicId = 1; // Assuming an existing topic ID
            var authorId = 1; // Assuming an existing author ID

            _topicRepo.Setup(x => x.Exists(CancellationToken.None, topicId))
                      .ReturnsAsync(false); // Simulate topic not found

            // Act
            var task = async () => await _topicService.DeleteTopicAsync(CancellationToken.None, topicId, authorId);

            // Assert
            var exception = await Assert.ThrowsAsync<TopicNotFoundException>(task);
            Assert.NotNull(exception);
        }

        [Fact]
        public async Task GetTopicByIdAsync_WhenTopicNotFound_ShouldThrowTopicNotFoundException()
        {
            // Arrange
            var topicId = 1; // Assuming an existing topic ID

            _topicRepo.Setup(x => x.Exists(CancellationToken.None, topicId))
                      .ReturnsAsync(false); // Simulate topic not found

            // Act
            var task = async () => await _topicService.GetTopicByIdAsync(topicId, CancellationToken.None);

            // Assert
            var exception = await Assert.ThrowsAsync<TopicNotFoundException>(task);
            Assert.NotNull(exception);
        }

        [Fact]
        public async Task UpdateTopicAsync_WhenTopicNotFound_ShouldThrowTopicNotFoundException()
        {
            // Arrange
            var topicId = 1; // Assuming an existing topic ID
            var authorId = 1; // Assuming an existing author ID
            var topicUpdateModel = new TopicUpdateModel(); // Provide necessary topic data for update

            _topicRepo.Setup(x => x.Exists(CancellationToken.None, topicId))
                      .ReturnsAsync(false); // Simulate topic not found

            // Act
            var task = async () => await _topicService.UpdateTopicAsync(CancellationToken.None, topicUpdateModel, topicId, authorId);

            // Assert
            var exception = await Assert.ThrowsAsync<TopicNotFoundException>(task);
            Assert.NotNull(exception);
        }

        [Fact]
        public async Task ChangeStateAsync_WhenTopicNotFound_ShouldThrowTopicNotFoundException()
        {
            // Arrange
            var topicId = 1; // Assuming an existing topic ID

            _topicRepo.Setup(x => x.Exists(CancellationToken.None, topicId))
                      .ReturnsAsync(false); // Simulate topic not found

            // Act
            var task = async () => await _topicService.ChangeStateAsync(CancellationToken.None, topicId, true);

            // Assert
            var exception = await Assert.ThrowsAsync<TopicNotFoundException>(task);
            Assert.NotNull(exception);
        }

        [Fact]
        public async Task ChangeStatusAsync_WhenTopicNotFound_ShouldThrowTopicNotFoundException()
        {
            // Arrange
            var topicId = 1; // Assuming an existing topic ID

            _topicRepo.Setup(x => x.Exists(CancellationToken.None, topicId))
                      .ReturnsAsync(false); // Simulate topic not found

            // Act
            var task = async () => await _topicService.ChangeStatusAsync(CancellationToken.None, topicId, true);

            // Assert
            var exception = await Assert.ThrowsAsync<TopicNotFoundException>(task);
            Assert.NotNull(exception);
        }

        [Fact]
        public async Task GetOldTopicsAsync_ShouldReturnListOfOldTopics()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            var days = 30; // Assuming you want topics older than 30 days

            var oldTopics = new List<Topic>(); // Provide sample list of old topics

            _topicRepo.Setup(x => x.GetOldTopicsAsync(cancellationToken, days))
                      .ReturnsAsync(oldTopics); // Simulate returning old topics

            // Act
            var result = await _topicService.GetOldTopicsAsync(cancellationToken, days);

            // Assert
            Assert.NotNull(result);
            // Add more assertions as needed
        }



    }
}