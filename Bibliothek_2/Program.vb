Imports System

Module Program
    Sub Main(args As String())

        'Testdaten für hinterlegte Bücher

        Dim libraryTestData As String =
        "978-3-16-148410-0;Einführung in die Informatik;Müller;verfügbar|" &
        "978-0-13-110362-7;Programmieren mit VB.NET;Schneider;verfügbar|" &
        "978-3-540-69006-6;Grundlagen der Softwaretechnik;Meier;ausgeliehen|" &
        "978-3-642-05445-3;Datenstrukturen und Algorithmen;Klein;verfügbar"

        'Testdaten für hinterlegte Benutzer

        Dim usrTestData As String =
        "U001;Max Mustermann|" &
        "U002;Erika Musterfrau|" &
        "U003;Hans Meier|" &
        "U004;Laura Schmidt"


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
                    Dim books As String() = libraryTestData.Split("|"c)
                    For Each book As String In books
                        Dim bookDetails As String() = book.Split(";"c)
                        Console.WriteLine($"ISBN: {bookDetails(0)}, Titel: {bookDetails(1)}")
                    Next

                Case "3"
                    Console.WriteLine("ALLE NUTZER.")
                    'Alle Benutzer mit Id und Name aus String ausgeben
                    Dim users As String() = usrTestData.Split("|"c)
                    For Each user As String In users
                        Dim userDetails As String() = user.Split(";"c)
                        Console.WriteLine($"ID: {userDetails(0)}, Name: {userDetails(1)}")
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




