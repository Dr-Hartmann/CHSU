package com.example.application.lab.lab2;

import java.util.Arrays;
import java.util.Collection;
import java.util.SortedSet;
import java.util.TreeSet;
import java.util.Vector;

public class Container4<T extends Workshop> {
    private TreeSet<T> array = new TreeSet<>();

    public Container4() {
    }

    public Container4(Collection<T> obj) {
        fillData(obj);
    }

    public void fillData(Collection<T> obj) {
        array.addAll(obj);
    }

    public void fillAllFromVector(Vector<T> vector) {
        array.addAll(vector);
    }

    public void orderByDescending() {
        array = (TreeSet<T>) array.descendingSet();
    }

    public String viewData() {
        var out = new StringBuilder();
        array.forEach(obj -> out.append(obj).append("\n\n"));
        out.append("\n");
        return out.toString();
    }

    public SortedSet<T> getArray() {
        return array;
    }

    @SafeVarargs
    public final void concat(Collection<? extends T>... coll) {
        Arrays.stream(coll)
                .flatMap(Collection::stream)
                .forEach(array::add);
    }
}