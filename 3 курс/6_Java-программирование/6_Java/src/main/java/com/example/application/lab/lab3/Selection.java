package com.example.application.lab.lab3;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.List;
import java.util.NoSuchElementException;
import static java.util.Collections.swap;

public class Selection extends Array {
    private final List<NumberLab3> array = new ArrayList<>();
    private int counter = 0;

    @Override
    public void add(Double item) {
        array.add(new NumberLab3(counter++, item));
    }

    @Override
    public Collection<NumberLab3> getAll() {
        var out = new ArrayList<NumberLab3>();
        var a = forEach();
        while (a.hasNext()) {
            out.add(a.next());
        }
        return out;
    }

    @Override
    public void sort() {
        for (int left = 0; left < array.size(); left++) {
            int minInd = left;
            for (int i = left; i < array.size(); i++) {
                if (array.get(i).compareTo(array.get(minInd)) < 0)
                    minInd = i;
            }
            swap(array, left, minInd);
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
                ++currentIndex;
                if (currentIndex >= 0 && currentIndex < array.size()) {
                    var obj = array.get(currentIndex);
                    return new NumberLab3(obj.getId(), Math.log(obj.getValue()));
                } else {
                    throw new NoSuchElementException();
                }
            }
        };
    }
}
