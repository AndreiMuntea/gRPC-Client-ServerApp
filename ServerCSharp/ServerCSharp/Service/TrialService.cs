using System;
using System.Collections.Generic;
using ServerCSharp.Domain;
using ServerCSharp.Repository.Interface;
using ServerCSharp.Validator;

namespace ServerCSharp.Service
{
   public class TrialService
   {
      private readonly ITrialRepository _repository;
      private readonly IAgeCategoryRepository _ageCategoryRepository;
      private readonly IValidator<Trial> _validator;

      public TrialService(ITrialRepository repository, IAgeCategoryRepository ageCategoryRepository, IValidator<Trial> validator)
      {
         _repository = repository;
         _validator = validator;
         _ageCategoryRepository = ageCategoryRepository;
      }

      public List<Trial> GetAll()
      {
         return _repository.GetAll();
      }

      public List<Participant> GetParticipantsForTrial(String trialName, String ageCategoryName)
      {
         AgeCategory ageCategory = _ageCategoryRepository.GetItem(ageCategoryName);
         return _repository.GetParticipantsForTrial(trialName, ageCategory);
      }

   }
}
