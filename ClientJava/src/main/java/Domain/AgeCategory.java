package Domain;

/**
 * Created by andrei on 2017-04-28.
 */
public class AgeCategory implements HasId<String> {
    private String name;
    private Integer minAge;
    private Integer maxAge;

    public AgeCategory() {
    }

    public AgeCategory(String name, Integer minAge, Integer maxAge) {
        this.name = name;
        this.minAge = minAge;
        this.maxAge = maxAge;
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

    public Integer getMinAge() {
        return minAge;
    }

    public void setMinAge(Integer minAge) {
        this.minAge = minAge;
    }

    public Integer getMaxAge() {
        return maxAge;
    }

    public void setMaxAge(Integer maxAge) {
        this.maxAge = maxAge;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof AgeCategory)) return false;

        AgeCategory that = (AgeCategory) o;

        if (getName() != null ? !getName().equals(that.getName()) : that.getName() != null) return false;
        if (getMinAge() != null ? !getMinAge().equals(that.getMinAge()) : that.getMinAge() != null) return false;
        return getMaxAge() != null ? getMaxAge().equals(that.getMaxAge()) : that.getMaxAge() == null;
    }

    @Override
    public int hashCode() {
        int result = getName() != null ? getName().hashCode() : 0;
        result = 31 * result + (getMinAge() != null ? getMinAge().hashCode() : 0);
        result = 31 * result + (getMaxAge() != null ? getMaxAge().hashCode() : 0);
        return result;
    }

    @Override
    public String toString() {
        return "AgeCategory{" +
                "name='" + name + '\'' +
                ", minAge=" + minAge +
                ", maxAge=" + maxAge +
                '}';
    }
}
