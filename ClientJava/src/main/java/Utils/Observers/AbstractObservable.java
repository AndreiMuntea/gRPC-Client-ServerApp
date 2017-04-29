package Utils.Observers;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by andrei on 2017-04-08.
 */
public abstract class AbstractObservable implements IObservable{
    private List<IObserver> observers;

    public AbstractObservable() {
        this.observers =  new ArrayList<>();
    }

    @Override
    public void addObserver(IObserver o)
    {
        if (observers.contains(o)) return;
        observers.add(o);
    }

    @Override
    public void notifyObservers(Object... obj) {
        observers.forEach(o->o.updateRequired(obj));
    }
}
