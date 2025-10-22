package com.example.application.lab.lab2;

import java.util.Collection;
import java.util.Vector;

public class Container3<T extends Workshop> {
    private Vector<T> array = new Vector<>();

    public Container3(Collection<T> obj) {
        fillData(obj);
    }

    public void fillData(Collection<T> obj) {
        array.addAll(obj);
    }

    public void fillAllFromContainer(Container3<T> container) {
        array.addAll(container.getArray());
    }

    public void orderByName() {
        array.sort((obj1, obj2) -> obj1.getName().compareTo(obj2.getName()));
    }

    public void orderByNameReverse() {
        array.sort((obj1, obj2) -> obj2.getName().compareTo(obj1.getName()));
    }

    public void orderByChief() {
        array.sort((obj1, obj2) -> obj1.getChief().compareTo(obj2.getChief()));
    }

    public void orderByNumberOfWorkers() {
        array.sort((obj1, obj2) -> Integer.compare(obj1.getNumberOfWorkers(), obj2.getNumberOfWorkers()));
    }

    public String viewData() {
        StringBuilder out = new StringBuilder();
        array.forEach(obj -> out.append(obj).append("\n\n"));
        out.append("\n");
        return out.toString();
    }

    public Vector<T> getArray() {
        return array;
    }

    public Vector<T> getByNumberOfWorkersInRange(int min, int max) {
        var output = new Vector<T>();
        array.forEach(obj -> {
            if (obj.getNumberOfWorkers() <= max && obj.getNumberOfWorkers() >= min) {
                output.add(obj);
            }
        });
        return output;
    }

    public Vector<T> extractByNumberOfWorkersInRange(int min, int max) {
        var output = new Vector<T>();
        array.forEach(obj -> {
            if (obj.getNumberOfWorkers() <= max && obj.getNumberOfWorkers() >= min) {
                output.add(obj);
            }
        });
        array.removeAll(output);
        return output;
    }
}
