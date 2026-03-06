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

    Private Sub BorrowBook(ByRef libraryData() As String, userData() As String)
        Console.WriteLine("Buch ausleihen ausgewählt.")
        'Pfad zur Bibliotheksdatei ermitteln
        Dim libraryPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "library_books.csv")
        Dim borrowRecordsPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "borrowed_records.csv")

        'ISBN abfragen
        Console.WriteLine("Bitte geben Sie die ISBN des Buches ein, das Sie ausleihen möchten:")
        Dim inputIsbn As String = Console.ReadLine().Trim()

        'Benutzer-ID abfragen
        Console.WriteLine("Bitte geben Sie die Benutzer-ID ein (z. B. U001):")
        Dim inputUserId As String = Console.ReadLine().Trim()

        'Benutzer prüfen
        Dim userFound As Boolean = False
        For Each uline As String In userData
            Dim ufields As String() = uline.Split(","c)
            If ufields.Length >= 1 AndAlso ufields(0).Trim().ToLower() = inputUserId.ToLower() Then
                userFound = True
                Exit For
            End If
        Next
        If Not userFound Then
            Console.WriteLine($"Benutzer mit ID '{inputUserId}' wurde nicht gefunden.")
            Console.WriteLine("")
            Console.WriteLine("Um zum Menü zu kommen geben sie 'menu' ein.")
            Return
        End If

        'Buch suchen und Status prüfen
        Dim found As Boolean = False
        For i As Integer = 0 To libraryData.Length - 1
            Dim fields As String() = libraryData(i).Split(","c)
            If fields.Length >= 4 Then
                Dim isbn As String = fields(0).Trim()
                Dim status As String = fields(3).Trim().ToLower()
                If isbn = inputIsbn Then
                    found = True
                    If status <> "available" Then
                        Console.WriteLine("Das Buch mit der angegebenen ISBN ist nicht verfügbar.")
                        Console.WriteLine("")
                        Console.WriteLine("Um zum Menü zu kommen geben sie 'menu' ein.")
                        Return
                    End If

                    'Alle Prüfungen bestanden - Ausleihe durchführen
                    fields(3) = "borrowed"
                    libraryData(i) = String.Join(",", fields)

                    'Bibliotheksdatei aktualisieren
                    File.WriteAllLines(libraryPath, libraryData)

                    'Borrow-Record speichern (BenutzerID,ISBN)
                    If Not File.Exists(borrowRecordsPath) Then
                        File.WriteAllText(borrowRecordsPath, "UserId,ISBN")
                    End If
                    File.AppendAllText(borrowRecordsPath, Environment.NewLine & inputUserId & "," & inputIsbn)

                    Console.WriteLine($"Ausleihe erfolgreich: Benutzer {inputUserId} hat Buch {inputIsbn} ausgeliehen.")
                    Console.WriteLine("")
                    Console.WriteLine("Um zum Menü zu kommen geben sie 'menu' ein.")
                    Return
                End If
            End If
        Next

        If Not found Then
            Console.WriteLine("Keine Übereinstimmung für die angegebene ISBN gefunden.")
            Console.WriteLine("")
            Console.WriteLine("Um zum Menü zu kommen geben sie 'menu' ein.")
        End If
    End Sub

    Private Sub ReturnBook()
        Console.WriteLine("Buch zurückgeben ausgewählt.")
        Dim libraryPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "library_books.csv")
        Dim borrowRecordsPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "borrowed_records.csv")

        Console.WriteLine("Bitte geben Sie die ISBN des zurückzugebenden Buches ein:")
        Dim inputIsbn As String = Console.ReadLine().Trim()

        Dim libraryData() As String = File.ReadAllLines(libraryPath)
        Dim found As Boolean = False
        For i As Integer = 0 To libraryData.Length - 1
            Dim fields As String() = libraryData(i).Split(","c)
            If fields.Length >= 4 Then
                Dim isbn As String = fields(0).Trim()
                Dim status As String = fields(3).Trim().ToLower()
                If isbn = inputIsbn Then
                    found = True
                    If status = "available" Then
                        Console.WriteLine("Das Buch ist bereits als verfügbar markiert.")
                        Console.WriteLine("")
                        Console.WriteLine("Um zum Menü zu kommen geben sie 'menu' ein.")
                        Return
                    End If

                    'Status zurücksetzen
                    fields(3) = "available"
                    libraryData(i) = String.Join(",", fields)
                    File.WriteAllLines(libraryPath, libraryData)

                    'Borrow-Record entfernen (alle Einträge mit dieser ISBN)
                    If File.Exists(borrowRecordsPath) Then
                        Dim records As New List(Of String)(File.ReadAllLines(borrowRecordsPath))
                        'Erste Zeile ist Header, behalten
                        Dim header As String = records(0)
                        Dim newRecords As New List(Of String)
                        newRecords.Add(header)
                        For j As Integer = 1 To records.Count - 1
                            Dim parts As String() = records(j).Split(","c)
                            If parts.Length >= 2 Then
                                If parts(1).Trim() <> inputIsbn Then
                                    newRecords.Add(records(j))
                                End If
                            End If
                        Next
                        File.WriteAllLines(borrowRecordsPath, newRecords)
                    End If

                    Console.WriteLine($"Rückgabe erfolgreich: ISBN {inputIsbn} wurde zurückgegeben.")
                    Console.WriteLine("")
                    Console.WriteLine("Um zum Menü zu kommen geben sie 'menu' ein.")
                    Return
                End If
            End If
        Next

        If Not found Then
            Console.WriteLine("Keine Übereinstimmung für die angegebene ISBN gefunden.")
            Console.WriteLine("")
            Console.WriteLine("Um zum Menü zu kommen geben sie 'menu' ein.")
        End If
    End Sub

    Private Sub ShowBorrowedBooks()
        Console.WriteLine("Ausgeliehene Bücher eines Benutzers anzeigen ausgewählt.")
        Dim borrowRecordsPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "borrowed_records.csv")
        Dim libraryPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "library_books.csv")

        If Not File.Exists(borrowRecordsPath) Then
            Console.WriteLine("Es sind keine Ausleihen gespeichert.")
            Console.WriteLine("")
            Console.WriteLine("Um zum Menü zu kommen geben sie 'menu' ein.")
            Return
        End If

        Console.WriteLine("Bitte geben Sie die Benutzer-ID ein (z. B. U001):")
        Dim inputUserId As String = Console.ReadLine().Trim()

        Dim records As String() = File.ReadAllLines(borrowRecordsPath)
        Dim borrowedIsbns As New List(Of String)
        For i As Integer = 1 To records.Length - 1
            Dim parts As String() = records(i).Split(","c)
            If parts.Length >= 2 Then
                If parts(0).Trim().ToLower() = inputUserId.ToLower() Then
                    borrowedIsbns.Add(parts(1).Trim())
                End If
            End If
        Next

        If borrowedIsbns.Count = 0 Then
            Console.WriteLine($"Benutzer {inputUserId} hat keine ausgeliehenen Bücher.")
            Console.WriteLine("")
            Console.WriteLine("Um zum Menü zu kommen geben sie 'menu' ein.")
            Return
        End If

        'Titel aus Bibliotheksdatei nachschlagen
        Dim libraryData() As String = File.ReadAllLines(libraryPath)
        Console.WriteLine($"Ausgeliehene Bücher von {inputUserId}:")
        For Each isbn In borrowedIsbns
            Dim foundTitle As String = "(Titel nicht gefunden)"
            For Each line As String In libraryData
                Dim f As String() = line.Split(","c)
                If f.Length >= 2 AndAlso f(0).Trim() = isbn Then
                    foundTitle = f(1).Trim()
                    Exit For
                End If
            Next
            Console.WriteLine($"ISBN: {isbn}, Titel: {foundTitle}")
        Next

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




