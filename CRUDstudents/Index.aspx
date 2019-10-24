﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="CRUDstudents.Index" %>

<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Students</title>
    <script src="Scripts/jquery-3.0.0.js"></script>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.js"></script>
    <script>
        $(document).ready(function () {
            $("#btnPost").click(function() {
                var favorite = [];
                $.each($("input[name='checkBox']:checked"),
                    function() {
                        favorite.push(this.id);
                    });
                alert("My favourite sports are: " + favorite.join(", "));
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>


        <div>
            <asp:Button ID="btnAdd" runat="server" Text="Add" />
            <asp:Button ID="btnRemove" runat="server" Text="Remove" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" />
        </div>


    </div>
    </form>
</body>
</html>

