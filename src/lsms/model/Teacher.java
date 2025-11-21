package lsms.model;

import java.io.Serializable;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class Teacher extends User implements Serializable {

    private static final long serialVersionUID = 1L;

    private static final List<Teacher> extent = new ArrayList<>();

    // static/class attribute
    private static double YEARLY_SALARY_INCREASE_PERCENT = 2.0;

    private LocalDate hireDate;
    private double baseSalary;
    private int yearsOfExperience;
    private double bonusPercentage; // 0..100

    public Teacher(long id,
                   String name,
                   String surname,
                   String schoolName,
                   String email,
                   LocalDate hireDate,
                   double baseSalary,
                   int yearsOfExperience,
                   double bonusPercentage) {
        super(id, name, surname, schoolName, email);
        setHireDate(hireDate);
        setBaseSalary(baseSalary);
        setYearsOfExperience(yearsOfExperience);
        setBonusPercentage(bonusPercentage);
        addToExtent(this);
    }

    // extent
    private static void addToExtent(Teacher t) {
        if (t == null) {
            throw new IllegalArgumentException("teacher cannot be null");
        }
        extent.add(t);
    }

    public static List<Teacher> getExtent() {
        return Collections.unmodifiableList(extent);
    }

    static void clearExtentForTests() {
        extent.clear();
    }

    static void restoreExtent(List<Teacher> teachers) {
        extent.clear();
        if (teachers != null) {
            extent.addAll(teachers);
        }
    }

    // static attribute
    public static double getYearlySalaryIncreasePercent() {
        return YEARLY_SALARY_INCREASE_PERCENT;
    }

    public static void setYearlySalaryIncreasePercent(double percent) {
        if (percent < 0 || percent > 50) {
            throw new IllegalArgumentException("unrealistic yearly salary increase percent");
        }
        YEARLY_SALARY_INCREASE_PERCENT = percent;
    }

    // getters/setters

    public LocalDate getHireDate() {
        return hireDate;
    }

    public void setHireDate(LocalDate hireDate) {
        if (hireDate == null) {
            throw new IllegalArgumentException("hireDate cannot be null");
        }
        if (hireDate.isAfter(LocalDate.now())) {
            throw new IllegalArgumentException("hireDate cannot be in the future");
        }
        this.hireDate = hireDate;
    }

    public double getBaseSalary() {
        return baseSalary;
    }

    public void setBaseSalary(double baseSalary) {
        if (baseSalary < 0) {
            throw new IllegalArgumentException("baseSalary cannot be negative");
        }
        this.baseSalary = baseSalary;
    }

    public int getYearsOfExperience() {
        return yearsOfExperience;
    }

    public void setYearsOfExperience(int yearsOfExperience) {
        if (yearsOfExperience < 0) {
            throw new IllegalArgumentException("yearsOfExperience cannot be negative");
        }
        this.yearsOfExperience = yearsOfExperience;
    }

    public double getBonusPercentage() {
        return bonusPercentage;
    }

    public void setBonusPercentage(double bonusPercentage) {
        if (bonusPercentage < 0 || bonusPercentage > 100) {
            throw new IllegalArgumentException("bonusPercentage must be between 0 and 100");
        }
        this.bonusPercentage = bonusPercentage;
    }

    // derived attribute
    public double getCurrentSalary() {
        double expFactor = 1.0 + (YEARLY_SALARY_INCREASE_PERCENT / 100.0) * yearsOfExperience;
        double withExperience = baseSalary * expFactor;
        double withBonus = withExperience * (1.0 + bonusPercentage / 100.0);
        return withBonus;
    }
}
