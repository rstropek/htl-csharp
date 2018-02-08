# Aufgabe 2

* Anzahl Punkte: 17
* Kalkulierte Zeit: 40 Minuten

## Einleitung

### Aufgabe

Ihre Aufgabe ist die Entwicklung eines plattformunabhängigen Kommandozeilentools **mit *.NET Core*** zum Loggen von Daten aus einem (simulierten) Temperatursensor in eine neue **SQL Server Datenbank**. Ihr Programm muss die Temperatur **einmal pro Sekunde** abfragen und in die DB schreiben. Wenn die Temperatur kleiner als -10 Grad oder größer als 25 Grad ist, muss in eine eigene Tabelle eine **Warnung** (*Alert*) geschrieben werden.

> Falls Sie keinen SQL Server lokal installiert haben, installieren Sie bitte während der Vorbereitungszeit [*LocalDB*](install-localdb.md). *LocalDB* ist kostenlos.

### Temperatursensor

Während des Tests steht uns kein echter Temperatursensor zur Verfügung. Kopieren Sie daher folgenden Code in die Lösung. Er enthält eine Klasse, die einen Temperatursensor simuliert, mit dem man die aktuelle Temperatur mit der Methode `TemperatureLogger.GetCurrentTemperatureAsync` abfragen kann.

```
using System;
using System.Threading.Tasks;

// Note: This file is given for the exam; students do not have to code this.

namespace TemperatureLogger
{
    /// <summary>
    /// Represents a temperature sensor
    /// </summary>
    public class TemperatureSensor
    {
        private Random random = new Random();

        /// <summary>
        /// Get current temperature asynchronously
        /// </summary>
        /// <returns>
        /// Task whose result represents the current temperature
        /// </returns>
        public async Task<double> GetCurrentTemperatureAsync()
        {
            await Task.Delay(10);
            return random.NextDouble() * 60d - 20d;
        }
    }
}
```

### Zieldatenbank

Die Zieldatenbank muss folgenden Anforderungen entsprechen:

* Legen Sie eine **eigene Tabelle/Klasse für Warnungen** (*Alerts*) an, in der ein Text für die Warnung (*Too Low* oder *Too High*) und eine eindeutige ID für die Warnung gespeichert sind.
* Jede Warnung enthält einen Verweis zum Log-Datensatz der Temperatur, der die Warnung ausgelöst hat.

Grafisch dargestellt sieht die Struktur der Zieldatenbank wie folgt aus (Tabellennamen sind Vorschläge, können aber auch anders gewählt werden):

![ERD](ERD.svg)


## Anforderungen

Beachten Sie beim Lösen der Aufgabe folgende Anforderungen.

### Pflichtaufgaben (9 Punkte)

Pflichtaufgaben, die alle korrekt gelöst werden müssen, um Punkte für das Beispiel zu erhalten:

* .NET Core Kommandozeilenanwendung
* Entity Framework Model-Klassen, die der geforderten Datenbankstruktur entsprechen
* Entity Framework *Data Context* zum Zugriff auf die Zieldatenbank
* Generierte Entity Framework *Migration* oder *SQL Script* zum initialen Anlegen der Datenbank
* Sekündliches Schreiben der Temperatur in die Tabelle *TemperatureReadings* **mit oder ohne Warnungen**. D.h. die Pflichtaufgabe gilt auch als gelöst, falls das Speichern von Warnungen nicht gemacht wurde oder nicht funktioniert.

### Optionale Aufgaben

Optionale Aufgaben, um die volle Punktzahl für das Beispiel zu erhalten:

* 3 Punkte: Korrektes Befüllen der Warnungs-Tabelle inkl. Verweis von der Warnungs- auf die Temperatur-Tabelle 
* 2 Punkte: Mindestens ein Unit-Test, der einen Aspekt des Datenzugriffs mit Hilfe des [*InMemory Providers* von *Entity Framework*](https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory) testet
* 2 Punkte: Korrektes, durchgängiges Verwenden von `Task` und/oder `async`/`await` 
* 1 Punkt: Guter Programmierstil (z.B. *readme.md*, *.gitignore*, keine unnötigen Dateien eingecheckt, etc.) und effizienter Algorithmus (z.B. keine unnötigen DB-Operationen)
