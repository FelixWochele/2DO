# C# - Klausurersatzleistung

<img align="right" width="220" height="90" src="https://upload.wikimedia.org/wikipedia/de/thumb/1/1d/DHBW-Logo.svg/541px-DHBW-Logo.svg.png?20110626153129">

[![forthebadge made-with-java](https://img.shields.io/badge/MADE_WITH-JAVA-orange?style=for-the-badge)](https://java.com/)
[![forthebadge](https://img.shields.io/badge/BUILD_WITH-LOVE_%3C3-D20E0E?style=for-the-badge)](https://github.com/FelixWochele/Bierbock)

## Dokumentation

[Hier gehts zu der Dokumentation!](https://github.com/FelixWochele/2DO/blob/master/Dokumentation%20-%20Wochele%20Felix%20-%20TInf2020.pdf)

## Bei Problemen:

### SQL Light Fehler
Schaune ob die Packet SQLite und System.Data.SQLite in BEIDEN Projekten instaliert sind 

### Debuging funktioniert nicht
Beim Server in den Debug einstellungen Aktivieren

### Keine Verbindung zum WCF Service
Client und Server können nicht in einer gemeinsamen Debuging Instanz von VS laufen. 
In den einstellungen darauf achten, dass es unterschiedliche Instanzen sind.

### Fehlende Bilder 
Projekt neu clonen oder Bilder als "Build Action" : "None" und dann wieder "Ressource" oder "Content" makieren

## Git:

### clone:
```shell
git clone https://github.com/FelixWochele/2048.git
```
### commit & push:
```shell
git add * 
git commit -m "Commit-Message"
git push origin main
```
