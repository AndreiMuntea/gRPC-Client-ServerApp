package Domain;

/**
 * Created by andrei on 2017-04-06.
 */
public interface HasId<T> {
    T getId();
    void setId(T id);
}
