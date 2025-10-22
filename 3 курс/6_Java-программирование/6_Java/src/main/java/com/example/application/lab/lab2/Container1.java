package com.example.application.lab.lab2;

import java.util.Arrays;
import java.util.Vector;

public class Container1 {
    private Vector<Character> array = new Vector<>();

    public Container1(Character... chars) {
        fillData(chars);
    }

    public void fillData(Character... chars) {
        array.addAll(Arrays.asList(chars));
    }

    public void fillAllFromContainer(Container1 container) {
        array.addAll(container.getArray());
    }

    public void remove(int start, int count) {
        if (start >= array.size())
            return;

        start = start <= 0 ? 0 : start;
        count = count <= 0 ? 0 : count;
        count = count >= array.size() - start ? array.size() - start : count;

        while (count-- > 0) {
            array.remove(start);
        }
    }

    public void replaceElementAt(int index, Character newElement) {
        array.setElementAt(newElement, index);
    }

    public String viewDataIterator() {
        var out = new StringBuilder();
        array.forEach(c -> out.append(c).append(", "));
        out.append("\n");
        return out.toString();
    }

    public String viewData() {
        var out = new StringBuilder();
        for (int i = 0; i < array.size(); ++i) {
            out.append(array.elementAt(i))
                    .append(i != array.size() - 1 ? ", " : ".\n");
        }
        return out.toString();
    }

    private Vector<Character> getArray() {
        return array;
    }
}