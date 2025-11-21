package lsms.model;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.time.LocalDate;

import static org.junit.jupiter.api.Assertions.*;

public class LsmsModelTests {

    @BeforeEach
    void setUp() {
        ExtentManager.clearAllExtents();
        // reset some static values if needed
        Student.setProbationGpaThreshold(3.0);
        Teacher.setYearlySalaryIncreasePercent(2.0);
        Payment.setMinAmount(1.0);
    }

    // 1. Student creation + extent
    @Test
    void createValidStudentAddsToExtent() {
        assertEquals(0, Student.getExtent().size());

        Student s = new Student(
                1L,                        // id
                "Alice",                   // name
                "Smith",                   // surname
                "PJATK Language School",   // schoolName
                "alice@example.com",       // email
                "S123",                    // studentNumber
                1,                         // semester
                1,                         // yearOfStudy
                4.0,                       // gpa
                0.0,                       // accountBalance
                LocalDate.now().minusDays(1) // enrollmentDate
        );

        assertEquals(1, Student.getExtent().size());
        assertTrue(Student.getExtent().contains(s));
    }

    // 2. Student invalid GPA throws exception
    @Test
    void settingInvalidGpaThrows() {
        Student s = new Student(
                1L,
                "Alice",
                "Smith",
                "PJATK Language School",
                "alice@example.com",
                "S123",
                1,
                1,
                3.0,
                0.0,
                LocalDate.now().minusDays(1)
        );

        assertThrows(IllegalArgumentException.class, () -> s.setGpa(10.0));
    }

    // 3. Student derived isOnProbation
    @Test
    void studentIsOnProbationDerivedCorrectly() {
        Student.setProbationGpaThreshold(3.5);

        Student s = new Student(
                1L,
                "Bob",
                "Brown",
                "PJATK Language School",
                "bob@example.com",
                "S124",
                1,
                1,
                3.0,
                0.0,
                LocalDate.now().minusDays(1)
        );

        assertTrue(s.isOnProbation());
        s.setGpa(4.0);
        assertFalse(s.isOnProbation());
    }

    // 4. Course multi-value attribute is immutable outside
    @Test
    void courseSpecializationsAreEncapsulated() {
        Course c = new Course(
                "ENG101",          // code
                "English Basics",  // name
                "Beginner",        // level
                "English",         // language
                5,                 // ects
                1.0,               // gpaWeight
                true,              // mandatory
                false              // hasExtraCertificate
        );
        c.addSpecialization("Exam preparation");

        assertEquals(1, c.getSpecializations().size());
        assertThrows(UnsupportedOperationException.class, () -> {
            c.getSpecializations().add("TOEFL");
        });
    }

    // 5. Payment derived isOverdue / isPaid
    @Test
    void paymentOverdueAndPaidDerived() {
        Payment p = new Payment(
                1L,                        // id
                100.0,                     // amount
                LocalDate.now().plusDays(1), // dueDate
                PaymentMethod.CARD         // paymentMethod
        );
        assertFalse(p.isPaid());
        assertFalse(p.isOverdue());

        // симулируем просрочку: двигаем дату назад
        p.setDueDate(LocalDate.now().minusDays(2));
        assertTrue(p.isOverdue());

        // оплачиваем сегодня
        p.setPaymentDate(LocalDate.now());
        assertTrue(p.isPaid());
        assertFalse(p.isOverdue());
    }

    // 6. Teacher currentSalary derived correctly
    @Test
    void teacherCurrentSalaryDerived() {
        Teacher.setYearlySalaryIncreasePercent(5.0);

        Teacher t = new Teacher(
                1L,
                "John",
                "Doe",
                "PJATK Language School",
                "john@example.com",
                LocalDate.now().minusYears(3), // hireDate
                1000.0,                        // baseSalary
                3,                             // yearsOfExperience
                10.0                           // bonusPercentage
        );

        double current = t.getCurrentSalary();
        // base * (1 + 0.05 * 3) = 1000 * 1.15 = 1150
        // bonus 10% => *1.10 => 1265
        assertEquals(1265.0, current, 0.0001);
    }

    // 7. Enrollment finalGrade only when COMPLETE
    @Test
    void enrollmentFinalGradeConstraint() {
        Enrollment e = new Enrollment(
                1L,
                LocalDate.now().minusDays(1),
                EnrollmentStatus.ACTIVE
        );

        assertThrows(IllegalStateException.class, () -> e.setFinalGrade(4.0));

        e.setStatus(EnrollmentStatus.COMPLETE);
        e.setFinalGrade(4.0);

        assertEquals(4.0, e.getFinalGrade());
        assertTrue(e.isPassed());
    }

    // 8. Extent is not directly modifiable from outside
    @Test
    void studentExtentIsUnmodifiable() {
        Student s = new Student(
                1L,
                "Alice",
                "Smith",
                "PJATK Language School",
                "alice@example.com",
                "S123",
                1,
                1,
                4.0,
                0.0,
                LocalDate.now().minusDays(1)
        );

        assertThrows(UnsupportedOperationException.class, () -> {
            Student.getExtent().add(s);
        });
    }

    // 9. Extent persistence save and load
    @Test
    void extentPersistenceRoundTrip() throws IOException {
        // create some objects
        new Student(
                1L,
                "Alice",
                "Smith",
                "PJATK Language School",
                "alice@example.com",
                "S123",
                1,
                1,
                4.0,
                0.0,
                LocalDate.now().minusDays(1)
        );
        new Teacher(
                2L,
                "John",
                "Doe",
                "PJATK Language School",
                "john@example.com",
                LocalDate.now().minusYears(1),
                2000.0,
                1,
                5.0
        );
        new Course(
                "ENG101",
                "English Basics",
                "Beginner",
                "English",
                5,
                1.0,
                true,
                false
        );
        new Enrollment(
                1L,
                LocalDate.now().minusDays(1),
                EnrollmentStatus.ACTIVE
        );
        new Payment(
                1L,
                100.0,
                LocalDate.now().plusDays(5),
                PaymentMethod.CARD
        );

        assertEquals(1, Student.getExtent().size());
        assertEquals(1, Teacher.getExtent().size());
        assertEquals(1, Course.getExtent().size());
        assertEquals(1, Enrollment.getExtent().size());
        assertEquals(1, Payment.getExtent().size());

        Path tmp = Files.createTempFile("lsms_extents", ".bin");

        // save
        ExtentManager.saveAll(tmp);

        // clear and ensure empty
        ExtentManager.clearAllExtents();
        assertEquals(0, Student.getExtent().size());

        // load
        boolean loaded = ExtentManager.loadAll(tmp);
        assertTrue(loaded);

        assertEquals(1, Student.getExtent().size());
        assertEquals(1, Teacher.getExtent().size());
        assertEquals(1, Course.getExtent().size());
        assertEquals(1, Enrollment.getExtent().size());
        assertEquals(1, Payment.getExtent().size());
    }

    // 10. Validation of Payment amount
    @Test
    void paymentRespectsMinAmountConstraint() {
        Payment.setMinAmount(50.0);

        assertThrows(IllegalArgumentException.class, () -> {
            new Payment(
                    1L,
                    10.0,                         // слишком мало
                    LocalDate.now().plusDays(2),
                    PaymentMethod.CARD
            );
        });

        Payment p = new Payment(
                2L,
                100.0,
                LocalDate.now().plusDays(2),
                PaymentMethod.CARD
        );
        assertEquals(100.0, p.getAmount());
    }
}
