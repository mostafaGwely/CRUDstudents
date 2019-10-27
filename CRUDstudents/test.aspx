<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="CRUDstudents.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-3.0.0.js"></script>

    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/DataTables/css/jquery.dataTables.css" rel="stylesheet" />
    <script src="Scripts/DataTables/jquery.dataTables.js"></script>


    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#studentsTable").DataTable();
            alert("adaaa");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="studentsTable" class="table" >
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">First</th>
                        <th scope="col">Last</th>
                        <th scope="col">Handle</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <th scope="row">1</th>
                        <td>Mark</td>
                        <td>Otto</td>
                        <td>@mdo</td>
                    </tr>
                    <tr>
                        <th scope="row">2</th>
                        <td>Jacob</td>
                        <td>Thornton</td>
                        <td>@fat</td>
                    </tr>
                    <tr>
                        <th scope="row">3</th>
                        <td>Larry</td>
                        <td>the Bird</td>
                        <td>@twitter</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
