# Use Case Diagram

```mermaid
graph TD
    actor Administrator

    subgraph "Hospital System"
        subgraph "Patient Management"
            UC1("Register Patient")
            UC2("Edit Patient")
            UC3("List Patients")
        end

        subgraph "Doctor Management"
            UC4("Register Doctor")
            UC5("Edit Doctor")
            UC6("List Doctors")
            UC7("List Doctors by Specialty")
        end

        subgraph "Appointment Management"
            UC8("Schedule Appointment")
            UC9("Cancel Appointment")
            UC10("Mark Appointment as Attended")
            UC11("View Appointments by Patient")
            UC12("View Appointments by Doctor")
        end
    end

    Administrator --> UC1
    Administrator --> UC2
    Administrator --> UC3
    Administrator --> UC4
    Administrator --> UC5
    Administrator --> UC6
    Administrator --> UC7
    Administrator --> UC8
    Administrator --> UC9
    Administrator --> UC10
    Administrator --> UC11
    Administrator --> UC12
```
