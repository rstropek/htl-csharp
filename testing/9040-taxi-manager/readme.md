# Taxi Manager

## Einleitung

Bei diesem Test ist es Ihre Aufgabe, ein begonnenes C#-Projekt fertigzustellen. Inhaltlich geht es um einen *Taxi-Manager*, der folgende Aufgaben erfüllt:

* Verwaltung einer Liste an Taxis
* Verwaltung einer Liste an Taxifahrer (*Driver*)
* Verwaltung einer Liste von Fahrten (*Taxi Rides*)
  * Eine Fahrt kann aktiv (*ongoing*) sein; d.h. das Taxi ist gerade mit einem Gast unterwegs
  * Eine Fahrt kann abgeschlossen (*completed*) sein; d.h. die Fahrt hat einen Ende-Zeitpunkt und der Fahrtpreis (*charge*) ist bekannt

Technisch besteht das Projekt aus folgenden Teilen:

* Datenbankzugriff mit *Entity Framework Core*
* Web API mit *ASP.NET Core*
* *.NET Standard* Klassenbibliothek für das Datenmodell
* Benutzerschnittstelle mit *Windows Presentation Foundation (WPF)*
* Unit Tests mit *.NET Core* (vorgegeben, müssen nicht von Ihnen geschrieben werden)

> Sie können selbst während des Tests überprüfen, ob Ihr Code funktioniert und wie viele Punkte Sie erreicht haben. Nutzen Sie dafür die Unit Tests.

## Benotung

Grundsätzlich gibt es für jeden grünen Unit Test einen Punkt. Folgende Unit Tests sind Ausnahmen, für sie gibt es zwei Punkte wenn sie grün sind:

* `DataContextMethods.GetDriverStatistics`
* `WebApi.StartRide`
* `WebApi.EndRide`
* `ViewModel.Initialize`

Dadurch sind 35 Punkte erreichbar.

## Aufgabe 1: Datenbankzugriff

### Einleitung

Um Ihnen Zeit zu sparen, werden Ihnen die Datenmodellklassen fertig zur Verfügung gestellt. Sie finden diese in [*Model.cs*](TaxiManager/TaxiManager.Shared/Model.cs). Relevant für den Datenbankzugriff sind die folgenden Klassen aus dieser Datei:

* `Taxi`
* `Driver`
* `TaxiRide`

Ihre Aufgabe ist das Hinzufügen eines *Entity Framework Data Context* zum Projekt [*TaxiManager.WebApi*](TaxiManager/TaxiManager.WebApi). Das Projekt hat bereits alle für *Entity Framework Core* notwendigen NuGet-Pakete installiert. Auch ein rudimentärer, **jedoch nicht vollständiger** Data Context ist bereits in [*TaxiDataContext.cs*](TaxiManager/TaxiManager.WebApi/Data/TaxiDataContext.cs) vorhanden. Sie müssen ihn entsprechend der folgenden Spezifikation fertigstellen.

Falls Sie zum Testen Testdaten erzeugen wollen, steht Ihnen in [*DemoDataCreationScript.sql*](DemoDataCreationScript.sql) ein dafür vorgesehenes SQL-Script zur Verfügung.

### Spezifikation

1. Tragen Sie in [*appsettings.json*](TaxiManager/TaxiManager.WebApi/appsettings.json) einen SQL Server *Connection String* zu Ihrer SQL Server Datenbank ein. Damit die Unit Tests funktionieren, müssen Sie den *Connection String* **auch in den Unit Tests in [appsettings.json](TaxiManager/TaxiManager.Tests/appsettings.json) eintragen**.

1. Veröffentlichen Sie den *Data Context* mit Hilfe von *ASP.NET Core Dependency Injection* in der Methode `ConfigureServices` in [*Startup.cs*](TaxiManager/TaxiManager.WebApi/Startup.cs).

1. Sorgen Sie durch Erweiterung der Klasse `TaxiDataContext` ([TaxiDataContext.cs](TaxiManager/TaxiManager.WebApi/Data/TaxiDataContext.cs)) dafür, dass folgende Properties aus dem Datenmodell in der Datenbank nicht `NULL` sein dürfen:
    * `Taxi.LicensePlate`
    * `Driver.Name`
    * `TaxiRide.Start`

1. Sorgen Sie durch Erweiterung der Klasse `TaxiDataContext` dafür, dass folgende Relationen aus dem Datenmodell in der Datenbank nicht `NULL` sein dürfen:
    * `TaxiRide.Taxi`
    * `TaxiRide.Driver`

1. Erstellen Sie die *Migrations* mit der *Entity Framework Core CLI*. Erstellen Sie anschließend die SQL Server Datenbankstruktur ebenfalls mit der *Entity Framework Core CLI*.

1. Fügen Sie die fehlenden Implementierungen der Methoden in der Klasse `TaxiDataContext` ein (Methoden, die im Moment nur aus `throw new NotImplementedException();` bestehen). **Sie dürfen weder Name noch Parameter noch Rückgabetyp ändern**. Die genauen Anforderungen finden Sie als Kommentare in der bestehenden Codedatei.

## Aufgabe 2: Web API

### Einleitung

Um Ihnen Zeit zu sparen, wurde das Grundgerüst für das Web API Projekt ([*TaxiManager.WebApi*](TaxiManager/TaxiManager.WebApi)) bereits erstellt. Auch zwei Controller sind bereits fertig (`DriversController` zum Abfragen der Taxifahrer und `TaxisController` zum Abfragen der Taxis). Sie müssen den `RidesController` zum Verwalten der Taxifahren fertigstellen. Sie finden Ihn in [*RidesController.cs*](TaxiManager/TaxiManager.WebApi/Controllers/RidesController.cs).

### Spezifikation

1. Fügen Sie dem `RidesController` einen Konstruktor hinzu, mit dessen Hilfe Sie zu einem `TaxiDataContext` kommen (*Dependency Injection*).

1. Fügen Sie die fehlenden Implementierungen der Methoden in der Klasse `RidesController` ein (Methoden, die im Moment nur aus `throw new NotImplementedException();` bestehen). **Sie dürfen weder Name noch Parameter noch Rückgabetyp ändern**. Die genauen Anforderungen finden Sie als Kommentare in der bestehenden Codedatei.

## Aufgabe 3: Benutzerschnittstelle

### Einleitung

Um Ihnen Zeit zu sparen, steht Ihnen als Ausgangspunkt ein großer Teil der WPF Benutzerschnittstelle bereits zur Verfügung ([*TaxiManager.UI*](TaxiManager/TaxiManager.UI)). Sie müssen das View-Model `MainWindowViewModel` fertigstellen. Sie finden es in [*MainWindowViewModel.cs*](TaxiManager/TaxiManager.UI/MainWindowViewModel.cs).

### Spezifikation

1. Tragen Sie die URL Ihrer lokalen Web API in [*App.config*](TaxiManager/TaxiManager.UI/App.config) ein.

1. Implementieren Sie das .NET Interface `INotifyPropertyChanged` (wichtig für *Data Binding*) für die Klasse `MainWindowViewModel`. Sie dürfen dafür die Library `Prism` verwenden, die im Unterricht besprochen wurde. Sie ist bereits in das Projekt eingebunden.
    * **Hinweis:** Die bestehenden Property-Implementierungen in `MainWindowViewModel` müssen bei diesem Schritt angepasst werden!

1. Die Properties `Taxis`, `Drivers`, `OngoingRides` und `CompletedRides` sind im Moment Listen. Listen sind für WPF Data Binding ungeeignet. Ersetzen Sie die Listen durch den für Data Binding richtigen Collection-Datentyp.

1. Fügen Sie die fehlenden Implementierungen der Methoden in der Klasse `MainWindowViewModel` ein (Methoden, die im Moment nur aus `throw new NotImplementedException();` bestehen). **Sie dürfen weder Name noch Parameter noch Rückgabetyp ändern**. Die genauen Anforderungen finden Sie als Kommentare in der bestehenden Codedatei.
