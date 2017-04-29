using System;
using System.Collections.Generic;
using ServerCSharp.Domain;

namespace ServerCSharp.Repository.Interface
{
   public interface ITrialRepository : ICrudRepository<String, Trial>
   {
      List<Participant> GetParticipantsForTrial(String trialName, AgeCategory ageCategory);
   }
}
