using lscCommon.configLang.commandDomain.Entities;

namespace lscCommon.configLang.commandDomain.Test
{
	/// <summary>
	/// Test class for Lang entity.
	/// </summary>
	public class LangTest
	{
		[Fact]
		public void Should_Create_Lang_With_Valid_Properties()
		{
			// Arrange
			var id = "VALIDID";
			var description = "Valid Description";
			var vn = "Tên hợp lệ";
			var en = "Valid Name";

			// Act
			var lang = new Lang
			{
				Id = id,
				Description = description,
				Vn = vn,
				En = en
			};

			// Assert
			Assert.Equal(id, lang.Id);
			Assert.Equal(description, lang.Description);
			Assert.Equal(vn, lang.Vn);
			Assert.Equal(en, lang.En);
		}

		[Fact]
		public void Should_Update_Lang_Properties()
		{
			// Arrange
			var lang = new Lang
			{
				Id = "OLDID",
				Description = "Old Description",
				Vn = "Tên cũ",
				En = "Old Name"
			};
			var newId = "NEWID";
			var newDescription = "New Description";
			var newVn = "Tên mới";
			var newEn = "New Name";

			// Act
			lang.Update(newId, newDescription, newVn, newEn);

			// Assert
			Assert.Equal(newId, lang.Id);
			Assert.Equal(newDescription, lang.Description);
			Assert.Equal(newVn, lang.Vn);
			Assert.Equal(newEn, lang.En);
		}

		[Fact]
		public void Should_Not_Update_With_Null_Values()
		{
			// Arrange
			var id = "VALIDID";
			var description = "Valid Description";
			var vn = "Tên hợp lệ";
			var en = "Valid Name";
			var lang = new Lang
			{
				Id = id,
				Description = description,
				Vn = vn,
				En = en
			};

			// Act
			lang.Update(null, null, null, null);

			// Assert
			Assert.Equal(id, lang.Id);
			Assert.Equal(description, lang.Description);
			Assert.Equal(vn, lang.Vn);
			Assert.Equal(en, lang.En);
		}

		[Fact]
		public void Should_Validate_Lang_With_Valid_Properties()
		{
			// Arrange
			var lang = new Lang
			{
				Id = "VALIDID",
				Description = "Valid Description",
				Vn = "Tên hợp lệ",
				En = "Valid Name"
			};

			// Act & Assert
			var exception = Record.Exception(() => lang.Validate());
			Assert.Null(exception);
		}
	}
}
