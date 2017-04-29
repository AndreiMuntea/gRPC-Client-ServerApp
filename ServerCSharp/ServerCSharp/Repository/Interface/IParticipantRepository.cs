using System;
using System.Collections.Generic;
using ServerCSharp.Domain;

namespace ServerCSharp.Repository.Interface
{
   public interface IParticipantRepository : ICrudRepository<String, Participant>
   {
      Int32 CountTrialsForParticipant(String participantName);
      void RegisterParticipant(Participant participant, String trialName);
      List<String> GetTrialsForParticipant(String participantName);
   }
}
