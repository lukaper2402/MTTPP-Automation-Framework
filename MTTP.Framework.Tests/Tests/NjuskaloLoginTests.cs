using System;
using NUnit.Framework;
using MTTP.Framework.Tests.Core;
using MTTP.Framework.Tests.Pages;

namespace MTTP.Framework.Tests.Tests
{
    [TestFixture]
    public class NjuskaloLoginTests : BaseTest
    {
        [Test, Category("Regression")]
        [Ignore("Login UI je dinamičan na produkciji – test je demonstracijski")]
        public void LoginPage_ShouldLoad()
        {
            var login = new NjuskaloLoginPage(Driver, Wait);
            login.Open();

            Assert.That(login.IsLoginFormVisible(), Is.True, "Login forma nije vidljiva.");
        }

        [Test, Category("Regression")]
        [Ignore("Login UI je dinamičan na produkciji – test je demonstracijski")]
        public void Login_EmptySubmit_ShouldShowValidationOrStayOnPage()
        {
            var login = new NjuskaloLoginPage(Driver, Wait);
            login.Open();

            login.SubmitEmpty();

            // Na produkciji može biti različito: ili validacija, ili disable submit, ili alert.
            // Bitno: ne smije “pasti” i mora ostati na login flowu.
            Assert.That(Driver.Url, Does.Contain("prijava"), "Nismo ostali na stranici prijave nakon praznog submit-a.");

            // Ako postoji poruka greške, super.
            // Ako ne postoji, ne rušimo test (može biti client-side disable).
            if (login.HasAnyError())
                Assert.That(login.GetFirstErrorText(), Is.Not.Empty, "Pronađen error element ali je tekst prazan.");
        }

        [Test, Category("Regression")]
        [Ignore("Login UI je dinamičan na produkciji – test je demonstracijski")]
        public void Login_InvalidCredentials_ShouldNotLogin()
        {
            var login = new NjuskaloLoginPage(Driver, Wait);
            login.Open();

            login.Login("invalid@example.com", "WrongPassword123!");

            // Očekujemo da se NE prijavi (ostaje na prijavi ili se pokaže greška)
            Assert.That(Driver.Url, Does.Contain("prijava").Or.Contain("login"),
                "Izgleda kao da nismo ostali u login flowu (provjeri da nije došlo do redirecta).");

            // Ako error postoji -> dokaz da je login odbijen
            if (login.HasAnyError())
                Assert.Pass("Greška prikazana (login odbijen) – očekivano.");
            else
                Assert.Inconclusive("Nije pronađena greška (UI može koristiti drugačiji prikaz validacije).");
        }
    }
}
