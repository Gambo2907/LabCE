package com.example.appmovil;

public class Prof_view {
    private int Id;
    private String name;
    private int age;
    private boolean isActive;

    public Prof_view(int Id, String name, int age, boolean isActive) {
        this.Id = Id;
        this.name = name;
        this.age = age;
        this.isActive = isActive;
    }

    public Prof_view() {

    }

    @Override
    public String toString() {
        return "Prof_view{" +
                "Id=" + Id +
                ", name='" + name + '\'' +
                ", age=" + age +
                ", isActive=" + isActive +
                '}';
    }

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        Id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getAge() {
        return age;
    }

    public void setAge(int age) {
        this.age = age;
    }

    public boolean isActive() {
        return isActive;
    }

    public void setActive(boolean active) {
        isActive = active;
    }
}
