﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelfTesting.aspx.cs" Inherits="RMS.Centralize.Website.Monitoring.SelfTesting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        table, th, td {
            border: 1px solid black;
            font-family: tahoma;
            font-size: 10pt;
        }

        .title {
            width: 450px;
        }

        .detail {
            width: 250px;
        }

        .result {
            width: 900px;
        }

        th, td {
            padding: 5px;
            vertical-align: text-top;
        }

        .txt-red {
            color: red;
        }

        .txt-green {
            color: green;
        }
    </style>
</head>
<body>
    <asp:Panel ID="Panel2" runat="server">
    <div>
        <table>
            <tr>
                <th class="title">Check List</th>
                <th class="result">Result</th>
            </tr>
            <tr>
                <td class="title">Check Web Server - Web.config</td>
                <td class="result">
                    <label id="rWebConfig" runat="server"></label>
                </td>
            </tr>
            <tr>
                <td class="title">Check Web Server -> Web Service Server Connection</td>
                <td class="result">
                    <label id="rWebToWsConnection" runat="server"></label>
                </td>
            </tr>
            <tr>
                <td class="title">Check Web Service Server - Web.Config</td>
                <td class="result">
                    <label id="rWebServiceConfig" runat="server"></label>
                </td>
            </tr>
            <tr>
                <td class="title">Check Web Service Server -> Database Server Connection</td>
                <td class="result">
                    <label id="rWSToDbConnection" runat="server"></label>
                </td>
            </tr>
            <tr>
                <td class="title">Check Web Service Server -> Monitoring Agent Connection
                    <br />
                    (*Need one monitoring agent started)</td>
                <td class="result">
                    <label id="rWsToAgentConnection" runat="server"></label>
                </td>
            </tr>
            <tr>
                <td class="title">Check Web Service Server -> Email & SMS Server Connection</td>
                <td class="result">
                    <label id="rWsToEmailConnection" runat="server"></label>
                </td>
            </tr>
        </table>
    </div>
    </asp:Panel>
    <asp:Panel ID="Panel1" runat="server">
        <br />
        <form id="form1" runat="server">
            <asp:TextBox ID="Textbox2" runat="server" Width="100px"></asp:TextBox>
            <asp:TextBox ID="Textbox3" runat="server" Width="100px"></asp:TextBox><br/>
            <asp:TextBox ID="Textbox1" runat="server" Height="107px" Width="752px" TextMode="MultiLine"></asp:TextBox>
            <br />
            <asp:Button ID="Button1" AccessKey="E" runat="server" Text="Execute" OnClick="btnexcu_Click" />
            <asp:Button ID="Button2" runat="server" Text="Reset" OnClick="btnReset_Click" />
            <br />
            <br />
            <div style="width: 100%; overflow: auto;">
                <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="Horizontal" AllowPaging="True" AllowSorting="True" EmptyDataText="No Rows Found" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="10"  FirstPageText="First" LastPageText="Last" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </div>
        </form>
    </asp:Panel>
</body>
</html>
