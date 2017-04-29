using System;
using System.Collections.Generic;
using ServerCSharp.Domain;
using ServerCSharp.Repository.Exceptions;
using ServerCSharp.Repository.Interface;
using ServerCSharp.Validator;

namespace ServerCSharp.Service
{
   public class ParticipantService
   {
      private readonly IParticipantRepository _repository;
      private readonly IAgeCategoryRepository _ageCategoryRepository;
      private readonly IValidator<Participant> _validator;

      public ParticipantService(IParticipantRepository repository, IAgeCategoryRepository ageCategoryRepository, IValidator<Participant> validator)
      {
         _repository = repository;
         _ageCategoryRepository = ageCategoryRepository;
         _validator = validator;
      }

      public void AddParticipant(Participant p)
      {
         _repository.AddItem(p);
      }

      public void RegisterParticipant(String participantName, Int32 age, List<String> trialName)
      {
         AgeCategory ageCategory = _ageCategoryRepository.FindSuitableAgeCategory(age);

         Participant p = null;
          if (!_repository.ExistsItem(participantName))
            _repository.AddItem(new Participant(participantName,age,ageCategory));
         p = _repository.GetItem(participantName);

         if (p.Age != age)
            throw new RepositoryException("Participant already exists in database with a different age!\n");


         List<String> trials = _repository.GetTrialsForParticipant(participantName);

         if (trials.Count + trialName.Count > 2)
            throw new ServiceException("A participant can be registered only at 2 trials!\n");

         foreach (string t in trialName)
            if (trials.Contains(t))
               throw new ServiceException(participantName + " already participates at " + t + "!\n");

         foreach (string t in trialName)
            _repository.RegisterParticipant(p, t);
      }

      public Int32 CountTrialsForParticipant(String participantName)
      {
         return _repository.CountTrialsForParticipant(participantName);
      }

      public List<String> GetTrialsForParticipant(String participantName)
      {
         return _repository.GetTrialsForParticipant(participantName);
      }

      public List<Participant> GetAll()
      {
         return _repository.GetAll();
      }
   }
}