using lscCommon.configLang.queryContract.Enumerations;
using lscCommon.configLang.queryDomain.Abstractions.Repositories;
using lscCommon.configLang.queryDomain.Entities;
using Moq;
using System.Linq.Expressions;
using UserCases;
namespace lscCommon.configLang.queryApplication.Test
{
	public class GetAllLangTest
	{
		private readonly Mock<ILangRepository> langRepositoryMock;
		private readonly GetAllLangQueryHandler handler;

		public GetAllLangTest()
		{
			langRepositoryMock = new Mock<ILangRepository>();
			handler = new GetAllLangQueryHandler(langRepositoryMock.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnAllLangs()
		{
			// Arrange
			var langs = new List<Lang>
			{
				new() { Id = "LANG1", Description = "Description 1", Vn = "VN1", En = "EN1" },
				new() { Id = "LANG2", Description = "Description 2", Vn = "VN2", En = "EN2" }
			};

			langRepositoryMock
				.Setup(repo => repo.FindAll(false, null, It.IsAny<Expression<Func<Lang, object>>[]>()))
				.Returns(langs.AsQueryable());

			// Act
			var result = await handler.Handle(new GetAllLangsQuery(), CancellationToken.None);

			// Assert
			Assert.Equal(StatusCode.Ok, result.StatusCode);
			Assert.Equal(langs, result.Data);
		}

		[Fact]
		public async Task Handle_ShouldReturnEmptyList_WhenNoLangsFound()
		{
			// Arrange
			langRepositoryMock
				.Setup(repo => repo.FindAll(false, null, It.IsAny<Expression<Func<Lang, object>>[]>()))
				.Returns(new List<Lang>().AsQueryable());

			// Act
			var result = await handler.Handle(new GetAllLangsQuery(), CancellationToken.None);

			// Assert
			Assert.Equal(StatusCode.Ok, result.StatusCode);
			Assert.Empty(result.Data);
		}
	}
}
