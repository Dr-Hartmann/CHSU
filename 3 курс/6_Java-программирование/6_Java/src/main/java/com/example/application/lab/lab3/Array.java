package com.example.application.lab.lab3;

import java.util.Collection;
import java.util.Iterator;

public abstract class Array {
    public abstract void sort();
    public abstract Iterator<NumberLab3> forEach();
    public abstract void add(Double item);
    public abstract Collection<NumberLab3> getAll();
}
