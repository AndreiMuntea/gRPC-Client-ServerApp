package Utils.Observers;

/**
 * Created by andrei on 2017-04-08.
 */
public interface IObservable {
    void addObserver(IObserver o);
    void notifyObservers(Object... obj);
}
