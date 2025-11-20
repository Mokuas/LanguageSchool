package lsms.model;

import java.io.Serializable;
import java.util.Objects;

public abstract class User implements Serializable {

    private static final long serialVersionUID = 1L;

    private long id;
    private String name;
    private String surname;
    private String middleName;
    private String schoolName;
    private String email;

    protected User(long id,
                   String name,
                   String surname,
                   String schoolName,
                   String email) {
        setId(id);
        setName(name);
        setSurname(surname);
        setSchoolName(schoolName);
        setEmail(email);
    }


    protected static void requireNonNullOrBlank(String value, String field) {
        if (value == null || value.trim().isEmpty()) {
            throw new IllegalArgumentException(field + " cannot be null or blank");
        }
    }



    public long getId() {
        return id;
    }

    public void setId(long id) {
        if (id <= 0) {
            throw new IllegalArgumentException("id must be positive");
        }
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        requireNonNullOrBlank(name, "name");
        this.name = name.trim();
    }

    public String getSurname() {
        return surname;
    }

    public void setSurname(String surname) {
        requireNonNullOrBlank(surname, "surname");
        this.surname = surname.trim();
    }

    public String getMiddleName() {
        return middleName;
    }


    public void setMiddleName(String middleName) {
        if (middleName == null) {
            this.middleName = null;
        } else {
            String trimmed = middleName.trim();
            if (trimmed.isEmpty()) {
                throw new IllegalArgumentException("middleName cannot be empty if provided");
            }
            this.middleName = trimmed;
        }
    }

    public String getSchoolName() {
        return schoolName;
    }

    public void setSchoolName(String schoolName) {
        requireNonNullOrBlank(schoolName, "schoolName");
        this.schoolName = schoolName.trim();
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        requireNonNullOrBlank(email, "email");
        String trimmed = email.trim();
        if (!trimmed.contains("@")) {
            throw new IllegalArgumentException("email must contain '@'");
        }
        this.email = trimmed;
    }


    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof User user)) return false;
        return id == user.id;
    }

    @Override
    public int hashCode() {
        return Objects.hash(id);
    }
}
