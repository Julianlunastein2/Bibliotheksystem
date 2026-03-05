Imports System
Imports System.IO

Module Program
    Sub Main(args As String())

        'daten für hinterlegte Bücher aus in Projektmappe liegender Datei "library_books.csv" laden
        Dim libraryPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "library_books.csv")
        Dim libraryData() As String = File.ReadAllLines(libraryPath)

        'Testdaten für hinterlegte Benutzer
        Dim userPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "library_users.csv")
        Dim userData() As String = File.ReadAllLines(userPath)


        'Willkommenstext und Menüoptionen anzeigen
        Console.WriteLine("==========================================================================")
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
        Console.WriteLine("==========================================================================")

        'Schleife, um das Menü erneut anzuzeigen, bis der Benutzer das Programm schließt
        Dim exitProgram As Boolean = False
        While exitProgram = False


            'Benutzereingabe einlesen und entsprechende Aktion ausführen
            Dim auswahl As String = Console.ReadLine()
            Select Case auswahl
                Case "1"
                    CreateUser(userData, userPath)
                Case "2"
                    ShowAllBooks(libraryData)
                Case "3"
                    ShowAllUsers(userData)
                Case "4"
                    BorrowBook(libraryData, userData)
                Case "5"
                    ReturnBook()
                Case "6"
                    ShowBorrowedBooks()
                Case "7"
                    Console.WriteLine("Programm wird geschlossen. Auf Wiedersehen!")
                    exitProgram = True
                Case "menu"
                    ShowMenu()
                Case Else
                    'Absicherung vor ungültiger Eingabe
                    Console.WriteLine("Ungültige Auswahl. Bitte wählen Sie eine gültige Option aus.")
                    Console.WriteLine("")
            End Select
        End While

    End Sub

    Private Sub CreateUser(ByRef userData() As String, userPath As String)
        Console.WriteLine("Neuen Benutzer anlegen ausgewählt.")
        If userData.Length >= 999 Then
            Console.WriteLine("Maximale Anzahl von Benutzern erreicht. Es können keine weiteren Benutzer angelegt werden.")
            Console.WriteLine("")
            Console.WriteLine("Um zum Menü zu kommen geben sie 'menu' ein.")
        Else
            Console.WriteLine("Bitte geben Sie den Namen des neuen Benutzers ein:")
            Console.Write("Vorname Nachname")
            Console.WriteLine("")

            Dim Name As String = Console.ReadLine()

            Dim IDNumber As Integer = userData.Length
            Dim ID As String = IDNumber.ToString("D3") 'ID mit führenden Nullen formatieren

            File.AppendAllText(userPath, Environment.NewLine & "U" & ID & "," & Name)

            userData = File.ReadAllLines(userPath) 'Aktualisierte Benutzerdaten erneut einlesen, um die aktuelle Anzahl der Benutzer zu erhalten

            Console.WriteLine($"Neuer Benutzer '{Name}' mit ID 'U{ID}' wurde angelegt.")
            Console.WriteLine("")
            Console.WriteLine("Um zum Menü zu kommen geben sie 'menu' ein.")
        End If
    End Sub

    Private Sub ShowAllBooks(libraryData() As String)
        Console.WriteLine("BÜCHER BROWSER")
        'Alle Bücher mit Isbn und Titel aus vorher augelsener Datei ausgeben
        For Each line As String In libraryData
            Dim bookDetails As String() = line.Split(","c)
            Console.WriteLine($"ISBN: {bookDetails(0)}, Titel: {bookDetails(1)}")
        Next
        Console.WriteLine("")
        Console.WriteLine("Um zum Menü zu kommen geben sie 'menu' ein.")
    End Sub

    Private Sub ShowAllUsers(userData() As String)
        Console.WriteLine("ALLE NUTZER.")
        'Alle Benutzer mit Id und Name aus String ausgeben
        For Each user As String In userData
            Dim userDetails As String() = user.Split(","c)
            Console.WriteLine($"ID: {userDetails(0)}, Name: {userDetails(1)}")
        Next
        Console.WriteLine("")
        Console.WriteLine("Um zum Menü zu kommen geben sie 'menu' ein.")
    End Sub

    Private Sub BorrowBook(libraryData() As String, userData() As String)
        Console.WriteLine("Buch ausleihen ausgewählt.")
        'Implementierung einer Routine, um anhand einer eingegebenen Nutzer-ID sowie ISBN ein Buch auszuleihen, sofern dieses verfügbar ist.
        Console.WriteLine("Bitte geben Sie die ISBN des Buches ein, das Sie ausleihen möchten:")
        If Array.IndexOf(libraryData, Console.ReadLine()) = "unavailable" Then
            Console.WriteLine("Das Buch mit der angegebenen ISBN ist nicht verfügbar.")
            Console.WriteLine("")
            Console.WriteLine("Um zum Menü zu kommen geben sie 'menu' ein.")
        Else
            Console.WriteLine("Bitte geben Sie die ID des Benutzers ein, der ein Buch ausleihen möchte:")
            Dim userID As String = Console.ReadLine()
        End If

        Console.WriteLine("")
        Console.WriteLine("Um zum Menü zu kommen geben sie 'menu' ein.")
    End Sub

    Private Sub ReturnBook()
        Console.WriteLine("Buch zurückgeben ausgewählt.")
        'Hier könnte der Code zum Zurückgeben eines Buches eingefügt werden
        Console.WriteLine("")
        Console.WriteLine("Um zum Menü zu kommen geben sie 'menu' ein.")
    End Sub

    Private Sub ShowBorrowedBooks()
        Console.WriteLine("Ausgeliehene Bücher eines Benutzers anzeigen ausgewählt.")
        'Hier könnte der Code zum Anzeigen ausgeliehener Bücher eines Benutzers eingefügt werden
        Console.WriteLine("")
        Console.WriteLine("Um zum Menü zu kommen geben sie 'menu' ein.")
    End Sub

    Private Sub ShowMenu()
        Console.WriteLine("==========================================================================")
        Console.WriteLine("(1) Neuen Benutzer anlegen")
        Console.WriteLine("(2) Alle Bücher anzeigen")
        Console.WriteLine("(3) Alle Benutzer anzeigen")
        Console.WriteLine("(4) Buch ausleihen")
        Console.WriteLine("(5) Buch zurückgeben")
        Console.WriteLine("(6) Ausgeliehene Bücher eines Benutzers anzeigen")
        Console.WriteLine("(7) Programm schließen")
        Console.WriteLine("==========================================================================")
    End Sub

End Module




