TTPP Automation Framework  
### Automatsko testiranje web i API aplikacija (UI + Backend)

Ovaj repozitorij sadrži **studentski razvijen okvir (framework) za automatsko testiranje programske podrške**, izrađen u sklopu kolegija **Metode i tehnike testiranja programske podrške (MTTPP)**.

Projekt demonstrira **primjenu teorijskih i praktičnih znanja s laboratorijskih vježbi (LV1–LV6)** kroz jedinstven, modularan i proširiv testni framework.

---

## 🎯 Cilj projekta

Cilj projekta je:
- izraditi **vlastiti automation framework**
- primijeniti **različite vrste testiranja**
- koristiti **moderne alate i dobre prakse**
- omogućiti **automatizirano izvođenje testova lokalno i kroz CI**

Framework obuhvaća:
- **ručno testiranje**
- **automatizirano UI testiranje (Selenium)**
- **automatizirano API testiranje**
- **CI integraciju (GitHub Actions)**

---

## 🧪 Testirani sustavi

### 1️⃣ Web aplikacija (UI)
- **Sustav:** Njuškalo  
- **URL:** https://www.njuskalo.hr  
- **Testirane funkcionalnosti:**
  - početna stranica
  - pretraga oglasa
  - rezultati pretrage
  - filtriranje i sortiranje
  - stabilnost ponovljenih pretraga
  - login flow (demonstracijski)

### 2️⃣ REST API
- **Sustav:** Postman Echo API  
- **URL:** https://postman-echo.com  
- **Testirano:**
  - GET / POST zahtjevi
  - status kodovi
  - povratni payload
  - osnovna validacija odgovora

---

## 🧱 Korištene tehnologije i alati

- **.NET 8**
- **C#**
- **NUnit**
- **Selenium WebDriver**
- **REST API testiranje (HttpClient)**
- **Git & GitHub**
- **GitHub Actions (CI)**
- **Chrome WebDriver**

---

## 📂 Struktura projekta

MTTPP-Automation-Framework
│
├── MTTP.Framework.Tests # UI testovi (Selenium)
│ ├── Core # Base klase i helperi
│ ├── Pages # Page Object Model
│ ├── Tests # Smoke / Regression testovi
│ └── ManualTesting # Ručni testovi i dokumentacija
│
├── MTTP.Framework.ApiTests # API testovi (REST)
│ ├── Core
│ └── Tests
│
├── .github/workflows # CI pipeline (GitHub Actions)
├── .gitignore
├── MTTPP.Framework.sln
└── README.md


---

## 🧩 Primijenjene napredne tehnike

U projektu su implementirane sljedeće **napredne tehnike** (prema uputama kolegija):

✔ Page Object Model (POM)  
✔ Explicit Wait / WebDriverWait  
✔ OOP pristup (BaseTest, BasePage)  
✔ Kategorizacija testova (Smoke / Regression)  
✔ Odvajanje UI i API testova  
✔ CI integracija (GitHub Actions)  
✔ Automatski test reporti (TRX artifacts)  
✔ Ručno testiranje + dokumentacija  
✔ Kontrolirano isključivanje nestabilnih testova  

---

## 🧪 Kategorizacija testova

Testovi su označeni pomoću NUnit kategorija:

- **Smoke** – osnovna provjera kritičnih funkcionalnosti
- **Regression** – detaljniji testovi ponašanja sustava

### Pokretanje Smoke testova
```bash
dotnet test MTTP.Framework.Tests --filter TestCategory=Smoke

Pokretanje Regression testova

dotnet test MTTP.Framework.Tests --filter TestCategory=Regression

Pokretanje API testova

dotnet test MTTP.Framework.ApiTests

🔐 Login testovi – objašnjenje

Login funkcionalnost na produkcijskom Njuškalu:

    ima dinamičan UI

    koristi anti-bot mehanizme

    često mijenja selektore

Zbog toga su login testovi:

    implementirani demonstracijski

    isključeni po defaultu

    mogu se uključiti pomoću environment varijable

Uključivanje login testova

Windows (CMD):

set RUN_NJUSKALO_LOGIN=true
dotnet test MTTP.Framework.Tests --filter TestCategory=Regression

PowerShell:

$env:RUN_NJUSKALO_LOGIN="true"
dotnet test MTTP.Framework.Tests --filter TestCategory=Regression

Ovakav pristup demonstrira svjesno upravljanje nestabilnim testovima, što je standardna praksa u industriji.
🧾 Ručno testiranje (LV1)

U folderu ManualTesting nalaze se:

    TEST_CASES.md – ručno definirani testni slučajevi

    BUG_REPORTS.md – predložak za prijavu grešaka

    TEST_SUMMARY_REPORT.md – sažetak izvršenja testova

Ručni testovi su usklađeni s automatiziranim testovima, što omogućuje potpunu sljedivost.
⚙️ CI – GitHub Actions

Projekt koristi GitHub Actions CI pipeline koji:

    automatski pokreće testove na push i pull request

    odvaja UI i API testove u zasebne jobove

    sprema test reporte (.trx) kao artifacts

CI status je vidljiv unutar GitHub repozitorija.
✅ Zaključak

Ovaj projekt predstavlja:

    cjelovit primjer automatiziranog testiranja

    kombinaciju ručne i automatske provjere

    primjenu dobrih praksi iz industrije

    jasnu povezanost s laboratorijskim vježbama kolegija MTTPP

Framework je modularan, proširiv i spreman za daljnje nadogradnje.

Autor: Luka
Kolegij: Metode i tehnike testiranja programske podrške
Godina: 2025./2026.