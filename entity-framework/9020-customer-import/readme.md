# Aufgabe 2

* Anzahl Punkte: 18
* Kalkulierte Zeit: 40 Minuten

## Einleitung

### Aufgabe

Ihre Aufgabe ist die Entwicklung eines plattformunabhängigen Kommandozeilentools **mit *.NET Core*** zum Importieren von Daten aus der CSV-Datei [*customer-data.csv*](customer-data.csv) in eine neue **SQL Server Datenbank**.

> Falls Sie keinen SQL Server lokal installiert haben, installieren Sie bitte während der Vorbereitungszeit [*LocalDB*](install-localdb.md). *LocalDB* ist kostenlos.

### Importdatei

Die zu importierende CSV-Datei enthält eine Liste von Kundendatensätzen. Beispiel:

```
ID,FirstName,LastName,Birthday,Address.City,Address.Country
1,Claudia,Dorran,1968-04-13,Stari Grad,Croatia
2,Martguerita,Kearle,1988-02-05,,
...
7,Bradly,Bentham3,,Abeokuta,Nigeria
...
```

| Spalte | Datentyp | Anmerkung |
|---|---|---|
| ID | `int` | Aufsteigende Numerierung der Kunden beginnend mit 1, immer befüllt |
| FirstName | `string` | Immer befüllt |
| LastName | `string` | Immer befüllt |
| Birthday | `DateTime` | Format ISO 8601 ohne Zeitzone, **nicht** immer befüllt |
| Address.City | `string` | **Nicht** immer befüllt |
| Address.Country | `string` | **Nicht** immer befüllt |


* Die erste Zeile enthält die Spaltenüberschriften. Sie muss beim Import ignoriert werden.
* Die Datei verwendet das Encoding *UTF8*.
* Die Zeilen der Datei enden mit [*LF*](https://en.wikipedia.org/wiki/Newline) (`\n`).
* Am Ende der Datei ist eine leere Zeile.
* Sie können davon ausgehen, dass die CSV-Datei keine ungültigen Daten (z.B. Zeilen mit zu wenig Spalten, falsche Spaltenreihenfolge, falsches Datumsformat etc.) enthält.
* Sie können davon ausgehen, dass die Spalten immer in der gleichen Reihenfolge enthalten sind. Eine Prüfung der Spaltenreihenfolge anhand der ersten Zeile der Datei ist nicht notwendig.
* Sie können davon ausgehen, dass die Datei als ganzes in den Hauptspeicher geladen werden kann. Sie wird nie so groß sein, dass das nicht möglich wäre.

### Kommandozeilenargument

Das von Ihnen zu entwickelnde Kommandozeilentool erhält den **Pfad zu CSV-Datei**, die zu importieren ist, **als Kommandozeilenargument** (z.B. `import c:\temp\customers.csv`).

### Zieldatenbank

Die Zieldatenbank muss folgenden Anforderungen entsprechen:

* Legen Sie eine **eigene Tabelle/Klasse für Städte** an, in der der Name der Stadt (CSV-Spalte `Address.City`) und die Bezeichnung des Landes  (CSV-Spalte `Address.Country`) gespeichert sind.
* Jeder Kunde enthält einen Verweis zur Stadt des jeweiligen Kunden. Der Verweis ist `null` wenn zu dem Kunden keine Adresse bekannt ist.
* Jede **Stadt** darf **nur einmal** in die Tabelle eingetragen werden. Wenn mehrere Kunden zu einer Stadt gehören, müssen alle Kunden auf den einen *Stadt*-Datensatz verweisen.

Grafisch dargestellt sieht die Struktur der Zieldatenbank wie folgt aus (Tabellennamen sind Vorschläge, können aber auch anders gewählt werden):

![ERD](ERD.svg)


## Anforderungen

Beachten Sie beim Lösen der Aufgabe folgende Anforderungen.

### Pflichtaufgaben (9 Punkte)

Pflichtaufgaben, die alle korrekt gelöst werden müssen, um Punkte für das Beispiel zu erhalten:

* .NET Core Kommandozeilenanwendung, die den Kommandozeilenparameter auswertet (Pfad zur CSV-Datei)
* Wenn der Kommandozeilenparameter fehlt oder falsch ist (z. B. Datei existiert nicht), muss ein entsprechender Fehler ausgegeben werden. Eine unbehandelte Ausnahme ist nicht ausreichend.
* Entity Framework Model-Klassen, die der geforderten Datenbankstruktur entsprechen
* Entity Framework *Data Context* zum Zugriff auf die Zieldatenbank
* Generierte Entity Framework *Migration* zum initialen Anlegen der Datenbank
* Korrektes Importieren der Kundendaten **mit oder ohne Städte**. D.h. die Pflichtaufgabe gilt auch als gelöst, falls der Import der Städte nicht gemacht wurde oder nicht funktioniert.

### Optionale Aufgaben

Optionale Aufgaben, um die volle Punktzahl für das Beispiel zu erhalten:

* 3 Punkte: Korrektes Befüllen der Städte-Tabelle inkl. Verweis von der Kunden- auf die Städte-Tabelle 
* 2 Punkte: Mindestens ein Unit-Test, der den Import mit Hilfe des [*InMemory Providers* von *Entity Framework*](https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory) testet
* 2 Punkt: Verwenden von LINQ beim Verarbeiten der Eingabewerte aus der CSV-Datei
* 1 Punkt: Import-Logik ausgegliedert in eine **eigene *.NET Standard* Class Library**
* 1 Punkt: Guter Programmierstil (z.B. *readme.md*, *.gitignore*, keine unnötigen Dateien eingecheckt, etc.) und effizienter Algorithmus (z.B. keine unnötigen DB-Operationen)
