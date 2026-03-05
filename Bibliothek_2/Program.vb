Imports System
Imports System.IO

Module Program
    Sub Main(args As String())

        'ABgeänderte Variante von Bibliothek_1:
        'Einlesen der bereitgestellten csv-Dateien mit Datensätzen für Bücher und
        'Anwender.Diese sollen die Testdaten aus Teil I ersetzen.
        'c) Speicherung dieser Daten in einem sinnvollen Datentyp während der
        'Programmausführung.Eine Speicherung der Daten über das Ende des
        'Programms hinaus ist nicht notwendig.
        'd) Implementierung einer Routine, um neue Benutzer anhand von Eingabedaten des
        'Anwenders anzulegen.
        'e) Definition und Berücksichtigung eines Limits von 999 hinterlegten Benutzern.
        'f) Anpassung der Funktionalität aus Teil I, alle hinterlegten Bücher auszugeben.

        'g) Anpassung der Funktionalität aus Teil I, alle hinterlegten Benutzer auszugeben.
        'h) Implementierung einer Routine, um anhand einer eingegebenen Nutzer-ID sowie
        'ISBN ein Buch auszuleihen, sofern dieses verfügbar ist.
        'i) Implementierung einer Routine, um anhand einer eingegebenen Nutzer-ID sowie
        'ISBN ein Buch zurückzugeben, sofern dieses ausgeliehen ist.
        'j) Dokumentation des Codes wo nötig sowie aller Funktionen und Subs mittels der
        'Header-Templates im Anhang von Teil I.
        'k) Erfüllung aller funktionalen und technischen Anforderungen.

        'Einlesen der bereitgestellten csv-Dateien in ein dynamisches Array mit Datensätzen für Bücher und Anwender
        Dim libraryData As String = File.ReadAllText("C:\Users\julia\source\repos\Informatik\Bibliothek\Bibliothek_2\library_books.csv")

        'Testdaten für hinterlegte Benutzer

        Dim usrData As String = File.ReadAllText("C:\Users\julia\source\repos\Informatik\Bibliothek\Bibliothek_2\library_users.csv")

        'Willkommenstext und Menüoptionen anzeigen

        Console.WriteLine("Willkommen zum Bibliothekssystem!")
        Console.WriteLine("Bitte wählen Sie eine Aktion aus:")
        Console.WriteLine("")

        'Menüoptionen anzeigen mit einheitlicher Formatierung

        Console.WriteLine("(1) Neuen Benutzer anlegen")
        Console.WriteLine("(2) Alle Bücher anzeigen")
        Console.WriteLine("(3) Alle Benutzer anzeigen")
        Console.WriteLine("(4) Buch ausleihen")
        Console.WriteLine("(5) Buch zurückgeben")
        Console.WriteLine("(6) Ausgeliehene Bücher eines Benutzers anzeigen")
        Console.WriteLine("(7) Programm schließen")
        Console.WriteLine("")
        'Schleife, um das Menü erneut anzuzeigen, bis der Benutzer das Programm schließt
        Dim exitProgram As Boolean = False
        While exitProgram = False


            'Benutzereingabe einlesen und gewäglte Aktion ausführen

            Dim auswahl As String = Console.ReadLine()
            Select Case auswahl
                Case "1"
                    Console.WriteLine("Neuen Benutzer anlegen ausgewählt.")
                'Hier könnte der Code zum Anlegen eines neuen Benutzers eingefügt werden
                Case "2"
                    Console.WriteLine("BÜCHER BROWSER")
                    'Alle Bücher mit Isbn und Titel aus String ausgeben
                    Dim lines As String() = libraryData.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
                    For Each line As String In lines
                        Dim fields As String() = line.Split(","c)
                        Dim isbn As String = fields(0)
                        Dim title As String = fields(1)
                        Console.WriteLine("ISBN: " & isbn & " | Titel: " & title)
                    Next


                Case "3"
                    Console.WriteLine("ALLE NUTZER.")
                    'Alle Benutzer mit Id und Name aus der oben ausgelesenen Datei ausgeben
                    Dim usrLines As String() = usrData.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
                    For Each line As String In usrLines
                        Dim fields As String() = line.Split(","c)
                        Dim id As String = fields(0)
                        Dim name As String = fields(1)
                        Console.WriteLine("ID: " & id & " | Name: " & name)
                    Next


                Case "4"
                    Console.WriteLine("Buch ausleihen ausgewählt.")
                'Hier könnte der Code zum Ausleihen eines Buches eingefügt werden
                Case "5"
                    Console.WriteLine("Buch zurückgeben ausgewählt.")
                'Hier könnte der Code zum Zurückgeben eines Buches eingefügt werden
                Case "6"
                    Console.WriteLine("Ausgeliehene Bücher eines Benutzers anzeigen ausgewählt.")
                'Hier könnte der Code zum Anzeigen ausgeliehener Bücher eines Benutzers eingefügt werden
                Case "7"
                    Console.WriteLine("Programm wird geschlossen. Auf Wiedersehen!")
                    exitProgram = True

                    'Absicherung vor ungültiger Eingabe
                Case Else
                    Console.WriteLine("Ungültige Auswahl. Bitte wählen Sie eine gültige Option aus.")
            End Select
        End While

    End Sub
End Module

