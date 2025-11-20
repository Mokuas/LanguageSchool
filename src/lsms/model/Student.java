package lsms.model;

import java.io.Serializable;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class Student extends User implements Serializable {

    private static final long serialVersionUID = 1L;

    private static final List<Student> extent = new ArrayList<>();

    // static/class attribute
    private static double PROBATION_GPA_THRESHOLD = 3.0;

    private String studentNumber;
    private int semester;
    private int yearOfStudy;
    private double gpa;
    private double accountBalance;
    private LocalDate enrollmentDate;

    public Student(long id,
                   String name,
                   String surname,
                   String schoolName,
                   String email,
                   String studentNumber,
                   int semester,
                   int yearOfStudy,
                   double gpa,
                   double accountBalance,
                   LocalDate enrollmentDate) {
        super(id, name, surname, schoolName, email);
        setStudentNumber(studentNumber);
        setSemester(semester);
        setYearOfStudy(yearOfStudy);
        setGpa(gpa);
        setAccountBalance(accountBalance);
        setEnrollmentDate(enrollmentDate);
        addToExtent(this);
    }

    // --- extent methods ---
    private static void addToExtent(Student s) {
        if (s == null) {
            throw new IllegalArgumentException("student cannot be null");
        }
        extent.add(s);
    }

    public static List<Student> getExtent() {
        return Collections.unmodifiableList(extent);
    }

    static void clearExtentForTests() {
        extent.clear();
    }

    static void restoreExtent(List<Student> students) {
        extent.clear();
        if (students != null) {
            extent.addAll(students);
        }
    }

    // --- static attributes ---
    public static double getProbationGpaThreshold() {
        return PROBATION_GPA_THRESHOLD;
    }

    public static void setProbationGpaThreshold(double threshold) {
        if (threshold <= 0.0 || threshold > 5.0) {
            throw new IllegalArgumentException("Invalid probation GPA threshold");
        }
        PROBATION_GPA_THRESHOLD = threshold;
    }

    // --- getters/setters with validation ---

    public String getStudentNumber() {
        return studentNumber;
    }

    public void setStudentNumber(String studentNumber) {
        requireNonNullOrBlank(studentNumber, "studentNumber");
        this.studentNumber = studentNumber.trim();
    }

    public int getSemester() {
        return semester;
    }

    public void setSemester(int semester) {
        if (semester <= 0) {
            throw new IllegalArgumentException("semester must be positive");
        }
        this.semester = semester;
    }

    public int getYearOfStudy() {
        return yearOfStudy;
    }

    public void setYearOfStudy(int yearOfStudy) {
        if (yearOfStudy <= 0) {
            throw new IllegalArgumentException("yearOfStudy must be positive");
        }
        this.yearOfStudy = yearOfStudy;
    }

    public double getGpa() {
        return gpa;
    }

    public void setGpa(double gpa) {
        if (gpa < 0.0 || gpa > 5.0) {
            throw new IllegalArgumentException("gpa must be between 0.0 and 5.0");
        }
        this.gpa = gpa;
    }

    public double getAccountBalance() {
        return accountBalance;
    }

    public void setAccountBalance(double accountBalance) {
        if (accountBalance < 0.0) {
            throw new IllegalArgumentException("accountBalance cannot be negative");
        }
        this.accountBalance = accountBalance;
    }

    public LocalDate getEnrollmentDate() {
        return enrollmentDate;
    }

    public void setEnrollmentDate(LocalDate enrollmentDate) {
        if (enrollmentDate == null) {
            throw new IllegalArgumentException("enrollmentDate cannot be null");
        }
        if (enrollmentDate.isAfter(LocalDate.now())) {
            throw new IllegalArgumentException("enrollmentDate cannot be in the future");
        }
        this.enrollmentDate = enrollmentDate;
    }

    // --- derived attribute ---
    public boolean isOnProbation() {
        return this.gpa < PROBATION_GPA_THRESHOLD;
    }
}
