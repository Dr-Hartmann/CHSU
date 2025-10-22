package com.example.application.lab.lab1;

import java.util.Arrays;
import java.util.Objects;
import java.util.Random;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class Customer implements Person {
    private String lastname;
    private String firstname;
    private String patronymic;
    private int creditCardNumber;
    private int bankAccountNumber;

    private static Random random = new Random();
    private static Customer[] customers;

    public Customer(int size) {
        customers = new Customer[size];
        for (int i = 0; i < customers.length; ++i) {
            customers[i] = new Customer();
            customers[i].setCreditCardNumber(random.nextInt() * 1000 / random.nextInt() + 1);
            customers[i].setBankAccountNumber(random.nextInt() * 10000 / random.nextInt() + 1);
        }
    }

    private Customer() {
    }

    public Customer getByIndex(int index) {
        if (index < customers.length && customers[index] != null)
            return customers[index];
        throw new NullPointerException("Не существует" + index + "!");
    }

    public int getIndex() {
        return Arrays.asList(customers).indexOf(this);
    }

    @Override
    public boolean equals(Object obj) {
        if (this == obj)
            return true;
        if (obj == null || getClass() != obj.getClass())
            return false;
        Customer newObj = (Customer) obj;
        return Objects.equals(lastname, newObj.lastname)
                && Objects.equals(firstname, newObj.firstname)
                && Objects.equals(patronymic, newObj.patronymic)
                && bankAccountNumber == newObj.bankAccountNumber
                && creditCardNumber == newObj.creditCardNumber;
    }

    public static Customer[] getArray() {
        return customers;
    }

    public static Customer[] orderBy(Customer[] array, SortingType type) {
        if (type == SortingType.ALPHABETICALLY) {
            Arrays.sort(array, (c1, c2) -> c1.getFirstname().compareTo(c2.getFirstname()));
        } else if (type == SortingType.ALPHABETICALLYREVERSE) {
            Arrays.sort(array, (c1, c2) -> c2.getFirstname().compareTo(c1.getFirstname()));
        }
        return array;
    }

    @Override
    public String toString() {
        return String.format(
                "%d: Фамилия: %s\nИмя: %s\nОтчество: %s\nНомер кредитной карты: %s\nНомер банковского счёта: %s",
                this.getIndex(),
                this.getLastname(),
                this.getFirstname(),
                this.getPatronymic(),
                this.getCreditCardNumber(),
                this.getBankAccountNumber());
    }

    public String printAll() {
        StringBuilder output = new StringBuilder();
        for (var customer : customers)
            output.append(String.format("%s\n\n", customer));
        return output.toString();
    }

    public String printAndOrderByAll(SortingType type) {
        Customer[] out = new Customer[customers.length];
        System.arraycopy(customers, 0, out, 0, customers.length);
        StringBuilder output = new StringBuilder();
        for (Customer customer : out)
            output.append(String.format("%s\n\n", customer));
        return output.toString();
    }

    public String GetAllByCreditCardNumber(float min, float max) {
        StringBuilder output = new StringBuilder();
        for (Customer customer : customers) {
            if (customer.getCreditCardNumber() <= max && customer.getCreditCardNumber() >= min) {
                output.append(String.format("%d: %s %s %s - %d\n",
                        customer.getIndex(),
                        customer.getLastname(),
                        customer.getFirstname(),
                        customer.getPatronymic(),
                        customer.getCreditCardNumber()));
            }
        }
        return output.toString();
    }
}