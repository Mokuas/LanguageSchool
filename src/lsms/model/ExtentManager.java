package lsms.model;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.io.Serializable;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class ExtentManager {

    private ExtentManager() {
    }

    // контейнер для сериализации
    private static class ExtentContainer implements Serializable {
        private static final long serialVersionUID = 1L;

        Map<String, List<?>> data = new HashMap<>();
    }

    public static void saveAll(Path filePath) throws IOException {
        ExtentContainer container = new ExtentContainer();
        container.data.put("students", List.copyOf(Student.getExtent()));
        container.data.put("teachers", List.copyOf(Teacher.getExtent()));
        container.data.put("courses", List.copyOf(Course.getExtent()));
        container.data.put("enrollments", List.copyOf(Enrollment.getExtent()));
        container.data.put("payments", List.copyOf(Payment.getExtent()));

        try (ObjectOutputStream oos =
                     new ObjectOutputStream(Files.newOutputStream(filePath))) {
            oos.writeObject(container);
        }
    }

    @SuppressWarnings("unchecked")
    public static boolean loadAll(Path filePath) {
        if (!Files.exists(filePath)) {
            clearAllExtents();
            return false;
        }

        try (ObjectInputStream ois =
                     new ObjectInputStream(Files.newInputStream(filePath))) {
            Object obj = ois.readObject();
            if (!(obj instanceof ExtentContainer container)) {
                clearAllExtents();
                return false;
            }

            Student.restoreExtent((List<Student>) container.data.getOrDefault("students", List.of()));
            Teacher.restoreExtent((List<Teacher>) container.data.getOrDefault("teachers", List.of()));
            Course.restoreExtent((List<Course>) container.data.getOrDefault("courses", List.of()));
            Enrollment.restoreExtent((List<Enrollment>) container.data.getOrDefault("enrollments", List.of()));
            Payment.restoreExtent((List<Payment>) container.data.getOrDefault("payments", List.of()));

            return true;
        } catch (Exception e) {
            clearAllExtents();
            return false;
        }
    }

    public static void clearAllExtents() {
        Student.clearExtentForTests();
        Teacher.clearExtentForTests();
        Course.clearExtentForTests();
        Enrollment.clearExtentForTests();
        Payment.clearExtentForTests();
    }
}
