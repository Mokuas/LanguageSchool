package lsms.model;

import java.io.Serializable;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class Payment implements Serializable {

    private static final long serialVersionUID = 1L;

    private static final List<Payment> extent = new ArrayList<>();

    // static/class
    private static double MIN_AMOUNT = 1.0;

    private long id;
    private double amount;
    private LocalDate dueDate;
    private PaymentMethod paymentMethod;

    // optional
    private LocalDate paymentDate;
    private Double scholarshipPercentage; // 0..100

    public Payment(long id,
                   double amount,
                   LocalDate dueDate,
                   PaymentMethod paymentMethod) {
        setId(id);
        setAmount(amount);
        setDueDate(dueDate);
        setPaymentMethod(paymentMethod);
        addToExtent(this);
    }

    private static void addToExtent(Payment p) {
        if (p == null) {
            throw new IllegalArgumentException("payment cannot be null");
        }
        extent.add(p);
    }

    public static List<Payment> getExtent() {
        return Collections.unmodifiableList(extent);
    }

    static void clearExtentForTests() {
        extent.clear();
    }

    static void restoreExtent(List<Payment> payments) {
        extent.clear();
        if (payments != null) {
            extent.addAll(payments);
        }
    }

    // static
    public static double getMinAmount() {
        return MIN_AMOUNT;
    }

    public static void setMinAmount(double minAmount) {
        if (minAmount <= 0) {
            throw new IllegalArgumentException("minAmount must be positive");
        }
        MIN_AMOUNT = minAmount;
    }

    // getters/setters

    public long getId() {
        return id;
    }

    public void setId(long id) {
        if (id <= 0) {
            throw new IllegalArgumentException("id must be positive");
        }
        this.id = id;
    }

    public double getAmount() {
        return amount;
    }

    public void setAmount(double amount) {
        if (amount < MIN_AMOUNT) {
            throw new IllegalArgumentException("amount must be >= " + MIN_AMOUNT);
        }
        this.amount = amount;
    }

    public LocalDate getDueDate() {
        return dueDate;
    }

    public void setDueDate(LocalDate dueDate) {
        if (dueDate == null) {
            throw new IllegalArgumentException("dueDate cannot be null");
        }

        this.dueDate = dueDate;
    }


    public PaymentMethod getPaymentMethod() {
        return paymentMethod;
    }

    public void setPaymentMethod(PaymentMethod paymentMethod) {
        if (paymentMethod == null) {
            throw new IllegalArgumentException("paymentMethod cannot be null");
        }
        this.paymentMethod = paymentMethod;
    }

    public LocalDate getPaymentDate() {
        return paymentDate;
    }

    public void setPaymentDate(LocalDate paymentDate) {
        if (paymentDate == null) {
            this.paymentDate = null;
            return;
        }
        if (paymentDate.isBefore(dueDate)) {
            throw new IllegalArgumentException("paymentDate cannot be before dueDate");
        }
        if (paymentDate.isAfter(LocalDate.now())) {
            throw new IllegalArgumentException("paymentDate cannot be in the future");
        }
        this.paymentDate = paymentDate;
    }

    public Double getScholarshipPercentage() {
        return scholarshipPercentage;
    }

    public void setScholarshipPercentage(Double scholarshipPercentage) {
        if (scholarshipPercentage == null) {
            this.scholarshipPercentage = null;
            return;
        }
        if (scholarshipPercentage < 0 || scholarshipPercentage > 100) {
            throw new IllegalArgumentException("scholarshipPercentage must be between 0 and 100");
        }
        this.scholarshipPercentage = scholarshipPercentage;
    }

    // derived
    public boolean isPaid() {
        return paymentDate != null;
    }

    public boolean isOverdue() {
        return !isPaid() && LocalDate.now().isAfter(dueDate);
    }
}
