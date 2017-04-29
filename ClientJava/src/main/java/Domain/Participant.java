package Domain;

/**
 * Created by andrei on 2017-04-06.
 */
public class Participant implements HasId<String> {
    private String name;
    private Integer Age;
    private AgeCategory ageCategory;

    public Participant() {
    }

    public Participant(String name, Integer age, AgeCategory ageCategory) {
        this.name = name;
        Age = age;
        this.ageCategory = ageCategory;
    }

    public String getId() {
        return name;
    }

    public void setId(String id) {
        name = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Integer getAge() {
        return Age;
    }

    public void setAge(Integer age) {
        Age = age;
    }

    public AgeCategory getAgeCategory() {
        return ageCategory;
    }

    public void setAgeCategory(AgeCategory ageCategory) {
        this.ageCategory = ageCategory;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof Participant)) return false;

        Participant that = (Participant) o;

        if (getName() != null ? !getName().equals(that.getName()) : that.getName() != null) return false;
        if (getAge() != null ? !getAge().equals(that.getAge()) : that.getAge() != null) return false;
        return getAgeCategory() != null ? getAgeCategory().equals(that.getAgeCategory()) : that.getAgeCategory() == null;
    }

    @Override
    public int hashCode() {
        int result = getName() != null ? getName().hashCode() : 0;
        result = 31 * result + (getAge() != null ? getAge().hashCode() : 0);
        result = 31 * result + (getAgeCategory() != null ? getAgeCategory().hashCode() : 0);
        return result;
    }

    @Override
    public String toString() {
        return "Participant{" +
                "name='" + name + '\'' +
                ", Age=" + Age +
                ", ageCategory=" + ageCategory +
                '}';
    }
}
