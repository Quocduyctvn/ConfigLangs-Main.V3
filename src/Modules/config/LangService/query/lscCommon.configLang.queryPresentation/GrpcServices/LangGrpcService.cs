using Grpc.Core;
using LangGrpc;
using lscCommon.configLang.queryContract.Exceptions;
using MediatR;
using UserCases;

namespace lscCommon.configLang.queryPresentation.GrpcServices
{
	/// <summary>
	/// Lang grpc service override from LangBase
	/// </summary>
	public class LangGrpcService : Lang.LangBase
	{
		private readonly IMediator mediator;

		public LangGrpcService(IMediator mediator)
		{
			this.mediator = mediator;
		}

		/// <summary>
		/// Get Lang by id
		/// </summary>
		/// <param name="request">Rpc get Lang request</param>
		/// <param name="context">Context of request</param>
		/// <returns>Response contain Lang</returns>
		/// <exception cref="RpcException">Exception will throw when resources not found or catch any exception</exception>
		public override async Task<GetLangResponse> GetLang(GetLangRequest request, ServerCallContext context)
		{
			var id = request.Id;
			var query = new GetLangByIdQuery(id);
			try
			{
				var result = await mediator.Send(query);
				var value = result.Data;
				var data = new GetLangResponse
				{
					Id = value.Id,
					Description = value.Description ?? string.Empty,
					Vn = value.Vn,
					En = value.En ?? string.Empty,
				};
				return data;
			}
			catch (NotFoundException e)
			{
				throw new RpcException(new Status(StatusCode.NotFound, e.Message));
			}
			catch (Exception)
			{
				throw new RpcException(new Status(StatusCode.Internal, "Internal Server Error"));
			}
		}

		/// <summary>
		/// Get all Langs
		/// </summary>
		/// <param name="request">Rpc get all Langs request</param>
		/// <param name="context">Context of request</param>
		/// <returns>Response contain list of Langs</returns>
		/// <exception cref="RpcException">Exception will throw when catch any exception</exception>
		public override async Task<GetAllLangResponse> GetAllLang(GetAllLangRequest request,
																		ServerCallContext context)
		{
			var query = new GetAllLangsQuery();
			try
			{
				var result = await mediator.Send(query);
				var data = new List<GetLangResponse>();
				foreach (var lang in result.Data)
					data.Add(new GetLangResponse
					{
						Id = lang.Id,
						Description = lang.Description ?? string.Empty,
						Vn = lang.Vn,
						En = lang.En ?? string.Empty,
					});
				var list = new GetAllLangResponse();
				list.Lang.Add(data);
				return list;
			}
			catch (Exception)
			{
				throw new RpcException(new Status(StatusCode.Internal, "Internal Server Error"));
			}
		}
	}
}