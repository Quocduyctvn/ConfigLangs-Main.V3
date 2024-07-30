using lscCommon.configLang.commandContract.Exceptions;
using lscCommon.configLang.commandDomain.Abstractions.Repositories;
using lscCommon.configLang.commandDomain.Constants;
using lscCommon.configLang.commandDomain.Entities;
using Moq;
using System.ComponentModel.DataAnnotations;
using System.Data;
using UserCases;

namespace lscCommon.configLang.commandApplication.Test
{
	/// <summary>
	/// Test class for creating Lang entities.
	/// </summary>
	public class CreateLangTest
	{
		private readonly Mock<ILangRepository> langRepositoryMock;
		private readonly CreateLangCommandHandler handler;

		public CreateLangTest()
		{
			langRepositoryMock = new Mock<ILangRepository>();
			handler = new CreateLangCommandHandler(langRepositoryMock.Object);
		}

		[Fact]
		public async Task Handle_ShouldCreateLang_WhenRequestIsValid()
		{
			// Arrange
			var command = new CreateLangCommand
			{
				Id = "CONFIG_ADD",
				Description = "dùng để thêm",
				Vn = "Thêm",
				En = "Create"
			};
			var lang = new Lang
			{
				Id = command.Id,
				Description = command.Description,
				Vn = command.Vn,
				En = command.En
			};
			langRepositoryMock.Setup(repo => repo.BeginTransactionAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(Mock.Of<IDbTransaction>());
			langRepositoryMock.Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(1);

			// Act
			var result = await handler.Handle(command, CancellationToken.None);

			// Assert
			langRepositoryMock.Verify(repo => repo.Add(It.IsAny<Lang>()), Times.Once);
			langRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
			Assert.Equal(lang.Id, result.Data.Id);
			Assert.Equal(lang.Description, result.Data.Description);
			Assert.Equal(lang.Vn, result.Data.Vn);
			Assert.Equal(lang.En, result.Data.En);
		}

		[Fact]
		public async Task Handle_ShouldThrowException_WhenTransactionFails()
		{
			// Arrange
			var command = new CreateLangCommand
			{
				Id = "CONFIG_ADD",
				Description = "dùng để thêm",
				Vn = "Thêm",
				En = "Create"
			};
			langRepositoryMock.Setup(repo => repo.BeginTransactionAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(Mock.Of<IDbTransaction>());
			langRepositoryMock.Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.ThrowsAsync(new Exception());

			// Act & Assert
			await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
			langRepositoryMock.Verify(repo => repo.Add(It.IsAny<Lang>()), Times.Once);
			langRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		}

		[Fact]
		public async Task Handle_ShouldThrowValidationExceptionForInvalidId()
		{
			// Arrange
			var command = new CreateLangCommand
			{
				Id = null, // Invalid Id
				Description = "dùng để thêm",
				Vn = "Thêm",
				En = "Create"
			};

			// Act
			var exception = await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));

			// Assert
			Assert.Equal(LangConstant.INVALID_ID_NOTNULL_OR_EMPTY_MESSAGE, exception.Message); // Adjust message to match your actual validation message
		}

		[Fact]
		public async Task Handle_ShouldThrowValidationExceptionForInvalidVn()
		{
			// Arrange
			var command = new CreateLangCommand
			{
				Id = "CONFIG_ADD",
				Description = "dùng để thêm",
				Vn = "", // Invalid Vn
				En = "Create"
			};

			// Act
			var exception = await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));

			// Assert
			Assert.Equal(LangConstant.INVALID_VN_NOT_EMPTY_MESSAGE, exception.Message); // Adjust message to match your actual validation message
		}

		[Fact]
		public async Task Handle_ShouldThrowValidationExceptionWhenLangRepositoryThrows()
		{
			// Arrange
			var command = new CreateLangCommand
			{
				Id = "CONFIG_ADD",
				Description = "dùng để thêm",
				Vn = "Thêm",
				En = "Create"
			};
			var lang = new Lang
			{
				Id = command.Id,
				Description = command.Description,
				Vn = command.Vn,
				En = command.En
			};
			langRepositoryMock.Setup(repo => repo.BeginTransactionAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(Mock.Of<IDbTransaction>());
			langRepositoryMock.Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.ThrowsAsync(new DomainValidationException("Lang repository exception"));

			// Act & Assert
			await Assert.ThrowsAsync<DomainValidationException>(() => handler.Handle(command, CancellationToken.None));
		}
	}
}
