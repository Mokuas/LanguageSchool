package lsms.model;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class Course implements Serializable {

    private static final long serialVersionUID = 1L;

    private static final List<Course> extent = new ArrayList<>();

    public static final int MIN_ECTS = 1;
    public static final double MIN_GPA_WEIGHT = 0.1;

    private String code;
    private String name;
    private String level;
    private String language;
    private int ects;
    private double gpaWeight;
    private boolean mandatory;
    private boolean hasExtraCertificate;
    private String dressCode;
    private final List<String> specializations = new ArrayList<>(); // multi-value

    public Course(String code,
                  String name,
                  String level,
                  String language,
                  int ects,
                  double gpaWeight,
                  boolean mandatory,
                  boolean hasExtraCertificate,
                  String dressCode) {

        setCode(code);
        setName(name);
        setLevel(level);
        setLanguage(language);
        setEcts(ects);
        setGpaWeight(gpaWeight);
        this.mandatory = mandatory;
        this.hasExtraCertificate = hasExtraCertificate;
        setDressCode(dressCode);
        addToExtent(this);
    }

    public Course(String code,
                  String name,
                  String level,
                  String language,
                  int ects,
                  double gpaWeight,
                  boolean mandatory,
                  boolean hasExtraCertificate) {
        this(code, name, level, language, ects, gpaWeight, mandatory, hasExtraCertificate, null);
    }

    private static void addToExtent(Course c) {
        if (c == null) {
            throw new IllegalArgumentException("course cannot be null");
        }
        extent.add(c);
    }

    public static List<Course> getExtent() {
        return Collections.unmodifiableList(extent);
    }

    static void restoreExtent(List<Course> courses) {
        extent.clear();
        if (courses != null) {
            extent.addAll(courses);
        }
    }

    static void clearExtentForTests() {
        extent.clear();
    }

    private void requireNonBlank(String value, String fieldName) {
        if (value == null || value.trim().isEmpty()) {
            throw new IllegalArgumentException(fieldName + " must not be empty");
        }
    }

    public String getCode() {
        return code;
    }

    public void setCode(String code) {
        requireNonBlank(code, "code");
        this.code = code.trim();
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        requireNonBlank(name, "name");
        this.name = name.trim();
    }

    public String getLevel() {
        return level;
    }

    public void setLevel(String level) {
        requireNonBlank(level, "level");
        this.level = level.trim();
    }

    public String getLanguage() {
        return language;
    }

    public void setLanguage(String language) {
        requireNonBlank(language, "language");
        this.language = language.trim();
    }

    public int getEcts() {
        return ects;
    }

    public void setEcts(int ects) {
        if (ects < MIN_ECTS) {
            throw new IllegalArgumentException("ects must be at least " + MIN_ECTS);
        }
        this.ects = ects;
    }

    public double getGpaWeight() {
        return gpaWeight;
    }

    public void setGpaWeight(double gpaWeight) {
        if (gpaWeight < MIN_GPA_WEIGHT) {
            throw new IllegalArgumentException("gpaWeight must be at least " + MIN_GPA_WEIGHT);
        }
        this.gpaWeight = gpaWeight;
    }

    public boolean isMandatory() {
        return mandatory;
    }

    public boolean hasExtraCertificate() {
        return hasExtraCertificate;
    }

    public String getDressCode() {
        return dressCode;
    }

    public void setDressCode(String dressCode) {
        if (dressCode == null) {
            this.dressCode = null;
        } else {
            if (dressCode.trim().isEmpty()) {
                throw new IllegalArgumentException("dressCode cannot be blank if provided");
            }
            this.dressCode = dressCode.trim();
        }
    }

    public void addSpecialization(String specialization) {
        if (specialization == null || specialization.trim().isEmpty()) {
            throw new IllegalArgumentException("specialization cannot be empty");
        }
        String trimmed = specialization.trim();
        if (!specializations.contains(trimmed)) {
            specializations.add(trimmed);
        }
    }

    public List<String> getSpecializations() {
        return Collections.unmodifiableList(specializations);
    }

    public boolean isHighImpact() {
        return ects >= 5 && gpaWeight >= 1.5;
    }
}
