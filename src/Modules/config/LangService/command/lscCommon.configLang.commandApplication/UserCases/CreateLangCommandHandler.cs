using lscCommon.configLang.commandContract.DependencyInjection.Options;
using lscCommon.configLang.commandContract.Enumerations;
using lscCommon.configLang.commandContract.Errors;
using lscCommon.configLang.commandContract.Shared;
using lscCommon.configLang.commandDomain.Abstractions.Repositories;
using lscCommon.configLang.commandDomain.Constants;
using lscCommon.configLang.commandDomain.Entities;
using MediatR;
using Entities = lscCommon.configLang.commandDomain.Entities;

namespace UserCases
{
	/// <summary>
	/// Request to create Lang, contain Id, description, vn, en 
	/// </summary>
	public class CreateLangCommand : IRequest<Result<Entities.Lang>>
	{
		public string Id { get; set; }
		public string? Description { get; set; }
		public string Vn { get; set; }
		public string? En { get; set; }
	}

	public class CreateLangCommandHandler : IRequestHandler<CreateLangCommand, Result<Lang>>
	{
		private readonly ILangRepository langRepository;

		/// <summary>
		/// Handler for create lang request
		/// </summary>
		public CreateLangCommandHandler(ILangRepository langRepository)
		{
			this.langRepository = langRepository;
		}

		/// <summary>
		/// Handle create lang request
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<Result<Lang>> Handle(CreateLangCommand request, CancellationToken cancellationToken)
		{
			// Create new Lang from request
			var lang = new Lang()
			{
				Id = request.Id,
				Description = request.Description,
				Vn = request.Vn,
				En = request.En,
			};

			// Begin transaction
			using var transaction = await langRepository.BeginTransactionAsync(cancellationToken);
			try
			{
				var findOption = new FindOption
				{
					AllowNullReturn = true, // Allow null return if not found
					IsTracking = true
				};

				// Check if Lang with the given Id exists
				var existingLang = await langRepository.FindByIdAsync(request.Id, findOption, cancellationToken);

				if (existingLang != null)
				{
					var errorCode = LangConstant.IS_EXIST.Replace(Args.PROPERTY_NAME, request.Id);
					var error = new Error(ErrorType.Conflict, errorCode, errorCode);
					return new Result<Lang>(false, StatusCode.Conflict, error: error);
				}

				// Add data
				langRepository.Add(lang);

				// Save data
				await langRepository.SaveChangesAsync(cancellationToken);

				// Commit transaction
				transaction.Commit();
				return lang;
			}
			catch (Exception)
			{
				// Rollback transaction
				transaction.Rollback();
				throw;
			}
		}
	}
}
