package com.example.application.lab.lab3;

import java.util.Collection;
import java.util.Iterator;

public interface IArray {
    void sort();
    Iterator<NumberLab3> forEach();
    void add(Double item);
    Collection<NumberLab3> getAll();
}
