Cinema Ticket App
Temat aplikacji
Aplikacja desktopowa typu WinForms służąca do zarządzania sprzedażą biletów do kina:
umożliwia przeglądanie repertuaru, zakładanie kont, logowanie, rezerwację biletów oraz zarządzanie filmami przez administratora.

Użyte technologie
Język programowania: C# (.NET Framework / .NET 6+)

Framework: Windows Forms (WinForms)

Serializacja danych: System.Text.Json

Pliki danych: movies.json, reservations.json

IDE: Visual Studio

Lista funkcjonalności
Rejestracja użytkownika – tworzenie nowego konta użytkownika.

Logowanie użytkownika – logowanie na konto użytkownika lub administratora.

Wyświetlanie repertuaru – pokazuje aktualnie dostępne filmy z pliku movies.json.

Rezerwacja biletów – użytkownicy mogą tworzyć nowe rezerwacje na wybrany film.

Wyświetlanie własnych rezerwacji – lista wszystkich rezerwacji zalogowanego użytkownika z opcją usunięcia.

Zmiana hasła – możliwość zmiany hasła przez zalogowanego użytkownika.

Zarządzanie repertuarem (tylko dla administratora) – możliwość dodawania i usuwania filmów z repertuaru.

Wylogowanie – bezpieczne zakończenie sesji.

Zamknięcie aplikacji – przycisk umożliwiający zamknięcie aplikacji.

Opis architektury
Aplikacja jest podzielona zgodnie z zasadami programowania obiektowego (OOP):

Encapsulation (Enkapsulacja)
Każda klasa przechowuje własne dane i odpowiedzialność – np. User, TicketReservation, Movie, ReservationManager.

Abstraction (Abstrakcja)
Użytkownik końcowy widzi tylko interfejs graficzny i funkcjonalność, nie widzi implementacji w tle (np. operacji na plikach JSON).

Inheritance (Dziedziczenie)
Formularze (Form) dziedziczą po klasie bazowej System.Windows.Forms.Form.

Polymorphism (Polimorfizm)
Przykładowo zdarzenia kliknięcia przycisków są polimorficznie przypisane do różnych metod obsługi.

Główne komponenty:
Form1.cs – główna forma aplikacji, obsługuje interfejs użytkownika.

LoginForm.cs / RegisterForm.cs / ChangePasswordForm.cs – osobne okna do obsługi operacji użytkownika.

ManageRepertoireForm.cs – zarządzanie filmami (administrator).

ShowRepertoireForm.cs – wyświetlanie aktualnego repertuaru.

TicketReservation.cs – logika tworzenia i usuwania rezerwacji.

Movie.cs – reprezentacja obiektowa filmu.

User.cs / UserManager.cs – zarządzanie użytkownikami.


Diagram klas UML:

Form1
├── SetLoggedInUser(user: User)
├── AddReservation(reservation: TicketReservation)
├── Logout()
├── Przyciski:
│   ├── loginButton
│   ├── registerButton
│   ├── createReservationButton
│   ├── viewReservationsButton
│   ├── changePasswordButton
│   ├── logoutButton
│   ├── manageRepertoireButton
│   └── closeButton

User
├── Username: string
├── Password: string
├── Role: string ("User" lub "Admin")

UserManager
├── AddUser(user: User)
├── Authenticate(username: string, password: string)

Movie
├── Title: string
├── Genre: string
├── Description: string
├── ScreeningDate: DateTime

TicketReservation
├── MovieTitle: string
├── CustomerName: string
├── SeatNumber: int
├── ReservationDate: DateTime
├── CreateReservation(customerName: string)
├── DeleteReservation(customerName: string)

ReservationManager
├── AddReservation(reservation: TicketReservation)
├── GetReservations(): List<TicketReservation>
