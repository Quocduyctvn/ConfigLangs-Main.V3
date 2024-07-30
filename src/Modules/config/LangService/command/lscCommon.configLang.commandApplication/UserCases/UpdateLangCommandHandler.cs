using lscCommon.configLang.commandContract.DependencyInjection.Options;
using lscCommon.configLang.commandContract.Shared;
using lscCommon.configLang.commandContract.Validators;
using lscCommon.configLang.commandDomain.Abstractions.Repositories;
using MediatR;

namespace UserCases
{
	/// <summary>
	/// Request to update Lang, contain Lang: Id, description, vn, en
	/// </summary>
	public class UpdateLangCommand : IRequest<Result>
	{
		public string Id { get; set; }
		public string? Description { get; set; }
		public string Vn { get; set; }
		public string? En { get; set; }
	}

	public class UpdateLangCommandHandler : IRequestHandler<UpdateLangCommand, Result>
	{
		private readonly ILangRepository langRepository;

		/// <summary>
		/// Handler for update lang request
		/// </summary>
		public UpdateLangCommandHandler(ILangRepository langRepository)
		{
			this.langRepository = langRepository;
		}

		/// <summary>
		///  Handle update lang request
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<Result> Handle(UpdateLangCommand request, CancellationToken cancellationToken)
		{
			var validator = Validator.Create(request);
			// Create validator rule for Id that Id must be valid key
			validator.RuleFor(x => x.Id).MustBeValidKey();
			validator.Validate();

			using var transaction = await langRepository.BeginTransactionAsync(cancellationToken);
			try
			{
				var findOption = new FindOption
				{
					AllowNullReturn = true,
					IsTracking = true
				};
				// Need tracking to delete lang
				var lang = await langRepository.FindByIdAsync(request.Id, findOption, cancellationToken);
				// Update lang, keep original data if request is null
				lang!.Update(request.Id, request.Description, request.Vn, request.En);
				// Mark lang as Updated state
				langRepository.Update(lang);
				// Save lang to database
				await langRepository.SaveChangesAsync(cancellationToken);
				// Commit transaction
				transaction.Commit();
				return Result.Ok();
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