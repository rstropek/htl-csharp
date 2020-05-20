# WPF Prüfung

## Einleitung

Sie arbeiten in einem Schraubenhandel. Kunden können Schrauben in 100er Packungen oder nach Gewicht kaufen. Schreiben Sie ein Programm, das die Anzahl an Packungen, die ein Kunde kauft, berechnet.

## Testdaten

| Schraubentyp | kg pro 100 Schrauben | Preis pro 100 Schrauben |
| ------------ | -------------------- | ----------------------- |
| M4, 6mm      | 0.133                | 1,35€                   |
| M4, 8mm      | 0.149                | 1,40€                   |
| M5, 6mm      | 0.218                | 1,65€                   |
| M5, 8mm      | 0.238                | 1,80€                   |

## Spezifikation

* Benutzer können Bestellpositionen zu einer Bestellliste (Hauptspeicher, Speichern in Datei oder DB ist *nicht* notwendig) hinzufügen. Dabei geben Sie den Schraubentyp und die Menge an. Die Menge kann auf zwei verschiedene Arten eingegeben werden:
  * Eingabe Anzahl 100er Packungen
  * Eingabe kg

* Eingegebene kg müssen entsprechend der oben dargestellten Tabelle in Anzahl Packungen umgerechnet werden.
  
* Runden Sie eingegebene oder errechnete Packungsanzahl kaufmännisch (aufrunden bei `>= .5`, abrunden bei `< .5`).

* Das Programm muss den Inhalt der Liste darstellen.

* Das Programm muss den Preis jeder hinzugefügten Bestellposition berechnen und diesen ebenfalls in der Liste darstellen.

* Das Programm muss die Summe des Preises aller Bestellpositionen in der Liste berechnen und darstellen.

* Verwenden Sie C# und WPF (.NET Core).

## Bewertungskriterien

* Negativ bewertet werden Anwendungen,...
  * ...deren Code wegen Syntaxfehlern nicht kompiliert werden kann.
  * ...die nach Start sofort abstürzen ohne irgendeine sinnvolle Benutzerinteraktion zu erlauben.
  * ...deren Sourcecode nicht ordnungsgemäß über GitHub abgegeben wird (ZIP-Dateien werden *nicht* akzeptiert, Einchecken von unnötigen Ordnern wie *bin* oder *obj* führt zu Punkteabzug).

* Um positiv bewertet zu werden, muss die Anwendung mindestens folgende Funktionen korrekt ausführen können:
  * Hinzufügen der Bestellpositionen zu einer Liste im Hauptspeicher
  * Eingabe der geforderten Parameter je Bestellposition
  * Darstellen des Inhalts der Liste

* Folgende Kriterien werden bei der Benotung berücksichtigt:
  * Korrekte Umrechnung von kg in Packungen
  * Ausgliedern der Berechnungs- und View-Logik in eigene Klassen
  * Korrekte Verwendung von *Data Binding*
  * Ergonomische Gestaltung der Benutzeroberfläche (visuelles Design ist zweitrangig, Bedienbarkeit und Aussagekraft sind wichtiger)

## Testdaten mit Referenzergebnissen

| Schraubentyp | Eingegebene Anzahl Packungen |   kg | Berechnete Anzahl Packungen | Preis |
| ------------ | ---------------------------: | ---: | --------------------------: | ----: |
| M4, 6mm      |                            3 |  N/A |                         N/A | 4,05€ |
| M4, 8mm      |                          N/A | 0,75 |                           5 | 7,00€ |
| M5, 6mm      |                          N/A | 0,86 |                           4 | 6,60€ |

Summe: 17,65€
