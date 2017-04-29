using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generated.Protobuf;
using ServerCSharp.Domain;

namespace ServerCSharp.Protobuf
{
   public static class ProtoUtils
   {
      public static AgeCategoryDTO AgeCategoryToDto(AgeCategory ageCategory)
      {
         return new AgeCategoryDTO {MaxAge = ageCategory.MaxAge, MinAge = ageCategory.MinAge, Name = ageCategory.Name};
      }

      public static AgeCategory AgeCategoryFromDto(AgeCategoryDTO ageCategoryDto)
      {
         return new AgeCategory(ageCategoryDto.Name, ageCategoryDto.MinAge, ageCategoryDto.MaxAge);
      }

      public static UserDTO UserToDto(User user)
      {
         return new UserDTO {Password = user.Password, UserName = user.UserName};
      }

      public static User UserFromDto(UserDTO userDto)
      {
         return new User(userDto.UserName, userDto.Password);
      }

      public static TrialDTO TrialToDto(Trial trial)
      {
         return new TrialDTO {Name = trial.Name, Difficulty = trial.Difficulty};
      }

      public static Trial TrialFromDto(TrialDTO trialDto)
      {
         return new Trial(trialDto.Name, trialDto.Difficulty);
      }

      public static ParticipantDTO ParticipantToDto(Participant participant)
      {
         return new ParticipantDTO
         {
            Age = participant.Age,
            AgeCategory = AgeCategoryToDto(participant.AgeCategory),
            Name = participant.Name
         };
      }

      public static Participant ParticipantFromDto(ParticipantDTO participantDto)
      {
         return new Participant(participantDto.Name, participantDto.Age, AgeCategoryFromDto(participantDto.AgeCategory));
      }

      public static List<AgeCategoryDTO> AgeCategoryListToDto(List<AgeCategory> ageCategoriesList)
      {
         List<AgeCategoryDTO> resultedList = new List<AgeCategoryDTO>();
         ageCategoriesList.ForEach(o => { resultedList.Add(AgeCategoryToDto(o)); });
         return resultedList;
      }

      public static List<AgeCategory> AgeCategoryListFromDto(List<AgeCategoryDTO> ageCategoriesDtoList)
      {
         List<AgeCategory> resultedList = new List<AgeCategory>();
         ageCategoriesDtoList.ForEach(o => { resultedList.Add(AgeCategoryFromDto(o)); });
         return resultedList;
      }

      public static List<TrialDTO> TrialsListToDto(List<Trial> trialsList)
      {
         List<TrialDTO> resultedList = new List<TrialDTO>();
         trialsList.ForEach(o => { resultedList.Add(TrialToDto(o)); });
         return resultedList;
      }

      public static List<Trial> TrialsListFromDto(List<TrialDTO> trialsDtoList)
      {
         List<Trial> resultedList = new List<Trial>();
         trialsDtoList.ForEach(o => { resultedList.Add(TrialFromDto(o)); });
         return resultedList;
      }

      public static List<ParticipantDTO> ParticipantsListToDto(List<Participant> participantsList)
      {
         List<ParticipantDTO> resultedList = new List<ParticipantDTO>();
         participantsList.ForEach(o => { resultedList.Add(ParticipantToDto(o)); });
         return resultedList;
      }
      public static List<Participant> ParticipantsListFromDto(List<ParticipantDTO> participantsDtoList)
      {
         List<Participant> resultedList = new List<Participant>();
         participantsDtoList.ForEach(o => { resultedList.Add(ParticipantFromDto(o)); });
         return resultedList;
      }
   }
}