# Ćwiczenia 2 - Projekt obiektowy w C#

Mała aplikacja konsolowa w C# do ogarniania wypożyczeń sprzętu na uczelni.

## Architektura i decyzje projektowe

Głównym celem było uniknięcie wrzucania wszystkiego do jednego wora i tworzenia wielkich klas, które robią wszystko naraz i w których robi się totalny śmietnik. Dlatego podzieliłem kod na logiczne katalogi: `Models` (stan i dane), `Services` (logika biznesowa i operacje) oraz `Exceptions` (customowe błędy).

### 1. Odpowiedzialność klas (SRP)
Każda klasa zajmuje się konkretnie sprecyzowanymi wymaganiami:
* **`Program.cs`** robi tu tylko za interfejs.
* **`DefaultPenaltyCalculator`** zajmuje sie obliczaniem kar za nieoddanie rzeczy.
* **`ReportService`** tylko wyciąga dane z innych serwisów i wrzuca na konsole raport.

### 2. Kohezja
Starałem się trzymać funkcje blisko danych, na których operują. Dobrym przykładem jest klasa **`Rental`**. Zamiast robić w serwisie zewnętrzne metody do sprawdzania, czy sprzęt jest oddany na czas, dałem te właściwości (`IsRented`, `IsOverdue`) bezpośrednio do modelu `Rental`. Obiekt sam wie najlepiej, w jakim jest stanie.

### 3. Coupling
Żeby klasy nie były ze sobą na sztywno powiązane, użyłem wstrzykiwania zależności.
Dlatego np. `RentalService` nie tworzy sam z siebie list użytkowników ani sprzętu. Zamiast tego przyjmuje interfejsy przez konstruktor. Dzięki temu łatwo zapanować nad zależnościami i gdybym chciał podpiąć tu kiedyś prawdziwą bazę danych, `RentalService` nie wymagałby żadnych zmian.

### 4. Dziedziczenie i obsługa błędów
* Użyłem dziedziczenia (`User`, `Equipment`). Klasy `Student` i `Employee` dziedziczą po `User`, ale dzięki temu każdy typ sam definiuje swój limit wypożyczeń (`GetMaxActiveRentals()`).
* Walidacja i błędy: przy błedzie rzucam po prostu własne wyjątki.