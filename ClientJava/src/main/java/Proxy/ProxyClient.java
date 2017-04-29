package Proxy;

import Domain.AgeCategory;
import Domain.Participant;
import Domain.Trial;
import Domain.User;
import Generated.Protobuf.*;
import Utils.Observers.IObservable;
import Utils.Observers.IObserver;
import io.grpc.ManagedChannel;
import io.grpc.ManagedChannelBuilder;
import io.grpc.stub.StreamObserver;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

/**
 * Created by andrei on 2017-04-13.
 */
public class ProxyClient implements IObservable {


    private User user;
    private List<IObserver> observers;
    private BlockingQueue<Response> queue;
    private StreamObserver<Request> obs;
    private AppServiceGrpc.AppServiceStub stub;

    public ProxyClient(String host, Integer port) {
        ManagedChannel channel = ManagedChannelBuilder.forAddress(host, port).usePlaintext(true).build();
        stub = AppServiceGrpc.newStub(channel);

        user = null;
        queue = new LinkedBlockingQueue<>();
        this.observers = new ArrayList<>();
    }

    public Response getResponse() {
        try {
            return queue.take();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        return null;
    }


    public void logIn(String userName, String password) throws ServiceException {
        if (user != null) throw new ServiceException("An user is already logged in!\n");
        User tempUser = new User(userName, password);
        obs = stub.sendRequest(new StreamObserver<Response>() {
            @Override
            public void onNext(Response value) {
                if (value == null)
                    return;
                if (value.getResponseType().equals(ResponseType.UpdateResponse))
                    newParticipantRegistered(value.getAgeCategoryName(), value.getRegisteredTrialsList());
                else
                    queue.add(value);
            }

            @Override
            public void onError(Throwable t) {

            }

            @Override
            public void onCompleted() {

            }
        });
        obs.onNext(Request
                .newBuilder()
                .setRequestType(RequestType.LoginRequest)
                .setUser(ProtoUtils.userToDto(tempUser))
                .build());

        Response r = getResponse();
        if (r.getResponseType() != ResponseType.OkResponse) {
            obs.onCompleted();
            throw new ServiceException(r.getErrorMessage());
        }

        user = tempUser;
    }

    public void logout() throws ServiceException {
        obs.onNext(Request
                .newBuilder()
                .setRequestType(RequestType.LogoutRequest)
                .setUsername(user.getUserName())
                .build());


        Response r = getResponse();
        obs.onCompleted();
        if (r.getResponseType() != ResponseType.OkResponse) {
            throw new ServiceException(r.getErrorMessage());
        }
        user = null;
    }

    public List<AgeCategory> getAgeCategories() throws ServiceException {
        obs.onNext(Request
                .newBuilder()
                .setRequestType(RequestType.GetAgeCategoriesRequest)
                .build());

        Response r = getResponse();
        if (r.getResponseType() != ResponseType.OkResponse) {
            throw new ServiceException(r.getErrorMessage());
        }

        return ProtoUtils.ageCategoryListFromDto(r.getAgeCategoriesList());
    }

    public List<Trial> getTrials() throws ServiceException {
        obs.onNext(Request
                .newBuilder()
                .setRequestType(RequestType.GetTrialsRequest)
                .build());

        Response r = getResponse();
        if (r.getResponseType() != ResponseType.OkResponse) {
            throw new ServiceException(r.getErrorMessage());
        }

        return ProtoUtils.trialsFromDto(r.getTrialsList());
    }

    public List<Participant> getParticipantsForTrial(String trialName, String ageCategoryName) throws ServiceException {
        obs.onNext(Request
                .newBuilder()
                .setRequestType(RequestType.GetParticipantsForTrialRequest)
                .setTrialName(trialName)
                .setAgeCategoryName(ageCategoryName)
                .build());

        Response r = getResponse();
        if (r.getResponseType() != ResponseType.OkResponse) {
            throw new ServiceException(r.getErrorMessage());
        }

        return ProtoUtils.participantsListFromDto(r.getParticipantsList());
    }

    public void registerParticipant(String participantName, String age, List<String> trials) throws ServiceException {
        Integer ageInt = null;
        try {
            ageInt = Integer.parseInt(age);
        } catch (NumberFormatException e) {
            throw new ServiceException("Age must be a number!\n");
        }


        obs.onNext(Request
                .newBuilder()
                .setRequestType(RequestType.RegisterParticipantRequest)
                .setParticipantName(participantName)
                .setParticipantAge(ageInt)
                .addAllTrials(trials)
                .setUsername(user.getUserName())
                .build());

        Response r = getResponse();
        if (r.getResponseType() != ResponseType.OkResponse) {
            throw new ServiceException(r.getErrorMessage());
        }
        newParticipantRegistered(r.getAgeCategoryName(), r.getRegisteredTrialsList());
    }

    @Override
    protected void finalize() throws Throwable {
        if (user != null) logout();
        super.finalize();
    }

    public void newParticipantRegistered(String ageCategoryName, List<String> trials) {
        notifyObservers(ageCategoryName, trials);
    }

    @Override
    public void addObserver(IObserver o) {
        if (observers.contains(o)) return;
        observers.add(o);
    }

    @Override
    public void notifyObservers(Object... obj) {
        for (IObserver o : observers) o.updateRequired(obj);
    }
}
