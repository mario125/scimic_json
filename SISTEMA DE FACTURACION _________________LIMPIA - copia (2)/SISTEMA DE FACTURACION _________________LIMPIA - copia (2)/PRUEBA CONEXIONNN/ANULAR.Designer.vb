<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ANULAR
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtticket = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.numanul = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.iddoc = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.serie = New System.Windows.Forms.TextBox()
        Me.motivo = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.secuencia = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.TXTHASHCDR = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.TXTHASHCPE = New System.Windows.Forms.TextBox()
        Me.TXT_MSJ_SUNAT = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TXTCOD_SUNAT = New System.Windows.Forms.TextBox()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtticket)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Button2)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.numanul)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 133)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(396, 72)
        Me.GroupBox3.TabIndex = 53
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "CONSULTAR"
        '
        'txtticket
        '
        Me.txtticket.Location = New System.Drawing.Point(152, 16)
        Me.txtticket.Name = "txtticket"
        Me.txtticket.Size = New System.Drawing.Size(160, 20)
        Me.txtticket.TabIndex = 36
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(74, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 35
        Me.Label1.Text = "NRO TICKET:"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(318, 16)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(68, 43)
        Me.Button2.TabIndex = 37
        Me.Button2.Text = "Consulta Ticket"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(5, 39)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(144, 13)
        Me.Label6.TabIndex = 47
        Me.Label6.Text = "SERIE FECHA-SECUENCIA:"
        '
        'numanul
        '
        Me.numanul.Location = New System.Drawing.Point(152, 39)
        Me.numanul.Name = "numanul"
        Me.numanul.Size = New System.Drawing.Size(160, 20)
        Me.numanul.TabIndex = 48
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ComboBox1)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.iddoc)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.serie)
        Me.GroupBox1.Controls.Add(Me.motivo)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Controls.Add(Me.secuencia)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(396, 96)
        Me.GroupBox1.TabIndex = 52
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "ANULAR FACTURA"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"FACTURA", "BOLETA"})
        Me.ComboBox1.Location = New System.Drawing.Point(239, 16)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(73, 21)
        Me.ComboBox1.TabIndex = 48
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(198, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 13)
        Me.Label3.TabIndex = 47
        Me.Label3.Text = "TIPO:"
        '
        'iddoc
        '
        Me.iddoc.Location = New System.Drawing.Point(106, 19)
        Me.iddoc.Name = "iddoc"
        Me.iddoc.Size = New System.Drawing.Size(86, 20)
        Me.iddoc.TabIndex = 39
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 13)
        Me.Label4.TabIndex = 44
        Me.Label4.Text = "ID DOCUMENTO:"
        '
        'serie
        '
        Me.serie.Location = New System.Drawing.Point(106, 39)
        Me.serie.Name = "serie"
        Me.serie.Size = New System.Drawing.Size(129, 20)
        Me.serie.TabIndex = 40
        '
        'motivo
        '
        Me.motivo.Location = New System.Drawing.Point(106, 60)
        Me.motivo.Name = "motivo"
        Me.motivo.Size = New System.Drawing.Size(206, 20)
        Me.motivo.TabIndex = 45
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(49, 62)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 46
        Me.Label5.Text = "MOTIVO:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(21, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 13)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "SERIE FECHA:"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(318, 19)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(68, 61)
        Me.Button3.TabIndex = 38
        Me.Button3.Text = "ENVIAR ANULACION"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'secuencia
        '
        Me.secuencia.Location = New System.Drawing.Point(240, 39)
        Me.secuencia.Name = "secuencia"
        Me.secuencia.Size = New System.Drawing.Size(71, 20)
        Me.secuencia.TabIndex = 42
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.TXTHASHCDR)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.TXTHASHCPE)
        Me.GroupBox2.Controls.Add(Me.TXT_MSJ_SUNAT)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.TXTCOD_SUNAT)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 211)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(396, 88)
        Me.GroupBox2.TabIndex = 51
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Respuesta Sunat"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(4, 65)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(51, 13)
        Me.Label16.TabIndex = 33
        Me.Label16.Text = "Hash Cdr"
        '
        'TXTHASHCDR
        '
        Me.TXTHASHCDR.Location = New System.Drawing.Point(64, 61)
        Me.TXTHASHCDR.Name = "TXTHASHCDR"
        Me.TXTHASHCDR.Size = New System.Drawing.Size(321, 20)
        Me.TXTHASHCDR.TabIndex = 34
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(4, 44)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(54, 13)
        Me.Label15.TabIndex = 31
        Me.Label15.Text = "Hash Cpe"
        '
        'TXTHASHCPE
        '
        Me.TXTHASHCPE.Location = New System.Drawing.Point(64, 40)
        Me.TXTHASHCPE.Name = "TXTHASHCPE"
        Me.TXTHASHCPE.Size = New System.Drawing.Size(321, 20)
        Me.TXTHASHCPE.TabIndex = 32
        '
        'TXT_MSJ_SUNAT
        '
        Me.TXT_MSJ_SUNAT.Location = New System.Drawing.Point(103, 19)
        Me.TXT_MSJ_SUNAT.Name = "TXT_MSJ_SUNAT"
        Me.TXT_MSJ_SUNAT.Size = New System.Drawing.Size(282, 20)
        Me.TXT_MSJ_SUNAT.TabIndex = 30
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(4, 23)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(54, 13)
        Me.Label13.TabIndex = 28
        Me.Label13.Text = "Msj.Sunat"
        '
        'TXTCOD_SUNAT
        '
        Me.TXTCOD_SUNAT.Location = New System.Drawing.Point(64, 19)
        Me.TXTCOD_SUNAT.Name = "TXTCOD_SUNAT"
        Me.TXTCOD_SUNAT.Size = New System.Drawing.Size(37, 20)
        Me.TXTCOD_SUNAT.TabIndex = 29
        '
        'ANULAR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(417, 300)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Name = "ANULAR"
        Me.Text = "ANULAR"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents txtticket As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents numanul As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents iddoc As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents serie As TextBox
    Friend WithEvents motivo As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents secuencia As TextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label16 As Label
    Friend WithEvents TXTHASHCDR As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents TXTHASHCPE As TextBox
    Friend WithEvents TXT_MSJ_SUNAT As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents TXTCOD_SUNAT As TextBox
End Class
