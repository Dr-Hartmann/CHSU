package com.example.application.lab.lab2;

import java.util.Collection;
import java.util.Vector;

public class Container2<T extends Workshop> {
    private Vector<T> array = new Vector<>();

    public Container2(Collection<T> obj) {
        fillData(obj);
    }

    public void fillData(Collection<T> obj) {
        array.addAll(obj);
    }

    public void fillAllFromContainer(Container2<T> container) {
        array.addAll(container.getArray());
    }

    public void remove(int start, int count) {
        start = start <= 0 ? 0 : start;
        count = count <= 0 ? 0 : count;
        count = count >= array.size() - start ? array.size() - start : count;

        while (count-- > 0) {
            array.remove(start);
        }
    }

    public void replaceElementAt(int index, T newElement) {
        array.setElementAt(newElement, index);
    }

    public String viewDataIterator() {
        var out = new StringBuilder();
        array.forEach(c -> out.append(c).append("\n\n"));
        out.append("\n");
        return out.toString();
    }

    public String viewData() {
        var out = new StringBuilder();
        for (int i = 0; i < array.size(); ++i) {
            out.append(array.elementAt(i)).append("\n\n");
        }
        return out.toString();
    }

    private Vector<T> getArray() {
        return array;
    }
}