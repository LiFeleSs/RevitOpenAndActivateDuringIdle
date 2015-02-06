﻿Imports RUI = Autodesk.Revit.UI
Imports RDB = Autodesk.Revit.DB

Namespace Commands

    <Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)>
    <Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)>
    <Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.NoCommandData)>
    Public Class OpenAFile
        Implements Autodesk.Revit.UI.IExternalCommand

        Public Function Execute(commandData As RUI.ExternalCommandData, ByRef message As String, elements As RDB.ElementSet) As RUI.Result Implements RUI.IExternalCommand.Execute
            Dim task = New System.Threading.Tasks.Task(
                Sub()
                    Dim sf = New UserInterface.SelectFile()
                    If Not sf.ShowDialog() Then Return
                    Try
                        commandData.Application.OpenAndActivateDocument(sf.FilePath.Text)
                    Catch ex As Exception
                        System.Diagnostics.Debugger.Break()
                    End Try

                End Sub
            )
            ExternalApplication.Singleton.ThingsToDoOnIdling.Enqueue(task)
            Return Autodesk.Revit.UI.Result.Succeeded
        End Function

    End Class
End Namespace

