package Proxy;

import Domain.AgeCategory;
import Domain.Participant;
import Domain.Trial;
import Domain.User;
import Generated.Protobuf.AgeCategoryDTO;
import Generated.Protobuf.ParticipantDTO;
import Generated.Protobuf.TrialDTO;
import Generated.Protobuf.UserDTO;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by andrei on 2017-04-29.
 */
public class ProtoUtils {
    public static User userFromDto(UserDTO userDto) {
        return new User(userDto.getUserName(), userDto.getPassword());
    }

    public static UserDTO userToDto(User user) {
        return UserDTO.newBuilder().setUserName(user.getUserName()).setPassword(user.getPassword()).build();
    }

    public static AgeCategory ageCategoryFromDto(AgeCategoryDTO ageCategoryDTO) {
        return new AgeCategory(ageCategoryDTO.getName(), ageCategoryDTO.getMinAge(), ageCategoryDTO.getMaxAge());
    }

    public static AgeCategoryDTO ageCategoryToDto(AgeCategory ageCategory) {
        return AgeCategoryDTO.newBuilder().setName(ageCategory.getName()).setMaxAge(ageCategory.getMaxAge()).setMinAge(ageCategory.getMinAge()).build();
    }


    public static Participant participantFromDto(ParticipantDTO participantDTO) {
        return new Participant(participantDTO.getName(), participantDTO.getAge(), ageCategoryFromDto(participantDTO.getAgeCategory()));
    }

    public static ParticipantDTO participantToDto(Participant participant) {
        return ParticipantDTO.newBuilder().setName(participant.getName()).setAge(participant.getAge()).setAgeCategory(ageCategoryToDto(participant.getAgeCategory())).build();
    }

    public static Trial trialFromDto(TrialDTO trialDTO) {
        return new Trial(trialDTO.getName(), trialDTO.getDifficulty());
    }

    public static TrialDTO trialToDto(Trial trial) {
        return TrialDTO.newBuilder().setName(trial.getName()).setDifficulty(trial.getDifficulty()).build();
    }

    public static List<AgeCategoryDTO> ageCategoryListToDto(List<AgeCategory> ageCategoryList) {
        List<AgeCategoryDTO> all = new ArrayList<>();
        for (AgeCategory a : ageCategoryList) all.add(ageCategoryToDto(a));
        return all;
    }

    public static List<AgeCategory> ageCategoryListFromDto(List<AgeCategoryDTO> ageCategoryDtoList) {
        List<AgeCategory> all = new ArrayList<>();
        for (AgeCategoryDTO a : ageCategoryDtoList) all.add(ageCategoryFromDto(a));
        return all;
    }

    public static List<TrialDTO> trialsListToDto(List<Trial> trialsList) {
        List<TrialDTO> all = new ArrayList<>();
        for (Trial t : trialsList) all.add(trialToDto(t));
        return all;
    }

    public static List<Trial> trialsFromDto(List<TrialDTO> trialDTOList) {
        List<Trial> all = new ArrayList<>();
        for (TrialDTO t : trialDTOList) all.add(trialFromDto(t));
        return all;
    }

    public static List<ParticipantDTO> participantsListToDto(List<Participant> participantList) {
        List<ParticipantDTO> all = new ArrayList<>();
        for (Participant t : participantList) all.add(participantToDto(t));
        return all;
    }

    public static List<Participant> participantsListFromDto(List<ParticipantDTO> participantDtoList) {
        List<Participant> all = new ArrayList<>();
        for (ParticipantDTO t : participantDtoList) all.add(participantFromDto(t));
        return all;
    }
}
