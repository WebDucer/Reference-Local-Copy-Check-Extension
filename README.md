# VSIX: Lokale Kopie Einstellung Überprüfung

## Beschreibung
Die Visual Studio Erweiterung prüft die Lokale Kopie Einstellung von allen Verweisen der Projekte in einer Projektmappe. Der aktuelle Zustand kann für den späteren Vergleich und Widerherstellung gespeichert werden.

Source-Code und Issue-Tracker sind bei [Bitbucket](https://bitbucket.org/webducertutorials/reference-local-copy-check-extension) zu finden.

Folgende Features sind bereits über die Erweiterung abgedeckt:
- Auflistung aller Verweise (C# Projekte) der Projektmappe
- Filterung nach Verweisen, die den "Lokale Kopie" Attribut gesetzt / nicht gesetzt haben
- Speichern eines Projektmappen-Zustandes als Referenz
- Vergleich des aktuellen Zustanden mit der gespeicherten Referenz (Anzeige der Konflikte)
- Bearbeitung des Attributs direkt über die Erweiterung
- Wiederherstellen des Attributzustandes aus der gespeicherten Referenz-Einstellung

## Problem, das zu dieser Erweiterung führte

Bei unseren modularen Projekten werden die DLLs zum Teil in Unterordner gebildet. Die über Nuget eingebundenen Bibliotheken sollen dabei immer nur in das Wurzelverzeichnis kopiert werden, da es sonst zur Laufzeit zu Problemen führen kann, wenn einen DLL sowohl im Wurzelverzeichnis, als auch in einen der Unterverzeichnisse liegt.

Stellt man den "Lokale Kopie"-Attribut bei der Verweisen auf "false" bei Projekten, die in Unterverzeichnisse gebildet werden, funktioniert alles einwandfrei. Wenn man die Bibliotheken über Nuget aktualisiert, wird der Attribut immer auf "true" gesetzt, unabhängig davon, was vorher für diese Bibliothek im Projekt definiert war. Mit dieser Erweiterung können wir nun den gewünschten Zustand des Attributes nach einer Aktualisierung wiederherstellen.
