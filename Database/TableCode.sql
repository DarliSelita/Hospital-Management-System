CREATE TABLE Patients
(
    Id INT PRIMARY KEY,
    Name VARCHAR(255),
    Surname VARCHAR(255),
    DateOfBirth DATE,
    FileNumber VARCHAR(255),
    PhoneNumber VARCHAR(20),
    Email VARCHAR(255),
    AssignedDoctor VARCHAR(255)
);

CREATE TABLE Appointments
(
    Id INT PRIMARY KEY AUTO_INCREMENT,
    PatientName VARCHAR(255) CHARACTER SET UTF8MB4 NOT NULL,
    PatientFileNumber VARCHAR(50) CHARACTER SET UTF8MB4 NOT NULL,
    ScheduleHour DATETIME NOT NULL,
    DoctorName VARCHAR(255) CHARACTER SET UTF8MB4 NOT NULL,
    Type INT NOT NULL
);

-- Billing Table
CREATE TABLE Billing
(
    BillingID INT PRIMARY KEY AUTO_INCREMENT,
    PatientID INT NOT NULL,
    PatientName VARCHAR(255) NOT NULL,
    BillingDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
    -- Add any additional columns as needed
);

-- Product Table
CREATE TABLE Product
(
    ProductID INT PRIMARY KEY AUTO_INCREMENT,
    BillingID INT NOT NULL,
    ServiceName VARCHAR(255) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL
    -- Add any additional columns as needed
);

-- Foreign Key Relationship
ALTER TABLE Product
ADD CONSTRAINT FK_Product_Billing
FOREIGN KEY (BillingID)
REFERENCES Billing(BillingID);

ALTER TABLE Billing
ADD COLUMN PatientFileNumber VARCHAR(255); -- Adjust the data type and size accordingly

select * from product,billing

-- Create PatientRecord table
CREATE TABLE PatientRecords (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    PatientFile VARCHAR(255) UNIQUE,
    PatientName VARCHAR(255),
    Gender VARCHAR(255),
    PhoneNumber VARCHAR(15),
    ReasonForAppointment TEXT,
    Allergies TEXT,
    MedicationsAndVaccines TEXT,
    TobaccoUse BOOLEAN,
    AlcoholConsumptionPerWeek DOUBLE,
    DrugUse BOOLEAN,
    DiagnosticId INT,  -- The foreign key column
    FOREIGN KEY (DiagnosticId) REFERENCES Diagnostic(Id)
);

-- Create ChronicIllness table
CREATE TABLE ChronicIllnesses (
    ChronicIllnessId INT AUTO_INCREMENT PRIMARY KEY,
    ChronicIllnessName VARCHAR(255),
    DiagnosticId INT,  -- The foreign key column
    FOREIGN KEY (DiagnosticId) REFERENCES Diagnostic(Id)
);

-- Create VitalSigns table
CREATE TABLE VitalSigns (
    VitalSignsId INT AUTO_INCREMENT PRIMARY KEY,
    BloodPressure INT,
    HeartRate INT,
    RespiratoryRate INT,
    Temperature FLOAT,
    Height FLOAT,
    Weight FLOAT,
    DiagnosticId INT,  -- The foreign key column
    FOREIGN KEY (DiagnosticId) REFERENCES PatientRecords(Id)
);


-- Create MedicalTest table
CREATE TABLE MedicalTest (
    TestId INT AUTO_INCREMENT PRIMARY KEY,
    TestName VARCHAR(255),
    TestDate DATETIME,
    FilePath VARCHAR(255),
    DiagnosticId INT,  -- The foreign key column
    TestFile BLOB,
    FOREIGN KEY (DiagnosticId) REFERENCES Diagnostic(Id)
);

INSERT INTO Medications (Name, Dosage, Frequency, Price)
VALUES ('New Medication', '10mg', 'Once a day', 19.99);



-- Create Diagnose table
CREATE TABLE Diagnoses (
    DiagnoseId INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(255),
    Description TEXT
);

-- Create PatientPrc table
CREATE TABLE PatientPrcs (
    PatientId INT PRIMARY KEY AUTO_INCREMENT,
    FirstName VARCHAR(255),
    LastName VARCHAR(255),
    DateOfBirth DATE,
    PhoneNumber VARCHAR(20),
    Email VARCHAR(255)
);

-- Create PatientDetails table
CREATE TABLE PatientDetails (
    PatientDetailsId INT PRIMARY KEY AUTO_INCREMENT,
    PatientId INT,
    Address VARCHAR(255),
    EmergencyContactName VARCHAR(255),
    EmergencyContactNumber VARCHAR(20),
    FOREIGN KEY (PatientId) REFERENCES PatientPrcs(PatientId) ON DELETE CASCADE
);

-- Create Medication table
CREATE TABLE Medication (
    MedicationId INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(255),
    Dosage VARCHAR(255),
    Frequency VARCHAR(255),
    Price DECIMAL(10, 2)
);

-- Create Prescription table
CREATE TABLE Prescriptions (
    PrescriptionId INT PRIMARY KEY AUTO_INCREMENT,
    PatientId INT,
    DiagnoseId INT,
    StartDate DATETIME,
    EndDate DATETIME,
    FOREIGN KEY (PatientId) REFERENCES PatientPrcs(PatientId) ON DELETE CASCADE,
    FOREIGN KEY (DiagnoseId) REFERENCES Diagnoses(DiagnoseId) ON DELETE CASCADE
);

-- Create PrescribedMedication table
CREATE TABLE PrescribedMedication (
    PrescribedMedicationId INT PRIMARY KEY AUTO_INCREMENT,
    PrescriptionId INT,
    MedicationId INT,
    Quantity INT,
    FOREIGN KEY (PrescriptionId) REFERENCES Prescriptions(PrescriptionId) ON DELETE CASCADE,
    FOREIGN KEY (MedicationId) REFERENCES Medication(MedicationId) ON DELETE CASCADE
);

-- Create DispensingRecord table
CREATE TABLE DispensingRecord (
    DispensingRecordId INT PRIMARY KEY AUTO_INCREMENT,
    PrescriptionId INT,
    QuantityDispensed INT,
    DispensingDate DATETIME,
    FOREIGN KEY (PrescriptionId) REFERENCES Prescriptions(PrescriptionId) ON DELETE CASCADE
);

-- Create MedicalInventories table
CREATE TABLE MedicalInventory (
    MedicalInventoryId INT PRIMARY KEY AUTO_INCREMENT,
    MedicationId INT,
    QuantityOnHand INT,
    ReorderLevel INT,
    SupplierInformation VARCHAR(255),
    ManufacturingDate DATETIME,
    ExpiryDate DATETIME,
    StorageLocation VARCHAR(255),
    FOREIGN KEY (MedicationId) REFERENCES Medication(MedicationId) ON DELETE CASCADE
);


 
-- Insert medications into MedicalInventories table
-- Insert medications into MedicalInventories table
INSERT INTO MedicalInventories (MedicationId, QuantityOnHand, ReorderLevel, SupplierInformation, ManufacturingDate, ExpiryDate, StorageLocation)
SELECT 
    MedicationId,
    1800 AS QuantityOnHand,
    CASE
        WHEN Price < 20 THEN 1
        WHEN Price < 30 THEN 2
        ELSE 3
    END AS ReorderLevel,
    CASE
        WHEN Price < 25 THEN 'MedPharma'
        ELSE 'CoPharma'
    END AS SupplierInformation,
    DATE_ADD(NOW(), INTERVAL -15 MONTH) AS ManufacturingDate,
    DATE_ADD(DATE_ADD(NOW(), INTERVAL 15 MONTH), INTERVAL 1 YEAR) AS ExpiryDate,
    CASE
        WHEN Price < 25 THEN 'Tirane'
        ELSE 'Fier'
    END AS StorageLocation
FROM Medications
WHERE Name IN (
    'Serenitol', 'ProximaXen', 'FlexiCalm', 'BioRegenix', 'EquiVitae', 'NeuroZen', 'PurityPlus',
    'OmniRelief', 'CardiaGuard', 'DuraFlex', 'VitaCure', 'ZenithFlow', 'LunaPulse', 'AlloHeal',
    'SolaceSoothe', 'VitaPro', 'Tranquilix', 'RapidRestore', 'NeoVital', 'ZephyrAid', 'MediTonic',
    'ResilientRx', 'HarmonyEase', 'EverGlow', 'QuantumWell', 'VitaFlare', 'HarmonyDose', 'BioBalance',
    'OptiViva', 'RadiantRevive', 'ZenithAura', 'AstraEase', 'PurityBlend', 'OptiRevive', 'ResilientBlend',
    'LunaElixir', 'SereneWave', 'NeuroHarmony', 'VitaSpark', 'TranquilBlend', 'HarmonyFlare', 'SolisDose',
    'QuantumWave', 'Meditide', 'HarmonyWave', 'EquiViva', 'OptiHeal', 'SolisAid', 'QuantumEase', 'BioVerve',
    'ZenithSpark', 'VitalAegis', 'SerenityPlus', 'LunaVitality', 'EquiPulse', 'NovaGlo', 'PureCalm',
    'VitaSoothe', 'RadiantGlow', 'ZenithRevitalize', 'UltraElixir', 'CoreRejuvenate', 'ZenithBlend',
    'VitaPulse', 'AstraZenith', 'OptiFlex', 'BioHarmony', 'NovaCure', 'DynaVibe', 'TranquilAura', 'NeuroBliss',
    'PinnacleRelief', 'QuantaWell', 'VitaHarmony', 'RadiantBlend', 'EverEase', 'ZephyrSoothe', 'OptiRelief',
    'NeuroPulse', 'TranquilRevive', 'BioRevitalize', 'NovaCalm', 'HarmonyVibe', 'QuantumRevive', 'VitaBliss',
    'LunaEase', 'ZenithRevitalize', 'SolaceBlend', 'EquiRevive', 'BioVitalize', 'OptiBliss', 'RadiantEase',
    'TranquilGlow'
);





CREATE TABLE Users (
    UserId INT PRIMARY KEY,
    Username VARCHAR(50) CHARACTER SET utf8mb4 NOT NULL,
    Password VARCHAR(50) CHARACTER SET utf8mb4 NOT NULL,
    RoleId INT NOT NULL
);

-- Insert Admin account
INSERT INTO Users (UserId, Username, Password, RoleId) VALUES (1, 'admin', 'admin', 1);

-- Insert Doctor account
INSERT INTO Users (UserId, Username, Password, RoleId) VALUES (2, 'doctor', '1234', 2);

-- Insert Receptionist account
INSERT INTO Users (UserId, Username, Password, RoleId) VALUES (3, 'receptionist', '1234', 3);

-- Insert Finance account
INSERT INTO Users (UserId, Username, Password, RoleId) VALUES (4, 'finance', '1234', 4);




INSERT INTO Diagnoses (Name, Description)
VALUES
  ('Common Cold', 'An upper respiratory tract infection caused by a virus.'),
  ('Hypertension', 'High blood pressure, a condition where the force of the blood against the artery walls is consistently too high.'),
  ('Type 2 Diabetes', 'A chronic condition that affects the way the body metabolizes sugar (glucose).'),
  ('Influenza', 'A contagious respiratory illness caused by influenza viruses.'),
  ('Migraine', 'A type of headache characterized by a throbbing or pulsating sensation, often accompanied by nausea and sensitivity to light.'),
  ('Asthma', 'A chronic respiratory condition that causes difficulty in breathing due to inflammation and narrowing of the airways.'),
  ('Osteoarthritis', 'A degenerative joint disease characterized by the breakdown of joint cartilage and underlying bone.'),
  ('Anxiety Disorder', 'A mental health condition characterized by excessive worry, fear, or apprehension.'),
  ('Gastritis', 'Inflammation of the lining of the stomach, which can cause stomach pain and discomfort.'),
  ('Coronary Artery Disease', 'A condition where the blood vessels supplying the heart muscle become narrowed or blocked.'),
  ('Depression', 'A mood disorder that causes persistent feelings of sadness and loss of interest in activities.'),
  ('Rheumatoid Arthritis', 'An autoimmune disorder that affects the joints, causing pain, swelling, and stiffness.'),
  ('Celiac Disease', 'An immune reaction to eating gluten, a protein found in wheat, barley, and rye.'),
  ('Urinary Tract Infection (UTI)', 'An infection in any part of the urinary system, including the kidneys, bladder, and urethra.'),
  ('Osteoporosis', 'A condition characterized by the weakening of bones, making them fragile and more prone to fractures.'),
  ('Sleep Apnea', 'A sleep disorder characterized by pauses in breathing or periods of shallow breathing during sleep.'),
  ('Chronic Obstructive Pulmonary Disease (COPD)', 'A group of lung diseases that block airflow and make it difficult to breathe.'),
  ('Eczema', 'A chronic skin condition characterized by inflammation, itching, and redness.'),
  ('Gastroesophageal Reflux Disease (GERD)', 'A digestive disorder that occurs when stomach acid frequently flows back into the esophagus.'),
  ('Hepatitis B', 'A viral infection that attacks the liver and can cause both acute and chronic diseases.'),
  ('Allergies', 'An exaggerated response of the immune system to substances that are generally not harmful.'),
  ('Hypothyroidism', 'A condition where the thyroid gland does not produce enough thyroid hormones.'),
  ('Gout', 'A form of arthritis characterized by sudden, severe attacks of pain, redness, and tenderness in the joints.'),
  ('Psoriasis', 'A chronic skin condition characterized by red, itchy, and scaly patches.'),
  ('Multiple Sclerosis', 'A disease in which the immune system attacks the protective covering of nerve fibers.'),
  ('HIV/AIDS', 'A viral infection that attacks the immune system and can lead to acquired immunodeficiency syndrome (AIDS).'),
  ('Cataracts', 'Clouding of the lens of the eye that can cause vision impairment.'),
  ('Fibromyalgia', 'A disorder characterized by widespread musculoskeletal pain, fatigue, and tenderness in localized areas.'),
  ('Hyperthyroidism', 'A condition where the thyroid gland produces too much thyroid hormone.'),
  ('Epilepsy', 'A neurological disorder characterized by recurrent seizures or convulsions.'),
  ('Pancreatitis', 'Inflammation of the pancreas that can cause abdominal pain and digestive problems.'),
  ('Melanoma', 'A type of skin cancer that develops from pigment-producing cells called melanocytes.'),
  ('Atrial Fibrillation', 'An irregular and often rapid heart rate that can lead to stroke and other heart-related complications.'),
  ('Cirrhosis', 'Scarring of the liver tissue often caused by long-term liver damage.'),
  ('Endometriosis', 'A painful disorder in which tissue similar to the lining of the uterus grows outside the uterus.'),
  ('Glaucoma', 'A group of eye conditions that can cause vision loss by damaging the optic nerve.'),
  ('Crohn\'s Disease', 'An inflammatory bowel disease that causes inflammation of the digestive tract.'),
  ('Hemorrhoids', 'Swollen veins in the rectum and anus that can cause discomfort and bleeding.'),
  ('Chronic Kidney Disease', 'A gradual loss of kidney function over time.'),
  ('Schizophrenia', 'A mental disorder characterized by distorted thinking, hallucinations, and altered perceptions.'),
  ('Tuberculosis', 'A bacterial infection that primarily affects the lungs.'),
  ('Scoliosis', 'An abnormal curvature of the spine.'),
  ('Myasthenia Gravis', 'A neuromuscular disorder that causes weakness and fatigue of voluntary muscles.'),
  ('Polycystic Ovary Syndrome (PCOS)', 'A hormonal disorder common among women of reproductive age.'),
  ('Achilles Tendinitis', 'Inflammation of the Achilles tendon, often resulting from overuse or injury.'),
  ('Hemophilia', 'A genetic disorder that impairs the body\'s ability to control blood clotting.'),
  ('Diverticulitis', 'Inflammation or infection of small pouches (diverticula) that can form in the walls of the colon.'),
  ('Restless Legs Syndrome', 'A neurological disorder characterized by an irresistible urge to move the legs.'),
  ('Ovarian Cancer', 'Cancer that begins in the ovaries, the female reproductive organs.'),
  ('Peripheral Neuropathy', 'Damage to the peripheral nerves, often causing weakness, numbness, and pain.'),
  ('Aortic Aneurysm', 'A bulge or ballooning in the wall of the aorta, the body\'s largest artery.'),
  ('Addison\'s Disease', 'A disorder that occurs when the adrenal glands do not produce enough hormones.'),
  ('Interstitial Cystitis', 'A chronic condition characterized by bladder pain and pressure.'),
  ('Bell\'s Palsy', 'A sudden, temporary weakness or paralysis of the muscles on one side of the face.'),
  ('Polio', 'A highly infectious viral disease that can lead to paralysis.'),
  ('Huntington\'s Disease', 'A genetic disorder that causes progressive deterioration of nerve cells in the brain.'),
  ('Obstructive Sleep Apnea', 'A sleep disorder characterized by repeated interruptions in breathing during sleep.'),
  ('Pneumonia', 'Inflammation of the air sacs in the lungs, often caused by infection.'),
  ('Systemic Lupus Erythematosus', 'An autoimmune disease that can affect various parts of the body, including the joints, skin, and organs.'),
  ('Osteoarthritis', 'A degenerative joint disease that occurs when the cartilage between joints wears down over time.'),
  ('Amyotrophic Lateral Sclerosis (ALS)', 'A progressive neurodegenerative disease that affects nerve cells in the brain and spinal cord.'),
  ('Chronic Obstructive Pulmonary Disease (COPD)', 'A group of lung diseases that block airflow and make it difficult to breathe.'),
  ('Rheumatoid Arthritis', 'An autoimmune disorder that causes chronic inflammation of the joints.'),
  ('Migraine', 'A type of headache characterized by severe pain, nausea, and sensitivity to light and sound.'),
  ('Anxiety Disorder', 'A mental health condition characterized by excessive worry and fear.'),
  ('Asthma', 'A chronic respiratory condition that causes inflammation and narrowing of the airways.'),
  ('Lupus Nephritis', 'Inflammation of the kidneys caused by systemic lupus erythematosus (SLE).'),
  ('Gastroesophageal Reflux Disease (GERD)', 'A chronic condition where stomach acid flows back into the esophagus, causing irritation.'),
  ('Peripheral Artery Disease (PAD)', 'A circulatory condition that causes reduced blood flow to the limbs.'),
  ('Anemia', 'A condition characterized by a deficiency of red blood cells or hemoglobin in the blood.'),
  ('Bipolar Disorder', 'A mental health condition characterized by extreme mood swings, including episodes of mania and depression.'),
  ('Carpal Tunnel Syndrome', 'A condition that causes numbness, tingling, and weakness in the hand due to pressure on the median nerve.'),
  ('Polycystic Kidney Disease (PKD)', 'A genetic disorder that causes fluid-filled cysts to form in the kidneys.'),
  ('Osteoporosis', 'A condition characterized by weakened bones that are more susceptible to fractures.'),
  ('Fibrous Dysplasia', 'A bone disorder in which fibrous tissue replaces normal bone, leading to deformity and fractures.'),
  ('Panic Disorder', 'A type of anxiety disorder characterized by sudden and repeated attacks of intense fear.'),
  ('Aplastic Anemia', 'A rare condition in which the bone marrow fails to produce enough blood cells.'),
  ('Temporal Arteritis', 'Inflammation of the large blood vessels near the temples, causing headaches and vision problems.'),
  ('Tourette Syndrome', 'A neurological disorder characterized by repetitive, involuntary movements and vocalizations.'),
  ('Interstitial Lung Disease', 'A group of lung disorders that cause inflammation and scarring of lung tissue.'),
  ('Hepatitis C', 'A viral infection that can lead to liver damage and inflammation.'),
  ('Hypertension', 'High blood pressure, a condition that can lead to heart disease and other complications.'),
  ('Eczema', 'A skin condition characterized by red, itchy, and inflamed skin.'),
  ('Chronic Fatigue Syndrome', 'A disorder characterized by persistent, unexplained fatigue that doesn\'t improve with rest.'),
  ('Paget\'s Disease of Bone', 'A chronic disorder that results in enlarged and misshapen bones.'),
  ('Herniated Disc', 'A condition in which the cushion-like discs between spinal vertebrae rupture or bulge.'),
  ('Lyme Disease', 'An infectious disease caused by ticks carrying the bacterium Borrelia burgdorferi.'),
  ('Mitral Valve Prolapse', 'A heart valve disorder in which the valve between the left atrium and left ventricle doesn\'t close properly.'),
  ('Raynaud\'s Disease', 'A condition that affects blood flow to certain parts of the body, usually the fingers and toes.'),
  ('Gallstones', 'Hardened deposits that form in the gallbladder, causing pain and other symptoms.'),
  ('Sjögren\'s Syndrome', 'An autoimmune disorder that affects moisture-producing glands, leading to dry eyes and mouth.'),
  ('Diverticulosis', 'The presence of small pouches (diverticula) in the wall of the colon.'),
  ('Sarcoidosis', 'A condition that causes inflammation in various organs, often affecting the lungs and lymph nodes.'),
  ('Chlamydia Infection', 'A common sexually transmitted infection caused by the bacterium Chlamydia trachomatis.'),
  ('Wrist Fracture', 'A break or crack in the bones of the wrist.'),
  ('Gastritis', 'Inflammation of the stomach lining, often causing pain and discomfort.'),
  ('Obsessive-Compulsive Disorder (OCD)', 'A mental health condition characterized by persistent, intrusive thoughts and repetitive behaviors.'),
  ('Cluster Headache', 'A type of headache that occurs in clusters, causing severe pain on one side of the head.'),
  ('Shingles', 'A viral infection that causes a painful rash, often with blisters, along a nerve pathway.');




INSERT INTO Medications (Name, Dosage, Frequency, Price)
VALUES
  ('Serenitol', '10mg', 'Once a day', 19.99),
  ('ProximaXen', '20mg', 'Twice a day', 29.99),
  ('FlexiCalm', '15mg', 'Once a day', 24.99),
  ('BioRegenix', '25mg', 'Three times a day', 34.99),
  ('EquiVitae', '30mg', 'Once a day', 39.99),
  ('NeuroZen', '5mg', 'Once a day', 14.99),
  ('PurityPlus', '40mg', 'Twice a day', 44.99),
  ('OmniRelief', '10mg', 'Once a day', 19.99),
  ('CardiaGuard', '15mg', 'Once a day', 24.99),
  ('DuraFlex', '25mg', 'Twice a day', 34.99),
  ('VitaCure', '12.5mg', 'Once a day', 22.99),
  ('ZenithFlow', '30mg', 'Once a day', 39.99),
  ('LunaPulse', '10mg', 'Once a day', 19.99),
  ('AlloHeal', '15mg', 'Once a day', 24.99),
  ('SolaceSoothe', '20mg', 'Twice a day', 29.99),
  ('VitaPro', '25mg', 'Three times a day', 34.99),
  ('Tranquilix', '15mg', 'Once a day', 24.99),
  ('RapidRestore', '30mg', 'Once a day', 39.99),
  ('NeoVital', '10mg', 'Once a day', 19.99),
  ('ZephyrAid', '20mg', 'Twice a day', 29.99),
  ('MediTonic', '15mg', 'Once a day', 24.99),
  ('ResilientRx', '25mg', 'Three times a day', 34.99),
  ('HarmonyEase', '30mg', 'Once a day', 39.99),
  ('EverGlow', '5mg', 'Once a day', 14.99),
  ('QuantumWell', '40mg', 'Twice a day', 44.99),
  ('VitaFlare', '10mg', 'Once a day', 19.99),
  ('HarmonyDose', '15mg', 'Once a day', 24.99),
  ('BioBalance', '25mg', 'Three times a day', 34.99),
  ('OptiViva', '30mg', 'Once a day', 39.99),
  ('RadiantRevive', '12.5mg', 'Once a day', 22.99),
  ('ZenovaSync', '30mg', 'Once a day', 39.99),
  ('UltraElixir', '10mg', 'Once a day', 19.99),
  ('CoreRejuvenate', '15mg', 'Once a day', 24.99),
  ('ZenithBlend', '20mg', 'Twice a day', 29.99),
  ('VitaPulse', '25mg', 'Three times a day', 34.99),
  ('AstraZenith', '15mg', 'Once a day', 24.99),
  ('OptiFlex', '30mg', 'Once a day', 39.99),
  ('BioHarmony', '10mg', 'Once a day', 19.99),
  ('NovaCure', '20mg', 'Twice a day', 29.99),
  ('DynaVibe', '15mg', 'Once a day', 24.99),
  ('TranquilAura', '25mg', 'Three times a day', 34.99),
  ('NeuroBliss', '30mg', 'Once a day', 39.99),
  ('PinnacleRelief', '5mg', 'Once a day', 14.99),
  ('QuantaWell', '40mg', 'Twice a day', 44.99),
  ('Meditide', '10mg', 'Once a day', 19.99),
  ('HarmonyWave', '15mg', 'Once a day', 24.99),
  ('EquiViva', '25mg', 'Three times a day', 34.99),
  ('OptiHeal', '30mg', 'Once a day', 39.99),
  ('SolisAid', '12.5mg', 'Once a day', 22.99),
  ('QuantumEase', '30mg', 'Once a day', 39.99),
  ('BioVerve', '10mg', 'Once a day', 19.99),
  ('ZenithSpark', '15mg', 'Once a day', 24.99),
  ('VitalAegis', '20mg', 'Twice a day', 29.99),
  ('SerenityPlus', '25mg', 'Three times a day', 34.99),
  ('LunaVitality', '15mg', 'Once a day', 24.99),
  ('EquiPulse', '30mg', 'Once a day', 39.99),
  ('NovaGlo', '10mg', 'Once a day', 19.99),
  ('PureCalm', '15mg', 'Once a day', 24.99),
  ('VitaSoothe', '25mg', 'Three times a day', 34.99),
  ('RadiantGlow', '30mg', 'Once a day', 39.99),
  ('ZenithAura', '5mg', 'Once a day', 14.99),
  ('AstraEase', '40mg', 'Twice a day', 44.99),
  ('PurityBlend', '10mg', 'Once a day', 19.99),
  ('OptiRevive', '15mg', 'Once a day', 24.99),
  ('ResilientBlend', '25mg', 'Three times a day', 34.99),
  ('LunaElixir', '30mg', 'Once a day', 39.99),
  ('SereneWave', '12.5mg', 'Once a day', 22.99),
  ('NeuroHarmony', '30mg', 'Once a day', 39.99),
  ('VitaSpark', '10mg', 'Once a day', 19.99),
  ('TranquilBlend', '15mg', 'Once a day', 24.99),
  ('HarmonyFlare', '20mg', 'Twice a day', 29.99),
  ('SolisDose', '25mg', 'Three times a day', 34.99),
  ('QuantumWave', '15mg', 'Once a day', 24.99),
  ('MediTonic', '30mg', 'Once a day', 39.99),
  ('BioVitality', '10mg', 'Once a day', 19.99),
  ('LunaFlex', '15mg', 'Once a day', 24.99),
  ('ZenovaVibe', '25mg', 'Three times a day', 34.99),
  ('CorePulse', '30mg', 'Once a day', 39.99),
  ('PinnacleCalm', '5mg', 'Once a day', 14.99),
  ('EquiAura', '40mg', 'Twice a day', 44.99),
  ('VitaHarmony', '10mg', 'Once a day', 19.99),
  ('RadiantBlend', '15mg', 'Once a day', 24.99),
  ('EverEase', '25mg', 'Three times a day', 34.99),
  ('ZephyrSoothe', '30mg', 'Once a day', 39.99),
  ('OptiRelief', '12.5mg', 'Once a day', 22.99),
  ('NeuroPulse', '30mg', 'Once a day', 39.99),
  ('TranquilRevive', '10mg', 'Once a day', 19.99),
  ('BioRevitalize', '15mg', 'Once a day', 24.99),
  ('NovaCalm', '20mg', 'Twice a day', 29.99),
  ('HarmonyVibe', '25mg', 'Three times a day', 34.99),
  ('QuantumRevive', '15mg', 'Once a day', 24.99),
  ('VitaBliss', '30mg', 'Once a day', 39.99),
  ('LunaEase', '10mg', 'Once a day', 19.99),
  ('ZenithRevitalize', '15mg', 'Once a day', 24.99),
  ('SolaceBlend', '25mg', 'Three times a day', 34.99),
  ('EquiRevive', '30mg', 'Once a day', 39.99),
  ('BioVitalize', '12.5mg', 'Once a day', 22.99),
  ('OptiBliss', '30mg', 'Once a day', 39.99),
  ('RadiantEase', '10mg', 'Once a day', 19.99),
  ('TranquilGlow', '15mg', 'Once a day', 24.99);



















