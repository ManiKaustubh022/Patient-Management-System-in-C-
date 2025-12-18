using System;
using System.Collections.Generic;

namespace HospitalPatientManagementSystem
{
    // ==================== ENUMS FOR PATIENT CLASSIFICATION ====================
    
    // Enum for Patient Condition Severity
    public enum PatientCondition
    {
        Stable = 1,      // Normal condition - no extra charges
        Moderate = 2,    // Moderate condition - 10% extra
        Serious = 3,     // Serious condition - 25% extra
        Critical = 4     // Critical condition - 50% extra, priority care
    }

    // Enum for Patient Priority
    public enum PatientPriority
    {
        Normal = 1,
        High = 2,
        Urgent = 3,
        Emergency = 4
    }

    // Enum for Room Type (for InPatients)
    public enum RoomType
    {
        General = 1,     // Standard room
        SemiPrivate = 2, // Semi-private room
        Private = 3,     // Private room
        ICU = 4,         // Intensive Care Unit
        NICU = 5         // Neonatal ICU
    }

    // ==================== CRITERIA 1: CLASS AND OBJECTS ====================

    // Base Patient class demonstrating encapsulation with private fields and properties
    public class Patient
    {
        // Private fields (Encapsulation)
        private int patientId;
        private string name;
        private int age;
        private string ailment;
        private decimal baseTreatmentCost;
        private PatientCondition condition;
        private PatientPriority priority;
        private DateTime admissionDate;

        // Public Properties (Encapsulation with get/set)
        public int PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Age
        {
            get { return age; }
            set
            {
                if (value > 0 && value <= 150)
                    age = value;
                else
                    throw new ArgumentException("Age must be between 1 and 150");
            }
        }

        public string Ailment
        {
            get { return ailment; }
            set { ailment = value; }
        }

        public decimal BaseTreatmentCost
        {
            get { return baseTreatmentCost; }
            set { baseTreatmentCost = value; }
        }

        public PatientCondition Condition
        {
            get { return condition; }
            set { condition = value; }
        }

        public PatientPriority Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public DateTime AdmissionDate
        {
            get { return admissionDate; }
            set { admissionDate = value; }
        }

        // Constructor
        public Patient(int id, string name, int age, string ailment, decimal cost, 
                       PatientCondition condition, PatientPriority priority)
        {
            this.PatientId = id;
            this.Name = name;
            this.Age = age;
            this.Ailment = ailment;
            this.BaseTreatmentCost = cost;
            this.Condition = condition;
            this.Priority = priority;
            this.AdmissionDate = DateTime.Now;
        }

        // Method to get condition multiplier for billing
        public virtual decimal GetConditionMultiplier()
        {
            switch (Condition)
            {
                case PatientCondition.Stable:
                    return 1.0m;
                case PatientCondition.Moderate:
                    return 1.10m;  // 10% extra
                case PatientCondition.Serious:
                    return 1.25m;  // 25% extra
                case PatientCondition.Critical:
                    return 1.50m;  // 50% extra
                default:
                    return 1.0m;
            }
        }

        // Virtual method for polymorphism
        public virtual decimal CalculateTotalBill()
        {
            return BaseTreatmentCost * GetConditionMultiplier();
        }

        // Method to get condition description
        public string GetConditionDescription()
        {
            switch (Condition)
            {
                case PatientCondition.Stable:
                    return "Stable - Patient is in normal condition";
                case PatientCondition.Moderate:
                    return "Moderate - Patient requires regular monitoring";
                case PatientCondition.Serious:
                    return "Serious - Patient requires intensive care";
                case PatientCondition.Critical:
                    return "CRITICAL - Patient requires immediate attention!";
                default:
                    return "Unknown";
            }
        }

        // Method to get priority description
        public string GetPriorityDescription()
        {
            switch (Priority)
            {
                case PatientPriority.Normal:
                    return "Normal Priority";
                case PatientPriority.High:
                    return "High Priority";
                case PatientPriority.Urgent:
                    return "URGENT Priority";
                case PatientPriority.Emergency:
                    return "EMERGENCY - Immediate Action Required!";
                default:
                    return "Unknown";
            }
        }

        // Virtual method for displaying patient info
        public virtual void DisplayInfo()
        {
            Console.WriteLine("\n+============================================+");
            Console.WriteLine("|        PATIENT INFORMATION                 |");
            Console.WriteLine("+============================================+");
            Console.WriteLine("| Patient ID      : {0,-24}|", PatientId);
            Console.WriteLine("| Name            : {0,-24}|", Name);
            Console.WriteLine("| Age             : {0,-24}|", Age);
            Console.WriteLine("| Ailment         : {0,-24}|", Ailment);
            Console.WriteLine("| Base Cost       : ${0,-23:F2}|", BaseTreatmentCost);
            Console.WriteLine("| Condition       : {0,-24}|", Condition);
            Console.WriteLine("| Priority        : {0,-24}|", Priority);
            Console.WriteLine("| Admission Date  : {0,-24}|", AdmissionDate.ToString("yyyy-MM-dd HH:mm"));
            Console.WriteLine("+============================================+");
            
            // Display condition alert if serious or critical
            if (Condition == PatientCondition.Serious || Condition == PatientCondition.Critical)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("!!! ALERT: {0}", GetConditionDescription());
                Console.ResetColor();
            }
        }
    }

    // ==================== CRITERIA 2: OOPS CONCEPT ====================

    // InPatient class demonstrating Inheritance and Polymorphism
    public class InPatient : Patient
    {
        private int numberOfDays;
        private decimal roomChargePerDay;
        private RoomType roomType;
        private bool requiresSpecialDiet;
        private bool requiresPhysiotherapy;

        public int NumberOfDays
        {
            get { return numberOfDays; }
            set { numberOfDays = value; }
        }

        public decimal RoomChargePerDay
        {
            get { return roomChargePerDay; }
            set { roomChargePerDay = value; }
        }

        public RoomType RoomType
        {
            get { return roomType; }
            set { roomType = value; }
        }

        public bool RequiresSpecialDiet
        {
            get { return requiresSpecialDiet; }
            set { requiresSpecialDiet = value; }
        }

        public bool RequiresPhysiotherapy
        {
            get { return requiresPhysiotherapy; }
            set { requiresPhysiotherapy = value; }
        }

        // Constructor calling base constructor
        public InPatient(int id, string name, int age, string ailment, decimal cost,
                         PatientCondition condition, PatientPriority priority,
                         int days, decimal roomCharge, RoomType roomType,
                         bool specialDiet, bool physiotherapy)
            : base(id, name, age, ailment, cost, condition, priority)
        {
            this.NumberOfDays = days;
            this.RoomChargePerDay = roomCharge;
            this.RoomType = roomType;
            this.RequiresSpecialDiet = specialDiet;
            this.RequiresPhysiotherapy = physiotherapy;
        }

        // Get room type multiplier
        private decimal GetRoomTypeMultiplier()
        {
            switch (RoomType)
            {
                case RoomType.General:
                    return 1.0m;
                case RoomType.SemiPrivate:
                    return 1.5m;
                case RoomType.Private:
                    return 2.0m;
                case RoomType.ICU:
                    return 3.0m;
                case RoomType.NICU:
                    return 3.5m;
                default:
                    return 1.0m;
            }
        }

        // Method Overriding (Polymorphism)
        public override decimal CalculateTotalBill()
        {
            decimal baseBill = BaseTreatmentCost * GetConditionMultiplier();
            decimal totalRoomCharges = NumberOfDays * RoomChargePerDay * GetRoomTypeMultiplier();
            decimal specialDietCharges = RequiresSpecialDiet ? (NumberOfDays * 50) : 0;
            decimal physiotherapyCharges = RequiresPhysiotherapy ? (NumberOfDays * 100) : 0;
            
            return baseBill + totalRoomCharges + specialDietCharges + physiotherapyCharges;
        }

        // Method Overriding
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine("+============================================+");
            Console.WriteLine("|        IN-PATIENT DETAILS                  |");
            Console.WriteLine("+============================================+");
            Console.WriteLine("| Patient Type    : In-Patient               |");
            Console.WriteLine("| Number of Days  : {0,-24}|", NumberOfDays);
            Console.WriteLine("| Room Type       : {0,-24}|", RoomType);
            Console.WriteLine("| Room Charge/Day : ${0,-23:F2}|", RoomChargePerDay);
            Console.WriteLine("| Special Diet    : {0,-24}|", (RequiresSpecialDiet ? "Yes" : "No"));
            Console.WriteLine("| Physiotherapy   : {0,-24}|", (RequiresPhysiotherapy ? "Yes" : "No"));
            Console.WriteLine("+============================================+");
        }
    }

    // OutPatient class demonstrating Inheritance and Polymorphism
    public class OutPatient : Patient
    {
        private int numberOfVisits;
        private decimal consultationFeePerVisit;
        private bool requiresLabTests;
        private bool requiresXRay;

        public int NumberOfVisits
        {
            get { return numberOfVisits; }
            set { numberOfVisits = value; }
        }

        public decimal ConsultationFeePerVisit
        {
            get { return consultationFeePerVisit; }
            set { consultationFeePerVisit = value; }
        }

        public bool RequiresLabTests
        {
            get { return requiresLabTests; }
            set { requiresLabTests = value; }
        }

        public bool RequiresXRay
        {
            get { return requiresXRay; }
            set { requiresXRay = value; }
        }

        // Constructor
        public OutPatient(int id, string name, int age, string ailment, decimal cost,
                          PatientCondition condition, PatientPriority priority,
                          int visits, decimal consultationFee, bool labTests, bool xRay)
            : base(id, name, age, ailment, cost, condition, priority)
        {
            this.NumberOfVisits = visits;
            this.ConsultationFeePerVisit = consultationFee;
            this.RequiresLabTests = labTests;
            this.RequiresXRay = xRay;
        }

        // Method Overriding (Polymorphism)
        public override decimal CalculateTotalBill()
        {
            decimal baseBill = BaseTreatmentCost * GetConditionMultiplier();
            decimal totalConsultationFees = NumberOfVisits * ConsultationFeePerVisit;
            decimal labTestCharges = RequiresLabTests ? 200 : 0;
            decimal xRayCharges = RequiresXRay ? 150 : 0;
            
            return baseBill + totalConsultationFees + labTestCharges + xRayCharges;
        }

        // Method Overriding
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine("+============================================+");
            Console.WriteLine("|        OUT-PATIENT DETAILS                 |");
            Console.WriteLine("+============================================+");
            Console.WriteLine("| Patient Type     : Out-Patient             |");
            Console.WriteLine("| Number of Visits : {0,-23}|", NumberOfVisits);
            Console.WriteLine("| Consult Fee/Visit: ${0,-22:F2}|", ConsultationFeePerVisit);
            Console.WriteLine("| Lab Tests        : {0,-23}|", (RequiresLabTests ? "Yes ($200)" : "No"));
            Console.WriteLine("| X-Ray            : {0,-23}|", (RequiresXRay ? "Yes ($150)" : "No"));
            Console.WriteLine("+============================================+");
        }
    }

    // EmergencyPatient class demonstrating Inheritance and Polymorphism
    public class EmergencyPatient : Patient
    {
        private decimal emergencySurcharge;
        private bool requiresAmbulance;
        private bool requiresSurgery;
        private bool requiresBloodTransfusion;
        private string emergencyType;

        public decimal EmergencySurcharge
        {
            get { return emergencySurcharge; }
            set { emergencySurcharge = value; }
        }

        public bool RequiresAmbulance
        {
            get { return requiresAmbulance; }
            set { requiresAmbulance = value; }
        }

        public bool RequiresSurgery
        {
            get { return requiresSurgery; }
            set { requiresSurgery = value; }
        }

        public bool RequiresBloodTransfusion
        {
            get { return requiresBloodTransfusion; }
            set { requiresBloodTransfusion = value; }
        }

        public string EmergencyType
        {
            get { return emergencyType; }
            set { emergencyType = value; }
        }

        // Constructor
        public EmergencyPatient(int id, string name, int age, string ailment, decimal cost,
                                PatientCondition condition, PatientPriority priority,
                                decimal surcharge, bool ambulance, bool surgery, 
                                bool bloodTransfusion, string emergencyType)
            : base(id, name, age, ailment, cost, condition, priority)
        {
            this.EmergencySurcharge = surcharge;
            this.RequiresAmbulance = ambulance;
            this.RequiresSurgery = surgery;
            this.RequiresBloodTransfusion = bloodTransfusion;
            this.EmergencyType = emergencyType;
        }

        // Method Overriding (Polymorphism)
        public override decimal CalculateTotalBill()
        {
            decimal baseBill = BaseTreatmentCost * GetConditionMultiplier();
            decimal ambulanceFee = RequiresAmbulance ? 500 : 0;
            decimal surgeryFee = RequiresSurgery ? 5000 : 0;
            decimal bloodTransfusionFee = RequiresBloodTransfusion ? 1000 : 0;
            
            return baseBill + EmergencySurcharge + ambulanceFee + surgeryFee + bloodTransfusionFee;
        }

        // Method Overriding
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("+============================================+");
            Console.WriteLine("|        EMERGENCY PATIENT DETAILS           |");
            Console.WriteLine("+============================================+");
            Console.WriteLine("| Patient Type     : Emergency Patient       |");
            Console.WriteLine("| Emergency Type   : {0,-23}|", EmergencyType);
            Console.WriteLine("| Surcharge        : ${0,-22:F2}|", EmergencySurcharge);
            Console.WriteLine("| Ambulance        : {0,-23}|", (RequiresAmbulance ? "Yes ($500)" : "No"));
            Console.WriteLine("| Surgery          : {0,-23}|", (RequiresSurgery ? "Yes ($5000)" : "No"));
            Console.WriteLine("| Blood Transfusion: {0,-23}|", (RequiresBloodTransfusion ? "Yes ($1000)" : "No"));
            Console.WriteLine("+============================================+");
            Console.ResetColor();
        }
    }

    // ==================== CRITERIA 3: DELEGATES ====================

    // Delegate for billing strategies
    public delegate decimal BillingStrategy(decimal baseBill);

    // Delegate for patient condition alerts
    public delegate void ConditionAlertHandler(Patient patient);

    // Billing manager class that uses delegates
    public static class BillingManager
    {
        // Predefined billing strategies using delegates
        public static decimal InsuranceBilling(decimal baseBill)
        {
            Console.WriteLine("[Insurance] Applying Insurance Billing Strategy (30% patient pays)...");
            return baseBill * 0.30m;
        }

        public static decimal DiscountBilling(decimal baseBill)
        {
            Console.WriteLine("[Discount] Applying Discount Billing Strategy (20% discount)...");
            return baseBill * 0.80m;
        }

        public static decimal StandardBilling(decimal baseBill)
        {
            Console.WriteLine("[Standard] Applying Standard Billing Strategy (Full payment)...");
            return baseBill;
        }

        public static decimal EmergencyBilling(decimal baseBill)
        {
            Console.WriteLine("[Emergency] Applying Emergency Billing Strategy (10% discount)...");
            return baseBill * 0.90m;
        }

        public static decimal SeniorCitizenBilling(decimal baseBill)
        {
            Console.WriteLine("[Senior] Applying Senior Citizen Billing Strategy (30% discount)...");
            return baseBill * 0.70m;
        }

        public static decimal GovernmentSchemeBilling(decimal baseBill)
        {
            Console.WriteLine("[Govt] Applying Government Scheme Billing (50% subsidized)...");
            return baseBill * 0.50m;
        }

        public static decimal CorporateBilling(decimal baseBill)
        {
            Console.WriteLine("[Corporate] Applying Corporate Billing Strategy (15% discount)...");
            return baseBill * 0.85m;
        }
    }

    // Alert Manager using delegates
    public static class AlertManager
    {
        public static void CriticalPatientAlert(Patient patient)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n!!! CRITICAL PATIENT ALERT !!!");
            Console.WriteLine("Patient: {0} (ID: {1})", patient.Name, patient.PatientId);
            Console.WriteLine("Condition: {0}", patient.GetConditionDescription());
            Console.WriteLine("Immediate medical attention required!");
            Console.ResetColor();
        }

        public static void SeriousPatientAlert(Patient patient)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n*** SERIOUS PATIENT ALERT ***");
            Console.WriteLine("Patient: {0} (ID: {1})", patient.Name, patient.PatientId);
            Console.WriteLine("Condition: {0}", patient.GetConditionDescription());
            Console.WriteLine("Close monitoring required!");
            Console.ResetColor();
        }
    }

    // ==================== CRITERIA 4: EVENT & DELEGATE ====================

    // Custom EventArgs for patient admission
    public class PatientAdmittedEventArgs : EventArgs
    {
        public Patient Patient { get; set; }
        public DateTime AdmissionTime { get; set; }

        public PatientAdmittedEventArgs(Patient patient)
        {
            this.Patient = patient;
            this.AdmissionTime = DateTime.Now;
        }
    }

    // Custom EventArgs for billing events
    public class BillGeneratedEventArgs : EventArgs
    {
        public Patient Patient { get; set; }
        public decimal BaseBill { get; set; }
        public decimal FinalBill { get; set; }
        public string BillingStrategy { get; set; }
        public decimal Discount { get; set; }

        public BillGeneratedEventArgs(Patient patient, decimal baseBill, decimal finalBill, string strategy)
        {
            this.Patient = patient;
            this.BaseBill = baseBill;
            this.FinalBill = finalBill;
            this.BillingStrategy = strategy;
            this.Discount = baseBill - finalBill;
        }
    }

    // Custom EventArgs for critical patient alerts
    public class CriticalPatientEventArgs : EventArgs
    {
        public Patient Patient { get; set; }
        public string AlertMessage { get; set; }
        public DateTime AlertTime { get; set; }

        public CriticalPatientEventArgs(Patient patient, string message)
        {
            this.Patient = patient;
            this.AlertMessage = message;
            this.AlertTime = DateTime.Now;
        }
    }

    // Hospital class that publishes events
    public class Hospital
    {
        private string hospitalName;
        private List<Patient> patients;
        private Dictionary<PatientPriority, Queue<Patient>> priorityQueues;

        // Event declarations (Publisher)
        public event EventHandler<PatientAdmittedEventArgs> PatientAdmitted;
        public event EventHandler<BillGeneratedEventArgs> BillGenerated;
        public event EventHandler<CriticalPatientEventArgs> CriticalPatientDetected;

        // Delegate for condition-based alerts
        public ConditionAlertHandler ConditionAlert;

        public Hospital(string name)
        {
            this.hospitalName = name;
            this.patients = new List<Patient>();
            this.priorityQueues = new Dictionary<PatientPriority, Queue<Patient>>();
            this.priorityQueues.Add(PatientPriority.Emergency, new Queue<Patient>());
            this.priorityQueues.Add(PatientPriority.Urgent, new Queue<Patient>());
            this.priorityQueues.Add(PatientPriority.High, new Queue<Patient>());
            this.priorityQueues.Add(PatientPriority.Normal, new Queue<Patient>());
        }

        // Method to raise PatientAdmitted event
        protected virtual void OnPatientAdmitted(PatientAdmittedEventArgs e)
        {
            if (PatientAdmitted != null)
            {
                PatientAdmitted(this, e);
            }
        }

        // Method to raise BillGenerated event
        protected virtual void OnBillGenerated(BillGeneratedEventArgs e)
        {
            if (BillGenerated != null)
            {
                BillGenerated(this, e);
            }
        }

        // Method to raise CriticalPatient event
        protected virtual void OnCriticalPatientDetected(CriticalPatientEventArgs e)
        {
            if (CriticalPatientDetected != null)
            {
                CriticalPatientDetected(this, e);
            }
        }

        // Admit patient and trigger event
        public void AdmitPatient(Patient patient)
        {
            patients.Add(patient);
            priorityQueues[patient.Priority].Enqueue(patient);

            Console.WriteLine("\n+============================================================+");
            Console.WriteLine("|  [OK] Patient {0} admitted to {1}", patient.Name.PadRight(15), hospitalName.PadRight(15) + "|");
            Console.WriteLine("|  Priority Queue: {0}", patient.Priority.ToString().PadRight(40) + "|");
            Console.WriteLine("+============================================================+");

            // Raise admission event
            OnPatientAdmitted(new PatientAdmittedEventArgs(patient));

            // Check for critical/serious conditions and raise alerts
            if (patient.Condition == PatientCondition.Critical)
            {
                OnCriticalPatientDetected(new CriticalPatientEventArgs(patient, 
                    "CRITICAL condition patient admitted - immediate care required!"));
                
                // Invoke delegate for alert
                if (ConditionAlert != null)
                {
                    ConditionAlert(patient);
                }
            }
            else if (patient.Condition == PatientCondition.Serious)
            {
                OnCriticalPatientDetected(new CriticalPatientEventArgs(patient, 
                    "SERIOUS condition patient admitted - close monitoring required!"));
            }
        }

        // Generate bill using delegate and trigger event
        public void GenerateBill(Patient patient, BillingStrategy strategy, string strategyName)
        {
            decimal baseBill = patient.CalculateTotalBill();
            decimal finalBill = strategy(baseBill);

            Console.WriteLine("\n+============================================+");
            Console.WriteLine("|          BILL SUMMARY                      |");
            Console.WriteLine("+============================================+");
            Console.WriteLine("| Patient Name    : {0,-24}|", patient.Name);
            Console.WriteLine("| Patient ID      : {0,-24}|", patient.PatientId);
            Console.WriteLine("| Condition       : {0,-24}|", patient.Condition);
            Console.WriteLine("| Condition Multi.: {0,-24}|", patient.GetConditionMultiplier().ToString("P0"));
            Console.WriteLine("+--------------------------------------------+");
            Console.WriteLine("| Base Bill       : ${0,-23:F2}|", baseBill);
            Console.WriteLine("| Strategy        : {0,-24}|", strategyName);
            Console.WriteLine("| Discount        : ${0,-23:F2}|", (baseBill - finalBill));
            Console.WriteLine("+--------------------------------------------+");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("| FINAL BILL      : ${0,-23:F2}|", finalBill);
            Console.ResetColor();
            Console.WriteLine("+============================================+");

            // Raise the event
            OnBillGenerated(new BillGeneratedEventArgs(patient, baseBill, finalBill, strategyName));
        }

        // Display priority queue status
        public void DisplayQueueStatus()
        {
            Console.WriteLine("\n+============================================+");
            Console.WriteLine("|       PATIENT PRIORITY QUEUE STATUS        |");
            Console.WriteLine("+============================================+");
            Console.WriteLine("| Emergency Queue : {0,-24}|", priorityQueues[PatientPriority.Emergency].Count + " patients");
            Console.WriteLine("| Urgent Queue    : {0,-24}|", priorityQueues[PatientPriority.Urgent].Count + " patients");
            Console.WriteLine("| High Queue      : {0,-24}|", priorityQueues[PatientPriority.High].Count + " patients");
            Console.WriteLine("| Normal Queue    : {0,-24}|", priorityQueues[PatientPriority.Normal].Count + " patients");
            Console.WriteLine("+============================================+");
        }
    }

    // ==================== CRITERIA 5: PUBLISHER & SUBSCRIBER ====================

    // Subscriber classes that handle events
    public class ReceptionDepartment
    {
        public void OnPatientAdmitted(object sender, PatientAdmittedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[RECEPTION] Patient {0} (ID: {1}) admitted at {2}. Preparing documentation...",
                e.Patient.Name, e.Patient.PatientId, e.AdmissionTime.ToString("HH:mm:ss"));
            Console.WriteLine("[RECEPTION] Patient Condition: {0} | Priority: {1}",
                e.Patient.Condition, e.Patient.Priority);
            Console.ResetColor();
        }

        public void OnBillGenerated(object sender, BillGeneratedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[RECEPTION] Bill of ${0:F2} generated for {1}. Strategy: {2}",
                e.FinalBill, e.Patient.Name, e.BillingStrategy);
            Console.ResetColor();
        }
    }

    public class MedicalDepartment
    {
        public void OnPatientAdmitted(object sender, PatientAdmittedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[MEDICAL] New patient {0} assigned. Ailment: {1}. Scheduling treatment...",
                e.Patient.Name, e.Patient.Ailment);
            Console.WriteLine("[MEDICAL] Patient Condition: {0} - {1}",
                e.Patient.Condition, e.Patient.GetConditionDescription());
            Console.ResetColor();
        }

        public void OnCriticalPatient(object sender, CriticalPatientEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[MEDICAL - ALERT] {0}", e.AlertMessage);
            Console.WriteLine("[MEDICAL - ALERT] Patient: {0} | Time: {1}", 
                e.Patient.Name, e.AlertTime.ToString("HH:mm:ss"));
            Console.ResetColor();
        }
    }

    public class AccountsDepartment
    {
        public void OnBillGenerated(object sender, BillGeneratedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[ACCOUNTS] Recording payment of ${0:F2} for patient ID: {1}",
                e.FinalBill, e.Patient.PatientId);
            Console.WriteLine("[ACCOUNTS] Discount applied: ${0:F2} | Strategy: {1}",
                e.Discount, e.BillingStrategy);
            Console.ResetColor();
        }
    }

    public class PharmacyDepartment
    {
        public void OnPatientAdmitted(object sender, PatientAdmittedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("[PHARMACY] Preparing medications for {0}. Ailment: {1}",
                e.Patient.Name, e.Patient.Ailment);
            Console.ResetColor();
        }
    }

    public class ICUDepartment
    {
        public void OnCriticalPatient(object sender, CriticalPatientEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ICU] ALERT! Preparing ICU bed for critical patient: {0}", e.Patient.Name);
            Console.WriteLine("[ICU] Condition: {0} | Alert Time: {1}", 
                e.Patient.Condition, e.AlertTime.ToString("HH:mm:ss"));
            Console.ResetColor();
        }
    }

    public class NotificationService
    {
        public void OnCriticalPatient(object sender, CriticalPatientEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("[NOTIFICATION] Sending SMS/Email alert to emergency contacts...");
            Console.WriteLine("[NOTIFICATION] Critical patient: {0} | Message: {1}",
                e.Patient.Name, e.AlertMessage);
            Console.ResetColor();
        }
    }

    // ==================== MAIN PROGRAM ====================
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("+============================================================+");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|        HOSPITAL PATIENT MANAGEMENT SYSTEM                  |");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("|            Advanced OOP Demonstration Project              |");
            Console.WriteLine("|                                                            |");
            Console.WriteLine("+============================================================+");
            Console.ResetColor();

            // Create hospital instance
            Hospital hospital = new Hospital("City General Hospital");

            // Create department instances (Subscribers)
            ReceptionDepartment reception = new ReceptionDepartment();
            MedicalDepartment medical = new MedicalDepartment();
            AccountsDepartment accounts = new AccountsDepartment();
            PharmacyDepartment pharmacy = new PharmacyDepartment();
            ICUDepartment icu = new ICUDepartment();
            NotificationService notification = new NotificationService();

            // Subscribe departments to hospital events (Event Subscription)
            hospital.PatientAdmitted += reception.OnPatientAdmitted;
            hospital.PatientAdmitted += medical.OnPatientAdmitted;
            hospital.PatientAdmitted += pharmacy.OnPatientAdmitted;

            hospital.BillGenerated += reception.OnBillGenerated;
            hospital.BillGenerated += accounts.OnBillGenerated;

            hospital.CriticalPatientDetected += medical.OnCriticalPatient;
            hospital.CriticalPatientDetected += icu.OnCriticalPatient;
            hospital.CriticalPatientDetected += notification.OnCriticalPatient;

            // Set up condition alert delegate
            hospital.ConditionAlert = AlertManager.CriticalPatientAlert;

            bool continueProgram = true;
            int patientIdCounter = 1000;

            while (continueProgram)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AdmitInPatient(hospital, ref patientIdCounter);
                        break;
                    case "2":
                        AdmitOutPatient(hospital, ref patientIdCounter);
                        break;
                    case "3":
                        AdmitEmergencyPatient(hospital, ref patientIdCounter);
                        break;
                    case "4":
                        hospital.DisplayQueueStatus();
                        break;
                    case "5":
                        DisplayConditionInfo();
                        break;
                    case "6":
                        DisplayBillingStrategiesInfo();
                        break;
                    case "7":
                        continueProgram = false;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n[OK] Thank you for using the Patient Management System!");
                        Console.ResetColor();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n[ERROR] Invalid choice! Please try again.");
                        Console.ResetColor();
                        break;
                }
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static void DisplayMainMenu()
        {
            Console.WriteLine("\n+============================================+");
            Console.WriteLine("|              MAIN MENU                     |");
            Console.WriteLine("+============================================+");
            Console.WriteLine("|  1. Admit In-Patient                       |");
            Console.WriteLine("|  2. Admit Out-Patient                      |");
            Console.WriteLine("|  3. Admit Emergency Patient                |");
            Console.WriteLine("|  4. View Priority Queue Status             |");
            Console.WriteLine("|  5. View Condition Types Info              |");
            Console.WriteLine("|  6. View Billing Strategies Info           |");
            Console.WriteLine("|  7. Exit                                   |");
            Console.WriteLine("+============================================+");
            Console.Write("\nEnter your choice: ");
        }

        static void DisplayConditionInfo()
        {
            Console.WriteLine("\n+============================================================+");
            Console.WriteLine("|           PATIENT CONDITION TYPES                          |");
            Console.WriteLine("+============================================================+");
            Console.WriteLine("|  1. STABLE    - Normal condition (No extra charges)        |");
            Console.WriteLine("|  2. MODERATE  - Regular monitoring (+10% charges)          |");
            Console.WriteLine("|  3. SERIOUS   - Intensive care needed (+25% charges)       |");
            Console.WriteLine("|  4. CRITICAL  - Immediate attention (+50% charges)         |");
            Console.WriteLine("+============================================================+");

            Console.WriteLine("\n+============================================================+");
            Console.WriteLine("|           PATIENT PRIORITY LEVELS                          |");
            Console.WriteLine("+============================================================+");
            Console.WriteLine("|  1. NORMAL    - Regular queue                              |");
            Console.WriteLine("|  2. HIGH      - Priority treatment                         |");
            Console.WriteLine("|  3. URGENT    - Immediate attention                        |");
            Console.WriteLine("|  4. EMERGENCY - Highest priority                           |");
            Console.WriteLine("+============================================================+");
        }

        static void DisplayBillingStrategiesInfo()
        {
            Console.WriteLine("\n+============================================================+");
            Console.WriteLine("|           BILLING STRATEGIES AVAILABLE                     |");
            Console.WriteLine("+============================================================+");
            Console.WriteLine("|  1. Insurance Billing      - 70% covered, 30% patient      |");
            Console.WriteLine("|  2. Discount Billing       - 20% discount                  |");
            Console.WriteLine("|  3. Standard Billing       - Full payment                  |");
            Console.WriteLine("|  4. Emergency Billing      - 10% discount                  |");
            Console.WriteLine("|  5. Senior Citizen Billing - 30% discount                  |");
            Console.WriteLine("|  6. Government Scheme      - 50% subsidized                |");
            Console.WriteLine("|  7. Corporate Billing      - 15% discount                  |");
            Console.WriteLine("+============================================================+");
        }

        // Method to select patient condition
        static PatientCondition SelectPatientCondition()
        {
            Console.WriteLine("\n--- SELECT PATIENT CONDITION ---");
            Console.WriteLine("1. Stable (Normal condition)");
            Console.WriteLine("2. Moderate (Regular monitoring needed)");
            Console.WriteLine("3. Serious (Intensive care needed)");
            Console.WriteLine("4. Critical (Immediate attention required)");
            Console.Write("Enter choice (1-4): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    return PatientCondition.Stable;
                case "2":
                    return PatientCondition.Moderate;
                case "3":
                    return PatientCondition.Serious;
                case "4":
                    return PatientCondition.Critical;
                default:
                    Console.WriteLine("Invalid choice. Defaulting to Stable.");
                    return PatientCondition.Stable;
            }
        }

        // Method to select patient priority
        static PatientPriority SelectPatientPriority()
        {
            Console.WriteLine("\n--- SELECT PATIENT PRIORITY ---");
            Console.WriteLine("1. Normal (Regular queue)");
            Console.WriteLine("2. High (Priority treatment)");
            Console.WriteLine("3. Urgent (Immediate attention)");
            Console.WriteLine("4. Emergency (Highest priority)");
            Console.Write("Enter choice (1-4): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    return PatientPriority.Normal;
                case "2":
                    return PatientPriority.High;
                case "3":
                    return PatientPriority.Urgent;
                case "4":
                    return PatientPriority.Emergency;
                default:
                    Console.WriteLine("Invalid choice. Defaulting to Normal.");
                    return PatientPriority.Normal;
            }
        }

        // Method to select room type for in-patients
        static RoomType SelectRoomType()
        {
            Console.WriteLine("\n--- SELECT ROOM TYPE ---");
            Console.WriteLine("1. General (Standard room - 1x charge)");
            Console.WriteLine("2. Semi-Private (1.5x charge)");
            Console.WriteLine("3. Private (2x charge)");
            Console.WriteLine("4. ICU (Intensive Care - 3x charge)");
            Console.WriteLine("5. NICU (Neonatal ICU - 3.5x charge)");
            Console.Write("Enter choice (1-5): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    return RoomType.General;
                case "2":
                    return RoomType.SemiPrivate;
                case "3":
                    return RoomType.Private;
                case "4":
                    return RoomType.ICU;
                case "5":
                    return RoomType.NICU;
                default:
                    Console.WriteLine("Invalid choice. Defaulting to General.");
                    return RoomType.General;
            }
        }

        // Method to select emergency type
        static string SelectEmergencyType()
        {
            Console.WriteLine("\n--- SELECT EMERGENCY TYPE ---");
            Console.WriteLine("1. Accident");
            Console.WriteLine("2. Heart Attack");
            Console.WriteLine("3. Stroke");
            Console.WriteLine("4. Trauma");
            Console.WriteLine("5. Respiratory Emergency");
            Console.WriteLine("6. Other");
            Console.Write("Enter choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    return "Accident";
                case "2":
                    return "Heart Attack";
                case "3":
                    return "Stroke";
                case "4":
                    return "Trauma";
                case "5":
                    return "Respiratory Emergency";
                case "6":
                    return "Other Emergency";
                default:
                    return "Other Emergency";
            }
        }

        // Method to admit an In-Patient
        static void AdmitInPatient(Hospital hospital, ref int patientId)
        {
            Console.WriteLine("\n+============================================+");
            Console.WriteLine("|       ADMITTING IN-PATIENT                 |");
            Console.WriteLine("+============================================+");

            try
            {
                Console.Write("Enter patient name: ");
                string name = Console.ReadLine();

                Console.Write("Enter patient age: ");
                int age = int.Parse(Console.ReadLine());

                Console.Write("Enter ailment: ");
                string ailment = Console.ReadLine();

                Console.Write("Enter base treatment cost: $");
                decimal cost = decimal.Parse(Console.ReadLine());

                // Select patient condition
                PatientCondition condition = SelectPatientCondition();

                // Select patient priority
                PatientPriority priority = SelectPatientPriority();

                Console.Write("Enter number of days: ");
                int days = int.Parse(Console.ReadLine());

                Console.Write("Enter room charge per day: $");
                decimal roomCharge = decimal.Parse(Console.ReadLine());

                // Select room type
                RoomType roomType = SelectRoomType();

                Console.Write("Requires special diet? (y/n): ");
                bool specialDiet = Console.ReadLine().ToLower() == "y";

                Console.Write("Requires physiotherapy? (y/n): ");
                bool physiotherapy = Console.ReadLine().ToLower() == "y";

                // Create InPatient object
                InPatient inPatient = new InPatient(patientId++, name, age, ailment, cost,
                    condition, priority, days, roomCharge, roomType, specialDiet, physiotherapy);

                // Display patient info
                inPatient.DisplayInfo();

                // Admit patient (triggers event)
                hospital.AdmitPatient(inPatient);

                // Select billing strategy
                BillingStrategy strategy = SelectBillingStrategy(age);
                string strategyName = GetStrategyName(strategy);

                // Generate bill
                hospital.GenerateBill(inPatient, strategy, strategyName);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[ERROR] " + ex.Message);
                Console.ResetColor();
            }
        }

        // Method to admit an Out-Patient
        static void AdmitOutPatient(Hospital hospital, ref int patientId)
        {
            Console.WriteLine("\n+============================================+");
            Console.WriteLine("|       ADMITTING OUT-PATIENT                |");
            Console.WriteLine("+============================================+");

            try
            {
                Console.Write("Enter patient name: ");
                string name = Console.ReadLine();

                Console.Write("Enter patient age: ");
                int age = int.Parse(Console.ReadLine());

                Console.Write("Enter ailment: ");
                string ailment = Console.ReadLine();

                Console.Write("Enter base treatment cost: $");
                decimal cost = decimal.Parse(Console.ReadLine());

                // Select patient condition
                PatientCondition condition = SelectPatientCondition();

                // Select patient priority
                PatientPriority priority = SelectPatientPriority();

                Console.Write("Enter number of visits: ");
                int visits = int.Parse(Console.ReadLine());

                Console.Write("Enter consultation fee per visit: $");
                decimal consultationFee = decimal.Parse(Console.ReadLine());

                Console.Write("Requires lab tests? (y/n): ");
                bool labTests = Console.ReadLine().ToLower() == "y";

                Console.Write("Requires X-Ray? (y/n): ");
                bool xRay = Console.ReadLine().ToLower() == "y";

                // Create OutPatient object
                OutPatient outPatient = new OutPatient(patientId++, name, age, ailment, cost,
                    condition, priority, visits, consultationFee, labTests, xRay);

                // Display patient info
                outPatient.DisplayInfo();

                // Admit patient (triggers event)
                hospital.AdmitPatient(outPatient);

                // Select billing strategy
                BillingStrategy strategy = SelectBillingStrategy(age);
                string strategyName = GetStrategyName(strategy);

                // Generate bill
                hospital.GenerateBill(outPatient, strategy, strategyName);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[ERROR] " + ex.Message);
                Console.ResetColor();
            }
        }

        // Method to admit an Emergency Patient
        static void AdmitEmergencyPatient(Hospital hospital, ref int patientId)
        {
            Console.WriteLine("\n+============================================+");
            Console.WriteLine("|     ADMITTING EMERGENCY PATIENT            |");
            Console.WriteLine("+============================================+");

            try
            {
                Console.Write("Enter patient name: ");
                string name = Console.ReadLine();

                Console.Write("Enter patient age: ");
                int age = int.Parse(Console.ReadLine());

                Console.Write("Enter ailment: ");
                string ailment = Console.ReadLine();

                Console.Write("Enter base treatment cost: $");
                decimal cost = decimal.Parse(Console.ReadLine());

                // Select patient condition
                PatientCondition condition = SelectPatientCondition();

                // Select patient priority
                PatientPriority priority = SelectPatientPriority();

                // Select emergency type
                string emergencyType = SelectEmergencyType();

                Console.Write("Enter emergency surcharge: $");
                decimal surcharge = decimal.Parse(Console.ReadLine());

                Console.Write("Requires ambulance? (y/n): ");
                bool ambulance = Console.ReadLine().ToLower() == "y";

                Console.Write("Requires surgery? (y/n): ");
                bool surgery = Console.ReadLine().ToLower() == "y";

                Console.Write("Requires blood transfusion? (y/n): ");
                bool bloodTransfusion = Console.ReadLine().ToLower() == "y";

                // Create EmergencyPatient object
                EmergencyPatient emergencyPatient = new EmergencyPatient(patientId++, name, age, 
                    ailment, cost, condition, priority, surcharge, ambulance, surgery, 
                    bloodTransfusion, emergencyType);

                // Display patient info
                emergencyPatient.DisplayInfo();

                // Admit patient (triggers event)
                hospital.AdmitPatient(emergencyPatient);

                // Select billing strategy
                BillingStrategy strategy = SelectBillingStrategy(age);
                string strategyName = GetStrategyName(strategy);

                // Generate bill
                hospital.GenerateBill(emergencyPatient, strategy, strategyName);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[ERROR] " + ex.Message);
                Console.ResetColor();
            }
        }

        // Method to select billing strategy using delegates
        static BillingStrategy SelectBillingStrategy(int patientAge)
        {
            Console.WriteLine("\n+============================================+");
            Console.WriteLine("|      SELECT BILLING STRATEGY               |");
            Console.WriteLine("+============================================+");
            Console.WriteLine("|  1. Insurance Billing (70% covered)        |");
            Console.WriteLine("|  2. Discount Billing (20% off)             |");
            Console.WriteLine("|  3. Standard Billing (Full payment)        |");
            Console.WriteLine("|  4. Emergency Billing (10% off)            |");
            Console.WriteLine("|  5. Senior Citizen (30% off)               |");
            Console.WriteLine("|  6. Government Scheme (50% subsidized)     |");
            Console.WriteLine("|  7. Corporate Billing (15% off)            |");
            Console.WriteLine("+============================================+");

            // Auto-suggest for senior citizens
            if (patientAge >= 60)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[TIP] Patient is {0} years old. Senior Citizen discount (Option 5) recommended!", patientAge);
                Console.ResetColor();
            }

            Console.Write("Enter choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    return BillingManager.InsuranceBilling;
                case "2":
                    return BillingManager.DiscountBilling;
                case "3":
                    return BillingManager.StandardBilling;
                case "4":
                    return BillingManager.EmergencyBilling;
                case "5":
                    return BillingManager.SeniorCitizenBilling;
                case "6":
                    return BillingManager.GovernmentSchemeBilling;
                case "7":
                    return BillingManager.CorporateBilling;
                default:
                    Console.WriteLine("Invalid choice. Using Standard Billing.");
                    return BillingManager.StandardBilling;
            }
        }

        // Helper method to get strategy name
        static string GetStrategyName(BillingStrategy strategy)
        {
            if (strategy == BillingManager.InsuranceBilling)
                return "Insurance Billing";
            else if (strategy == BillingManager.DiscountBilling)
                return "Discount Billing";
            else if (strategy == BillingManager.StandardBilling)
                return "Standard Billing";
            else if (strategy == BillingManager.EmergencyBilling)
                return "Emergency Billing";
            else if (strategy == BillingManager.SeniorCitizenBilling)
                return "Senior Citizen Billing";
            else if (strategy == BillingManager.GovernmentSchemeBilling)
                return "Government Scheme";
            else if (strategy == BillingManager.CorporateBilling)
                return "Corporate Billing";
            else
                return "Unknown Strategy";
        }
    }
}