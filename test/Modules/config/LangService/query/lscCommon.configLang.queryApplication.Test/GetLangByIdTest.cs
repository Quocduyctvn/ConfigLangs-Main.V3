
using lscCommon.configLang.queryContract.DependencyInjection.Options;
using lscCommon.configLang.queryContract.Enumerations;
using lscCommon.configLang.queryContract.Exceptions;
using lscCommon.configLang.queryDomain.Abstractions.Repositories;
using lscCommon.configLang.queryDomain.Constants;
using lscCommon.configLang.queryDomain.Entities;
using Moq;
using UserCases;

namespace lscCommon.configLang.queryApplication.Test
{
	public class GetLangByIdTest
	{
		private readonly Mock<ILangRepository> langRepositoryMock;
		private readonly GetLangByIdQueryHandler handler;

		public GetLangByIdTest()
		{
			langRepositoryMock = new Mock<ILangRepository>();
			handler = new GetLangByIdQueryHandler(langRepositoryMock.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnLang_WhenLangExists()
		{
			// Arrange
			var lang = new Lang
			{
				Id = "LANG1",
				Description = "Description 1",
				Vn = "VN1",
				En = "EN1"
			};

			langRepositoryMock
				.Setup(repo => repo.FindByIdAsync(It.IsAny<string>(), It.IsAny<FindOption>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(lang);

			var query = new GetLangByIdQuery("LANG1");

			// Act
			var result = await handler.Handle(query, CancellationToken.None);

			// Assert
			Assert.Equal(StatusCode.Ok, result.StatusCode);
			Assert.NotNull(result.Data);
			Assert.Equal(lang.Id, result.Data.Id);
			Assert.Equal(lang.Description, result.Data.Description);
			Assert.Equal(lang.Vn, result.Data.Vn);
			Assert.Equal(lang.En, result.Data.En);
		}

		[Fact]
		public async Task Handle_ShouldThrowNotFoundException_WhenLangNotFound()
		{
			// Arrange
			langRepositoryMock
				.Setup(repo => repo.FindByIdAsync(It.IsAny<string>(), It.IsAny<FindOption>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync((Lang)null);

			var query = new GetLangByIdQuery("LANG1");

			// Act & Assert
			var exception = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, CancellationToken.None));
			Assert.Equal(LangConstant.INVALID_ID_IS_NOTFOUND_MESSAGE, exception.Message);
		}
	}
}