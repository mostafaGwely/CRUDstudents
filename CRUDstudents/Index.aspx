<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="CRUDstudents.Index" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Students</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/DataTables/css/jquery.dataTables.css" rel="stylesheet" />


    <script src="Scripts/jquery-3.0.0.js"></script>
    <script src="Scripts/DataTables/jquery.dataTables.js"></script>


    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            $("#studentsTable").DataTable();

            $("body").on("click", "#btnGet", function () {
                $.ajax({
                    url: "StudentsWebService.asmx/GetStudents",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "POST",
                    success: function (data) {
                        console.log(data);
                        alert(data);
                    },
                    error: function (x, y, z) {
                        alert(x.responseText + "  " + x.status);
                        console.log(x.responseText + "  " + x.status);
                    }
                });
            });

            $("body").on("click", "#btnDelete", function () {

                var favorite = [];
                $.each($("input[name='checkBox']:checked"),
                    function () {
                        favorite.push((this.id).replace("checkboxForId", ""));
                    });

                alert(favorite);

                var data;
                if (favorite.length == 1) {
                    data = favorite[0];
                } else {
                    data = favorite.join(',');
                }

                $.ajax({
                    url: "StudentsWebService.asmx/DelStudents",
                    contentType: "application/json; charset=utf-8",
                    type: "POST",
                    data: JSON.stringify({ Ids: data }),
                    success: function (data) {
                        console.log(data);
                        alert(data);
                    },
                    error: function (x, y, z) {
                        alert(x.responseText + "  " + x.status);
                        console.log(x.responseText + "  " + x.status);
                    }
                });
            });

            $("body").on("click", "#btnAdd", function () {

                var data = {
                    Name: $("#txbName").val(),
                    Gender: $("#txbGender").val(),
                    TotalMarks: $("#txbTotalMarks").val(),
                };

                $.ajax({
                    url: "StudentsWebService.asmx/AddStudent",
                    contentType: "application/json; charset=utf-8",
                    type: "POST",
                    data: JSON.stringify({ student: data }),
                    success: function (data) {
                        console.log(data);
                        alert(data);
                    },
                    error: function (x, y, z) {
                        alert(x.responseText + "  " + x.status);
                        console.log(x.responseText + "  " + x.status);
                    }
                });
            });

            $("body").on("click", "#btnUpdate", function () {

                var fff = {
                    student: {
                        Name: $("#txbName").val(),
                        Gender: $("#txbGender").val(),
                        TotalMarks: Number($("#txbTotalMarks").val()),
                        ID:0
                    },
                    Name: $("#txbName").val(),
                    Gender: $("#txbGender").val(),
                    TotalMarks: Number($("#txbTotalMarks").val()),
                    ID: 0
            };

                var Id = 0;

                $.each($("input[name='checkBox']:checked"),
                    function () {
                        Id = Number((this.id).replace("checkboxForId", ""));
                    });
                if (!Number.isInteger(Id)) {
                    alert("please select just one Id");
                } else {

                    fff.ID = Id;
                    fff.student.ID = Id;

                    $.ajax({
                        url: "StudentsWebService.asmx/UpdateStudent",
                        contentType: "application/json; charset=utf-8",
                        type: "POST",
                        data: JSON.stringify(fff),
                        success: function (data) {
                            console.log(data);
                            alert(data);
                        },
                        error: function (x, y, z) {
                            alert(x.responseText + "  " + x.status);
                            console.log(x.responseText + "  " + x.status);
                        }
                    });
                }

            });


            $("body").on("change", ".checkbox", function() {
                if (this.checked) {
                    $.ajax({
                        url: "StudentsWebService.asmx/GetStudentById",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data:JSON.stringify({ Id: (this.id).replace("checkboxForId", "") }),
                        type: "POST",
                        success: function (data) {
                            $("#txbName").val(data["d"].Name);
                            $("#txbGender").val(data["d"].Gender);
                            $("#txbTotalMarks").val(data["d"].TotalMarks);
                            console.log(data);

                        },
                        error: function (x, y, z) {
                            alert(x.responseText + "  " + x.status);
                            console.log(x.responseText + "  " + x.status);
                        }
                    });


                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:Label ID="Label1" runat="server" CssClass="border" Text=""></asp:Label>


                <div>

                    <br />

                    <div id="controls" class="border">
                        <div id="form">

                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txbName" CssClass="form-control" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="lblGender" runat="server" Text="Gender"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txbGender" CssClass="form-control" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="lblTotalMarks" runat="server" Text="TotalMarks"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txbTotalMarks" CssClass="form-control" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>

                            <br />

                        </div>

                        <div>
                            <input type="submit" runat="server" value="Get" id="btnGet" class="btn btn-dark" />
                            <input type="submit" runat="server" value="Delete" id="btnDelete" class="btn btn-dark" />
                            <input type="submit" runat="server" value="Add" id="btnAdd"  class="btn btn-dark" />
                            <input type="submit" runat="server" value="Update" id="btnUpdate"  class="btn btn-dark" />

                        </div>

                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <h2><% Response.Write(DateTime.Now); %></h2>

        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    </form>

</body>
</html>

