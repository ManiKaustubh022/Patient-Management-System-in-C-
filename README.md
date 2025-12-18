# 🏥 Hospital Patient Management System (C#)

An **advanced console-based Hospital Patient Management System** built in **C#** to demonstrate **core and advanced Object-Oriented Programming (OOP) concepts**, along with **delegates, events, and publisher–subscriber architecture**.

This project simulates real-world hospital workflows such as patient admission, priority-based queues, billing strategies, and emergency alerts in a clean, structured, and extensible way.

---

## ✨ Key Highlights

* Fully **menu-driven console application**
* Real-world inspired hospital workflow simulation
* Clean separation of concerns using OOP principles
* Rich console UI with alerts and status indicators

---

## 🧠 OOP Concepts Demonstrated

* **Encapsulation** – Private fields with public properties
* **Inheritance** – `Patient` base class with specialized child classes
* **Polymorphism** – Method overriding for billing and display logic
* **Abstraction** – Logical separation of patient types and billing logic

---

## 👥 Patient Types Supported

* **In-Patient**

  * Room allocation (General, Private, ICU, NICU)
  * Stay duration billing
  * Special diet & physiotherapy charges

* **Out-Patient**

  * Visit-based consultation
  * Optional lab tests & X-Ray

* **Emergency Patient**

  * Emergency surcharge
  * Ambulance, surgery & blood transfusion handling
  * Highest priority admission

---

## 🚦 Patient Classification

### Condition Severity

* Stable
* Moderate (+10%)
* Serious (+25%)
* Critical (+50%)

### Priority Levels

* Normal
* High
* Urgent
* Emergency

Patients are automatically placed into **priority queues** based on urgency.

---

## 💰 Billing Strategies (Delegates)

Billing is calculated dynamically using **delegate-based strategies**:

* Insurance Billing (70% covered)
* Discount Billing (20% off)
* Standard Billing
* Emergency Billing (10% off)
* Senior Citizen Billing (30% off)
* Government Scheme (50% subsidized)
* Corporate Billing (15% off)

> 💡 The system auto-suggests senior citizen discounts for patients aged 60+.

---

## 🔔 Events & Publisher–Subscriber Model

The `Hospital` class acts as a **publisher**, while departments act as **subscribers**.

### Events Implemented

* Patient Admitted
* Bill Generated
* Critical Patient Detected

### Subscribed Departments

* Reception Department
* Medical Department
* Accounts Department
* Pharmacy Department
* ICU Department
* Notification Service (SMS / Email simulation)

Each department reacts automatically when an event is triggered.

---

## 📋 Console Features

* Interactive menus
* Color-coded alerts (Critical, Serious, Success, Errors)
* Real-time queue status
* Detailed bill summaries

---

## 🛠️ Tech Stack

* **Language:** C#
* **Framework:** .NET (Console Application)
* **Paradigm:** Object-Oriented Programming

---

## ▶️ How to Run

1. Open the project in **Visual Studio** or any C# IDE
2. Ensure `.NET SDK` is installed
3. Build and run the project
4. Use the menu to admit patients, generate bills, and view queues

---

## 📁 Project Structure (Logical)

* `Patient` (Base Class)
* `InPatient`, `OutPatient`, `EmergencyPatient`
* `Hospital` (Publisher)
* Department Classes (Subscribers)
* Billing & Alert Managers
* `Program.cs` (Entry Point)

---

## 🎓 Use Case

This project is ideal for:

* Academic OOP demonstrations
* Understanding delegates & events in C#
* Console-based system design practice
* Mini-projects & viva presentations

---

## 👨‍💻 Author

**Kaustubh Mani**
B.Tech (CSE) | Graphic Designer & Tech Enthusiast

---

## 📜 License

This project is for **educational purposes**. You are free to modify and extend it.

---

⭐ *If you like this project, consider starring the repository!*
