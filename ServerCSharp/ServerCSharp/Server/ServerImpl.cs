using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Generated.Protobuf;
using Grpc.Core;
using Grpc.Core.Utils;
using ServerCSharp.Protobuf;
using ServerCSharp.Service;
using ServerCSharp.Utils.Exceptions;


namespace ServerCSharp.Server
{
   public class ServerImpl : AppService.AppServiceBase
   {
      private readonly AgeCategoryService _ageCategoryService;
      private readonly TrialService _trialService;
      private readonly UserService _userService;
      private readonly ParticipantService _participantService;
      private Dictionary<String, IServerStreamWriter<Response>> _loggedClients;

      public ServerImpl(AgeCategoryService ageCategoryService, TrialService trialService, UserService userService,
         ParticipantService participantService)
      {
         this._ageCategoryService = ageCategoryService;
         this._trialService = trialService;
         this._userService = userService;
         this._participantService = participantService;
         this._loggedClients = new Dictionary<string, IServerStreamWriter<Response>>();
      }

      public override async Task sendRequest(IAsyncStreamReader<Request> requestStream, IServerStreamWriter<Response> responseStream, ServerCallContext context)
      {
         await requestStream.ForEachAsync(async o =>
         {
            await responseStream.WriteAsync(HandleRequest(responseStream, o));
         });

      }

      public Response HandleRequest(IServerStreamWriter<Response> responseStream, Request request)
      {
         switch (request.RequestType)
         {
            case RequestType.GetAgeCategoriesRequest:
            {
               return HandleGetAgeCategories(request);
            }
            case RequestType.GetParticipantsForTrialRequest:
            {
               return HandleGetParticipantsForTrial(request);
            }
            case RequestType.GetTrialsRequest:
            {
               return HandleGetTrials(request);
            }
            case RequestType.LoginRequest:
            {
               return HandleLogin(request, responseStream);
            }
            case RequestType.LogoutRequest:
            {
               return HandleLogout(request);
            }
            case RequestType.RegisterParticipantRequest:
            {
               return HandleRegisterParticipant(request);
            }
            default:
            {
               return HandleUnknownRequest(request);
            }
         }
      }

      private Response HandleInitialiseConnection(Request request)
      {
         return new Response {ResponseType = ResponseType.OkResponse};
      }

      private Response HandleUnknownRequest(Request request)
      {
         return new Response {ResponseType = ResponseType.InvalidRequest};
      }

      private Response HandleLogin(Request request, IServerStreamWriter<Response> responseStream)
      {
         if (_loggedClients.ContainsKey(request.User.UserName))
         {
            return
               new Response
               {
                  ResponseType = ResponseType.FailureResponse,
                  ErrorMessage = "USER ALREADY LOGGED IN\n"
               };
         }

         if (_userService.LogIn(request.User.UserName, request.User.Password))
         {
            _loggedClients.Add(request.User.UserName, responseStream);
            return new Response {ResponseType = ResponseType.OkResponse};
         }

         return
            new Response
            {
               ResponseType = ResponseType.FailureResponse,
               ErrorMessage = "INVALID USERNAME OR PASSWORD!\n"
            };
      }

      private Response HandleLogout(Request request)
      {
         if (!_loggedClients.ContainsKey(request.Username))
         {
            return
               new Response
               {
                  ResponseType = ResponseType.FailureResponse,
                  ErrorMessage = "USER ISN'T LOGGED IN\n"
               };
         }

         _loggedClients.Remove(request.Username);
         return new Response {ResponseType = ResponseType.OkResponse};
      }

      private Response HandleGetTrials(Request request)
      {
         return
            new Response
            {
               ResponseType = ResponseType.OkResponse,
               Trials = {ProtoUtils.TrialsListToDto(_trialService.GetAll())}
            };
      }

      private Response HandleGetAgeCategories(Request request)
      {
         if (request.RequestType != RequestType.GetAgeCategoriesRequest)
         {
            return new Response {ResponseType = ResponseType.InvalidRequest};
         }
         return
            new Response
            {
               ResponseType = ResponseType.OkResponse,
               AgeCategories = {ProtoUtils.AgeCategoryListToDto(_ageCategoryService.GetAll())}
            };
      }

      private Response HandleGetParticipantsForTrial(Request request)
      {
         try
         {
            return
               new Response
               {
                  ResponseType = ResponseType.OkResponse,
                  Participants =
                  {
                     ProtoUtils.ParticipantsListToDto(_trialService.GetParticipantsForTrial(request.TrialName,
                        request.AgeCategoryName))
                  }
               };
         }
         catch (CustomException e)
         {
            return new Response {ResponseType = ResponseType.FailureResponse, ErrorMessage = e.Message};
         }
      }

      private Response HandleRegisterParticipant(Request request)
      {
         try
         {
            List<String> trials = new List<string>();
            foreach (var requestTrial in request.Trials)
            {
               trials.Add(requestTrial);
            }
            _participantService.RegisterParticipant(request.ParticipantName, request.ParticipantAge, trials);
            AgeCategoryDTO ageCategoryDto =
               ProtoUtils.AgeCategoryToDto(_ageCategoryService.FindSuitableAgeCategory(request.ParticipantAge.ToString()));

            // notify

            foreach (var key in _loggedClients.Keys)
            {
               if (!key.Equals(request.Username))
               {
                  _loggedClients[key].WriteAsync(new Response
                  {
                     ResponseType = ResponseType.UpdateResponse,
                     RegisteredTrials = {request.Trials},
                     AgeCategoryName = ageCategoryDto.Name
                  });
               }
            }

            return new Response
            {
               ResponseType = ResponseType.OkResponse,
               AgeCategoryName = ageCategoryDto.Name,
               RegisteredTrials = {request.Trials}
            };
         }
         catch (CustomException e)
         {
            return new Response {ResponseType = ResponseType.FailureResponse, ErrorMessage = e.Message};
         }
      }
   }
}