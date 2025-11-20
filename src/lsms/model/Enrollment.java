package lsms.model;

import java.io.Serializable;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class Enrollment implements Serializable {

    private static final long serialVersionUID = 1L;

    private static final List<Enrollment> extent = new ArrayList<>();

    private long id;
    private LocalDate enrollmentDate;
    private EnrollmentStatus status;
    private Double finalGrade;

    public Enrollment(long id,
                      LocalDate enrollmentDate,
                      EnrollmentStatus status) {
        setId(id);
        setEnrollmentDate(enrollmentDate);
        setStatus(status);
        addToExtent(this);
    }

    private static void addToExtent(Enrollment e) {
        if (e == null) {
            throw new IllegalArgumentException("enrollment cannot be null");
        }
        extent.add(e);
    }

    public static List<Enrollment> getExtent() {
        return Collections.unmodifiableList(extent);
    }

    static void clearExtentForTests() {
        extent.clear();
    }

    static void restoreExtent(List<Enrollment> enrollments) {
        extent.clear();
        if (enrollments != null) {
            extent.addAll(enrollments);
        }
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

    public EnrollmentStatus getStatus() {
        return status;
    }

    public void setStatus(EnrollmentStatus status) {
        if (status == null) {
            throw new IllegalArgumentException("status cannot be null");
        }
        this.status = status;
    }

    public Double getFinalGrade() {
        return finalGrade;
    }

    public void setFinalGrade(Double finalGrade) {
        if (finalGrade == null) {
            this.finalGrade = null;
            return;
        }
        if (status != EnrollmentStatus.COMPLETE) {
            throw new IllegalStateException("Cannot set final grade when enrollment is not COMPLETE");
        }
        if (finalGrade < 2.0 || finalGrade > 5.0) {
            throw new IllegalArgumentException("finalGrade must be between 2.0 and 5.0");
        }
        this.finalGrade = finalGrade;
    }


    public boolean isPassed() {
        return finalGrade != null && finalGrade >= 3.0;
    }
}
