using System;
using Generated.Protobuf;
using Grpc.Core;
using ServerCSharp.Domain;
using ServerCSharp.Repository.DBRepository;
using ServerCSharp.Repository.DBRepository.DBConnection;
using ServerCSharp.Repository.Interface;
using ServerCSharp.Server;
using ServerCSharp.Service;
using ServerCSharp.Validator;

namespace ServerCSharp
{
   public class Program
   {
      public static void Main(string[] args)
      {
         // --------------- VALIDATORS ---------------
         IValidator<AgeCategory> ageCategoryValidator = new AgeCategoryValidator();
         IValidator<Trial> trialValidator = new TrialValidator();
         IValidator<User> userValidator = new UserValidator();
         IValidator<Participant> participantValidator = new ParticipantValidator();

         // --------------- REPOSITORIES ---------------
         IAgeCategoryRepository ageCategoryRepository = new AgeCategoryDatabaseRepository("ageCategories", QueryBuilder.GetInstance());
         IParticipantRepository participantRepository = new ParticipantDatabaseRepository("participants", QueryBuilder.GetInstance());
         ITrialRepository trialRepository = new TrialDatabaseRepository("trials", QueryBuilder.GetInstance());
         IUserRepository userRepository = new UserDatabaseRepository("users", QueryBuilder.GetInstance());

         // --------------- SERVICES ---------------
         AgeCategoryService ageCategoryService = new AgeCategoryService(ageCategoryRepository, ageCategoryValidator);
         TrialService trialService = new TrialService(trialRepository, ageCategoryRepository, trialValidator);
         UserService userService = new UserService(userRepository, userValidator);
         ParticipantService participantService = new ParticipantService(participantRepository, ageCategoryRepository, participantValidator);

         // --------------- SERVER ---------------
         const int port = 50052;
         ServerImpl serverImpl = new ServerImpl(ageCategoryService, trialService, userService, participantService);

         var server = new Grpc.Core.Server
         {
            Services = {AppService.BindService(serverImpl) },
            Ports = { new ServerPort("localhost", port, ServerCredentials.Insecure) }
         };
         server.Start();

         Console.WriteLine("Server listening on port " + port);
         Console.WriteLine("Press any key to stop the server...");
         Console.ReadKey();

         server.ShutdownAsync().Wait();
      }
   }
}
