
using lscCommon.configLang.commandContract.DependencyInjection.Options;
using lscCommon.configLang.commandContract.Exceptions;
using lscCommon.configLang.commandDomain.Abstractions.Repositories;
using lscCommon.configLang.commandDomain.Entities;
using Moq;
using System.Data;
using UserCases;

namespace lscCommon.configLang.commandApplication.Test
{
	/// <summary>
	/// Test class for updating Lang entities.
	/// </summary>
	public class UpdateLangTest
	{
		private readonly Mock<ILangRepository> langRepositoryMock;
		private readonly UpdateLangCommandHandler handler;

		public UpdateLangTest()
		{
			langRepositoryMock = new Mock<ILangRepository>();
			handler = new UpdateLangCommandHandler(langRepositoryMock.Object);

		}

		[Fact]
		public async Task Handle_ShouldUpdateLang_WhenRequestIsValid()
		{
			// Arrange
			var command = new UpdateLangCommand
			{
				Id = "VALIDID",
				Description = "Updated Description",
				Vn = "Tên đã cập nhật",
				En = "Updated Name"
			};
			var lang = new Lang
			{
				Id = "VALIDID",
				Description = "Old Description",
				Vn = "Tên cũ",
				En = "Old Name"
			};
			langRepositoryMock
				.Setup(repo => repo.FindByIdAsync(command.Id, It.IsAny<FindOption>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(lang);
			langRepositoryMock.Setup(repo => repo.BeginTransactionAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(Mock.Of<IDbTransaction>());
			langRepositoryMock.Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

			// Act
			var result = await handler.Handle(command, CancellationToken.None);

			// Assert
			langRepositoryMock.Verify(repo => repo.Update(It.IsAny<Lang>()), Times.Once);
			langRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
			Assert.Equal(command.Description, lang.Description);
			Assert.Equal(command.Vn, lang.Vn);
			Assert.Equal(command.En, lang.En);
		}

		[Fact]
		public async Task Handle_ShouldThrowException_WhenTransactionFails()
		{
			// Arrange
			var command = new UpdateLangCommand
			{
				Id = "VALIDID",
				Description = "Updated Description",
				Vn = "Tên đã cập nhật",
				En = "Updated Name"
			};
			var lang = new Lang
			{
				Id = "VALIDID",
				Description = "Old Description",
				Vn = "Tên cũ",
				En = "Old Name"
			};
			langRepositoryMock
				.Setup(repo => repo.FindByIdAsync(command.Id, It.IsAny<FindOption>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(lang);
			var dbTransactionMock = new Mock<IDbTransaction>();
			langRepositoryMock.Setup(repo => repo.BeginTransactionAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(dbTransactionMock.Object);
			langRepositoryMock.Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.ThrowsAsync(new Exception());

			// Act & Assert
			await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
			langRepositoryMock.Verify(repo => repo.Update(It.IsAny<Lang>()), Times.Once);
			langRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		}


		[Fact]
		public async Task Handle_ShouldThrowValidationExceptionWhenLangRepositoryThrows()
		{
			// Arrange
			var command = new UpdateLangCommand
			{
				Id = "VALIDID",
				Description = "Updated Description",
				Vn = "Tên đã cập nhật",
				En = "Updated Name"
			};
			var lang = new Lang
			{
				Id = "VALIDID",
				Description = "Old Description",
				Vn = "Tên cũ",
				En = "Old Name"
			};
			langRepositoryMock.Setup(repo => repo.FindByIdAsync(command.Id, It.IsAny<FindOption>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(lang);
			langRepositoryMock.Setup(repo => repo.BeginTransactionAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(Mock.Of<IDbTransaction>());
			langRepositoryMock.Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.ThrowsAsync(new DomainValidationException("Lang repository exception"));

			// Act & Assert
			await Assert.ThrowsAsync<DomainValidationException>(() => handler.Handle(command, CancellationToken.None));
		}
	}
}
