
## Okruženje
- OS: Windows 10/11
- Preglednik: Google Chrome (zadnja verzija)
- URL: https://www.njuskalo.hr
- Datum testiranja: 19/01/2026
- Tester: Luka

## Pretpostavke
- Korisnik nije prijavljen
- Cookies banner se može pojaviti (prihvatiti ako je potrebno)

---

## TC-01 – Učitavanje početne stranice
**Prioritet:** High  
**Preduvjeti:** Nema  
**Koraci:**
1. Otvori https://www.njuskalo.hr  
2. Ako se pojavi cookies banner, klikni “Prihvati”  
**Očekivani rezultat:** Početna stranica se učita i vidljivo je polje za pretragu.

---

## TC-02 – Pretraga s validnim pojmom
**Prioritet:** High  
**Preduvjeti:** Početna stranica učitana  
**Test podaci:** `golf 7`  
**Koraci:**
1. Klikni u polje za pretragu  
2. Upiši `golf 7`  
3. Pritisni Enter  
**Očekivani rezultat:** Otvara se stranica rezultata pretrage.

---

## TC-03 – Pretraga s drugim validnim pojmom
**Prioritet:** Medium  
**Test podaci:** `iphone`  
**Koraci:** kao TC-02  
**Očekivani rezultat:** Otvara se stranica rezultata pretrage.

---

## TC-04 – Pretraga s praznim unosom
**Prioritet:** Medium  
**Koraci:**
1. Klikni u polje za pretragu  
2. Ne upisuj ništa  
3. Pritisni Enter  
**Očekivani rezultat:** Sustav ne smije “pasti”; ostaje na istoj stranici ili prikazuje poruku/validaciju.

---

## TC-05 – Pretraga s besmislenim pojmom
**Prioritet:** Medium  
**Test podaci:** `asdfqwerty123456___nope`  
**Koraci:** kao TC-02  
**Očekivani rezultat:** Sustav prikazuje stranicu rezultata (moguće “slični rezultati” ili “nema rezultata”), ali bez greške.

---

## TC-06 – Pretraga s jednim znakom (boundary)
**Prioritet:** Low  
**Test podaci:** `a`  
**Koraci:** kao TC-02  
**Očekivani rezultat:** Sustav obradi upit bez greške (moguće puno rezultata).

---

## TC-07 – Pretraga s posebnim znakovima
**Prioritet:** Low  
**Test podaci:** `!!!@@@###`  
**Koraci:** kao TC-02  
**Očekivani rezultat:** Sustav obradi upit bez rušenja (može vratiti rezultate ili sugestije).

---

## TC-08 – Otvaranje oglasa iz rezultata
**Prioritet:** High  
**Preduvjeti:** Stranica rezultata pretrage je otvorena  
**Koraci:**
1. Na rezultatima klikni prvi oglas  
**Očekivani rezultat:** Otvara se detalj oglasa (naslov oglasa vidljiv).

---

## TC-09 – Povratak s oglasa na rezultate
**Prioritet:** Medium  
**Preduvjeti:** Detalj oglasa otvoren  
**Koraci:**
1. Klikni “Back” u pregledniku  
**Očekivani rezultat:** Vraća se stranica rezultata.

---

## TC-10 – Scroll na rezultatima (lazy load / paginacija)
**Prioritet:** Low  
**Preduvjeti:** Stranica rezultata otvorena  
**Koraci:**
1. Skrolaj prema dnu stranice  
**Očekivani rezultat:** Stranica ostaje responzivna, sadržaj se učitava/ostaje vidljiv bez greške.
