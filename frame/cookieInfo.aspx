<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cookieInfo.aspx.cs" Inherits="frame_cookieInfo" %>

<table class="table table-striped">
    <tr>
        <th>key</th>
        <th>value</th>
    </tr>

    <asp:repeater id="rptCookieInfo" runat="server">
        <ItemTemplate>
                <tr>
                <td><%# Eval("infokey") %></td>
                <td><%# Eval("infovalue") %></td>
                </tr>
        </ItemTemplate>
    </asp:repeater>
</table>
