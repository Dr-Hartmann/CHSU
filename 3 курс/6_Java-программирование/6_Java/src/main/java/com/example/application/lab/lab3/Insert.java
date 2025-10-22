package com.example.application.lab.lab3;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.NoSuchElementException;

public class Insert implements IArray {
    private final List<NumberLab3> array = new ArrayList<>();
    private int counter = 0;

    public void add(Double item) {
        array.add(new NumberLab3(counter++, item));
    }

    public ArrayList<NumberLab3> getAll() {
        var out = new ArrayList<NumberLab3>();
        var a = this.forEach();
        while (a.hasNext()) {
            out.add(a.next());
        }
        return out;
    }

    @Override
    public void sort() {
        for (int left = 0; left < array.size(); ++left) {
            var min = array.get(left);
            var i = left - 1;
            for (; i >= 0; --i) {
                var obj = array.get(i);
                if (obj.compareTo(min) > 0) {
                    array.set(i + 1, obj);
                } else {
                    break;
                }
            }
            array.set(i + 1, min);
        }
    }

    @Override
    public Iterator<NumberLab3> forEach() {
        return new Iterator<>() {
            private int currentIndex = -1;

            @Override
            public boolean hasNext() {
                return currentIndex + 1 < array.size();
            }

            @Override
            public NumberLab3 next() {
                if (++currentIndex >= 0 && currentIndex < array.size()) {
                    var obj = array.get(currentIndex);
                    return new NumberLab3(obj.getId(), Math.pow(obj.getValue(), 2));
                } else {
                    throw new NoSuchElementException();
                }
            }
        };
    }
}
